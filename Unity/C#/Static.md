# 목차
- [목차](#목차)
- [개요](#개요)
- [PS](#ps)
- [Static을 붙이면 어디에 저장되는가?](#static을-붙이면-어디에-저장되는가)
- [Static Class](#static-class)
- [Static Method](#static-method)
- [static의 초기화 시점](#static의-초기화-시점)

# 개요
- static은 공부를 해도 해도 끝이 없는 것 같다.

# PS
- 이 내용을 분명 어디에 적어 놓고 정리했었다.
- 찾아서 해당 내용과 비교한다.

# Static을 붙이면 어디에 저장되는가?
- :star:메모리의 데이터 영역에 저장된다.
- 스레드는 데이터 영역을 공유한다.
  - 그러면 모든 스레드에서 static은 참조 할 수 있을 것 이다.
  - 이게 임계 영역과 스레드의 공유 문제를 야기했던 것 같다. 
- 내 생각 : Data 영역이므로 모든 스크립트에서 참조가 가능한 게 아닌가?
  
# Static Class 
- Static Class는 객체를 만들 수 없다.
  - ![image](https://user-images.githubusercontent.com/55792986/206942597-1cf03389-9966-4d7a-9b28-b638b990ecce.png)
- 자기 자신 클래스를 객체처럼 이용하기 때문에 싱글턴과 헷갈릴 수 있다.
- 해당 클래스는 데이터 영역에 올라가므로 모든 클래스(스크립트)에서 사용이 가능하다.

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

# static의 초기화 시점
- 예전에 공부하다가 이 부분의 초기화 시점에 대한 글을 읽었던 기억이 난다.
- static을 좀 더 깊게 이해하고 해당 문서를 찾고 이해한다.