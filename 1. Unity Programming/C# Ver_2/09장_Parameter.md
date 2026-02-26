## :fireworks: 반드시 05장_2 [valueType] vs [referenceType] 혼동 포인트와 같이 본다.

<br><br>

## :fireworks: 여러 클래스와 메서드로 이루어진 프로젝트에서 매개변수 전달에서 버그를 만든 경험이 있다. <br> :fireworks: 그로 인해 매개변수가 복잡한 타입이면 메서드를 만들 때 살짝 주저를 하기도 한다. <br> :star: 가독성을 위해 새로운 변수에 캐싱을 하는 것과 매개변수에 인자로 전달하는 것은 컴파일러 입장에서 동일한 동작이다.
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
  - 1번 예제와 2번 예제 모두 <ins>1 , ab</ins>가 결과로 나타난다. 원본이 변하지 않는다.
- **DTO가 class일 때**
  - 1번 예제는 <ins>1 , ab</ins>고, 2번 예제는 <ins>5 , ef</ins>로 결과가 나타난다.
- 그러니까 기억해야 할 것은 단 하나다 매개변수 타입에 맞게 캐싱한다고 생각하면 된다. 

<br><br>

## :fire: **<ins>매개변수</ins>**는 메서드 호출시 메서드 내에서 자동으로 생성되는 **<ins>지역변수</ins>**다. <br> :fire: 그러므로 참조 타입을 인자로 전달하면 인자와 매개변수는 서로 다른 변수다. <br> 다만 그 둘이 같은 주소를 가리키기 때문에 같아 보인 것 이다. <br> :fireworks: 다음의 예제를 통해 매개변수를 새로운 타입으로 초기화하면 원본이 바뀌지 않는 이유를 확인한다.
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
	public int[] arr;
	
	public Test()
	{
		arr = new int[2];
		
		arr[0] = 1;
		arr[1] = 2;
	}	
	
	public void DoTest()
	{
		// 1. 실수할 여지가 많은 코드
		ConvertArrayBadCode(arr);
		Print();
		
		// 2. 실수할 여지를 차단하는 좋은 코드
		arr = ConvertArrayGoodCode(arr);
		Print();
	}
	
	public void ConvertArrayBadCode(int[] arr)
	{
		int len = arr.Length;
		var newArr = new int[len];
		
		//deep copy
		for(int idx=0; idx<len; idx++)
		{
			newArr[idx] = arr[idx];			
		}
		
		newArr[0] = 77;
		newArr[1] = 88;
		
		// 문제 코드
		arr = newArr;
	}
	
	public int[] ConvertArrayGoodCode(int[] arr)
	{
		int len = arr.Length;
		var newArr = new int[len];

		//deep copy
		for (int idx = 0; idx < len; idx++)
		{
			newArr[idx] = arr[idx];
		}

		newArr[0] = 77;
		newArr[1] = 88;
		
		return newArr;
	}

	public void Print()
	{
		foreach(var element in arr)
		{
			element.Dump();
		}
	}
}
~~~
- 1번 Print()에서는 1,2가 나온다. -> 원본이 변경되지 않는다.
- 2번 Print()에서는 77,88이 나온다. -> 원본이 변경된다.
- arr = newArr는 결국 ConvertArrayBadCode 메서드의 지역변수인 arr(필드의 arr가 아님)에 새로운 주소를 저장하는 것 이다.
- 그러므로, 당연히 원본인 인스턴스의 필드인 arr는 변경되지 않는다.
- ConvertArrayGoodCode 처럼 변경을 시키고 해당 arr를 리턴해서, 원본 arr에 초기화 해주는 게 제일 좋은 방식이다.
  - 보통 이런 Convert 메서드는 원본 변경의 책임을 갖기 때문이다. 

<br><br>

## :fire: valueType을 referenceType 처럼 전달하고 싶을 때 사용하는 키워드가 ref와 out이다. <br> :fire: 전달하기 직전의 값이 중요하면 ref를 사용한다. <br> :fire: 전달하기 직전의 값은 중요하지 않고 완전히 새롭게 사용한다면 out을 사용한다.
- 사용하는 의도에 따라 분류해서 사용한다.