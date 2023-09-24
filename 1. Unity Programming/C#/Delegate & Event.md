# 목차
- [목차](#목차)
- [해야 하는 작업](#해야-하는-작업)
- [Delegate 기본 개념](#delegate-기본-개념)
- [Delegate에 메서드를 추가하는 방법](#delegate에-메서드를-추가하는-방법)
- [:star:delegate 그래서 왜 사용할까?](#stardelegate-그래서-왜-사용할까)
    - [1. 메서드의 주소 값을 델리게이트에 저장하기 때문에 메서드를 인자로 넘겨주기에 용이하다.](#1-메서드의-주소-값을-델리게이트에-저장하기-때문에-메서드를-인자로-넘겨주기에-용이하다)
    - [2. 하나의 델리게이트에 여러 개의 메서드 주소를 저장할 수 있다. (=Delegate Chain)](#2-하나의-델리게이트에-여러-개의-메서드-주소를-저장할-수-있다-delegate-chain)
- [Delegate 기본 개념](#delegate-기본-개념-1)
- [Delegate \& Event의 사용 이유](#delegate--event의-사용-이유)
- [이벤트 주도적 프로그래밍](#이벤트-주도적-프로그래밍)

<br/><br/><br/>

# 해야 하는 작업
- Unity Study (2022.4 ~ 2022.6)의 delegate migration
- 작업을 완료하면 이 부분을 지운다. 

<br/><br/><br/>

# Delegate 기본 개념
- :star:**Delegate = 메서드의 주소(함수 포인터)의 모임**
  - 메서드 모양이 델리게이트와 같으면 델리게이트에 메서드의 주소를 저장한다.
~~~c#
delegate void FUNC(int arg) //FUNC는 타입이고, 이는 메서드의 호출정보를 담는 타입이다.
~~~
- ![image](https://user-images.githubusercontent.com/55792986/207513007-73a8072e-b444-4414-b101-103d61dce3fa.png)
  - Delegate는 사실 클래스다.
    - 참조 타입이다.
  - System.MulticastDelegate를 상속받는 클래스다.

<br/><br/><br/>

# Delegate에 메서드를 추가하는 방법
~~~c#
    delegate void FUNC();
    public class DelegateClass
    {
        //delegate에 넣을 static method
        public static void TestStaticMethodDelegate()
        {
            Console.WriteLine("TestStaticMethodDelegate");
        }

        //delegate에 넣을 instance method
        public void TestInstanceMethodDelegate()
        {
            Console.WriteLine("TestInstanceMethodDelegate");
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            FUNC myDelegate = DelegateClass.TestStaticMethodDelegate; //static method는 클래스.메서드로 추가시킨다.

            DelegateClass obj = new DelegateClass();
            myDelegate += obj.TestInstanceMethodDelegate; //instance method는 객체를 만들고 객체.메서드로 추가시킨다.
            myDelegate(); //위에서 등록시킨 두 함수가 호출된다.
        }
    }
~~~
- 정리본
  - ![image](https://user-images.githubusercontent.com/55792986/207514295-0876e973-aebe-46ef-9e01-849c157caaf8.png)

<br/><br/><br/>

# :star:delegate 그래서 왜 사용할까?
### 1. 메서드의 주소 값을 델리게이트에 저장하기 때문에 메서드를 인자로 넘겨주기에 용이하다.
- ![image](https://user-images.githubusercontent.com/55792986/207515384-6d15e198-a42a-42e8-8563-4ff73ce4af06.png)

### 2. 하나의 델리게이트에 여러 개의 메서드 주소를 저장할 수 있다. (=Delegate Chain)
- 연산자를 통해 메서드들을 델리게이트에 추가하고 제거할 수 있다.
- 델리게이트에 여러 메서드(모두 리턴 값이 존재)를 저장한다. 모든 메서드에 대해서 리턴 값을 갖고 싶다면 .GetInvocationList() 메서드를 이용한다.

<br/><br/><br/>

# Delegate 기본 개념 
- :link:[Unity_study](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/Unity/Unity%20Study%20(2022.04%20~%202022.06).pdf)

<br/><br/><br/>

# Delegate & Event의 사용 이유
- :star:사용 이유 : 이벤트를 사용하지 않으면 메서드들을 호출 시킬 때 메서드를 보유한 객체를 메서드들을 호출시키는 스크립트에서 선언을 해야 한다.
  - 다시 말해 **스크립트가 복잡**해진다.
- **이벤트를 사용하면 스크립트(클래스)간에 연결이 필요 없다.**
  - :star:이벤트에 어떤 메서드들이 등록되어 있는지 알 필요가 없다.  
  - :link:[Reference](https://daebalstudio.tistory.com/entry/%EC%9D%B4%EB%B2%A4%ED%8A%B8-%EC%99%84%EB%B2%BD%ED%95%98%EA%B2%8C-%EC%9D%B4%ED%95%B4%ED%95%98%EA%B8%B0)

<br/><br/><br/>

# 이벤트 주도적 프로그래밍
- 면접 때 잘못 이해하고 있어서 면접관님에게 지적 받은 부분이다. 제대로 이해하고 다시 작성해본다.
- 멤버 변수의 값이 변경 되는 시점에 이벤트 매니저에게 해당 이벤트를 처리를 요청하는 기법.
  
