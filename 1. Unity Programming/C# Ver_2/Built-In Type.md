## :fire: Built-In Type , Primitive Type , Value Type , Reference Type 관계도 
![alt text](./capture/20250214.png)
- Value Type 중 Primitive Type은 모두 struct다.
- <ins>소문자 string과 대문자 String은 완벽히 동일</ins>하다.
  - > C#의 string 키워드는 FCL 타입인 System.String으로 정확하게 연결되기 때문에, 둘 사이에는 전혀 차이점이 없기 때문이다.

<br><br>

## :fire: 오버플로우가 발생할 것 같은 연산:star:(특히 돈 관련):star:에서는 <br> :fire: checked 코드블럭과 try-catch를 이용해서 exception handling을 하자.
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

<br><br>

## :fire: ValueType Address는 ref, fixed를 이용해서 비교한다. <br> :fire: ReferenceType Address는 TypedReference, __makeref를 이용해서 비교한다.
#### [Address 비교]
~~~c#
void Main()
{
	AddressManager addressManager = new AddressManager();
	
	Test testObj1 = new Test();
	Test testObj2 = new Test();
	Test testObj1_Copy = testObj1;
	
	//1.
	addressManager.CompareAddress(ref testObj1.value, ref testObj1_Copy.value, true);
	
	//2.
	addressManager.CompareAddress(ref testObj1.value, ref testObj2.value);
	
	//3.
	addressManager.CompareAddress(ref testObj1.stringValue, ref testObj2.stringValue);
	
	//4.
	addressManager.CompareAddress(ref testObj1, ref testObj1_Copy);
	
	//5.
	addressManager.CompareAddress(ref testObj1, ref testObj2);
}

public class AddressManager
{
	private AddressManager GetThis()
	{
		return this;
	}

	public void CompareAddress<T> (ref T obj1, ref T obj2, bool checkBoxingTest = false)
	{
		if (typeof(T).IsValueType)
		{
			"[ValueType Compare]".Dump();
			CompareValueTypeAddress(ref obj1, ref obj2);
			
			if (checkBoxingTest)
			{
				"[ValueType Boxing Compare]".Dump();
				CompareValueTypeAddressUsingBoxing(obj1, obj2);
			}
		}
		else
		{
			"[ReferenceType Compare]".Dump();
			CompareReferenceTypeAddress (obj1, obj2);
		}
	}
	
	private void CompareValueTypeAddress<T> (ref T obj_1, ref T obj_2)
	{
		unsafe
		{
			string obj1_address = "";
			string obj2_address = "";
			
			fixed (T* ptr1 = &obj_1, ptr2 = &obj_2)
			{
				obj1_address = Convert.ToString((long)ptr1);
				obj2_address = Convert.ToString((long)ptr2);
			}
			
			$"{"obj_1 : "}{obj1_address}".Dump();
			$"{"obj_2 : "}{obj2_address}".Dump();

			(obj1_address == obj2_address ? "same\n" : "different\n").Dump();
		}
	}

	private void CompareReferenceTypeAddress<T> (T obj_1, T obj_2)
	{
		unsafe
		{
			TypedReference typedReference_1 = __makeref(obj_1);
			IntPtr ptr_1 = **(IntPtr**)(&typedReference_1);
			string obj1_address = Convert.ToString((long)ptr_1);

			TypedReference typedReference_2 = __makeref(obj_2);
			IntPtr ptr_2 = **(IntPtr**)(&typedReference_2);
			string obj2_address = Convert.ToString((long)ptr_2);

			$"{"obj_1 : "}{obj1_address}".Dump();
			$"{"obj_2 : "}{obj2_address}".Dump();

			(obj1_address == obj2_address ? "same\n" : "different\n").Dump();
		}
	}
	
	private void CompareValueTypeAddressUsingBoxing(object obj_1, object obj_2)
	{
		CompareReferenceTypeAddress<object>(obj_1, obj_2);
	}
}

public class Test
{
	public int value;
	public string stringValue;
	
	public Test()
	{
		value = 22;
		stringValue = "abcd";
	}
}

/*RESULT
------------------------------ 1 -----------------------
[ValueType Compare]
obj_1 : 1672689057016
obj_2 : 1672689057016
same

[ValueType Boxing Compare]
obj_1 : 1672689064800
obj_2 : 1672689064824
different

------------------------------ 2 -----------------------
[ValueType Compare]
obj_1 : 1672689057016
obj_2 : 1672689057048
different

------------------------------ 3 -----------------------
[ReferenceType Compare]
obj_1 : 1670744546184
obj_2 : 1670744546184
same

------------------------------ 4 -----------------------
[ReferenceType Compare]
obj_1 : 1672689057000
obj_2 : 1672689057000
same

------------------------------ 5 -----------------------
[ReferenceType Compare]
obj_1 : 1672689057000
obj_2 : 1672689057032
different
*/
~~~
- ref는 박싱을 막지 못한다.
- [ ] 둘 다 한번에 가능한 메서드도 있을 것 이다. 하지만 일단 이거로 하자.
- [ ] 이걸 이해하면 박싱과 클래스안의 int나 struct 이런 걸 모두 이해할 거로 기대한다.
- [ ] 1~5를 하나 하나 해석해서 적는다.
- [ ] 그래서 valuetype이 스택에 저장되면 생기는 이득은 무엇인가?
- [ ] 해당 코드를 확장해서 다양한 실험해보기.

## :fire: 클래스 내부에 있는 변수도 heap에 저장.
- heap에 저장되는 걸 알면 뭐가 좋은가?
- heap에 저장이 되는 것과 stack에 저장이 되는 게 callbyref이 정도 밖에 지식이 없는데. 이걸 넘어서는 걸 정리해야 한다.
- 어떤 클래스나 구조체를 생성할 때 내가 알아야 할 것
  - 회사에서 구조체를 많이 쓰지 않았는데 왜 였을까?
  - 사실 int도 구조체면. 구조체는 매우 흔한 개념인데.


