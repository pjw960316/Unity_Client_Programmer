# 목차
- [목차](#목차)
- [개요](#개요)
- [Action](#action)
- [Action Ref](#action-ref)
- [Action Invoke](#action-invoke)
- [Action 의문점](#action-의문점)
- [Func](#func)
- [Action 과 Func의 명확한 차이점](#action-과-func의-명확한-차이점)

# 개요
- Delegate에서 파생된 키워드와 문법을 이해한다.
- **정말 많이 쓴다.**
  
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

# Action Ref
- ![image](https://user-images.githubusercontent.com/55792986/212207753-45218da6-6914-4f8e-acd6-419c2ada6e28.png)

# Action Invoke
- ![image](https://user-images.githubusercontent.com/55792986/216921191-89b39638-ae26-4351-87d2-df270f77cc37.png)
  - 2번도 결과적으로 내부적으로는 Invoke를 하기 때문에 똑같다.
  - null 검사의 유무로 안정성이 1번이 뛰어나다.
    - Invoke가 좋다고 생각하고 명시적이다.
  
# Action 의문점
- =으로 1개만 덮을 수 있고, +=으로 여러개를 할 수 있다.
- 근데 왜 =으로 할까?
  - 낭비 아닌가

# Func
- 반환 값이 있는 Action이다.

# Action 과 Func의 명확한 차이점
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
- Action의 generic은 매개변수만.
- Func의 generic은 매개변수와 리턴 값 순서로 저장.
  - 1번 int와 2번 int는 매개변수, 3번 int는 리턴 값의 int를 담당한다.
- ![image](https://user-images.githubusercontent.com/55792986/207791912-ab0b1f25-447d-4778-9d20-544c19c06c3c.png)

