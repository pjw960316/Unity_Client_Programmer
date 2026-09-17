## :fire: Manager와의 소통은 Presenter를 통하는 것을 기본으로 권장한다.
- Presenter가 연결 통로를 맡으면 View가 데이터 조회와 처리 과정을 덜 알아도 된다.
- 이렇게 의존성을 줄이면 다른 부분의 변경이 View에 미치는 영향을 줄일 수 있다.
- 다만 모든 접근을 금지하는 절대 규칙은 아니다. 단순한 조회까지 전달 경로를 추가하면 코드가 복잡해질 수 있다.

<br><br>

## :fire: View의 단순 표시용 조회는 허용할 수 있다.

### 실제 사례 : 알람 종료 Toast
```c#
private void OnClickQuitAlarmButton()
{
    _onQuitAlarm.OnNext(default);

    _uiToastManager.ShowToast(EToastStringKey.EAlarmQuit, DateTime.Now.ToString("MM월 dd일"), MyCharacterManager.Instance.GetTodaySiestaMoney().ToString("N0"));
}
```

- 여기서 고민하는 부분은 `MyCharacterManager.Instance.GetTodaySiestaMoney()`이다.
- 포인트 계산은 Manager가 담당하고, View는 조회한 값을 표시한다. 이런 단순 표시용 조회는 허용할 수 있다.
- 기준은 getter라는 이름이 아니라, 조회가 데이터를 변경하지 않고 View에 게임 규칙 판단이나 갱신 과정의 조율을 넘기지 않는다는 점이다.
- 읽기 전용이어도 Manager에 대한 의존성은 남는다. 현재 예시는 Presenter가 기록을 동기적으로 갱신한 다음 View가 조회하는 흐름이다.

### 앞으로 보완할 범위
- 이번에 정한 범위는 View의 단순 표시용 조회까지다. Model의 Manager 접근이나 다른 예외는 실제 사례를 바탕으로 별도 검토한다.
