## :orange_book: 작가가 은유한 예시를 실제 Unity Programming 구현과 연관 짓는다. <br> :orange_book: 작가의 용어를 이해하고 :star:로 시작하는 문단을 이해한다.

<br><br>

## :fire: '요청' = '호출' = Method Call = Message Send
> 객체가 어떤 행동을 하는 유일한 이유는 다른 객체로부터 요청을 수신했기 때문이다.
- Method Call에는 적절한 argument와 함께 할 수 있다.

<br><br>

## :fire: '책임' = '행동' = Method Signature = Method Head 

<br><br>

## :fire: '책임 수행' = Method Body = Method 구현

<br><br>

## :fire: '역할' = Class = Type
> 어떤 객체가 수행하는 책임의 집합은 객체가 협력 안에서 수행하는 역할을 암시한다.

> 역할은 협력 안에서 구체적인 객체로 대체될 수 있는 추상적은 협력자다. 따라서 본질적으로 역할은 다른 객체에 의해 대체 가능함을 의미한다.
- 하나의 class를 만들면 해당 type으로 여러 instance를 생성 할 수 있다. (not singleton) 

<br><br>

## :fire: '협력' = Assembly = Unity Project 

<br><br>

## :fireworks: Interface 그리고 Abstract Class는 책임을 강제한다는 공통점이 있지만 차이점 또한 존재한다. <br><br> :fire: Interface는 순수하게 책임만 강제한다. <br> :fire: Abstract Class는 책임을 강제함과 동시에 <ins>책임 수행의 방향</ins>도 설정 할 수 있다. <br> abstract method를 통해 책임 수행을 강제할 수도 있고 <br> virtual method를 통해 책임 수행을 유연하게 조언 할수 도 있다. <br> :star: 정리하면, 여러 class가 동일한 책임을 가지면서 책임 수행 방식의 공통점도 다수 존재한다면 interface로 역할을 정의하고 그 역할을 구현한 abstract class를 추가로 설계한다. <br><br> :fire: 3가지 키워드 모두 책임을 강제하거나 조언하지만, Method가 구현되었다고 Method Call ('요청')을 강요하지는 않는다.
> 왕은 '재판을 수행해라'는 요청에 응답해야 하므로 '재판을 수행할' 책임을 지게 된다.
- 여기서 '재판을 수행'하는 것에만 집중해야 한다.
- '어떻게 재판을 수행'은 나중일이고, 이건 '책임 수행'에서 구현한다. 또한 이 것은 설계 단계에서 method 구현을 당장 고민하지 않음을 방증한다.
> 객체가 다른 객체로 부터 받은 요청을 처리하기 위해 객체가 수행하는 행동을 책임이라고 한다. 객체지향 설계의 핵심은 올바른 책임을 올바른 객체에게 할당하는 것이다.
- 직장에서 항상 고민하던 '이 Method(책임)는 어디에 넣어햐 하지'는 사실 객체지향 설계의 핵심이었다.

<br>

> 행동은 결국 객체가 협력에 참여하면서 완수해야 하는 책임을 의미한다.

> 크레이그 라만 : 객체지향 개발에서 가장 중요한 능력은 책임을 능숙하게 소프트웨어 객체에 할당하는 것

> A return type of a method isn't part of the signature of the method for the purposes of method overloading. However, it's part of the signature of the method when determining the compatibility between a delegate and the method that it points to.

<br>

- Interface의 Default 기능은 다루지 않는다.

<br><br>

## :question: 책임'만' 강제하는 Interface가 유연하다고 확실히 느낀 지점이 있다. <br> 개발을 하다보면 중복을 제거하고 싶어서 상위 타입으로 올리거나 <br> abstract class에 대한 고민을 하게 된다. <br> 만약 변경을 하게 되면, 책임 수행 까지 강제하기 때문에 <br> 유연하지 못하다. (=과거에는 비슷해서 책임 수행도 강제 했는데 추후에 변경하려면 다 바꿔야 한다.) <br> :fire: 결론적으로 Interface는 책임만 부여하기 때문에 코드에 대한 자율성이 높아지지만 의도는 분명하다.
- 나름 정리했는데 생각을 적은 나 조차도 이런 구현을 할 때만 딱 와닿는 애매한 글.

<br><br>

## :fire: Method Call('요청')은 Unirx의 Subject 와 Observable로 강제하거나 <br> Event(+Unity Event) System을 통해 강제 시킬 수 있다.
- 책임도 강제가 되고, 요청도 강제가 되면 설계자가 다른 프로그래머에게 내 의도를 강제 시킬 수 있다. 

<br><br>

## :star::fireworks: [Unity Project 설계 단계] <br> :fire: 1단계 : 객체의 적절한 책임(행동)을 설계한다. <br> :fire: 2단계 : 해당 책임(행동)을 수행하기 위해 필요한 데이터를 설계한다. <br> :fire: 3단계 : 필요한 데이터와 책임(행동)이 어느 정도 결정된 후에 클래스의 구현 방법을 결정한다.
- 1단계에서 interface 또는 Abstract Class를 설계하는 것.
  - 재사용 method가 많을수록 abstract class를 사용하는 설계가 올바르다고 현재는 판단한다.