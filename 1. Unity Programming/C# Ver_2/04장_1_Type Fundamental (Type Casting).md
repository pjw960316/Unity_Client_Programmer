## :fireworks: 용어 정리 <br>:fire: Base Type - Derived Type : 상위 타입 - 하위 타입 <br>:fire: Declared Type - Instance Type : Complie Type - Runtime Type <br>:fire: () : Explicit Casting

<br><br>

## :fire: is , as는 <ins>Runtime</ins>에 <br>:fire: 검사 대상의 Instance Type과 검사 타겟의 Declared Type을 비교한다. <br>:fire: 검사 대상의 Instance Type이 <br> 검사 타겟과 동일하거나, 검사 타겟의 Derived Type이면 True를 리턴한다. 

#### :spiral_notepad:[기본 예제]
~~~c#
public static class MainManager
{
    public static void Main()
    {
        var testManager = new TestManager();
    }
}

public class Fruit { }
public class Apple : Fruit { }

public class TestManager
{
    private object _successObj;
    private object _failObj;

    public TestManager()
    {
        Initialize();

		TestCasting(_successObj); // 캐스팅 성공
		TestCasting(_failObj);    // 캐스팅 실패    
	}

	private void Initialize()
	{
		Fruit fruit = new Apple();
		_successObj = fruit;

		Fruit fruit2 = new Fruit();
		_failObj = fruit2;
	}

	// object 타입의 모든 인자를 받을 수 있다.
	private void TestCasting(object obj)
	{
		Apple apple = obj as Apple;

		if (apple != null)
		{
			Console.WriteLine("캐스팅 성공");
		}
		else
		{
			Console.WriteLine("캐스팅 실패");
		}
	}
}
~~~
- **컴파일 단계**
  - 컴파일러는 <ins>컴파일 단계에서 명시적 캐스팅이 성공할지 실패할지를 검증하지 않는다.</ins> 대신, 이 책임을 런타임에 넘긴다.
  - 컴파일러는 "캐스팅 구문이 문법적으로 유효하다"고 판단하여 에러 없이 컴파일을 허용한다.
- **런타임 단계**
  - TestCasting은 object 타입이 parameter이므로 fruit과 fruit2를 받을 수 있다.
  - 그러나 Apple apple = obj as Apple을 확인해보자. 
    - fruit은 Apple 타입이고 이는 검사 타겟인 Apple 또는 Apple의 Derived 타입을 만족한다.
    - fruit2은 Fruit 타입이고 이는 검사 타겟인 Apple 또는 Apple의 Derived 타입을 만족하지 않는다.
    - 사과로 캐스팅 되려면 과일이 아니라 사과거나 초록사과 같은 객체여야 한다!
- is와 as 모두 runtime에 검사하는 캐스팅 연산자고, 둘 다 예외를 절대로 발생 시키지 않는다.
- 
<br><br>

#### [개발하다가 만든 예제]

~~~c#
public interface IManager : IFactory
{
    public void Initialize();
    public void SetModel(IEnumerable<IModel> models);
    public void ConnectInstanceByActivator(IManager instance);
}

// Note
// 공통로직을 담는 메서드가 굳이 IManager를 상속 받을 필요 없다.
public abstract class ManagerBase<T> where T : class, new()
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }

            return _instance;
        }
    }

    // Note
    // Activator를 통해 만든 Instance를 Singleton Instance에 초기화 시켜준다.
    public void ConnectInstanceByActivator(IManager instance)
    {
        _instance = instance as T;
    }
}

public class SoundManager : ManagerBase<SoundManager>, IManager, IDisposable
{}

// _managerTypeList만 보면 된다.
private void SetManagerTypesUsingReflection()
{
	_cSharpAssembly = AppDomain.CurrentDomain.GetAssemblies()
		.FirstOrDefault(asm => asm.GetName().Name == MAIN_ASSEMBLY);

	if (_cSharpAssembly == null)
	{
		throw new NullReferenceException("_cSharpAssembly is null");
	}

	_managerTypeList = _cSharpAssembly.GetTypes()
		.Where(type => typeof(IManager).IsAssignableFrom(type) && type.IsClass)
		.ToList();
}

private void CreateSingletonManagers()
{
	foreach (var type in _managerTypeList)
	{
		//1번 시점
		var objectTypeInstance = Activator.CreateInstance(type);

		//2번 시점
		if (objectTypeInstance is IManager manager)
		{
			//3번 시점
			manager.ConnectInstanceByActivator(manager);
			_managerList.Add(manager);
		}
	}
}
~~~
- 1번 시점에서 objectTypeInstance은 Declared Type은 Object지만, 실제로는 _managerTypeList에서 instance Type을 알고 있기에 Instance Type은 SoundManager 같이 concrete한 Type이다.
- 2번 시점에서 objectTypeInstance의 Instance Type이 SoundManager일 때, 검사대상의 Instance Type(=soundManager)이 검사 타겟의 Declared Type(=IManager)의 derived type이므로 true가 리턴된다.
- 3번 시점에서 다형성이 동작하여, SoundManager의 ConnectInstanceByActivator(arg)가 호출된다.
  - 실제로는 SoundManager : ManagerBase<T>이므로, ManagerBase<T>의 ConnectInstanceByActivator가 호출되었다.

<br><br>

## :fire: Declared Type <= Instance Type일 때만 암시적 할당 가능하다. <br>:fire: Declared Type > Instance Type인 경우, 명시적 캐스팅 필요하다.
![alt text](./capture/0117_1.png)
- Object(Super Base Type) - Parent(Base Type) - Child (Derived Type)
- Parent는 Object의 모든 멤버와 메서드를 갖고, Child는 Parent의 모든 멤버와 메서드를 갖기 때문에 크기 비교를 다음의 그림과 같이 정리 가능하다.
~~~c#
void Main()
{
	Object o1 = new Base(); // SUCCESS
	//Parent o2 = new Object(); // Compile ERROR : cannot implicitly convert type 'object' to 'Base'
	Object o3 = (Base) o1; // SUCCESS -> 명시적 캐스팅 (Explicit Casting)
	
	if(o3 is Base) {"TRUE".Dump();} // TRUE
	if(o3 is Object) {"TRUE".Dump();} // TRUE
}

class Base { }
~~~
<br><br>

## :fire: Instance Type이 Base Type인 객체를<br>:fire: Derived Type으로 Explicit Casting을 시도할 때<br>:fire: Runtime Exception과 함께 실패한다.

#### [예외 발생 코드]
~~~c#
void Main()
{
    Object obj1 = new Object();
	Base obj2 = (Base)obj1; //Invalid_Cast_Exception
	Object obj3 = (Base)obj1; //Invalid_Cast_Exception
}

public class Base { }
public class Derived : Base { }
~~~
- Base Type 객체는 Dervied Type에서 정의한 고유 멤버를 포함하지 않으므로, Derived Type으로 변환 할 수 없다.
- 그러므로 is 또는 as를 이용해서 검사해야 한다.
  
#### [예외 처리 코드]
~~~c#
void Main()
{
    Object obj1 = new Object();
	
	if(obj1 is Base)
	{
		Base obj2 = (Base)obj1;
	}
	else
	{
		Base obj2 = null;
	}
}

public class Base { }
public class Derived : Base { }
~~~
