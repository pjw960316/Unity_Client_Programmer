## :fireworks: 숫자 기본체계
- **컴파일러는 30은 int로, 30.0은 double로 해석한다.**
- :star:**정수 비교에 관해서는 long이 double보다 정밀하다.**
  - long과 double은 모두 8바이트다.
  - long은 64비트를 모두 정수 표기에 사용하고, double은 52비트만 정수 표기에 사용한다.

<br><br>

## :fire: 서로 다른 타입 끼리 계산할 때 C#은 더 범위가 넓은 타입을 리턴한다.
> If the operands are different integral or floating-point types, their values are converted to the closest containing type, if such a type exists.
  - int + long = long
    - int가 long으로 캐스팅되어 계산된다.
  - double / int = double
~~~c#
var arr = new long[52];
arr[0] = 1; // int는 long으로 캐스팅 된다.
arr[1] = 1;

for (int i = 2; i <= 51; i++)
{
    arr[i] = (arr[i-1] * 31) % 1234567891;
    // arr[i-1]은 long이고, 31은 int니까 결과는 long이다.
    // 1234567891은 int니까 long % int니까 결과는 long이다.
}
~~~

<br><br>

## :fire: double / double = double로 하자. <br> :fire: 이게 기억하기 쉽다.
- :airplane:[How can I divide two integers to get a double?](https://stackoverflow.com/questions/661028/how-can-i-divide-two-integers-to-get-a-double)

<br><br>

## :fireworks: 자주 실수하는 오류
- **int 와 long**
  - 100억은 int로 선언하면 오버플로우가 나는 걸 알기 때문에 long으로 선언한다. 그러나 계산 과정에서 1억 * 100을 int에 초기화 하는 경우가 종종 있다. 이는 오버플로우가 된다.
  - long a = 100억 * 10도 터진다. 100억도 long으로 선언해줘야 한다.

<br>

- **퍼센트 계산의 함정**
  - 소수점까지 필요하다면 double / double * 100의 이슈는 없다.
  - 하지만 정수형식의 퍼센트가 필요하다면 int * 100 / int를 해야 한다.

<br><br>

## :fireworks: Long → Double로 캐스팅 할 때 생기는 문제점
- **나머지 연산에서, 나누어 지는 숫자랑 나누는 숫자 중에 하나라도 double이 있다면, 다른 숫자를 컴파일러가 double로 바꾼다.**
  - 이 때 long → double로 캐스팅 될 때 2^53보다 크다면 <ins>정밀도 오류</ins>가 생길 수 있다.
  - 보통 double보다 범위가 큰 타입은 없으니까.

<br>

- **나머지 연산에서는 var을 사용하지 말자.**
  - 절대로 디버깅 때 잡아낼 수 없다. 

<br><br>

## :fire: .ToString()으로 소수점 반올림 출력하기
~~~c#
void Main()
{
	double num = 123.456789123;
	
	num.ToString("F4").Dump(); // 소숫점 네 번째 자리까지.
	
	//123.4568
}
~~~
- 우리가 아는 일반적인 반올림으로 출력된다.
- :airplane:[Standard numeric format strings](https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings)

<br><br>

## :fire: 일반적이지 않은 반올림 : Round(3.5)가 3일수도 있다!
- :airplane:[은행원 방식 반올림](https://euriion.com/bankers-rounding-%EC%9D%80%ED%96%89%EC%9B%90-%EB%B0%A9%EC%8B%9D-%EB%B0%98%EC%98%AC%EB%A6%BC)