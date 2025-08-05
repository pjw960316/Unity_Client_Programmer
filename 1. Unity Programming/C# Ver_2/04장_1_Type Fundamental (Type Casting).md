## :fire: 용어정리 <br>:fire: Base Type - Derived Type : 상위 타입과 하위 타입 <br>:fire: Declared Type - Instance Type : Complie Type & Runtime Type <br>:fire: () : Explicit Casting

<br><br>

## :fire: 컴파일 시점에는 타입 검사시에 Declared Type으로 한다.<br>:fire: 런타임 시점에는 타입 검사시에 Instance Type으로 한다.

#### [기본 예제]
~~~c#
void Main()
{
	Fruit fruit = new Fruit();
	Fruit fruit2 = new Apple(); //Fruit = Declared Type , Apple = Instance Type
	Apple apple = new Apple();
	GreenApple greenApple = new GreenApple();
	Animal animal = new Animal();
	
	//Test(fruit); //InvalidCastException
	//Test(animal); //InvalidCastException
	Test(fruit2); //Success
	Test(apple); //Success
	Test(greenApple); //Success
}

public static void Test(object o)
{
	Apple apple = (Apple) o;
	apple.GetType().Dump();
}

public class Fruit{}
public class Apple : Fruit{}
public class GreenApple : Apple{}
public class Animal{}
~~~
- **컴파일 단계**
  - 컴파일러는 <ins>컴파일 단계에서 명시적 캐스팅이 성공할지 실패할지를 검증하지 않는다.</ins> 대신, 이 책임을 런타임에 넘긴다.
  - 컴파일러는 "캐스팅 구문이 문법적으로 유효하다"고 판단하여 에러 없이 컴파일을 허용합니다.
- **런타임 단계**
  - 런타임 단계에서 Test 메서드에 있는 Apple apple = (Apple) o;이 성공하려면, <ins>instance type이 Apple 타입이거나 Apple의 derived Class</ins>이어야 한다.
  - 그러므로 fruit2와 apple은 instance type이 Apple 타입이고, greenApple은 Apple 타입의 Dervied Class이므로 캐스팅에 성공한다.

<br><br>

## :fire: Is는 <ins>Runtime</ins>에 <br>:fire: 검사 대상의 Instance Type과 검사 타겟의 Declared Type을 비교한다. <br>:fire: 검사 대상의 Instance Type이 <br> 검사 타겟과 동일하거나, 검사 타겟의 Derived Type이면 True를 리턴한다. 

#### [기본 예제]

<details>
  <summary> :point_up_2: 눌러서 코드를 확인 합시다  </summary>

~~~c#
void Main()
{
	Fruit fruit = new Fruit();
	Fruit apple_1 = new Apple();
	Apple apple_2 = new Apple();
	GreenApple apple_3 = new GreenApple();
	
	Test(fruit);
	Test(apple_1);
	Test(apple_2);
	Test(apple_3);
}

static void Test(Fruit inputObj)
{
	//inputObj(=검사 대상)가 Apple(=검사 타겟)과 같은 타입이거나
	//Apple의 Derived Type이면 TRUE를 리턴한다.
	if(inputObj is Apple) 
	{
		(inputObj.ToString() + " is Apple").Dump();
	}
	else
	{
		(inputObj.ToString() + " is not Apple").Dump();
	}
}

class Fruit {}
class Apple : Fruit{}
class GreenApple : Apple{}

/*
UserQuery+Fruit is not Apple
UserQuery+Apple is Apple
UserQuery+Apple is Apple
UserQuery+GreenApple is Apple
*/
~~~

</details>

- is와 as 모두 runtime에 검사하는 캐스팅 연산자고, 둘 다 예외를 절대로 발생 시키지 않는다.

#### [개발하다가 만든 좋은 예제]

~~~c#
public interface IManager : IFactory
{
    public void Initialize();
    public void SetModel(IEnumerable<IModel> models);
    public void ConnectInstanceByActivator(IManager instance);
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

<br><br>

## :fire: Explicit 3총사(is, as, 괄호) 중 나는 is만 사용할 것 이다.
- 가독성이 as 보다 좋다.
- 예외처리에서 is가 가장 안전하다고 판단한다.
- :link: [Pattern Matching](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/pattern-matching)