## :fire: Manager 구조와 MVP 구조에서의 uniRx는 다르다.
- Manager는 범위가 매우 넓고 자칫하면 상시로 uniRx가 살아있으므로 신중
- MVP 구조는 게임에서 계속 살아있는 객체가 아닐 가능성이 있기에 Dispose()만 잘 구현하면 Binding 실수를 없앨 수 있다.
  - 

<br>

#### :white_check_mark: UI 입력은 누가 발행하고 누가 구독할까?

- **View가 발행한다.** `private Subject<T>`에 `OnNext()`를 호출한다. 같은 View에서 이 신호를 `public IObservable<T>` 프로퍼티로 공개한다.
- **Widget이 이미 입력을 발행한다면** 상위 View는 Widget의 `IObservable<T>`를 Presenter에 그대로 전달할 수 있다.
- **Presenter가 구독한다.** View가 공개한 신호에 `Subscribe()`로 자신의 `private` 콜백을 등록한다. 반환된 `IDisposable`은 화면이 닫히거나 Presenter가 종료될 때 해제한다.

<br><br>

## :fireworks: 참고 문헌

- :airplane: [MVP Pattern · View](../Unity%20Ver_2/MVP%20Pattern/View.md)
- :airplane: [MVP Pattern · Presenter](../Unity%20Ver_2/MVP%20Pattern/Presenter.md)
- :airplane: [MVP Pattern · Close 구현](../Unity%20Ver_2/MVP%20Pattern/Close%20%EA%B5%AC%ED%98%84.md)
- :airplane: [UniRx](../Unity%20Ver_2/UniRx.md)
- :airplane: [콜백 기반 이벤트 전달 방식 선택 기준](../C%23%20Ver_2/17%EC%9E%A5_Delegate/%EC%BD%9C%EB%B0%B1%20%EA%B8%B0%EB%B0%98%20%EC%9D%B4%EB%B2%A4%ED%8A%B8%20%EC%A0%84%EB%8B%AC%20%EB%B0%A9%EC%8B%9D%20%EC%84%A0%ED%83%9D%20%EA%B8%B0%EC%A4%80.md)
- :airplane: [Single Event Channel Pattern](./Single%20Event%20Channel%20Pattern.md)
