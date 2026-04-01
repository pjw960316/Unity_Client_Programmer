## :fire: Inteface에 대한 의문이 있을 때 <br> '객체지향의 사실과 오해'의 P138 ~ P176을 천천히 읽어봐라.

#### ['객체지향의 사실과 오해'에서 중요한 개념인 message 와 method의 차이점은 이해하고 가자]
<details>
<summary> :point_up_2::point_up_2::point_up_2: 눌러서 코드를 확인하자  </summary>

~~~c#
void Main()
{
	MessageMan messageMan = new MessageMan();
	messageMan.ShowMessageExample();
}

public class MethodMan
{
	public void TestMethod()
	{
		"I am Method".Dump();
	}
}

public class MessageMan
{
	public MethodMan methodMan;
	
	public MessageMan()
	{
		methodMan = new MethodMan();
	}
	
	public void ShowMessageExample()
	{
		methodMan.TestMethod();
	}
}

// I am Method
~~~
</details>

- Message 와 Method의 차이만 알면 책을 쉽게 이해할 수 있다.
  - ShowMessageExample() method에서 methodMan instance의 TestMethod()를 call 하는 걸 <ins>message</ins>라고 부른다.
  - TestMethod()가 MethodMan instance의 메서드로 존재하는 데, 이걸 <ins>method</ins>라고 부른다.

<br><br>

## :fire: Interface는 class를 만드는 계획표다. <br> :fire: 구상 한 내용을 class로 바로 만들지 말고 Interface로 만들어라. 
- Interface 만들기 -> 상속 받은 class는 interface의 method를 구현해야 할 **'책임'**이 생김. -> 팀장님이든 1년차 신입이든든 상속 받은 class에서 method를 구현함 -> 훌륭하든 개판이든 method가 구현되어 있음 -> 호출하는 부분에서는 잘 돌아가겠지 하고 해당 메서드를 호출하면 된다.

<br><br>

## :fire: Interface 타입의 instance는 interface에 정의된 메서드는 반드시 동작 시킬 수 있다. <br> 그 외의 메서드를 더 동작 시키고 싶으면 캐스팅을 해라.
- 기본적으로 모든 Interface의 Method는 Public이 된다.

<br><br>

## :fire: 과거 문서
- :link: [Link](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/C%23%20Ver_1/Interface.md)