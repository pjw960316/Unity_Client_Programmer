## :fire: Built-In Type , Primitive Type , Value Type , Reference Type 관계도 
![alt text](./capture/20250214.png)
- Value Type 중 Primitive Type은 모두 struct다.
- <ins>소문자 string과 대문자 String은 완벽히 동일</ins>하다.
  - > C#의 string 키워드는 FCL 타입인 System.String으로 정확하게 연결되기 때문에, 둘 사이에는 전혀 차이점이 없기 때문이다.
- Value Type은 System.ValueType 타입으로부터 항상 상속된다.

<br><br>

## :fireworks: DTO를 만들 때 상황에 맞게 valueTuple 또는 struct 또는 class를 선택해서 만들 수 있어야 한다. <br> :fire: 데이터가 2개 or 3개고, 데이터가 모두 immutable => ValueTuple <br> :fire: 데이터가 4개 이상이고, 데이터가 모두 immutable => struct <br> :fire: 데이터가 2개 이상이지만, 데이터 중 1개라도 mutable referenceType => class <br> :fire: 헷갈리는 게 string인데, string은 immutable하므로 valueTuple 과 struct의 멤버로 사용하면 된다. 

#### 1. ValueTuple 내부에 존재하는 string
~~~c#
void Main()
{
	var a = (1,"hi");
	var b = a;
	b.Item2 = "bye";
	
	a.Dump(); // 1 hi
	b.Dump(); // 1 bye -> 원본 변경 X 
}
~~~
- ValueTuple b는 ValueTuple a를 깊은 복사한다.
- valueTuple a와 b는 처음에 같은 string 주소를 공유한다. string은 참조 타입이니까. 그러나, string의 immutable한 특성 때문에 문자열 변경시에 새로운 문자열("bye)을 만들고 이 문자열의 주소를 b.Item2에 교체한다. 그러므로 원본이 변경되지 않는다.

#### 2. 자료 근거 
- MSDN과 StackOverFlow 모두 이를 뒷받침 해준다.
> [MSDN] 

> AVOID defining a struct unless the type has all of the following characteristics: 

> It logically represents <ins>a single value, similar to primitive types</ins> (int, double, etc.). 

> It has an instance size under 16 bytes. 

> It is **<ins>immutable.</ins>** 

> It will not have to be boxed frequently. In all other cases, you should define your types as classes.

- :link:[Adding a reference to a list c# struct](https://stackoverflow.com/questions/13690509/adding-a-reference-to-a-list-c-sharp-struct?utm_source=chatgpt.com)

#### 3. :book: 제프리
> 값 타입의 변수를 다른 값 타입의 변수로 대입하려고 할 때, 필드 단위로 하나씩 복제가 이루어지게 된다. 그러나 참조 타입의 변수끼리 대입이 발생하면 단순히 메모리 주소만 복제된다.

> 어떤 객체 하나를 두 개 이상의 참조 타입 변수가 가리키는 일이 있을 수 있으며, 이 때 어떤 변수 하나에서 연산을 실행하면 그 결과가 다른 변수에도 그대로 영향을 주게 된다. 달리 말하면, 값 타입의 변수들은 서로 구분된 객체들이며, 상호간에 영향을 주는 것은 불가능하다. 

#### 4. Struct로 사용 할 수 있으면 struct를 사용하면 좋은 이유
![alt text](./capture/20260130.png)
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

## :fire: ValueType(int, ValueTuple, struct)를 ICollection<T>의 T로 사용 할 때 원본을 변경하지 않는다. <br> 값 복사한 후 대입을 하는 것 이다. <br> (int의 경우 눈속임이 발생하고, valueTuple과 struct는 컴파일러가 에러를 발생시켜 사전 차단한다.)
#### [int, valueTuple, struct, class -> 4가지를 List의 T로 사용]
~~~c#
public struct NumberStruct
{
	public int Num1;
	public int Num2;
	
	public NumberStruct(int a , int b)
	{
		Num1 = a;
		Num2 = b;
	}
}

public class NumberClass
{
	public int Num1;
	public int Num2;

	public NumberClass(int a, int b)
	{
		Num1 = a;
		Num2 = b;
	}
}

void Main()
{
	// 1. Int
	var intList = new List<int>();
	intList.Add(1);
	intList[0] = 2;
	intList[0].Dump(); 

	//2. Struct
	var structList = new List<NumberStruct>();
	structList.Add(new NumberStruct(1, 2));
	structList[0].Dump(); 
	
	// compile ERROR
	//structList[0].Num1 = 3;  

	//3. ValueTuple
	var tupleList = new List<(int, int)>();
	tupleList.Add((1 , 2));
	tupleList[0].Dump();
	
	// compile ERROR
	//tupleList[0].Item1 = 3;

	//4. Class
	var classList = new List<NumberClass>();
	classList.Add(new NumberClass(1, 2));
	classList[0].Num1 = 5; 
	classList[0].Dump(); // 얘는 원본이 변경된다.
}
~~~
![alt text](./capture/20260129.png)
- intList[0] = 2는 사실 컴파일러가 이렇게 동작시킨다. 그러니까 **원본을 참조해서 변경하는 것 처럼 보였다.**
~~~c#
int temp = intList[0]; // 값 복사
temp = 2;              // 값 변경
intList[0] = temp;     // 다시 넣기
~~~
- 그러나 structList[0].Num1 = 3; // ❌인 이유는
~~~c#
NumberStruct temp = structList[0]; // 값 복사
temp.Num1 = 3;                     // 복사본 수정
// 여기서 모호함이 발생하기에 C#은 컴파일에러를 낸다.
~~~
- valueTuple은 struct와 동일한 원리를 갖는다.
- class는 컴파일러가 값 복사를 하지 않고 참조를 하기 때문에 원본을 실제로 변경한다.

<br><br>

## :fireworks: Queue<(StringBuilder , int)> queue로 이해하기 <br> :fire: 값 복사인지 참조 복사인지 판단하는 것은 스택과 힙이 아니다. <br> :fire: 타겟 객체의 최종 타입이 어떤 타입인지 판단하면 된다. <br> (Queue -> ValueTuple -> StringBuilder니까 이 예제에서는 StringBuilder가 최종 타입) <br> :star: 최종 타입이 value type이면 값이 복사되고, reference type이면 참조값(=주소, =원본)이 복사된다.
~~~c#
void Main()
{
	Queue<(StringBuilder,int)> queue = new Queue<(System.Text.StringBuilder, int)>();
	StringBuilder sb = new StringBuilder();
	
	queue.Enqueue((sb,1));
	
	sb.Append("a");
	sb.Append("b");
	queue.Peek().Item1.Append("c");
	queue.Peek().ToString().Dump();
}

// result = (abc,1)
~~~
- 결과가 (abc,1)이 나온다. queue.Peek()을 통해 ValueTuple의 값 복사로 새로운 ValueTuple이 생성되지만 그 안에는 sb의 주소를 동일하게 저장하고 있다.
- 그러므로, 결국 같은 StringBuilder 객체인 sb를 참조해서 원본이 변경된다.

<br><br>

## :fireworks: 두 객체의 같음에 대해 정리한다. <br> :fire: 연산자 오버로딩이 되어있지 않는다고 가정한다. <br> '=='을 편하게 사용해라. <br> :fire: ValueType 끼리의 비교는 값의 동일함만 비교한다. <br> :fire: ReferenceType 끼리의 비교는 주소의 동일함만 비교한다. <br> 이는 static 메서드인 Object.ReferenceEquals()와 완전히 동일하다. <br> 당연하지만, ValueType과 ReferenceType에 대한 비교는 하지 않는다. (컴파일 에러!)
~~~c#
void Main()
{
	var num1 = 1;
	var num2 = 1;
	
	if(num1 == num2)
	{
		"Value Same".Dump();
	}
	
	var num3 = num2; //어차피 주소 공유 절대 하지 않는다.
	num3 = 3;
	
	if(num2 == num3)
	{
		"nice".Dump();
	}
	
	var list = new List<int>();
	var list2 = list;
	
	if(list == list2)
	{
		"Address Same".Dump();
	}
}
// Value Same
// Address Same
~~~
> It determines whether the two objects represent the same object reference. If they do, the method returns true. This test is equivalent to calling the ReferenceEquals method. In addition, if both objA and objB are null, the method returns true.

<br><br>

## :fireworks: .ToString()을 통해 Boxing을 이해한다. <br> 위 그림은 Object의, 아래 그림은 Int32의 .ToString() <br> :fire: .ToString()의 구조를 파악하면 Boxing이 발생하지 않음을 알고 편하게 사용이 가능하다.
![alt text](./capture/20260203_1.png)
![alt text](./capture/20260203_2.png)
- 보통 타입 정도는 명시적으로 알고 사용을 하기 때문에, MS에서 제공하는 override 된 .ToString()을 사용하게 된다.
- 그러면 박싱은 발생하지 않으며, object 타입에 대한 ToString()은 발생하니 이는 주의하자.

<br><br>

## :fire: Boxing을 피하고 싶다면 arrayList 대신에 List<T>를 쓰자. <br> :fire: 아래 그림과 내용을 읽고, 왜 박싱이 좋지 않은 지 이해한다. <br> :fire: 어차피 arrayList는 Legacy다.
![alt text](./capture/202504232.png)
- ArrayList에서 최종적으로 도달한 두 개의 int 객체는 각각 값 1과 2를 저장하는 <ins>Boxing된 객체</ins>이다.
  - 이 객체들은 값 타입이 참조 타입으로 변환되면서 Heap에 생성된 것으로, <ins>메모리 낭비</ins>의 대표적인 사례를 보여준다.
- 또한, 이 int 객체들은 배열처럼 연속된 메모리에 존재하지 않고,Heap 상에서 독립적으로 흩어져 할당된다.
  - 이로 인해 추가적인 <ins>참조 비용과 캐시 비효율성</ins>이 발생한다.
- 실제 클래스 해부
  - **ArrayList**
  - ![alt text](./capture/202504233.png)
  - **List**
  - ![alt text](./capture/202504234.png)
- Generic이 상위호환.

<br><br>

## :fire: Boxing 된 녀석의 GetType()를 하면 UnBoxing 된 타입이 나온다.
#### [arrayList로 확인]
~~~c#
void Main()
{
	ArrayList arrayList = new ArrayList();
	int a = 1;
	int b = 2;
	arrayList.Add(a); //boxing
	arrayList.Add(b); //boxing
	
	arrayList[1].GetType().Dump(); //unboxing 아니다!!!
}
~~~
- arrayList[1]는 object 타입이지만 Int32로 출력된다.

#### [참고만 하자 : Native C++의 .Net 런타임에서 Boxing을 확인하는 코드]
![alt text](./capture/20250423.png)
- Unbox 라는 키워드를 확인 할 수 있다.

> The answer is easy to spot. Prior to calling GetType() method, the boxing of the value type occurs (while the exact type is known to the compiler). Boxing operation allocates a new object on the heap, which layout is known to us already. In particular, it contains a proper MethodTable pointer.

<br>

> Hence GetType() is processed as usual. Since boxed object has a typical layout, we can use the standard Object.GetType() method which get object’s MethodTable and returns the :star:corresponding(상응하는) Type object.


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
