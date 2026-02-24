## :fireworks: 반드시 05장_2 [valueType] vs [referenceType] 혼동 포인트와 같이 본다.

<br><br>

## :fireworks: 여러 클래스와 메서드로 이루어진 프로젝트에서 매개변수 전달에서 버그를 만든 경험이 있다. <br> :fireworks: 그로 인해 매개변수가 복잡한 타입이면 메서드를 만들 때 살짝 주저를 하기도 한다. :star: 가독성을 위해 새로운 변수에 캐싱을 하는 것과 매개변수에 인자로 전달하는 것은 컴파일러 입장에서 동일한 동작이다.
~~~c#
public struct DTO
{
	public int a;
	public string b;
	
	public DTO(int aa, string bb)
	{
		a = aa;
		b = bb;
	}

	public string Print()
	{
		return $"{a} , {b}";
	}
}

public static void Main()
{
	var list = new List<DTO>();
	list.Add(new DTO(1,"ab"));
	
	// 1. 일반 캐싱
	DTO firstElement = list[0];
	firstElement.a = 5;
	firstElement.b = "ef";
	list[0].Print().Dump(); // 1 , "ab"
	
	// 2. 매개변수 전달
	TestDTO(list[0]);
	list[0].Print().Dump(); // 1 , "ab"
}

public static void TestDTO(DTO obj)
{
	obj.a = 5;
	obj.b = "ef";
}
~~~
- 둘 다 <ins>1 , "ab"</ins>로 결과를 출력한다.
- 그러므로, 캐싱과 매개변수에 인자 전달은 동등한 원리라고 정리 할 수 있다.
- 이 예제에서는 DTO가 struct고 valueType이니까 깊은 복사가 되어 원본을 변경하지 않는다.

<br><br>

## :fire: 05장_2와 같이 매개변수도 “매개변수 타입”이 valueType 인지, referenceType 인지가 전부다. <br> :fire: valueType이면 새로운 객체를 만들어서 메서드에서 사용하게 된다. <br> :fire: referenceType이면 원본 객체를 받아서 사용하게 된다. <br> :fire: **스택과 힙에 대한 존재는 조금 다를 수 있지만, 타입이 얕은 복사와 깊은 복사를 결정하는 주체다.**

#### [코드]
~~~c#
public struct DTO
{
	public int a;
	public string b;
	
	public DTO(int aa, string bb)
	{
		a = aa;
		b = bb;
	}
	
	public string Print()
	{
		return $"{a} , {b}";	
	}
}

public static void Main()
{	
	var list = new List<DTO>();
	list.Add(new DTO(1,"ab"));
	
	// 1. DTO의 필드를 추출해서 전달
	TestInt(list[0].a);
	TestString(list[0].b);
	list[0].Print().Dump(); // 1 , ab
	
	// 2. DTO자체를 전달
	TestDTO(list[0]);
	list[0].Print().Dump(); // 1 , ab
}

public static void TestInt(int num)
{
	num = 4;
}
public static void TestString(string str)
{
	str = "cd";
}

public static void TestDTO(DTO obj)
{
	obj.a = 5;
	obj.b = "ef"; 
}
~~~
- **DTO가 struct일 때**
  - 1번 예제와 2번 예제 모두 <ins>1 , ab</ins>가 결과로 나타난다.
- **DTO가 class일 때**
  - 1번 예제는 <ins>1 , ab</ins>고, 2번 예제는 <ins>5 , ef</ins>로 결과가 나타난다.
- 그러니까 기억해야 할 것은 단 하나다 매개변수 타입에 맞게 캐싱한다고 생각하면 된다. 

<br><br>

## :bangbang: 추후에 21장으로 옮기는 게 맞아보인다.
## :fireworks: struct를 매개변수로 하는 게 class를 매개변수로 하는 것 보다 무조건 이득인가? 아닐 수 있다. <br> :fireworks: GC 관련 내용이지만 여기서 정리한다. <br> 아래 내용을 천천히 읽어보자. (한 문장 정리가 어렵다.)
#### struct는 GC가 절대로 관리하지 않는다. -> 성능상 좋고, 값에 대해서 불필요한 객체를 만들지 않는다. (ex : 벡터)
- struct는 힙 메모리 안에 저장될 수 있지만(class 내부에 필드로 있을 때), 독립적인 힙 객체가 아니므로 GC가 추적하지 않는다. 
- GC는 힙에 있는 객체만 추적하고, 힙에 있는 데이터는 추적하지 않는다. struct가 힙에 저장될 때 객체로 저장되지 않는다.
  - struct는 new로 선언한다. 하지만 class처럼 힙에 존재하는 객체를 생성하는 것은 아니다. 
  - struct는 new로 선언하면 그저 값으로 존재하게 되고 초기화 하는 의미가 전부다.  
- struct는 전달 될 때 해당 크기만큼 메모리에 복사한다. 그러니까 크기가 크면 안 되는 것 이다.
- 하지만 class는 2가지 예제를 비교해보면, 결국 struct가 class보다 성능적 우위를 갖는 건 class가 new로 새로운 객체를 만들 때다. 그게 아니라 기존 객체를 쓰면 class는 8bytes의 변수만 만들고 전달한다.

#### [예제_1]
~~~c#
public void Main()
{
	// instance는 T 타입이고, 어디선가 만들어졌다고 가정한다.
	Test(instance); 
}

public void Test(T obj) where T : class
{}
~~~
- 여기서 obj라는 변수는 8bytes이고, instance의 주소를 갖고 있다. 그리고 GC는 발생하지 않는다.
- 그러므로 이런 방식은 오히려 struct보다 빠를 수 있다.
- 하지만 예제_2를 보자

#### [예제_2]
~~~c#
public void Main()
{
	// 생성해서 넣는다.
	var instance = new T();
	Test(instance); 
}

public void Test(T obj) where T : class
{}
~~~
- 결국, 참조 타입 매개변수를 갖는다면 힙에 할당하는 new를 허용하게 된다. 힙 객체 생성이 GC의 원인이 된다.

<br><br>

## :fire: valueType을 referenceType 처럼 전달하고 싶을 때 사용하는 키워드가 ref와 out이다. <br> :fire: 전달하기 직전의 값이 중요하면 ref를 사용한다. <br> :fire: 전달하기 직전의 값은 중요하지 않고 완전히 새롭게 사용한다면 out을 사용한다.
- 사용하는 의도에 따라 분류해서 사용한다.