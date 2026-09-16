## :fire: Manager는 Presenter와만 대화하도록 구현한다.
- Manager가 MVP 구조의 GameObject와 대화를 하기 위해서는 결국 누군가와는 의존을 가져야한다.
- Presenter의 가장 큰 책임은 결국 연결 통로기 때문에 Presenter와 대화를 한다.
- View와 Model은 Manager를 알지 못하며 의존성이 분리된다.

<br><br>

## :fire: 고민해 볼 문제 : View의 Manager 접근을 어디까지 허용할 것인가?

### 실제 사례 : 알람 종료 Toast
```c#
private void OnClickQuitAlarmButton()
{
    _onQuitAlarm.OnNext(default);

    _uiToastManager.ShowToast(EToastStringKey.EAlarmQuit, DateTime.Now.ToString("MM월 dd일"), MyCharacterManager.Instance.GetTodaySiestaMoney().ToString("N0"));
}
```

- 여기서 고민하는 부분은 `MyCharacterManager.Instance.GetTodaySiestaMoney()`이다.
- View가 Manager에 직접 접근하므로 기존 원칙에는 어긋난다. 하지만 데이터를 변경하거나 포인트 계산 규칙을 구현하지 않고, 조회한 값을 표시하는 정도는 괜찮지 않을까?
- 읽기 전용이어도 Manager에 대한 의존성은 생긴다. 현재는 종료 이벤트를 받은 Presenter가 기록을 동기적으로 갱신한 다음 View가 조회한다는 실행 순서에도 의존한다.

### 비교할 선택지
- **엄격하게 분리한다.** Presenter가 포인트를 조회하고 View에 전달한다. View의 독립성은 유지되지만 값을 전달하는 코드가 추가된다.
- **단순 표시에는 예외를 허용한다.** View가 읽기 전용 값을 직접 조회한다. 구현은 간단하지만, Manager 의존성을 일부 허용한다는 선택이다.

값을 전달할 때는 메서드 인자나 `IObservable<int>`처럼 `T`를 지정한 Observable을 사용할 수 있다. 다만 현재 `OnQuitAlarm`은 **View → Presenter** 방향이다. 이것의 `Unit`을 `int`로 바꾸는 것만으로 조회 결과가 View에 돌아오지는 않는다. Observable로 결과를 받으려면 **Presenter → View** 방향의 전달 흐름도 필요하다.

### 아직 결론을 내리지 않은 질문
- 이 화면에서 View의 독립성을 지키는 이점은, 단순한 값을 전달하는 코드를 추가할 만큼 큰가?
- 예외를 허용한다면 기준은 "UI가 간단하다"인가, "데이터 변경이나 업무 판단 없이 조회한 값을 표시만 한다"인가?
- 읽기 전용이라는 이유만으로 모든 Manager 조회를 허용해도 될까? 예외가 늘어날 때 경계는 어떻게 유지할까?
- 기록 갱신이 비동기로 바뀐다면, 갱신 완료 후 결과를 전달하도록 Presenter의 책임을 강화해야 할까?

지금은 예외 허용을 확정하지 않고, 원칙이 보호하려는 의존성 분리와 실제 구현 비용을 비교하며 판단해 본다.
