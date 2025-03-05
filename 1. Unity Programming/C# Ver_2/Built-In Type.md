## :fire: Built-In Type , Primitive Type , Value Type , Reference Type 관계도 
![alt text](./capture/20250214.png)
- Value Type 중 Primitive Type은 모두 struct다.
- <ins>소문자 string과 대문자 String은 완벽히 동일</ins>하다.
  - > C#의 string 키워드는 FCL 타입인 System.String으로 정확하게 연결되기 때문에, 둘 사이에는 전혀 차이점이 없기 때문이다.

<br><br>

## :fire: 오버플로우가 발생할 것 같은 연산에서는 <br> :fire: checked 코드 블럭 와 try-catch를 이용해서 exception handling을 하자.
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
			a = (Byte)(a + b * c); //오버플로우 날까봐 두려운 코드를 checked로 예외처리 하자.
		}
	}
	catch (OverflowException ex) // c가 2라면 overflow가 발생하고 예외가 잡힌다.
	{
		Console.WriteLine($"오버플로 예외 발생: {ex.Message}");
		a.Dump();  //126+125*2 지만 126으로 출력.
	}
}
// result
// 오버플로 예외 발생: Arithmetic operation resulted in an overflow.
// 126
~~~