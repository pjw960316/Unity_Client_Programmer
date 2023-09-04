# 목차
- [목차](#목차)
- [정리 한 이유](#정리-한-이유)
- [Abstract(추상) 와 Virtual(가상)](#abstract추상-와-virtual가상)
- [Overhead](#overhead)
- [Sealed](#sealed)
- [Interface](#interface)
- [인터페이스 왜 쓸까?](#인터페이스-왜-쓸까)
- [Virtual을 사용하면 자식 객체에서 override 한 메서드가 해당 함수를 대체한다.](#virtual을-사용하면-자식-객체에서-override-한-메서드가-해당-함수를-대체한다)

# 정리 한 이유
- 3개의 키워드는 모두 각자의 기능이 있고 명확하게 이해하고 구분해야 더 좋은 설계를 할 수 있을 것 같다.
- 구글링으로 정리했지만 조금 모호하게 적은 것 같아 '시작하세요 C# 10'으로 다시 정리했다.

# Abstract(추상) 와 Virtual(가상) 
- 어차피 Virtual Class는 존재하지 않으므로 비교할 필요가 없다.
- **:star:Abstract Method는 코드 없는 Virtual Method 이다.**
  - **Abstract Method는 Virtual Method에 속하는 개념이다.**
- Abstract Method는 선언한 곳에서 절대 구현하면 안 되고 반드시 상속을 받아서 구현해야 한다.
- Virtual Method는 선언한 곳에서 구현을 해도 괜찮다.
- Abstract 클래스는 왜 객체를 만들 수 없는가?
  - Abstract 클래스는 의외로 Abstract Method를 보유 하지 않아도 된다. 하지만 있을 수도 있으므로(애당초 Abstract를 붙였으면 Abstract Method를 만들어야지...) 막아 놓았다.
- Virtual method는 자식 클래스에서 재정의하지 않아도 컴파일할 때 오류가 발생하지 않지만 Abstract class의 abstract method는 자식 클래스에서 반드시 재정의 해야만 컴파일된다. 즉, 컴파일 단계에서부터 재정의를 강제하고 싶을 때 유용하게 사용할 수 있는 것이 바로 abstract class와 abstract method 이다.
~~~c#
public abstract class C4
{
    public void ImplementImplement()
    {
        Debug.Log("no abstract method, but OK");
    }
}
- **Abstract 클래스는 instance를 만들지 못하니까 무조건 상속 받아서 instance를 생성해야 하는 특징이 있다.**
~~~

# Overhead
![image](https://user-images.githubusercontent.com/55792986/185398970-e72a3592-75e7-4635-a363-2fcb0e5ef069.png)
- 내 생각 : 추상 함수, 가상 함수 모두 테이블이 만들어 지기 때문에 기존 보다는 성능저하가 발생 할 것 이다. 그럼에도 불구하고 이점이 많으니 사용하겠지.
      
# Sealed
- Virtual로 선언된 가상 메소드를 오버라이딩한 버전의 메소드가 오버라이딩 되지 않도록 봉인할 수 있다.
- ![image](https://user-images.githubusercontent.com/55792986/185403786-0f553666-5e3a-490c-bcd2-9c29afa5a538.png)
- ![image](https://user-images.githubusercontent.com/55792986/185403876-8345a38f-094d-4e42-867a-ccef624cd40b.png)

# Interface
- [내가 읽은 책](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/Books%20For%20Development/%EA%B0%9D%EC%B2%B4%EC%A7%80%ED%96%A5%EC%9D%98%20%EC%82%AC%EC%8B%A4%EA%B3%BC%20%EC%98%A4%ED%95%B4.md) (5장의 6번 항목에서 인터페이스를 자세하게 설명했다.)

# 인터페이스 왜 쓸까?
~~~c#
public class MyClass1 : ITestInterface
{
    public void NotInterInter()
    {
        Debug.Log("not interface");
    }
    public void InterInter()
    {
        Debug.Log("interface implemetation");
    }
}

public class MyClass2
{
    private ITestInterface obj = new MyClass1();
    
    public void Test()
    {
        obj.InterInter();
        //obj.NotInterInter(); 불가능 한 코드 
    }
}
~~~
  - **:star:어떤 객체를 인터페이스 타입으로 선언하면 해당 객체는 그 인터페이스에 명시된 메서드를 반드시 포함(당연히 구현까지)하고 있음을 보장한다.**
    - 이게 정말 중요한 내용이다.
  - 그러므로 아래처럼 어떤 메서드의 리턴 타입이 Iobservable 타입의 객체라면 얘는 Iobservable에 명시된 메서드를 호출할 수 있다.
    - ex : 아래 메서드 갖는 객체.DataObservable.Subscribe(람다);
~~~c#
public IObservable<byte> DataObservable
{
    get { return this.subject; } // Or this.subject.AsObservable();
}
~~~

# Virtual을 사용하면 자식 객체에서 override 한 메서드가 해당 함수를 대체한다.
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

/*result
None
Dog is barking.
Animal is eating.
===============================
None
Dog is barking.
Animal is eating.
*/
~~~
- ![20230904_133612](https://github.com/pjw960316/Unity_Client_Programmer/assets/55792986/5ce1fb02-2578-4d7c-b32b-83b708d2fc12)
