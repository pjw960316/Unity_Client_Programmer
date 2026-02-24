## :fireworks: 반드시 05장_2 [valueType] vs [referenceType] 혼동 포인트와 같이 본다.

<br><br>

## :fireworks: 여러 클래스와 메서드로 이루어진 프로젝트에서 매개변수 전달에서 버그를 만든 경험이 있다. <br> :fireworks: 그로 인해 매개변수가 복잡한 타입이면 메서드를 만들 때 살짝 주저를 하기도 한다. :star::star: 제일 중요한 개념은 결국, 가독성을 위해 캐싱을 하는 것과 매개변수에 전달하는 것은 같은 행위다.
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
- 둘다 <ins>1 , "ab"</ins>로 결과를 출력한다.
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

## :fire: valueType을 referenceType 처럼 전달하고 싶을 때 사용하는 키워드가 ref와 out이다. <br> :fire: 전달하기 직전의 값이 중요하면 ref를 사용한다. <br> :fire: 전달하기 직전의 값은 중요하지 않고 완전히 새롭게 사용한다면 out을 사용한다.
- 사용하는 의도에 따라 분류해서 사용한다.