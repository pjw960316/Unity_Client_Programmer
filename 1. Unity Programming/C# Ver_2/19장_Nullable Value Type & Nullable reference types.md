## :fireworks: Nullable Value Type 과 Nullable Reference Type은 <br> 모두 type에 ?을 붙이고, 이는 null을 허용함을 의미한다.

<br><br>

## :fire: Nullable Value Type은 <br> 2개의 필드 (bool hasValue 와 T value)를 <br> 들고 있는 struct다.

<br><br>

## :fire: Nullable Reference Type은 컴파일러에게 해당 객체가 null이 가능함을 알게 한다. <br> :fire: 컴파일러는 이를 통해 컴파일 단계에서 미리 Warning을 전달한다. <br> :fire: 개발자는 string과 string?를 구분해서 사용하여 타입의 안정성을 챙길 수 있다.
#### :spiral_notepad:요즘은 컴파일러가 null 가능성을 정적 분석하여 경고를 제공한다. <br> 단, 이는 개발자가 명시한 정보를 기반으로 동작한다.
~~~c#
#nullable enable
void Main()
{
	string str = null;
	Console.Write(str.ToString()); // Warning!!!
	
	string? str2 = null;
	Console.Write(str2.ToString()); // Warning!!!
	
	var str3 = "";
	str3 = null;
	Console.Write(str3.ToString()); // Warning!!!
}
~~~
> Nullable reference types are a group of features that minimize the likelihood that your code causes the runtime to throw System.NullReferenceException.

<br><br>

## :fire: [참고] Nullable 실제 구현
- ![alt text](./capture/20250828_2.png)

<br><br>

## :fire: MSDN Nullable Attribute
- ![alt text](./capture/20250828.png)