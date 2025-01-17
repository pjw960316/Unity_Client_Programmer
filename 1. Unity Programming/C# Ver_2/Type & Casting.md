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

# :fire: Is는 런타임 시점에 <br>:fire: 검사 대상의 Instance Type과 검사 타겟의 Instance Type을 비교한다. <br>:fire: 검사 타겟의 Instance Type과 같거나 <br>:fire: 검사 타겟의 Derived Instance Type이면 <br>:fire: TRUE를 리턴한다.
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
	if(inputObj is Apple) //inputObj(=검사 대상)가 Apple(=검사 타겟)과 같은 타입이거나, Apple의 Derived Type이면 TRUE를 리턴한다.
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

<br><br><br>

# :fire: Declared Type <= Instance Type일 때만 암시적 할당 가능하다. <br>:fire: Declared Type > Instance Type인 경우, 명시적 캐스팅 필요하다.
![alt text](./capture/0117_1.png)
- Parent는 Object의 모든 걸 갖고, Child는 Parent의 모든 걸 갖기 때문에 포함 관계를 위의 그림처럼 이해한다.
- 여기서 업캐스팅과 다운 캐스팅의 개념이 나오지만 굳이 기록 X (지울거)
- 아마 P120 ~ P127이 이걸 깊게 이해하는 내용일 테니 5장 공부 후 그 다음에 읽고 여기에도 정리하자.
- Type Safety 개념도 함께 정리
~~~c#
void Main()
{
	Object o1 = new Parent(); // SUCCESS!
	Parent o2 = new Object(); // Compile ERROR : cannot implicitly convert type 'object' to 'Parent'
}
~~~

<br/><br/><br/>

# As Casting을 연속으로 하는 경우
- As를 사용하여 연속적으로 캐스팅을 하는 경우 매 번 null을 검사하는 방어 코드를 작성하게 된다.
  - 연속적으로 if문에서 null을 체크하는 것이 보기 좋지는 않다.
- 해결 방법
  - ![Alt text](./Capture/20231113_205859.png)
    - as를 이용하는 구문에 대해서 라이더의 Alt+Enter를 이용하면 Pattern Matching으로 간결하게 바꾸어 준다.
    - Depth가 깊은 연속적인 캐스팅의 경우 따로 메서드로 만들면 가독성이 향상 된다.

<br/><br/><br/>

# 업 캐스팅 vs 다운 캐스팅 [erase]
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