## :fire: Built-In Type , Primitive Type , Value Type , Reference Type 관계도 
![alt text](./capture/20250214.png)
- Value Type 중 Primitive Type은 모두 struct다.
- <ins>소문자 string과 대문자 String은 완벽히 동일</ins>하다.
  - > C#의 string 키워드는 FCL 타입인 System.String으로 정확하게 연결되기 때문에, 둘 사이에는 전혀 차이점이 없기 때문이다.

<br><br>

## :fireworks: DTO를 만들 때 상황에 맞게 valueTuple 또는 struct 또는 class를 선택해서 만들 수 있어야 한다. <br>
## :fire: 데이터가 2개면 ValueTuple을 사용한다. Item1과 Item2가 valueType 또는 ReferenceType 이어도 상관없다.<br>
## :fire: 데이터가 3개 이상이고, 모든 데이터가 변경이 되지 않고, valueType이면 struct를 사용한다. <br>
## :fire: 데이터가 3개 이상이지만, 데이터의 변경이 발생하고, 데이터 중 1개라도 referenceType이면 class를 사용한다. 
#### 1. MSDN과 StackOverFlow 모두 이를 뒷받침 해준다.
> [MSDN] 

> AVOID defining a struct unless the type has all of the following characteristics: 

> It logically represents <ins>a single value, similar to primitive types</ins> (int, double, etc.). 

> It has an instance size under 16 bytes. 

> It is immutable. 

> It will not have to be boxed frequently. In all other cases, you should define your types as classes.

- :link:[Adding a reference to a list c# struct](https://stackoverflow.com/questions/13690509/adding-a-reference-to-a-list-c-sharp-struct?utm_source=chatgpt.com)

#### 2. struct에 List<T>를 넣으면 컴파일 에러가 발생할 가능성이 높아진다.
~~~c#
public struct Skill
{
	public int SkillId;
	public int Level;
	
	public Skill(int a , int b)
	{
		SkillId = a;
		Level = b;		
	}
}

public class SkillClass
{
	public int SkillId;
	public int Level;

	public SkillClass(int a, int b)
	{
		SkillId = a;
		Level = b;
	}
}

public class TestManager
{
	static void Main()
	{
		Skill qSkill = new Skill(111,1);
		Skill wSkill = new Skill(222,2);
		SkillClass eSkill = new SkillClass(333,3);
		
		var list = new List<Skill>();
		list.Add(qSkill);
		list.Add(wSkill);

		var list2 = new List<SkillClass>();
		list2.Add(eSkill);
		
		// complie ERROR CS1612
		//list[0].Level = 3;
		
		list2[0].Level = 77;
		list2[0].Level.Dump(); //result : 77
	}
}
~~~
> An attempt was made to modify a value type that is produced as the result of an intermediate expression but is not stored in a variable. This error can occur when you attempt to directly modify a struct in a generic collection

> This error occurs because value types are copied on assignment. When you retrieve a value type from a property or indexer, you are getting a copy of the object, not a reference to the object itself. The copy that is returned is not stored by the property or indexer because they are actually methods, not storage locations (variables). You must store the copy into a variable that you declare before you can modify it.

>The error does not occur with reference types because a property or indexer in that case returns a reference to an existing object, which is a storage location.

>If you are defining the class or struct, you can resolve this error by modifying your property declaration to provide access to the members of a struct. If you are writing client code, you can resolve the error by creating your own instance of the struct, modifying its fields, and then assigning the entire struct back to the property. As a third alternative, <ins>you can change your struct to a class. </ins>

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
