# 목차
- [목차](#목차)
- [Abstract Class vs Virtual Method (추상 클래스 vs 가상 메서드)](#abstract-class-vs-virtual-method-추상-클래스-vs-가상-메서드)
- [Abstract Class](#abstract-class)
    - [1. Abstract Class는 Instance를 생성 할 수 없다.](#1-abstract-class는-instance를-생성-할-수-없다)
    - [2. Abstract 클래스는 abstract method, virtual method, 일반 method을 모두 method로 들고 있을 수 있다.](#2-abstract-클래스는-abstract-method-virtual-method-일반-method을-모두-method로-들고-있을-수-있다)
- [Abstract Method](#abstract-method)
    - [1. Abstract Method는 반드시 자식 클래스에서 구현해야 한다.](#1-abstract-method는-반드시-자식-클래스에서-구현해야-한다)
- [Virtual을 사용하면 자식 객체에서 override 한 메서드가 해당 함수를 대체한다.](#virtual을-사용하면-자식-객체에서-override-한-메서드가-해당-함수를-대체한다)
    - [1. Override Method인 Bark가 대체 하는 경우](#1-override-method인-bark가-대체-하는-경우)
    - [2. 위의 코드에서 Bark를 Abstract로 변화해도 동일하게 동작한다.](#2-위의-코드에서-bark를-abstract로-변화해도-동일하게-동작한다)
    - [3. 1번 예제에서 base를 추가하면?](#3-1번-예제에서-base를-추가하면)
- [Overhead](#overhead)
- [Sealed](#sealed)

<br/><br/><br/>

# Abstract Class vs Virtual Method (추상 클래스 vs 가상 메서드)
- Abstract는 클래스가 있지만 Virtual 클래스는 없다.
- Virtual Method는 선언한 클래스에서 Body를 구현해도 된다.

<br/><br/><br/>

# Abstract Class
### 1. Abstract Class는 Instance를 생성 할 수 없다.
  - 반드시 상속을 받은 클래스를 생성해서 이용해야 한다.
  - Architecture 관점으로 보면 다형성을 이용할 수 있다.

<br/>

### 2. Abstract 클래스는 abstract method, virtual method, 일반 method을 모두 method로 들고 있을 수 있다.
~~~c#
public abstract class Shape
{
    public abstract void Draw();

	public virtual void Read()
	{
		Console.WriteLine("I Read Book");
	}
	
    public void DisplayInfo()
    {
        Console.WriteLine("This is a shape.");
    }
}

class Circle : Shape
{
    public override void Draw()
    {
        Console.WriteLine("Drawing a circle.");
    }

    public void CallCircle()
    {
        Console.WriteLine("I am Circle");
    }
}

class Rectangle : Shape
{
    public override void Draw()
    {
        Console.WriteLine("Drawing a rectangle.");
    }
}

void Main(string[] args)
{
    Shape circle = new Circle();
    circle.DisplayInfo();
    circle.Draw();
    circle.CallCircle(); //컴파일 에러, circle은 Shape 타입이므로 CallCircle을 호출 할 수 없다.

    Shape rectangle = new Rectangle();
    rectangle.DisplayInfo();
    rectangle.Draw();
}

/*Result
This is a shape.
Drawing a circle.

This is a shape.
Drawing a rectangle.
*/
~~~
- 컴파일 에러 증명
  - ![20230904_201229](https://github.com/pjw960316/Unity_Client_Programmer/assets/55792986/7b9d301e-2b40-469f-9731-848b614fbdac)

<br/><br/><br/>

# Abstract Method
### 1. Abstract Method는 반드시 자식 클래스에서 구현해야 한다.
- ![image](https://github.com/pjw960316/Unity_Client_Programmer/assets/55792986/84a51c73-bfc3-4d1e-9784-73fbc3df9e99)

<br/><br/><br/>

# Virtual을 사용하면 자식 객체에서 override 한 메서드가 해당 함수를 대체한다.
### 1. Override Method인 Bark가 대체 하는 경우
~~~c#
class Animal
{
    public void Eat()
    {
        Console.WriteLine("Animal is eating.");
    }
	
	public virtual void Bark()
    {
        Console.WriteLine("None");
    }
}

class Dog : Animal
{
    public override void Bark()
    {
        Console.WriteLine("Dog is barking.");
    }
}

class Program
{
    static void Main(string[] args)
    {
		//1. 내가 생각한 일반적으로 쓰는 다운 캐스팅
        Animal animal = new Dog();

        if (animal is Dog dog)
        {
            dog.Bark();  // 'dog'는 Dog 타입으로 캐스팅되었으므로 Dog 클래스의 메서드 사용 가능
            dog.Eat();   // 'dog'는 Animal 타입도 되므로 Animal 클래스의 메서드 역시 사용 가능
        }
		
		Console.WriteLine("===============================");

		//2. 이런 코드도 된다.
		Dog dog2 = new Dog();
		if(dog2 is Animal animal2)
		{
			animal2.Bark(); 
			animal2.Eat();
		}
    }
}

/*result
None
Dog is barking.
Animal is eating.
===============================
Dog is barking.
Animal is eating.
*/
~~~
- Result에서 절취선 아래의 로그를 확인해 보자.
  - animal2는 Animal 타입이므로 원래 animal2.Bark()를 호출하면 Animal의 Bark()가 호출되며 "None"을 출력해야 한다.
  - 하지만 해당 함수는 virtual 이므로 자식 클래스의 메서드가 부모 클래스의 메서드를 대체하기 때문에 Dog의 Bark()가 호출된다.
- ![20230904_133612](https://github.com/pjw960316/Unity_Client_Programmer/assets/55792986/5ce1fb02-2578-4d7c-b32b-83b708d2fc12)

<br/><br/>

### 2. 위의 코드에서 Bark를 Abstract로 변화해도 동일하게 동작한다.
~~~C#
abstract class Animal
{
    public void Eat()
    {
        Console.WriteLine("Animal is eating.");
    }
	
	public abstract void Bark();
}

class Dog : Animal
{
    public override void Bark()
    {
        Console.WriteLine("Dog is barking.");
    }
}

class Program
{
    static void Main(string[] args)
    {
		//1. 내가 생각한 일반적으로 쓰는 다운 캐스팅
        Animal animal = new Dog();

        if (animal is Dog dog)
        {
            dog.Bark();  // 'dog'는 Dog 타입으로 캐스팅되었으므로 Dog 클래스의 메서드 사용 가능
            dog.Eat();   // 'dog'는 Animal 타입도 되므로 Animal 클래스의 메서드 역시 사용 가능
        }
		
		Console.WriteLine("===============================");

		//2. 이런 코드도 된다.
		Dog dog2 = new Dog();
		if(dog2 is Animal animal2)
		{
			animal2.Bark(); 
			animal2.Eat();
		}
    }
}

/*result
Dog is barking.
Animal is eating.
===============================
Dog is barking.
Animal is eating.
*/
~~~

<br/><br/>

### 3. 1번 예제에서 base를 추가하면?
~~~c#
class Animal
{
    public void Eat()
    {
        Console.WriteLine("Animal is eating.");
    }
	
	public virtual void Bark()
    {
        Console.WriteLine("None");
    }
}

class Dog : Animal
{
    public override void Bark()
    {
		base.Bark();
        Console.WriteLine("Dog is barking.");
    }
}

class Program
{
    static void Main(string[] args)
    {
		//1. 내가 생각한 일반적으로 쓰는 다운 캐스팅
        Animal animal = new Dog();

        if (animal is Dog dog)
        {
            dog.Bark();  // 'dog'는 Dog 타입으로 캐스팅되었으므로 Dog 클래스의 메서드 사용 가능
            dog.Eat();   // 'dog'는 Animal 타입도 되므로 Animal 클래스의 메서드 역시 사용 가능
        }
		
		Console.WriteLine("===============================");

		//2. 이런 코드도 된다.
		Dog dog2 = new Dog();
		if(dog2 is Animal animal2)
		{
            // animal2는 Animal 타입이므로 None만 호출되어야 하지만 virtual 함수이므로 자식의 Bark()가 호출된다.
            // Dog의 Bark에 base.Bark()가 있으므로 부모의 Bark()도 호출된다. 
			animal2.Bark(); 
			animal2.Eat();
		}
    }
}
/* Result
None
Dog is barking.
Animal is eating.
===============================
None
Dog is barking.
Animal is eating.
*/
~~~
- base.Bark()에서 부모 클래스인 Animal의 Virtual 함수를 콜하기 때문에 "None"이 출력된다.
  - virtual 메서드에 Body가 있는 이유
<br/><br/><br/>

# Overhead
![image](https://user-images.githubusercontent.com/55792986/185398970-e72a3592-75e7-4635-a363-2fcb0e5ef069.png)
- 내 생각 : 추상 함수, 가상 함수 모두 테이블이 만들어 지기 때문에 기존 보다는 성능저하가 발생 할 것 이다. 그럼에도 불구하고 이점이 많으니 사용하겠지.
      
<br/><br/><br/>

# Sealed
- Virtual로 선언된 가상 메소드를 오버라이딩한 버전의 메소드가 오버라이딩 되지 않도록 봉인할 수 있다.
- ![image](https://user-images.githubusercontent.com/55792986/185403786-0f553666-5e3a-490c-bcd2-9c29afa5a538.png)
- ![image](https://user-images.githubusercontent.com/55792986/185403876-8345a38f-094d-4e42-867a-ccef624cd40b.png)


