## :fire: Private Field는 외부 접근을 차단하지만 <br> public method를 통해 변경이 가능하다. <br> :fire: 하지만 public method를 통한 변경은 결국 변경 요청이다. <br> 상태 변경 로직은 해당 클래스 내부에서만 수행되니까 주체가 통일된다. <br> 내부에서 invariant를 검증할 수도 있다.

#### :one: property나 public Method로 그냥 뚫린다.
~~~c#
// 3개의 Class에서 private의 방어가 뚫리는 구조
public class TestManager
{
	private PrivateTestSubject _privateTestSubject;

	public TestManager(PrivateTestSubject testSubject)
	{
		this._privateTestSubject = testSubject;
	}
	
	public void ChangeTestObjectNumber()
	{
		//1. 이건 당연히 안 됨.
		//_privateTestSubject._privateTestObject._privateNumber = 5;
		
		//2. 변경 가능
		_privateTestSubject.PrivateTestObject.ChangeNumber();
	}
}

public class PrivateTestSubject
{
	public PrivateTestObject PrivateTestObject; //만약 public으로 하면.

	public PrivateTestSubject(PrivateTestObject obj)
	{
		PrivateTestObject = obj;
	}
}

public class PrivateTestObject
{
	private int _privateNumber = 0;
	
	public void PrintPrivateNumber()
	{
		_privateNumber.Dump();
	}
	
	public void ChangeNumber()
	{
		_privateNumber = 77;
	}
}

void Main()
{
	PrivateTestObject privateTestObject = new PrivateTestObject();
	PrivateTestSubject privateTestSubject = new PrivateTestSubject(privateTestObject);
	TestManager testManager = new TestManager(privateTestSubject);

	testManager.ChangeTestObjectNumber();
	privateTestObject.PrintPrivateNumber();
}
~~~
- 결론적으로 public으로 변경하는 Setter가 존재하면 private로 설정해도 어디서든 바뀔 수 있다.
- F12가 있다고 해서 추적이 된다고 하지만, private이 무색하게 10개의 class 끼리 공유가 되면 답이 없지 않을까?
- 누군가 View Class에서 Model Class의 데이터를 바꾼다? 그냥 답이 없음.

<br>

#### :two: readonly로 기초적인 방어 수행
~~~c#
public class PrivateTestSubject
{
	private readonly int _privateNumber = 0;
	
	public void ChangePrivateNumber()
	{
		// 불가능
		// _privateNumber = 2;
	}
}
~~~
> Readonly 키워드가 붙은 멤버의 Set은 declaration 또는 constructor에서만 가능하다.
  - 그러므로 public Method로 private Field를 변경하는 방식을 막을 수 있다.
- 그러나 Unity에서는 Awake()의 존재 때문에 의미가 퇴색된다.

<br><br>

## :fire: Private Container는 readonly + Declaration으로도 방어가 불가능하다. <br> 그러므로 ImmutableContainer를 사용한다. <br> :fire: ImmutableList에 Add 또는 Remove를 해도 원본은 변경하지 않지만 <br> 새로운 ImmutableList를 만들게 된다. <br> 그러므로 정말로 값이 추가되거나 삭제되지 않고 readonly로 사용하고 싶은 Container에 대해서만 ImmutableList로 구현한다.

#### :one: Container는 readonly로 방어가 불가능하다.
~~~c#
public class PrivateTestSubject
{
	private readonly List<int> _privateList = new(); 
	
	public PrivateTestSubject()
	{
		//default setting
		_privateList.Add(2);
		_privateList.Add(4);
		_privateList.Add(6);
	}
	
	public void ChangeListInstance()
	{
		List<int> devilList = new();
		
		//이건 막는다.
		//_privateList = devilList;
	}
	
	public void InsertDataToList()
	{
		_privateList.Add(8);
	}

	public void PrintPrivateList()
	{
		foreach(var member in _privateList)
		{
			member.Dump();
		}
	}
}

public class TestManager
{
	private PrivateTestSubject _privateTestSubject;
	
	public TestManager(PrivateTestSubject arg)
	{
		this._privateTestSubject = arg;
	}
	
	public void InsertDataToList()
	{
		_privateTestSubject.InsertDataToList();
	}

	public void PrintPrivateList()
	{
		_privateTestSubject.PrintPrivateList();
	}
}

void Main()
{
	PrivateTestSubject privateTestSubject = new PrivateTestSubject();
	TestManager testManager = new TestManager(privateTestSubject);
	
	testManager.InsertDataToList();
	testManager.PrintPrivateList();
}
// RESULT
// 2 4 6 8
~~~
- ChangeListInstance()에서 새로운 devilList를 기존의 readonly List에 할당하는 것은 막을 수 있다.
- 그러나 readonly키워드로는 Container의 내부 멤버를 추가하는 것을 방어할 수 없다.

<br>

#### :two: Immutable한 readonly Container는 Immutable로 만들어 준다.
~~~c#
public class PrivateTestSubject
{
	private ImmutableList<int> _privateImmutableList = ImmutableList.Create(2,4,6);
	private ImmutableList<int> _privateNewImmutableList;

	public PrivateTestSubject() {}
	
	public void CreateNewList()
	{
		_privateNewImmutableList = _privateImmutableList.Add(8);
	}

	public void PrintPrivateImmutableList()
	{
		foreach(var member in _privateImmutableList)
		{
			member.Dump();
		}
	}

	public void PrintPrivateNewImmutableList()
	{
		foreach (var member in _privateNewImmutableList)
		{
			member.Dump();
			
		}
	}
}

public class TestManager
{
	private PrivateTestSubject _privateTestSubject;
	
	public TestManager(PrivateTestSubject arg)
	{
		this._privateTestSubject = arg;
	}
	
	public void CreateNewList()
	{
		_privateTestSubject.CreateNewList();
	}

	public void Print()
	{
		_privateTestSubject.PrintPrivateImmutableList();
		
		"\n".Dump();
		
		_privateTestSubject.PrintPrivateNewImmutableList();
	}
}

void Main()
{
	PrivateTestSubject privateTestSubject = new PrivateTestSubject();
	TestManager testManager = new TestManager(privateTestSubject);

	testManager.CreateNewList();
	testManager.Print(); 

	//Result
	//2 4 6
	//2 4 6 8
}
~~~  
- '_privateImmutableList.Add(8);'을 통해 8을 추가시켰지만 _privateImmutableList은 여전히 '2,4,6'만을 elements로 갖는다.
- '_privateNewImmutableList = _privateImmutableList.Add(8);'를 하면 Add나 Remove로 원본은 변화시키지 않고 새로운 ImmutableList를 생성한다.
- :bangbang: ImmutableList가 ReadOnlyCollection 보다 thread-safe 하기에 ImmutableList를 사용해야 한다.
  - :link:[Why use ImmutableList over ReadOnlyCollection?](https://stackoverflow.com/questions/30165810/why-use-immutablelist-over-readonlycollection)
  - :link:[MSDN_ImmutableList<T>](https://learn.microsoft.com/en-us/dotnet/api/system.collections.immutable.immutablelist-1?view=net-9.0)

<br>

#### :three: 활용도가 높은 실제 예제 <br> private field로 일반 Container 사용하고, public property로 Immutable Container를 사용한다.
~~~c#
private Dictionary<string, List<bool>> _routineRecordDictionary = new();

public ImmutableDictionary<string, ImmutableList<bool>> RoutineRecordDictionary
{
	get
	{
		return _routineRecordDictionary.ToImmutableDictionary
		(
			kvp => kvp.Key,
			kvp => kvp.Value.ToImmutableList()
		);
	}
}
~~~
- _routineRecordDictionary를 정렬해도 ImmutableDictionary의 정렬은 보장되지 않는다. 그러므로, ImmutableSortedDictionary를 사용하고 Compare Method는 직접 구현한다.