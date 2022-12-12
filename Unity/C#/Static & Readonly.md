# 목차
- [목차](#목차)

# 개요
- static은 공부를 해도 해도 끝이 없는 것 같다.

# PS
- 이 내용을 분명 어디에 적어 놓고 정리했었다.
- 찾아서 해당 내용과 비교한다.

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

# static은 왜 readonly를 같이 사용하는가?
### 1. 일반 상수
- 객체 지향에서 public은 접근이 용이하고 변경도 용이할 수 있다. 접근은 쉽게하고 변경은 불가능하게 하는 키워드라 생각한다.
~~~c#
     public class Program
    {
        //static-readonly
        public static readonly int NUM = 5; //static readonly 선언
        //entrance
        static void Main(string[] args)
        {
            WhyCantMakeInstance.PrintValueReadonly(); //다른 스크립트의 클래스의 메서드를 static으로 호출한다. 올바르게 호출된다.
        }
    }

    public static class WhyCantMakeInstance 
    {
        public static void PrintValueReadonly()
        {
            Console.Write("i am other class, plz write program.NUM value : " + Program.NUM);
            Program.NUM = 6; //ERROR
        }
    }
~~~
  - ![20221212_141428](https://user-images.githubusercontent.com/55792986/206965853-71e076ed-c32f-4590-90db-180e7a77d086.png)
  - 상수화 되었기에 변경할 수 없다.

### :star:2. 객체
- 그냥 static과 static readonly의 차이를 변수에서는 쉽게 알 수 있었다. 하지만 객체에서는 알기 쉽지 않다.
- 또한 어떤 상황에서 쓸까?
- 어떤 객체를 


# Static을 붙이면 어디에 저장되는가?
- 메모리의 데이터 영역에 저장된다.
- 스레드는 데이터 영역을 공유한다.
  - 그러면 모든 스레드에서 static은 참조 할 수 있을 것 이다.
  - 이게 임계 영역과 스레드의 공유 문제를 야기했던 것 같다. 


# 범위
- :star:static으로 선언을 한 메서드는 해당 클래스를 이용해서 모든 스크립트에서 접근이 가능하다.

# static의 초기화 시점
- 예전에 공부하다가 이 부분의 초기화 시점에 대한 글을 읽었던 기억이 난다.
- static을 좀 더 깊게 이해하고 해당 문서를 찾고 이해한다.