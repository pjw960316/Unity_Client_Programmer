## :fire: Inteface에 대한 의문이 있을 때 <br> '객체지향의 사실과 오해'의 P138 ~ P176을 천천히 읽어봐라.
~~~c#
<details>
<summary> :point_up_2: 접기 버튼에 적힐 텍스트 </summary>

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
</details>

~~~
- Message 와 Method의 차이만 알면 책을 쉽게 이해할 수 있다.
  - ShowMessageExample() method에서 methodMan instance의 TestMethod()를 call 하는 걸 message라고 부른다.
  - TestMethod()가 MethodMan instance의 메서드로 존재하는 데, 이걸 method라고 부른다.

<br><br>

## :fire: Interface는 class를 만드는 계획표다. <br> 게임 개발을 할 때 class 부터 만드는 게 아니라 <br> 구상 한 걸 Interface 부터 만들어라.
- Interface 만들기 -> 상속 받은 class는 interface의 method를 구현해야 할 **'책임'**이 생김. -> 팀장님이든 1년차 신입이든든 상속 받은 class에서 method를 구현함 -> 훌륭하든 개판이든 method가 구현되어 있음 -> 호출하는 부분에서는 잘 돌아가겠지 하고 해당 메서드를 호출하면 된다.

<br><br>

## :fire: 과거 문서
- :link: [Link](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/C%23%20Ver_1/Interface.md)