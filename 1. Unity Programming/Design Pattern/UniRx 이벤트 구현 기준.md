## :fire: Manager 구조와 MVP 구조에서의 uniRx는 다르다.
- Manager는 범위가 매우 넓고 자칫하면 상시로 uniRx가 살아있으므로 신중
- MVP 구조는 게임에서 계속 살아있는 객체가 아닐 가능성이 있기에 Dispose()만 잘 구현하면 Binding 실수를 없앨 수 있다.
  - 의도치 않게 연결된 bind code가 콜백되어 호출되는 -> 바로 null

<br><br>

## :fireworks: 한 번 깊게 생각해보자. <br> IObservable 과 Subejct를 어떻게 생성하고 관리할지.

#### :white_check_mark: 이벤트를 직접 발행하는 View에 <br> private Subject와 public IObservable을 함께 둔다.

- 여기서 중요한 게 MVP 객체급으로 거대한 View에 두어도 되고, <br> 아주 작은 Widget에 두어도 된다.
- Widget에 두어도 되는 이유는 View는 Widget을 들고 있기 때문이다. <br> Widget의 IObservable을 View가 들고 있고, <br> Presenter가 이걸 가져오면 된다.

- :airplane: [MVP Pattern · View](../Unity%20Ver_2/MVP%20Pattern/View.md)
- :airplane: [MVP Pattern · View - Widget](../Unity%20Ver_2/MVP%20Pattern/View%20-%20Widget.md)

<br>

#### :white_check_mark:  IObservable이 호출할 메서드는 Presenter에 둔다.

- 이 콜백의 결과로 호출될 메서드를 구현하는 것. <br> 당연히 Subscribe로 호출될 메서드는 Presenter에 두어야 한다. <br> 우리의 View는 멍청하니까.
- Presenter는 View를 알기 때문에 View의 public IObservable은 읽을 수 있다.

- :airplane: [MVP Pattern · Presenter](../Unity%20Ver_2/MVP%20Pattern/Presenter.md)

<br><br>

## :fireworks: IObservable 과 Subject는 어떤 View 계층에 생성시켜야 하나?

#### :white_check_mark: 경험

- IObservable 과 Subject의 위치가 View에 존재한다는 것은 이해했다.
- View는 결국 무수히 많은 widget의 계층으로 이뤄질 수도 있다.
  - 예를 들어 View(엄청 큰 Popup) -> Widget -> Widget -> ... -> widget
  - 이러면 나는 IObservable 과 Subject을 어디에 선언해야 하는가?
- 또한, View는 Presenter를 갖고, Widget은 보통 갖지 않는다. <br> Widget을 누르면 또 다른 View가 생성 될 수도 있다.
  - 이것이 의미하는 건 결국 생명주기가 다른 View와 Presenter 사이에서 Rx가 되는 순간이 있는데, <br> 이건 어찌 할건지

<br>

#### :white_check_mark: 가장 사용자 인풋에 가까운 곳에 IObservable 과 Subject을 선언한다.

- 경험상 괜히 복잡하게 할 이유가 없다.
- 과거에는 Manager에 UniRx를 생성해서 괜히 메모리에 오래 남지 않을까 걱정했다.
- 하지만, UI의 경우 즉시 사라지는 경우가 많으며 생명주기 관리만 잘되면 <br> Widget이 들고 있어도 무방하다.

<br><br>

## :fireworks: View의 이벤트를 Subscribe 하기 위해 Presenter는 Manager에게 정보를 받는다.

#### :white_check_mark: 경험

- 패널(Large-View) 출금 버튼(widget)이 확인 팝업(또 다른 View)을 여는 경우였다. 
- 패널의 Presenter는 이미 생성자가 돌았다. 확인 팝업은 나중에 생성된다.
- Presenter가 처음 이벤트를 연결할 때는 아직 확인 팝업이 없는데, <br> 언제 그 팝업의 `OnConfirmed`를 Subscribe할 수 있을까?
 <br> 그러면 PresenterManager가 
  팝업이 열렸다는 정보는 그때 살아 있는 모든 Presenter가 받아도 좋겠다고 봤다. 그래서 Manager에 팝업처럼 Presenter도 자료구조로 보관하는 방안을 떠올렸다.

<br>

#### :white_check_mark: Presenter는 다른 생명주기의 View 나 Widget의 이벤트를 <br> UIManager 나 PresenterManager 같은 싱글턴 매니저를 통해 받는다.

- 확인 팝업은 결국 View다. 멍청하다. 그러므로, 확인 팝업의 역할은 "저 생겼어요!" 가 전부다.
- Manager (지금은 PresenterManager가 좋다고 생각)가 확인 팝업이 열린 정보를 받는다. <br> Manager는 현재 열린 모든 Presenter에게 이를 알린다. <br> 그러면 Presenter는 이를 받아서 Subscribe를 할 수 있다.
  - PresenterManager가 그래서 현재 생성된 Presenter들을 Dictionary로 들고 있다.
  - IPresenter에 인터페이스 메서드로 구현해준다.

<br><br>

## :sparkles: 나중에 다시 볼 질문

#### :white_check_mark: 언제 Action, 그리고 언제 UniRx (R3)를 사용할 지 다시 정리해 봐야 한다!