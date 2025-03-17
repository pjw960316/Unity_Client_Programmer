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

	"1. 복사본의 인스턴스 주소 비교".Dump();
	addressManager.CompareAddress(ref testObj1, ref testObj1_Copy);
	
	"2. 복사본의 인스턴스 내부 멤버 비교".Dump();
	addressManager.CompareAddress(ref testObj1.value, ref testObj1_Copy.value);
	addressManager.CompareAddress(ref testObj1.person, ref testObj1_Copy.person);
	addressManager.CompareAddress(ref testObj1.person.age, ref testObj1_Copy.person.age);
	addressManager.CompareAddress(ref testObj1.person.name, ref testObj1_Copy.person.name);

	"3. 복사본의 인스턴스 값 변경 후 내부 멤버 비교".Dump();
	testObj1.value = 33; 
	testObj1.person.age = 8;
	testObj1.person.name = "jitwo";

	addressManager.CompareAddress(ref testObj1.value, ref testObj1_Copy.value); //boxing 검사 X
	addressManager.CompareAddress(ref testObj1.person.age, ref testObj1_Copy.person.age);
	addressManager.CompareAddress(ref testObj1.person.name, ref testObj1_Copy.person.name);

	"4. 복사본의 인스턴스 박싱 비교".Dump();
	addressManager.CompareAddress(ref testObj1.value, ref testObj1_Copy.value,true);
	
	"5. 서로 독립적으로 생성한 인스턴스 주소 비교".Dump();
	addressManager.CompareAddress(ref testObj1, ref testObj2);
	
	"6. 서로 독립적으로 생성한 인스턴스 내부 멤버 비교".Dump();
	addressManager.CompareAddress(ref testObj1.value, ref testObj2.value);
	addressManager.CompareAddress(ref testObj1.stringValue, ref testObj2.stringValue);
}

public class AddressManager
{
	private AddressManager GetThis()
	{
		return this;
	}

	public void CompareAddress<T>(ref T obj1, ref T obj2, bool checkBoxingTest = false)
	{
		if (typeof(T).IsValueType)
		{
			if (checkBoxingTest)
			{
				"[ValueType Boxing Compare]".Dump();
				CompareValueTypeAddressUsingBoxing(obj1, obj2);
			}
			else
			{
				"[ValueType Compare]".Dump();
				CompareValueTypeAddress(ref obj1, ref obj2);
			}
		}
		else
		{
			"[ReferenceType Compare]".Dump();
			CompareReferenceTypeAddress(obj1, obj2);
		}
	}

	private void CompareValueTypeAddress<T>(ref T obj_1, ref T obj_2)
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

	private void CompareReferenceTypeAddress<T>(T obj_1, T obj_2)
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
	public Person person;
	
	public struct Person 
	{
		public int age;
		public string name;
		
		public Person(int age, string name)
		{
			this.age = age;
			this.name = name;			
		}
	}
	
	public Test()
	{
		value = 22;
		stringValue = "abcd";
		person = new Person(30, "jiwon");
	}
}

/*RESULT
1. 복사본의 인스턴스 주소 비교
[ReferenceType Compare]
obj_1 : 2343732696632
obj_2 : 2343732696632
same

2. 복사본의 인스턴스 내부 멤버 비교
[ValueType Compare]
obj_1 : 2343732696648
obj_2 : 2343732696648
same

[ValueType Compare]
obj_1 : 2343732696656
obj_2 : 2343732696656
same

[ValueType Compare]
obj_1 : 2343732696664
obj_2 : 2343732696664
same

[ReferenceType Compare]
obj_1 : 2342906927344
obj_2 : 2342906927344
same

3. 복사본의 인스턴스 값 변경 후 내부 멤버 비교
[ValueType Compare]
obj_1 : 2343732696648
obj_2 : 2343732696648
same

[ValueType Compare]
obj_1 : 2343732696664
obj_2 : 2343732696664
same

[ReferenceType Compare]
obj_1 : 2342906930144
obj_2 : 2342906930144
same

4. 복사본의 인스턴스 박싱 비교
[ValueType Boxing Compare]
obj_1 : 2343732765368
obj_2 : 2343732765392
different

5. 서로 독립적으로 생성한 인스턴스 주소 비교
[ReferenceType Compare]
obj_1 : 2343732696632
obj_2 : 2343732696680
different

6. 서로 독립적으로 생성한 인스턴스 내부 멤버 비교
[ValueType Compare]
obj_1 : 2343732696648
obj_2 : 2343732696696
different

[ReferenceType Compare]
obj_1 : 2342906920880
obj_2 : 2342906920880
same

*/
~~~
- :star: **Main 함수에 있는 testobj1 인스턴스의 실제 메모리 주소는 스택에 저장된다. 그러나 인스턴스 내부에 존재하는 멤버들의 주소는 스택에 저장하지 않는다.**
  - stack에 저장한 인스턴스 메모리 주소를 보고 heap으로 이동을 한다.
  - heap에는 인스턴스의 멤버인 value와 stringValue가 <ins>순서대로 메모리에 저장</ins>되어 있기 때문에 스택에 이 들의 메모리 주소까지 저장할 필요가 없다.
  - 인스턴스는 일반적으로 각 멤버 변수가 선언된 순서대로 heap 메모리에 저장된다.

<br><br>

## :fire: class 내부에 존재하는 valueType (ex : int , struct)도 모두 힙에 저장된다. <br> :fire: class 내부에 존재하는 struct와 struct의 멤버 모두 힙에 저장된다. <br> :fire: 인스턴스와 인스턴스 복사본을 만들면 인스턴스들의 struct 멤버는 valueType 이지만 <br> 값 복사가 일어나지 않고 같은 주소를 갖는다.
- 위의 코드 예제 2번과 3번을 참고한다.