## :fire::one: Manager의 역할 및 책임
#### 1. Manager는 여러 View, 여러 Presenter, 여러 Model의 관리 책임만 유지한다. 각자의 기능 구현은 MVP에서 구현한다.
- 리팩터링 과정에서 관리 책임을 벗어나는 기능은 MVP로 옮긴다. 이 들을 관리하는 메서드만 Manager에 남긴다.
#### 2.좀 더 실력이 늘면 factory class와 분리하는 게 맞지만 지금은 factory class의 역할을 manager에서 해도 좋을 것 같다. 
- Factory class에서 presenter에 model과 view의 interface를 argument로 전달해서 DI를 진행한다.

<br><br>

## :fire::two: Manager가 멤버로 들고 있을 것

<br><br>

## :fire::three: Manager의 특징
#### 1.View의 Iobservable을 들고 있지 않고, Presenter가 Iobservable을 구독하고, Presenter로 부터 필요한 행동을 요청 받는다.**
- View가 제공한 Iobservable은 presenter가 구독하고, Presenter가 Manager에게 필요한 명령을 요청하는 구조가 가장 이상적이다.
- Manager가 직접 View의 이벤트를 구독하는 것은 MVP Pattern의 SRP를 위배한다.
- Manager는 Presenter를 관리하지, View를 관리할 책임은 없다.

<br><br>