## :fire: Class 내부의 Static Member는 모든 instance 간에 공유해야 하는 값을 저장한다.
> 정적 멤버는 클래스의 인스턴스가 없는 경우에도 클래스에서 호출할 수 있습니다

> 생성된 클래스의 인스턴스 수에 관계없이 정적 멤버의 복사본은 하나만 존재합니다.

> 전체 클래스를 정적으로 선언하는 것보다 일부 정적 멤버를 사용하여 비정적 클래스를 선언하는 것이 더 일반적입니다.

<br><br>

## :fire: Readonly 키워드가 붙은 멤버의 Set은 declaration 또는 constructor에서만 가능하다.
> 'readonly' indicates that assignment to the field can only occur as part of the declaration or in a constructor in the same class. 
- static member 같은 global 한 변수는 유지보수 관점에서는 readonly로 선언하는 게 좋다고 생각한다. 물론, 런타임에서 동적으로 바뀌어야 하면 예외지만.

#### [예제]
~~~c#
void Main()
{
	Test obj = new Test();
	
	obj.instanceField.Dump();
	Test.staticField.Dump();
}

public class Test
{
    // declaration
	public readonly int instanceField = 1;
	public readonly static int staticField = 1;
	
    // constructor
	public Test()
	{
		instanceField = 2;
	}

	static Test()
	{
		staticField = 3;
	}
}
// 2 3
~~~

<br><br>

## :fire: Private 필드는 외부에서 접근에 완벽히 안전하지 않다. 그러므로 다음의 두 가지 방식을 사용한다. <br> :one: private field에 readonly + Declaration을 쓰도록 한다. <br> :two: Container는 Immutable Type으로 구현한다.
#### [예제 1_private의 한계 : property나 public Method로 그냥 뚫린다.]

<details>
  <summary> :point_up_2: 눌러서 코드를 확인 합시다  </summary>

~~~c#
// 예제 1-1 : 2개의 Class에서 private의 방어가 뚫리는 구조
public class TestManager
{
	private PrivateTestSubject _privateTestSubject;
	
	public TestManager(PrivateTestSubject arg)
	{
		//DI
		this._privateTestSubject = arg;
	}
	
	public void TestFirstQuestion()
	{
		//Compile ERROR
		//_privateTestSubject._privateNumber = 1;
	}
	
	public void TestSecondQuestion()
	{
		_privateTestSubject.ChangePrivateNumber();
	}
	public void PrintPrivateNumber()
	{
		_privateTestSubject.PrintPrivateNumber();
	}
}

public class PrivateTestSubject
{
	private int _privateNumber = 0;
	
	public void ChangePrivateNumber()
	{
		_privateNumber = 2;
	}
	
	public void PrintPrivateNumber()
	{
		_privateNumber.Dump();
	}
	
}

void Main()
{
	PrivateTestSubject privateTestSubject = new PrivateTestSubject();
	TestManager testManager = new TestManager(privateTestSubject);
	
	testManager.TestSecondQuestion();
	testManager.PrintPrivateNumber();
}
// Result
// 2
~~~

</details>

- TestFirstQuestion()에서 DI로 받은 Instance 내부의 private Field는 접근이 불가능함을 보여준다.
- TestSecondQuestion()에서 Public Method로 바꿀 수 있다.

<details>
  <summary> :point_up_2: 눌러서 코드를 확인 합시다  </summary>

~~~c#
// 예제 1-2 : 3개의 Class에서 private의 방어가 뚫리는 구조
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
- 결론적으로 public으로 변경하는 Setter만 있을 시에 private로 설정해도 어디서든 바뀔 수 있다.
- F12가 있다고 해서 추적이 된다고 하지만, private이 무색하게 10개의 class 끼리 공유가 되면 답이 없지 않을까?
- 누군가 View Class에서 Model Class의 데이터를 바꾼다? 그냥 답이 없음.

</details>

<br>

#### [예제_2 : Non-Container Field는 readonly로 방어]

<details>
  <summary> :point_up_2: 눌러서 코드를 확인 합시다  </summary>

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

</details>

> Readonly 키워드가 붙은 멤버의 Set은 declaration 또는 constructor에서만 가능하다.
  - 그러므로 public Method로 private Field를 변경하는 방식을 막을 수 있다.

<br>

#### [예제_3 : Container는 readonly로 방어가 불가능 하다.]

<details>
  <summary> :point_up_2: 눌러서 코드를 확인 합시다  </summary>

~~~c#

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

void Main()
{
	PrivateTestSubject privateTestSubject = new PrivateTestSubject();
	TestManager testManager = new TestManager(privateTestSubject);
	
	testManager.InsertDataToList();
	testManager.PrintPrivateList();
}
~~~

</details>

- ChangeListInstance()에서 새로운 devilList를 기존의 readonly List에 할당하는 것은 막을 수 있다.
- 그러나 readonly키워드로는 Container의 내부 멤버를 추가하는 것을 방어할 수 없다. 

<br>

#### [예제_4 : readonly Container는 Immutable로 만들어 준다.]

<details>
  <summary> :point_up_2: 눌러서 코드를 확인 합시다  </summary>

~~~c#

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
	
	public void PrintFirstElementInImmutableList()
	{
		_privateTestSubject.GetFirstElement().Dump();
	}
}

public class PrivateTestSubject
{
	private ImmutableList<int> _privateImmutableList = ImmutableList.Create(2,4,6);

	public PrivateTestSubject() {}
	
	public void InsertDataToList()
	{
		_privateImmutableList.Add(8);
	}
	
	public int GetFirstElement()
	{
		return _privateImmutableList.FirstOrDefault();
	}

	public void PrintPrivateList()
	{
		foreach(var member in _privateImmutableList)
		{
			member.Dump();
		}
	}
}

void Main()
{
	PrivateTestSubject privateTestSubject = new PrivateTestSubject();
	TestManager testManager = new TestManager(privateTestSubject);
	
	testManager.InsertDataToList();
	testManager.PrintPrivateList(); // 2 4 6
	
	testManager.PrintFirstElementInImmutableList(); // 2
}
~~~  

</details>

- InsertDataToList()를 통해 8을 추가 시켰지만 전혀 동작하지 않는다.
- 그러나 외부에서 LINQ로 FirstOrDefault()하는 건 정상적으로 동작한다.
- :link:[MSDN_ImmutableList<T>](https://learn.microsoft.com/en-us/dotnet/api/system.collections.immutable.immutablelist-1?view=net-9.0)