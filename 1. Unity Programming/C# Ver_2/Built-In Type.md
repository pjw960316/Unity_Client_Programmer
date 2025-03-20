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

## :fire: Main 함수에 있는 testobj1 인스턴스의 실제 메모리 주소는 스택에 저장된다. <br> 그러나 인스턴스 내부에 존재하는 멤버들의 주소는 스택에 저장하지 않는다.
- stack에 저장한 인스턴스 메모리 주소를 보고 heap으로 이동을 한다.
- heap에는 인스턴스의 멤버인 value와 stringValue가 <ins>순서대로 메모리에 저장</ins>되어 있기 때문에 스택에 이 들의 메모리 주소까지 저장할 필요가 없다.
- 인스턴스는 일반적으로 각 멤버 변수가 선언된 순서대로 heap 메모리에 저장된다.
  

<br><br>

## :fire: Struct는 기본적으로는 Stack에 생성 되지만, <br> Struct가 Class의 멤버로 존재할 때는 Heap에 생성된다.
- [지울 설명] : 이 개념은 heap에 생성되는 것의 GC와 연관지어서 학습하면 좋을 것 

<br><br>

## :fire: Class 내부에 멤버로 존재하는 Struct는 Class의 instance가 복사 될 때 Deep-Copy(==데이터를 복사할 때 완전히 새로운 메모리 공간에 새로운 객체를 생성하여 복사)가 일어나지 않는다.
- **<ins>어떤 순간에도</ins> struct는 ValueType이다.** 하지만 ValueType이 항상 deep-copy를 하지는 않는다.
