# 목차
- [목차](#목차)
- [개요](#개요)
- [전체적인 결론](#전체적인-결론)
- [Action](#action)
- [Func](#func)
- [정리](#정리)

# 개요
- Delegate에서 파생된 키워드와 문법을 이해한다.

# 전체적인 결론
- Event, Action, Function 모두 delegate의 기본 목적인 메서드들을 담는 기조는 변하지 않는다.
- 하지만 delegate만 사용하면 불편하므로 C# 문법이 업그레이드 되면서 제공해주는 기능일 뿐 이다.
  
# Action
- Generic과 Delegate를 유용하게 사용하기 위해 C#에서 제공하는 문법이다.
- 먼저 generic을 이용해서 delegate에 넣을 메서드들의 타입에 대해서 자유로워졌다.
    - 
     ~~~c#
    //1. generic을 사용하지 않을 때
    //delegate와 함수를 클래스 내부에 선언한다.
    delegate void ActionDelegate(int arg1, int arg2);
    public static void Foo1(int arg1, int arg2)
    {
        Console.WriteLine("delegate" + arg1 + " " + arg2);
    }

    //해당 클래스의 메인에서 등록한다.
    ActionDelegate obj = Foo1;
    obj(1,2); //delegate 1 2 출력

    //2. Generic을 사용하면
    //delegate와 함수를 클래스 내부에 선언한다.
    delegate T1 ActionDelegate<T1,T2>(T1 arg1, T2 arg2);
    public static int Foo1(int arg1, double arg2)
    {
        Console.WriteLine("delegate" + arg1+arg2);
        return 1;
    }

    //해당 클래스의 메인에서 등록한다.
    ActionDelegate<int,double> obj = Foo1;
    obj(1,2);
    ~~~
- 하지만 매개변수가 1개, 2개, 3개, ... 15개에 대해서는 generic으로도 해결할 수 없어서 각각 대응을 해주어야 한다. 그러나 다행이도 C#에서는 이를 대응하기 위해 Action을 만들어서 제공해주고 있다.
    -  
    ~~~c#
        //Action에 넣을 함수
        public static void Foo2(int arg1, int arg2)
        {
            int sum = arg1 + arg2;
            Console.WriteLine("foo2" + sum);
        }
        
        public static void Foo3(int arg1, int arg2)
        {
            int sum = arg1 + arg2;
            Console.WriteLine("foo3" + sum);
        }

        //main에서 호출
        Action<int> myAction = Foo1;
        Action<int, int> myAction2 = Foo2;
        myAction2 += Foo3;
        myAction2(2,3);
    ~~~ 
- :star:**다양한 타입과 매개변수의 길이에 대해서 Action을 사용하면 편리하게 Delegate를 사용할 수 있다.**

# Action 의문점
- =으로 1개만 덮을 수 있고, +=으로 여러개를 할 수 있다.
- 근데 왜 =으로 할까?
  - 낭비 아닌가

# Func
- 반환 값이 있는 Action이다.

# Action Func
~~~c#
private void ActionFunc()
    {
        Action<int, int> a = (x,y) =>
        {
            Debug.Log($"{x}  {y}");
        };

        Func<int, int, int> b = (x, y) =>
        {
            var z = x + y;
            return z;
        };
    }
~~~
  - action의 generic은 매개변수만
  - func의 generic은 매개변수와 리턴 값 같이
    - 1번int와 2번 int는 매개변수, 3번 int는 리턴 값의 int를 담당

# 정리
- ![image](https://user-images.githubusercontent.com/55792986/207791912-ab0b1f25-447d-4778-9d20-544c19c06c3c.png)

# 나는 아직 Event에 대해 좀 더 자세히 읽고 정리해야 한다.
    - UniRX 때문
# Event에 대해 좀 더 깊게 공부해 본다.
### 1. Event를 이용할 때는 .Net의 EventHandler를 이용하는 방법이 좋다.
- [Reference](https://docs.microsoft.com/ko-kr/dotnet/api/system.eventhandler?view=net-6.0)
- .Net에서 이벤트를 일관된 패턴으로 사용하도록 구현해놓은 표준 객체
  - (object와 EventArgs)를 인자로 갖는 메서드를 등록한다.
### 2. public으로 선언해도 오직 해당 클래스 내부에서만 호출할 수 있습니다.
- 참고 자료_1
  - ![image](https://user-images.githubusercontent.com/55792986/186148873-168b521e-799c-41d9-8a5d-69990264e4e6.png)
  - ![image](https://user-images.githubusercontent.com/55792986/186146881-9ceb03ad-b7b9-44cc-babf-7bfd6a844e4f.png)
    - 다른 클래스에서 이벤트를 호출하려 하면 에러가 난다.
    
- 참고 자료_2 : 다른 클래스에서 이벤트를 호출하는 방법
  - <img width="659" alt="20220823_203316" src="https://user-images.githubusercontent.com/55792986/186147650-682687d7-f30c-437a-b223-c1626c33974e.png">
  - <img width="666" alt="20220823_203357" src="https://user-images.githubusercontent.com/55792986/186147772-512623d6-8e8e-4c4b-bf1b-e675f817d8df.png">

### 3. Event(==EventHandler)는 static으로 선언해야 하는가?
- 이건 실제로 코딩하면서 결정해보자. static으로 하면 이 이벤트처리기는 프로그램 전체에서 공유.

# Delegate 대신 Event를 사용하는 이유
- 이런 문제가 생길 수 있다.
- ![image](https://user-images.githubusercontent.com/55792986/207784130-8f985681-0f11-433b-9c45-9d0e7a614b7a.png)
  - B의 실수로 이전에 A가 델리게이트에 등록한 함수가 사라질 수 있다.
- 해결책 : Event 키워드
  - delegate에 event 키워드를 붙이면 해당 delegate에 메서드를 추가할 때 반드시 += 이나 -=을 이용해야 한다.

# Action Ref
- ![image](https://user-images.githubusercontent.com/55792986/212207753-45218da6-6914-4f8e-acd6-419c2ada6e28.png)

