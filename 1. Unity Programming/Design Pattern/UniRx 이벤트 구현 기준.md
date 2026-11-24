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

- Subscribe만 구현되어 있다.
- 다시 말해, 이 콜백의 결과로 호출될 메서드를 구현하는 것. <br> 당연히 Subscribe로 호출될 메서드는 Presenter에 두어야 한다. <br> 우리의 View는 멍청하니까.
- Presenter는 View를 알기 때문에 View의 public IObservable은 읽을 수 있다.

- :airplane: [MVP Pattern · Presenter](../Unity%20Ver_2/MVP%20Pattern/Presenter.md)

<br><br>

## :fireworks: 언제 Action, 그리고 언제 UniRx (R3)를 사용할 지 다시 정리해 봐야 한다!
