- 예제가 길어지고 복잡하니 가독성이 좋지 않다. 오히려.
- 잘못 만든 거 같다.
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