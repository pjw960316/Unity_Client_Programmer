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


~~~
- :star: **Main 함수에 있는 testobj1 인스턴스의 실제 메모리 주소는 스택에 저장된다. 그러나 인스턴스 내부에 존재하는 멤버들의 주소는 스택에 저장하지 않는다.**
  - stack에 저장한 인스턴스 메모리 주소를 보고 heap으로 이동을 한다.
  - heap에는 인스턴스의 멤버인 value와 stringValue가 <ins>순서대로 메모리에 저장</ins>되어 있기 때문에 스택에 이 들의 메모리 주소까지 저장할 필요가 없다.
  - 인스턴스는 일반적으로 각 멤버 변수가 선언된 순서대로 heap 메모리에 저장된다.

<br><br>

## :fire: class
- 위의 코드 예제 2번과 3번을 참고한다.


## 확실한 거
- class의 struct **멤버**는 heap에 저장(이건 안 중요함). 근데 valueType 성질을 보유
- class의 함수에 존재하는 지역변수 선언 struct와 다른 점. 