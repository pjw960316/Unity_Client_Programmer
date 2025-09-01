## :fire: Manager
#### :one: 역할 및 책임
- 좀 더 실력이 늘면 factory class와 분리하는 게 맞지만 지금은 factory class의 역할을 manager에서 해도 좋을 것 같다. (factory class에서 presenter에 model과 view의 interface를 argument로 전달해서 DI를 진행한다.)
- Manager에서 Model의 데이터 필드를 업데이트 한다. (property Set)

<br>

#### :two: 멤버로 들고 있을 것
- 게임에 상주하는 UnityEngine.Object 상속 받는 Object
  - Command Pattern
- MainCanvas (GameObject)
  - 항상 존재하므로.

<br>

#### :three: 기타 사항
- **View의 Iobservable을 들고 있지 않고, Presenter가 Iobservable을 구독하고, Presenter로 부터 필요한 행동을 요청 받는다.**
  - View가 제공한 Iobservable은 presenter가 구독하고, Presenter가 Manager에게 필요한 명령을 요청하는 구조가 가장 이상적이다.
  - Manager가 직접 View의 이벤트를 구독하는 것은 MVP Pattern의 SRP를 위배한다.
    - Manager는 Presenter를 관리하지, View를 관리할 책임은 없다.
:question: :link:[여러 개의 view와 1개의 model을 대응할 때 presenter?](https://chatgpt.com/c/68501688-00ec-8004-af44-6a66c19db681)
  - 나는 이런 걸 Manager로 해버리려 했다. 예를 들어 StringManager 1개가 모든 string을 관리하는 것.
  - 그러나 토론에서는 1:1로 presenter를 만들라는데, 일단 stringManager를 구현하면서 여기를 수정한다.
- **DTO (=Data Transfer Object)**
- Manager는 되도록 들고 있는 Model을 private으로 선언해서 관리한다.
  - Manager를 통해 Model의 데이터 필드를 참조할 때 method를 만들어서 getter 동작을 하도록 한다.

<br><br>