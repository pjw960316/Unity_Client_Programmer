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

## :fireworks: 

<br><br>

## :sparkles: 나중에 다시 볼 질문


#### :white_check_mark: 언제 Action, 그리고 언제 UniRx (R3)를 사용할 지 다시 정리해 봐야 한다!


#### :white_check_mark: Widget 이벤트를 상위 View에서 어떻게 전달할까?

- Widget의 IObservable을 그대로 공개하는 경우와 상위 View가 화면 단위의 이벤트를 직접 발행하는 경우를 어떻게 판단할까?
- 예를 들어 2~3번 탄다면? View -> Widget -> Widget-> Widget-> Widge

<br><br>

## :sparkles: 팝업의 입력 이벤트를 Presenter에서 구독하기

#### :white_check_mark: 경험

- 출금 확인 팝업의 `IObservable`을 Presenter가 어떻게 받는지 고민했다. <br>
  처음에는 Widget에서 최상단 View까지 신호를 여러 번 넘기는 흐름을 떠올렸다. 계층이 깊어질수록 중간 객체마다 전달 코드를 붙여야 해서 흐름을 읽기 어렵겠다고 생각했다.
- Presenter에서 `UIManager`로 팝업을 여는 코드를 다시 보니, 반환값이 방금 생성한 팝업이었다. <br>
  팝업을 연 자리에서 그 인스턴스의 확인 이벤트를 바로 구독하는 흐름이 직관적이라고 느꼈다.

<br>

#### :white_check_mark: 입력 이벤트를 발행하는 View에 Subject와 IObservable을 둔다

- View와 Widget은 모두 View 계층에 속한다. 사용자 입력을 가까이에서 받아 그 입력의 의미를 정하고 이벤트를 직접 발행하는 객체에 `private Subject`와 `public IObservable`을 함께 둔다.
- 확인 버튼은 ‘버튼이 눌렸다’를 알리고, 확인 팝업은 그 클릭을 ‘사용자가 확인했다’는 이벤트로 발행한다. <br>
  그래서 확인 이벤트의 `Subject`와 `IObservable`은 팝업에 둔다.

<br>

#### :white_check_mark: Presenter는 Manager가 생성한 팝업을 받아 구독한다

- Presenter가 `UIManager`를 통해 새 팝업을 열 때는 반환값으로 그 인스턴스를 받는다. <br>
  받은 팝업의 `OnConfirmed`를 직접 구독하면, Presenter는 자신이 연 팝업의 확인에만 반응한다.

  ```csharp
  var popup = _uiManager.OpenPopupByStringKey(EPopupKey.ConfirmPopup, _uiManager.MainCanvasTransform) as UIConfirmPopup;
  ```

<br><br>

## :sparkles: 생명주기가 다른 팝업의 이벤트를 Presenter에서 구독하기

- 연구 자체는 1시간 했다.

#### :white_check_mark: 경험

- 가장 헷갈렸던 건 출금 버튼이 확인 팝업을 여는 경우였다. 패널의 Presenter는 이미 살아 있고, 확인 팝업은 나중에 생성된다. <br>
  Presenter가 처음 이벤트를 연결할 때는 아직 팝업이 없는데, 언제 그 팝업의 `OnConfirmed`를 Subscribe할 수 있을까?
- 팝업을 생성하는 `UIManager`가 “방금 이 팝업이 열렸다”고 알려주면 된다고 생각했다. <br>
  팝업이 열렸다는 정보는 그때 살아 있는 모든 Presenter가 받아도 좋겠다고 봤다. 그래서 Manager에 팝업처럼 Presenter도 자료구조로 보관하는 방안을 떠올렸다.

<br>

#### :white_check_mark: 현재 OnOpenPopup이 알려주는 것

- `UIManager`는 생성한 팝업을 딕셔너리에 넣고 `_onOpenPopup.OnNext(popupKey)`를 호출한다. 이때 공개된 `OnOpenPopup`을 구독 중인 객체들이 팝업 키를 받는다. 현재 이 알림을 쓰는 예가 `CameraManager`다.
- Presenter를 보관하는 자료구조는 이미 `PresenterManager`에 있다. Presenter를 생성하면 `_livedPresenterHashSet`에 추가하고, 종료 처리할 때 제거한다. <br>
  이 목록에 들어가는 것과 `OnOpenPopup`을 구독하는 것은 별개다.

<br>

#### :white_check_mark: 설계 제안 — 열림 알림을 먼저 구독하고, 확인 이벤트는 팝업이 생겼을 때 구독한다

- 모든 활성 Presenter가 팝업 열림을 받아야 한다면, Presenter가 활성화될 때 공통으로 `OnOpenPopup`을 구독하고 종료할 때 해제하는 방식으로 구현할 수 있다. 이 방식에서는 `UIManager`에 Presenter 목록을 하나 더 둘 필요가 없다.
- 현재 알림이 전달하는 값은 `EPopupKey`다. 여기에 **방금 생성한 팝업 인스턴스**도 전달하면, 출금을 담당하는 Presenter가 그 자리에서 해당 팝업의 `OnConfirmed`를 구독할 수 있다. <br>
  같은 확인 팝업을 출금과 삭제에 재사용할 때는 어느 버튼이 열었는지도 함께 전달해 각 Presenter가 자기 요청의 확인만 처리하도록 한다.
