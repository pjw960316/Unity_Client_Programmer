# :fire: 컴파일 시점에는 타입 검사시에 Declared Type으로 한다.<br>:fire: 런타임 시점에는 타입 검사시에 Instance Type으로 한다.
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

<br><br><br>

# :fire: Is는 런타임 시점에 Instance Type을 비교해서 <br>:fire: 나와 같은 Instance Type인지 아니면 <br>:fire: 나의 Derived Instance Type인지 비교해서 <br>:fire: T/F를 리턴한다.
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

# 그림
- ![alt text](./capture/0117_1.png)
# is도 제대로 모르고 as를 봤군 (지워)

# As의 동작 [지우자]
~~~c#
class Animal
{
	public int Age = 1;
	
	public void Eat()
	{
		Console.WriteLine("Animal is eating.");
	}
}

class Dog : Animal
{
	public int DogValue = 2;
	public void Bark()
	{
		Console.WriteLine("Dog is barking.");
	}
}

class Program
{
	static void Main(string[] args)
	{
		Animal a = new Animal();
		Animal b = new Dog();
		
		//a의 경우 타입은 Animal이고 인스턴스 타입은 Animal 타입이다.
		var c = a as Dog; 

		//b의 경우 타입은 Animal이고 인스턴스 타입은 Dog 타입이다.
		//인스턴스가 Dog 타입이고 이건 Dog 타입으로 캐스팅이 된다. (as Dog;에서 Dog의 타입과 같거나 Dog의 하위 타입이니까)
		var d = b as Dog;
		
		c.Dump(); //null
		d.Dump();	
	}
}
/* Result
c는 null이 된다.
d는 다운 캐스팅에 성공한다.
*/
~~~
- ![Alt text](./Capture/202310231.png)
- ![Alt text](./Capture/20231023_114345.png)
> **new로 객체를 생성할 때 타입과 인스턴스를 구분해서 생각하는 것은 좋은 습관** 입니다. 이런 분리를 통해 코드의 의미와 작동 원리를 더 정확하게 이해할 수 있습니다.
> 타입 안정성(Type Safety): 타입을 명확히 알고 있으면, 해당 타입의 메서드와 프로퍼티만을 사용하게 됩니다. 이는 런타임 에러의 가능성을 줄입니다.
> 오버로딩과 다형성(Overloading & Polymorphism): 특히 객체지향 프로그래밍에서는 같은 이름의 메서드가 다른 타입에 대해 다르게 동작할 수 있습니다. 타입을 명확히 알고 있으면, 어떤 메서드가 호출될지 예측하기 쉽습니다.


<br/><br/><br/>

# As Casting을 연속으로 하는 경우
- As를 사용하여 연속적으로 캐스팅을 하는 경우 매 번 null을 검사하는 방어 코드를 작성하게 된다.
  - 연속적으로 if문에서 null을 체크하는 것이 보기 좋지는 않다.
- 해결 방법
  - ![Alt text](./Capture/20231113_205859.png)
    - as를 이용하는 구문에 대해서 라이더의 Alt+Enter를 이용하면 Pattern Matching으로 간결하게 바꾸어 준다.
    - Depth가 깊은 연속적인 캐스팅의 경우 따로 메서드로 만들면 가독성이 향상 된다.

<br/><br/><br/>

# 업 캐스팅 vs 다운 캐스팅 [지금 생각에는 컴파일 에러와 런타임 에러의 차이로 보이는데 이때 잘못 알고 한 듯. 다시 공부부]
- ![image](https://github.com/pjw960316/Unity_Client_Programmer/assets/55792986/275c0001-0696-4d0d-8e7c-d6e8bf010463)
~~~c#
class Program
{
    class Animal
{
	public int Age = 1;
	
	public void Eat()
	{
		Console.WriteLine("Animal is eating.");
	}
}

class Dog : Animal
{
	public int DogValue = 2;
	public void Bark()
	{
		Console.WriteLine("Dog is barking.");
	}
}

class Program
{
	static void Main(string[] args)
	{
		//1. 다운 캐스팅 (일반적으로 캐스팅을 할 때 쓰는 것)
		Animal animal = new Dog();

		if (animal is Dog dog)
		{
			dog.Age.Dump();
			dog.DogValue.Dump();
			
			dog.Bark();  // 'dog'는 Dog 타입으로 캐스팅되었으므로 Dog 클래스의 메서드 사용 가능
			dog.Eat();   // 'dog'는 Animal 타입도 되므로 Animal 클래스의 메서드 역시 사용 가능
		}

		Console.WriteLine("==========================================");
		
		//2. 업 캐스팅 (평소에 잘 사용하지 않았다.) 
		Dog dog2 = new Dog();
		if (dog2 is Animal animal2)
		{
			animal2.Age.Dump();
			//animal2.DogValue.Dump(); // Dog Class의 DogValue는 사용 불가능.
			
			//animal2.Bark(); //결국에 animal2는 Animal 타입이므로 Dog Class의 Bark를 사용할 수 없다.
			animal2.Eat();
		}
	}
}

/* Result
1
2
Dog is barking.
Animal is eating.
==========================================
1
Animal is eating.
*/
~~~