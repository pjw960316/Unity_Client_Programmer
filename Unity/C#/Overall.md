# 목차
- [목차](#목차)
- [개요](#개요)
- [var](#var)
- [객체](#객체)
- [Elvis Operator (= '?' = 널 조건부 연산자)](#elvis-operator----널-조건부-연산자)
- [Casting](#casting)
    - [1. Casting 기초](#1-casting-기초)
    - [2. is, as](#2-is-as)
- [크기 비교](#크기-비교)
- [Namespace \& Using](#namespace--using)
    - [1. Namespace](#1-namespace)
    - [2. Using](#2-using)
- [Enum](#enum)
    - [1. 개념](#1-개념)
    - [2. 특징](#2-특징)
    - [3. 연습](#3-연습)

# 개요
- 분류 하기 애매한 친구들은 여기에 정리하고 추후에 분류한다.
  
# var
- C는 초기화를 하지 않으면 쓰레기 값이 들어가나, C#은 초기화를 하지 않으면 에러가 난다.
- 우변의 표현식을 보고 좌변의 타입을 결정한다.
  - Rider는 var가 뭔지 적어도 준다.

# 객체
- C#의 모든 것은 객체다.
~~~c#
    var myString = 10.ToString(); //이런 게 된다...
~~~

# Elvis Operator (= '?' = 널 조건부 연산자)
~~~C#
string s1 = "hello"; //stack에 메모리를 저장하는 s1, heap에 실제 데이터인 hello를 저장.
string s2 = null; //stack에 메모리를 저장하는 s2, 하지만 heap에는 실제 데이터가 없다. 즉 참조하고 있지 않다.
int n1 = 10; // stack에 n1 변수를 만들고 그에 10이라는 값을 저장
Nullable<int> n2 = null; //nullable은 int와 bool을 모두 저장해서 stack에 n2 변수를 만들고, 이 것이 값이 없음을 표현한다.
int? n3 = null; //Nullable<int> n3 = null과 완벽히 동일한 코드다.
~~~
  - 코드에서 '?' 키워드를 많이 보았다.

# Casting
### 1. Casting 기초
- 데이터 손실이 발생하지 않은 경우 암시적 형 변환이 될 수 있다.
- 데이터 손실이 발생하는 경우 명시적 캐스팅을 반드시 해야한다.
~~~c#
int n = 3;
double d = 3.4;

d = n; // int를 double로 변환 시키는 것이므로 암시적으로 허락한다.
n = d; // double을 int로 변환 시키는 과정에서 데이터의 손실이 발생(0.4만큼)하므로 이는 에러가 난다. n = (int)d로 해주어야 한다.
~~~

### 2. is, as
- is
  - 참조 변수가 가리키는 실제 타입을 조사 할 때 사용한다.
- :star:as
  - 해당 객체의 타입이 as 뒤에 타입이랑 같은지 비교하고 같으면 그대로 나오고, 다르면 null을 리턴한다.
    - ex : jiwon as Point 
      - jiwon이 Point(클래스) 타입이면 Point로 캐스팅 하고, 아니면 null로 리턴한다.
  - null을 리턴했을 때 if를 이용해서 예외처리를 한다.



# 크기 비교
- ![image](https://user-images.githubusercontent.com/55792986/206983402-94306f9b-3c64-40b9-9fcd-f5a56dfd5888.png)
  - 사용자 지정 CompareTo()를 만들려면 이를 오버라이딩 해서 함수를 직접 만든다.
# Namespace & Using
### 1. Namespace
- ![image](https://user-images.githubusercontent.com/55792986/206999921-74a8a077-3a62-41ec-a9c3-c8934a685f21.png)
- 같은 이름의 클래스에 대한 중복을 해결해 주는 키워드이다.
- [C# Study](https://www.csharpstudy.com/CSharp/CSharp-namespace.aspx)


### 2. Using
- C++의 include 같은 기능.
- 매 번 namespace를 적기 귀찮아서 만들어진 키워드이다.
  - 이걸 적으면 해당 영역에 있는 것 들을 사용할 때 귀찮게 적지 않게 된다.
- [Naver Blog](https://m.blog.naver.com/bug_ping/221425846342)
~~~c#
class Program
    {
        static void Main()
        {
            Point pt = new Point(1, 2);
            System.Console.WriteLine(pt.x.ToString() , pt.y.ToString());
        }
    }
~~~
    - using system을 제거하면 console.writeline에 대해서 system.을 붙여 주어야 한다.


# Enum
### 1. 개념
- ![image](https://user-images.githubusercontent.com/55792986/207493471-ca28e90a-8012-4201-9731-2b3dcce0b7d5.png)
- ![image](https://user-images.githubusercontent.com/55792986/207494045-fedbcda8-f442-48bb-8e63-543a0a1a81ac.png)
  - :star:Enum은 상수를 저장하는 특수한 클래스다.

### 2. 특징
- 클래스 외부에도 선언이 가능하고 내부에도 선언이 가능하다.

### 3. 연습
~~~c#
    public enum Color
    {
        Red,
        Green = 10,
        Blue
    }
    static void Main(string[] args)
    {
        Console.WriteLine(StringFromColor(Color.Red));
        Console.WriteLine(StringFromColor(Color.Green));
        Console.WriteLine(StringFromColor(Color.Blue));
    }

    public static string StringFromColor(Color c) 
    {
        switch (c) 
        {
            case Color.Red:
                return c.ToString() + " "  + ((int)c).ToString();
            case Color.Green:
                return c.ToString() + " "  + ((int)c).ToString();
            case Color.Blue:
                return c.ToString() + " "  + ((int)c).ToString();
            default:
                return "Invalid color";
        }
    }
~~~
  - Main 함수와 StringFromColor 함수는 어떤 클래스에 포함되어 있고, enum Color는 그 외부에 있다.
    - 중략이 있다.
  - enum은 무조건 객체를 만들어야 하나?
  - 아닌 경우가 더 많다. 그냥 enum 그 자체의 이름을 이용하는 경우가 더 많다. Color.Red 처럼.
    - 근데 static으로 선언하지 않았는데 어떻게 스크립트.클래스
    