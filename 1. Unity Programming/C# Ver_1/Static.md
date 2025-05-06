# 목차
- [목차](#목차)
- [Static을 붙이면 어디에 저장되는가?](#static을-붙이면-어디에-저장되는가)
- [Static Class](#static-class)
- [Static Method](#static-method)
- [:question:static의 초기화 시점](#questionstatic의-초기화-시점)
- [static을 많이 쓰면 문제가 되지 않을까?](#static을-많이-쓰면-문제가-되지-않을까)

<br/><br/><br/>

# Static을 붙이면 어디에 저장되는가?
- :star:메모리의 데이터 영역에 저장된다.
- 스레드는 데이터 영역을 공유한다.
  - 그러면 모든 스레드에서 static은 참조 할 수 있을 것 이다.
  - 이게 임계 영역과 스레드의 공유 문제를 야기했던 것 같다. 
- 내 생각 : Data 영역이므로 모든 스크립트에서 참조가 가능한 게 아닌가?
  
<br/><br/><br/>

# Static Class 
- Static Class는 객체를 만들 수 없다.
  - ![image](https://user-images.githubusercontent.com/55792986/206942597-1cf03389-9966-4d7a-9b28-b638b990ecce.png)
- 자기 자신 클래스를 객체처럼 이용하기 때문에 싱글턴과 헷갈릴 수 있다.
- 해당 클래스는 데이터 영역에 올라가므로 모든 클래스(스크립트)에서 사용이 가능하다.

<br/><br/><br/>

# Static Method
- Static Class의 하위 method는 모두 static method여야 한다. Static Class가 아니어도 method는 static으로 선언할 수 있다.
~~~c#
public class C1
    {
        public static void Test()
        {
            System.Console.Write("hi");
        }

        public void NonStaticMethod()
        {
            System.Console.Write("i need obj");
        }
    }
~~~
  - 에러가 없다.

<br/><br/><br/>

# :question:static의 초기화 시점
- 예전에 공부하다가 이 부분의 초기화 시점에 대한 글을 읽었던 기억이 난다.
- main함수 전에 선언되어 프로그램의 시작과 동시에 할당되고 프로그램이 종료되어야 메모리에서 소멸된다.
  - 내 생각 : 이를 생각해보면 프로그램은 어차피 메인에서 참조가 일어나므로 static에 대한 참조에서는 오류가 날 일이 없는가?

<br/><br/><br/>

# static을 많이 쓰면 문제가 되지 않을까?
- Question_1
  - ![image](https://user-images.githubusercontent.com/55792986/207466969-27501026-6fd2-4562-9296-ee9f2ccca1d3.png)
- Answer_1
  - ![20221214_082705](https://user-images.githubusercontent.com/55792986/207467175-65d0ab44-23f2-4644-a6f8-2fe77634cad0.png)
  - unsafe한 static method가 위험하다.
- Question_2
  - 메모리 영역에서 데이터 영역은 어떻게 관리되는가?
- Answer_2
  - 전역 변수와 Static 변수가 존재한다.
  - 추가적인 자료는 찾지 못했다.