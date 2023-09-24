# 목차
- [목차](#목차)
- [개요](#개요)
- [인터페이스 왜 쓸까?](#인터페이스-왜-쓸까)
- [Interface](#interface)
- [인터페이스를 상속 받은 클래스에서 인터페이스의 메서드를 빈 메서드로 구현하는 것은 나쁜가?](#인터페이스를-상속-받은-클래스에서-인터페이스의-메서드를-빈-메서드로-구현하는-것은-나쁜가)

<br/><br/><br/>

# 개요
- Abstract 와 Virtual 문서에서 구분했다.
- 객체 지향의 핵심 요소고 비슷한 성향을 갖고 있지만 독립 문서를 만들 만큼 인터페이스는 중요하다.

<br/><br/><br/>

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

<br/><br/><br/>

# Interface
- [내가 읽은 책](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/Books%20For%20Development/%EA%B0%9D%EC%B2%B4%EC%A7%80%ED%96%A5%EC%9D%98%20%EC%82%AC%EC%8B%A4%EA%B3%BC%20%EC%98%A4%ED%95%B4.md) (5장의 6번 항목에서 인터페이스를 자세하게 설명했다.)

<br/><br/><br/>

# 인터페이스를 상속 받은 클래스에서 인터페이스의 메서드를 빈 메서드로 구현하는 것은 나쁜가?
- ![20230912_133436](https://github.com/pjw960316/Unity_Client_Programmer/assets/55792986/3c5578d2-4edf-43fc-83e1-1649724a983e)
- 전혀 나쁘지 않다.
