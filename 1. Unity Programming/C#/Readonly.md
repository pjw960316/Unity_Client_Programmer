# 목차
- [목차](#목차)
- [일반 변수에 대한 static-readonly](#일반-변수에-대한-static-readonly)
- [객체에 대한 static-readonly](#객체에-대한-static-readonly)
- [잘못된 생각으로 접근 한 것 : 객체에 대한 static-readonly](#잘못된-생각으로-접근-한-것--객체에-대한-static-readonly)
- [Effective C#\_아이템2 : Const 보다는 Readonly가 좋다.](#effective-c_아이템2--const-보다는-readonly가-좋다)

# 일반 변수에 대한 static-readonly
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
- ![image](https://user-images.githubusercontent.com/55792986/206969948-29595421-d5ac-44cc-9a3f-edf7201b6bd5.png)


# 객체에 대한 static-readonly
- 왜 readonly 객체인데 멤버 변수의 값이 변경 가능한가?
- ![image](https://user-images.githubusercontent.com/55792986/206971250-8bdbfa43-5088-42a4-8dcf-137bf178733d.png)
- ![image](https://user-images.githubusercontent.com/55792986/206972017-9564f4c2-bb78-4a84-be50-2b671e6e25fa.png)
  - :star: readonly로 선언한 객체는 다른 객체로의 참조로 변경할 수 없다.

# 잘못된 생각으로 접근 한 것 : 객체에 대한 static-readonly 
- 나는 static-readonly로 객체를 만들면 객체의 생성자에서 초기화를 하고 그 후에 해당 값을 변경하지 못하는 줄 알았다. 하지만 이는 잘못 된 개념이다.
  - 이거 위의 글을 참고하면 바로 알 수 있다.
~~~c#

    //중략..
    //다른 클래스에서 아래의 객체를 만들었다.
    var obj = new PlayLol();
    obj.Initialize();

    public class PlayLol
    {
        //다음과 같이 일반 클래스에서 static으로 객체를 생성할 수 있다.
        //나는 해당 객체가 다른 스크립트(클래스)에서도 쉽게 사용 될 것 으로 기대를 했다.
        public static Alice notReadonlyAlice = new Alice();
        public static readonly Alice readonlyAlice = new Alice();
        
        public void Initialize()
        {
            //1.일반 선언 객체
            notReadonlyAlice.ad = 77;
            notReadonlyAlice.ap = 78;
            notReadonlyAlice.TestAlice(); //77 78
            
            //2. Readonly 선언 객체
            readonlyAlice.ad = 87;
            readonlyAlice.ap = 88;
            readonlyAlice.TestAlice(); //87 88 
        }
    }
~~~
  - 위의 코드에서는 오류가 없다. 값 변경은 자유롭다!

# Effective C#_아이템2 : Const 보다는 Readonly가 좋다.
- 컴파일타임 상수인 const는 성능이 매우 중요하고 상수의 값이 절대로 바뀌지 않는 경우에만 제한적으로 사용하는 것이 좋다.
- const는 메서드 내부에서도 선언할 수 있지만, readonly는 메서드 내에서는 선언 할 수 없다.
  - 클래스의 멤버를 선언하는 곳에 선언하자.