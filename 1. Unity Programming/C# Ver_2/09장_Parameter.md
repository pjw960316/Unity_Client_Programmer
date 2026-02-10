## :fire: parameter에 적힌 타입의 “최종 타입”이 valueType인지, referenceType인지가 전부다. <br> :fire: valueType이면 새로운 객체를 만들어서 메서드에서 사용하게 된다. <br> :fire: referenceType이면 원본 객체를 받아서 사용하게 된다.
- 메서드를 만들 때 간헐적으로 주저를 하는 경우가 있다. 그 이유는 대부분 얕은 복사랑 깊은 복사가 헷갈릴 때다.
- 반드시 :fire: 개념을 항상 기억한다.
- :star:**스택과 힙에 대한 존재는 조금 다를 수 있지만, 타입이 얕은 복사와 깊은 복사를 결정하는 주체다.**
- :link: [5장과 연관된다.](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/C%23%20Ver_2/05%EC%9E%A5_Primitive%2C%20Reference%2C%20and%20Value%20Type.md#fireworks-%ED%97%B7%EA%B0%88%EB%A0%B8%EB%8D%98-%EA%B2%83--queuestringbuilder--int-queue--fire-%EA%B0%92-%EB%B3%B5%EC%82%AC%EC%9D%B8%EC%A7%80-%EC%B0%B8%EC%A1%B0-%EB%B3%B5%EC%82%AC%EC%9D%B8%EC%A7%80-%ED%8C%90%EB%8B%A8%ED%95%98%EB%8A%94-%EA%B2%83%EC%9D%80-%EC%8A%A4%ED%83%9D%EA%B3%BC-%ED%9E%99%EC%9D%B4-%EC%95%84%EB%8B%88%EB%8B%A4--fire-%ED%83%80%EA%B2%9F-%EA%B0%9D%EC%B2%B4%EC%9D%98-%EC%B5%9C%EC%A2%85-%ED%83%80%EC%9E%85%EC%9D%B4-%EC%96%B4%EB%96%A4-%ED%83%80%EC%9E%85%EC%9D%B8%EC%A7%80-%ED%8C%90%EB%8B%A8%ED%95%98%EB%A9%B4-%EB%90%9C%EB%8B%A4--queue---valuetuple---stringbuilder%EB%8B%88%EA%B9%8C-%EC%9D%B4-%EC%98%88%EC%A0%9C%EC%97%90%EC%84%9C%EB%8A%94-stringbuilder%EA%B0%80-%EC%B5%9C%EC%A2%85-%ED%83%80%EC%9E%85--fire-%EC%B5%9C%EC%A2%85-%ED%83%80%EC%9E%85%EC%9D%B4-value-type%EC%9D%B4%EB%A9%B4-%EA%B0%92%EC%9D%B4-%EB%B3%B5%EC%82%AC%EB%90%98%EA%B3%A0-reference-type%EC%9D%B4%EB%A9%B4-%EC%B0%B8%EC%A1%B0%EA%B0%92%EC%A3%BC%EC%86%8C-%EC%9B%90%EB%B3%B8%EC%9D%B4-%EB%B3%B5%EC%82%AC%EB%90%9C%EB%8B%A4)

#### [간단한 코드]
~~~c#
void Main()
{
	int a = 1;
	
	var list = new List<int>();
	list.Add(1);
	list.Add(2);
	
	Test(a,list);
	
	a.Dump(); // 1
	list.Dump(); // 1 2 3
}

public void Test(int a , List<int> list)
{
	a = 2;
	list.Add(3);	
}
~~~ 
- int는 값 타입 -> 깊은 복사
- List<int>는 참조 타입 -> 얕은 복사

<br><br>

## :fire: valueType을 referenceType 처럼 전달하고 싶을 때 사용하는 키워드가 ref와 out이다. <br> :fire: 전달하기 직전의 값이 중요하면 ref를 사용한다. <br> :fire: 전달하기 직전의 값은 중요하지 않고 완전히 새롭게 사용한다면 out을 사용한다.
- 사용하는 의도에 따라 분류해서 사용한다.