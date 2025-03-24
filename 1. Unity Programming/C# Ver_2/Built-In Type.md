## :fire: Built-In Type , Primitive Type , Value Type , Reference Type 관계도 
![alt text](./capture/20250214.png)
- Value Type 중 Primitive Type은 모두 struct다.
- <ins>소문자 string과 대문자 String은 완벽히 동일</ins>하다.
  - > C#의 string 키워드는 FCL 타입인 System.String으로 정확하게 연결되기 때문에, 둘 사이에는 전혀 차이점이 없기 때문이다.

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

<br><br>

## :fire: Struct는 기본적으로는 Stack에 생성 되지만, <br> Struct가 Class의 멤버로 존재할 때는 Heap에 생성된다.
- [지울 설명] : 이 개념은 heap에 생성되는 것의 GC와 연관지어서 학습하면 좋을 것 -> 당장은 그래서 뭐?

<br><br>

## :fire: Class 내부에 멤버로 존재하는 Struct는 <br> Class의 instance가 복사 될 때 deep-Copy가 일어나지 않는다.
- Deep-Copy의 개념 : 데이터를 복사할 때 완전히 새로운 메모리 공간에 새로운 객체를 생성하여 복사.
- **<ins>어떤 순간에도</ins> struct는 ValueType이다.** 하지만 ValueType이 항상 deep-copy를 하지는 않는다.

#### [지역변수 struct 와 class의 멤버인 struct의 복사 비교 예제]
~~~c#
void Main()
{
	$"************************************* 1. 지역변수 struct ************************************************".Dump();
	BasicStruct str_1 = new BasicStruct();
	BasicStruct str_2 = str_1; //둘 다 ValueType이다. 둘 다 stack에 있다. deep-copy가 일어난다.

	var str_1_address = ValueTypeAddressManager.GetAddress(ref str_1);
	var str_2_address = ValueTypeAddressManager.GetAddress(ref str_2);
	if (str_1_address != str_2_address)
	{
		$"{str_1_address} | {str_2_address} | different".Dump();
	}

	str_1.a = 3; //값 변경
	$"{str_1.a} vs {str_2.a}\n".Dump();

	$"************************************* 2. class의 멤버인 struct ************************************************".Dump();

	Book easyClassic = new Book(19000, "easyClassic");
	Book easyClassicCopy = easyClassic; //둘 다 ValueType이다. 둘 다 heap에 있다. 하지만 deep-copy가 일어나지 않는다.
	
	var member_1_address = easyClassic.GetStructMemberAddress();
	var member_2_address = easyClassicCopy.GetStructMemberAddress();
	if(member_1_address == member_2_address)
	{
		$"{member_1_address} | {member_2_address} | same".Dump();
	}
	
	easyClassic.SetFavoritePage(66,77); //원본 인스턴스에 존재하는 struct의 멤버 변경
	$"{easyClassic.myFavoritePage.num_1} vs {easyClassicCopy.myFavoritePage.num_1}".Dump();
}

public struct BasicStruct
{
	public int a;
	
	public BasicStruct()
	{
		a = 1;
	}
}

public class Book
{
	int price;
	string name;
	public FavoritePage myFavoritePage;
	
	public struct FavoritePage
	{
		public int num_1;
		public int num_2;
		
		public FavoritePage(int n1, int n2)
		{
			num_1 = n1;
			num_2 = n2;
		}
	}
	
	public Book(int price, string name)
	{
		this.price = price;
		this.name = name;
		
		SetFavoritePage(11,22);
	}
	
	public void SetFavoritePage(int n1,int n2)
	{
		myFavoritePage = new FavoritePage(n1 , n2);
	}
	
	public string GetStructMemberAddress()
	{
		return ValueTypeAddressManager.GetAddress(ref myFavoritePage.num_1);
	}
}

/*result
************************************* 지역변수 struct ************************************************
0xf2ceafcdd8 | 0xf2ceafcdd0 | different
3 vs 1

************************************* class의 멤버인 struct ************************************************
0x256184bafd4 | 0x256184bafd4 | same
66 vs 66
*/
~~~

<br><br>

## :fire: struct를 멤버로 포함한 instance를 method의 params로 전달할 때 <br> struct는 deep-copy가 일어나지 않는다.
#### [params로 전달하는 예제]
~~~c#
void Main()
{
	Book maskBook = new Book(19900, "mask");
	maskBook.ChangeFavoritePage(33,44); //instance의 struct 값 변경
	
	$"---------------------------------------- 1. instance의 주소와 instance의 struct 주소 출력 ---------------------------------------".Dump();
	ReferenceTypeAddressManager.GetAddress(maskBook).Dump();
	ValueTypeAddressManager.GetAddress(ref maskBook.myFavoritePage).Dump();
	
	MethodTestManager methodTestManager = new MethodTestManager(maskBook);
}

public class MethodTestManager
{
	public MethodTestManager(Book book)
	{
		$"---------------------------------------- 2. method의 params로 받은 instance의 주소와 instance의 struct 주소 출력 ---------------------------------------".Dump();
		ReferenceTypeAddressManager.GetAddress(book).Dump();
		ValueTypeAddressManager.GetAddress(ref book.myFavoritePage).Dump();
		
		$"---------------------------------------- 3. Main()에서 변경한 struct의 값이 반영되는지? ---------------------------------------".Dump();
		$"{book.myFavoritePage.num_1} | {book.myFavoritePage.num_2}".Dump();
	}
}

public class Book
{
	int price;
	string name;
	public FavoritePage myFavoritePage;

	public struct FavoritePage
	{
		public int num_1;
		public int num_2;

		public FavoritePage(int n1, int n2)
		{
			num_1 = n1;
			num_2 = n2;
		}
	}

	public Book(int price, string name)
	{
		this.price = price;
		this.name = name;

		myFavoritePage = new FavoritePage(11, 22);
	}

	public void ChangeFavoritePage(int n1, int n2)
	{
		myFavoritePage.num_1 = n1;
		myFavoritePage.num_2 = n2;
	}
}

/*result
---------------------------------------- 1. instance의 주소와 instance의 struct 주소 출력 ---------------------------------------
1807242423504
0x1a4c80aece4
---------------------------------------- 2. method의 params로 받은 instance의 주소와 instance의 struct 주소 출력 ---------------------------------------
1807242423504
0x1a4c80aece4
---------------------------------------- 3. Main()에서 변경한 struct의 값이 반영되는지? ---------------------------------------
33 | 44
*/
~~~
- params로 전달한 instance의 주소, instance의 struct member인 myFavoritePage의 주소가 모두 같게 유지된다. 
	- heap에 존재하며 deep-copy가 일어나지 않는다.
- 같은 주소를 가리키기 때문에 Main()에서 struct의 값을 변경하면 params로 받은 instance의 멤버의 myFavoritePage도 값이 변경된다.
- 직전 예제와 같은 내용일 수 있지만 한 번 더 정리한다.
- :star: 최종 정리 : <ins>클래스의 멤버로 선언된 struct는 해당 클래스를 메서드의 인자로 params로 전달해도 struct 내부까지 deep copy 되지 않으므로, 메모리 낭비 없이 안전하게 사용할 수 있다. 따라서 struct를 class 내부에서 데이터 묶음용으로 쓰는 건 좋은 방식이다.</ins>

<br><br>


## :fire: Main 함수에 있는 testobj1 인스턴스의 실제 메모리 주소는 스택에 저장된다. <br> 그러나 인스턴스 내부에 존재하는 멤버들의 주소는 스택에 저장하지 않는다.
- stack에 저장한 인스턴스 메모리 주소를 보고 heap으로 이동을 한다.
- heap에는 인스턴스의 멤버인 value와 stringValue가 <ins>순서대로 메모리에 저장</ins>되어 있기 때문에 스택에 이 들의 메모리 주소까지 저장할 필요가 없다.
- 인스턴스는 일반적으로 각 멤버 변수가 선언된 순서대로 heap 메모리에 저장된다. 