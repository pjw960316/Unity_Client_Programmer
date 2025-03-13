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
- :star: **Main 함수에 있는 testobj1 인스턴스의 실제 메모리 주소는 스택에 저장된다. 그러나 인스턴스 내부에 존재하는 멤버들의 주소는 스택에 저장하지 않는다.**
  - stack에 저장한 인스턴스 메모리 주소를 보고 heap으로 이동을 한다.
  - heap에는 인스턴스의 멤버인 value와 stringValue가 <ins>순서대로 메모리에 저장</ins>되어 있기 때문에 스택에 이 들의 메모리 주소까지 저장할 필요가 없다.
  - 인스턴스는 일반적으로 각 멤버 변수가 선언된 순서대로 heap 메모리에 저장된다.