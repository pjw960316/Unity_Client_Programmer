## :fire: Built-In Type , primitiveType , valueType , referenceType 관계도 
![alt text](../capture/20250214.png)
> 일부 데이터 타입들은 너무나 일반적이고 당연한 것들이어서 많은 컴파일러들이 코드를 작성하는 동안 단순화된 문법의 형태로 이를 사용할 수 있도록 지원해주고 있다. 이 문법은 앞의 코드보다 더 읽고 이해하기 쉬우며, 당연히 System.Int32 타입을 사용하도록 지시하는 앞의 코드와 의미가 동일한 IL 코드를 만들어준다. 이와 같이 컴파일러가 직접 지원하는 데이터 타입들을 <ins>기본 타입(Primitive Type)</ins>이라고 부른다.
- 소문자 string과 대문자 String은 완벽히 동일하다.
  - C#의 string 키워드는 FCL 타입인 System.String으로 정확하게 연결되기 때문에, 둘 사이에는 전혀 차이점이 없기 때문이다.

<br><br>

## :fire: 모든 valueType은 System.ValueType <ins>클래스</ins>를 상속받는다. <br> :fire: 클래스를 상속 받으면 참조타입 같지만. <br> CLR이 valueType을 정의할 때 값 타입으로 정의했다.

<br><br>

## :fireworks: primitiveType 정리 <br> :fire: int == System.Int32 <br> :fire: long == System.Int64 <br> :fire: double == System.Double <br> :fire: 범위 이슈가 없다면 정수는 int를 사용하고, 소수는 double을 사용한다.
~~~c#
void Main()
{
	var a = 0; //int	
	var c = 0.1; //double
}
~~~
- 기본타입을 컴파일러가 위의 코드로 해석한다. 

<br><br>

## :fireworks: DTO를 만들 때 상황에 맞게 <ins>휴리스틱한 기준</ins>으로 <br> valueTuple 또는 struct 또는 class를 선택할 수 있어야 한다. <br> :fireworks: 당연히 성능과 용도 관점에서는 어폐가 있다. <br> 애당초 valueTuple은 DTO가 아니다. 내부 데이터가 각각 4bytes라고 가정한다. <br> :fire: valueTuple => 데이터가 2개 or 3개고, 데이터가 모두 immutable <br> :fire: struct => 데이터가 4개 ~ 5개고, 데이터가 모두 immutable <br> :fire: class => 데이터 중 하나라도 mutable reference 거나, 데이터가 매우 많고 큰 경우 <br> :fire: string은 immutable하므로 valueTuple 과 struct의 멤버로 사용하면 된다. 

#### 1. valueTuple 내부에 존재하는 string
~~~c#
void Main()
{
	var a = (1,"hi");
	var b = a;
	b.Item2 = "bye";
	
	a.Dump(); // 1 hi -> 원본 변경 X 
	b.Dump(); // 1 bye
}
~~~
- ValueTuple b는 ValueTuple a를 깊은 복사한다.
- valueTuple a와 b는 처음에 같은 string 주소를 공유한다. string은 참조 타입이니까. 그러나, string의 immutable한 특성 때문에 문자열 변경시에 새로운 문자열("bye)을 만들고 이 문자열의 주소를 b.Item2에 교체한다. 그러므로 원본이 변경되지 않는다.

#### 2. 자료 근거 
- MSDN과 StackOverFlow 모두 이를 뒷받침 해준다.
> [MSDN] 

> AVOID defining a struct unless the type has all of the following characteristics: 

> It logically represents <ins>a single value, similar to primitive types</ins> (int, double, etc.). 

> It has an instance size **<ins>under 16 bytes.</ins>** 

> It is **<ins>immutable.</ins>** 

> It will not have to be boxed frequently. In all other cases, you should define your types as classes.

- :link:[Adding a reference to a list c# struct](https://stackoverflow.com/questions/13690509/adding-a-reference-to-a-list-c-sharp-struct?utm_source=chatgpt.com)

#### 3. :book: 제프리
> 값 타입의 변수를 다른 값 타입의 변수로 대입하려고 할 때, 필드 단위로 하나씩 복제가 이루어지게 된다. 그러나 참조 타입의 변수끼리 대입이 발생하면 단순히 메모리 주소만 복제된다.

> 어떤 객체 하나를 두 개 이상의 참조 타입 변수가 가리키는 일이 있을 수 있으며, 이 때 어떤 변수 하나에서 연산을 실행하면 그 결과가 다른 변수에도 그대로 영향을 주게 된다. 달리 말하면, 값 타입의 변수들은 서로 구분된 객체들이며, 상호간에 영향을 주는 것은 불가능하다. 

#### 4. Struct로 사용 할 수 있으면 struct를 사용하면 좋은 이유
![alt text](../capture/20260130.png)
- 아래는 DTO를 class로 만들었을 때, 위에는 DTO를 struct로 만들었을 때의 차이.
- 해당 문제에서 DTO instance의 값을 변경하는 시도는 하지 않았고, 모든 멤버가 int기 때문에 struct로 사용했다.
~~~c#
public class DTO
{
    public int row;
    public int col;
    public int day;
            
    public DTO(int a, int b , int c)
    {
        row = a;
        col = b;
        day = c;
    }
}

public class CSharpHabit
{
    static void Main()
    {
		// 생략

        var queue = new Queue<DTO>();

		//생략
	}
}
~~~

<br><br>

## :fireworks: 두 객체의 같음에 대해 정리한다. <br> :fire: ValueType 끼리의 비교와 ReferenceType 끼리의 비교에서는 '=='을 편하게 사용해라. <br> 오버로딩이 되어있지 않으면 안전하다. 
- ValueType 끼리의 '=='은 값의 동일함만 비교한다. 단, struct는 따로 정의해주어야 한다.
- ReferenceType 끼리의 '=='은 연산자 오버로딩이 되어있지 않으면 Object.ReferenceEquals()과 동일하다. 이는 두 객체의 주소가 같은지 비교한다. 
> It determines whether the two objects represent the same object reference. If they do, the method returns true. This test is equivalent to calling the ReferenceEquals method. In addition, if both objA and objB are null, the method returns true.

> The implementation of Equals in the System.Object universal base class also performs a reference equality check, but it is best not to use this because, if a class happens to override the method, the results might not be what you expect. The same is true for the == and != operators. When they are operating on reference types, the default behavior of == and != is to perform a reference equality check. However, derived classes can overload the operator to perform a value equality check. To minimize the potential for error, <ins>it is best to always use ReferenceEquals when you have to determine whether two objects have reference equality.</ins> (MSDN) 

#### [문자열은 조심한다] (LinqPad 복붙 가능)
~~~c#
public static class MainManager
{
	public static void Main()
	{
		var obj = new Test();
		
		obj.DoTest();
	}
}

public class Test
{
	public Test()
	{
	}

	public void DoTest()
	{
		string a = "jiwon";
		string b = "jiwon";
		
		if(a==b)
		{
			"string is same".Dump();
		}
		
		StringBuilder aa = new StringBuilder("jiwon");
		StringBuilder bb = new StringBuilder("jiwon");
		
		if(aa==bb)
		{
			"stringBuilder is same".Dump();
		}
	}
}

// string is same
~~~
> String.Equals has an overload where a StringComparison argument can be provided to alter its sorting rules. (MSDN)
- string은 오버로딩하여 값을 비교한다. 그래서 참조타입이지만 특수하게 Object.ReferenceEquals()으로 동작하지 않는다.
- stringBuilder는 Object.ReferenceEquals()로 동작한다. 그래서 값이 같지만 객체가 다르기 때문에 같지 않다.

<br><br>

## :fire: 오버플로우가 발생할 것 같은 연산:star:(특히 돈 관련):star:에서는 <br> checked 코드블럭과 try-catch를 이용해서 exception handling을 하자.
#### [checked 예제]

~~~c#
void Main()
{
    Byte a = 126;
    Byte b = 125;
    Byte c = 2; //만약 기획 데이터라면?

    try
  {
    checked
    {
      a = (Byte)(a + b * c); //오버플로우 날까봐 두려운 코드를 checked로 감싸자.
    }
  }
  catch (OverflowException ex) // c가 2라면 overflow가 발생하고 예외가 잡힌다.
  {
    Console.WriteLine($"오버플로 예외 발생: {ex.Message}");
    a.Dump();  //126 + 125 * 2 지만 오버플로우 발생해서 126으로 출력.
  }
}
// result
// 오버플로 예외 발생: Arithmetic operation resulted in an overflow.
// 126
~~~
