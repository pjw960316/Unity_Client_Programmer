## :fire: Widget은 Popup을 구성하는 작은 View 단위다.
- Popup은 Widget을 가진다.
  - Composition, `has-a`
- **Widget은 작은 책임을 맡는 View 클래스로 둔다.** 버튼은 클릭을 알리고, 텍스트 위젯은 전달받은 값을 표시한다.
- **MVP는 독립적인 화면이나 기능 단위에 적용한다.** 화면의 상태를 관리하고 사용자 입력에 따라 기능을 진행하는 팝업이나 패널에 `View + Presenter + Model`을 둔다.
- 공용 확인 팝업은 문구를 보여주고 확인 결과를 호출자에게 돌려준다. 출금에서는 `HorizontalPanelPresenter`가 확인 결과를 받아 정산을 요청한다.

<br><br>

## :fire: Widget은 Presenter를 모른다.
- Widget은 자신의 Presenter를 직접 들고 있지 않는다.
- Presenter는 Popup과 대화하고, Popup이 자신이 가진 Widget을 변경한다.