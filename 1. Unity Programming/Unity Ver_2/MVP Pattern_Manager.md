## :fire::one: Manager의 역할 및 책임
#### 1. Manager는 여러 View, 여러 Presenter, 여러 Model의 관리 책임만 유지한다. 각자의 기능 구현은 MVP에서 구현한다.
- 리팩터링 과정에서 관리 책임을 벗어나는 기능은 MVP로 옮긴다. 이 들을 관리하는 메서드만 Manager에 남긴다.
#### 2.좀 더 실력이 늘면 factory class와 분리하는 게 맞지만 지금은 factory class의 역할을 manager에서 해도 좋을 것 같다. 
- Factory class에서 presenter에 model과 view의 interface를 argument로 전달해서 DI를 진행한다.

<br><br>

## :fire::two: Manager가 멤버로 들고 있을 것
- Manager로 관리할 게임 씬에 존재하는 Mono 객체(ex : Camera) 
  - 이 객체는 Property의 Getter를 통해 외부에서 참조 할 수 있도록 하지 않는다.
  - 오로지 자신을 들고 있는 Manager에 의존해야 한다.

<br><br>

## :fire::three: Manager의 특징
#### 1.View의 Iobservable을 들고 있지 않고, Presenter가 Iobservable을 구독하고, Presenter로 부터 필요한 행동을 요청 받는다.**
- View가 제공한 Iobservable은 presenter가 구독하고, Presenter가 Manager에게 필요한 명령을 요청하는 구조가 가장 이상적이다.
- Manager가 직접 View의 이벤트를 구독하는 것은 MVP Pattern의 SRP를 위배한다.
- Manager는 Presenter를 관리하지, View를 관리할 책임은 없다.

#### 2. 서로 다른 Manager가 Request를 할 때 A_Manager는 B_Manager가 들고 있는 Mono 객체에 직접 접근하지 않는다.
- 동작 예시
  - A_Manager는 B_Manager의 Request 메서드를 호출한다.
  - B_Manager는 자신이 들고 있는 Mono 객체에게 명령을 내려서 필요한 데이터를 가져온다.
  - B_Manager는 자신의 Request의 리턴 값을 A_Manager에게 전달한다.
- 주의사항
  - Manager 끼리의 Request 메서드와, Manager가 Mono 객체에게 명령하는 메서드의 동작이 같은 경우도 있다.
  - 그러나 이런 경우에도 위의 규칙을 지켜야 한다. 확장성이 열려 있기 때문이다.
  - Mono 객체에게 데이터를 요청하고 -> Manager가 그걸 가공해서 다른 Manager에게 전달하게 될 수도 있다.