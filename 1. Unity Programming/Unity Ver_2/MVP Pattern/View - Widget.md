## :fire: Widget은 Popup을 구성하는 작은 View 단위다.
- Popup은 Widget을 가진다.
  - Composition, `has-a`
- MVP의 기준은 Widget 하나가 아니라 화면 또는 기능 단위로 잡는다.
  - `Popup/Panel View + Presenter + Model`
- 따라서 모든 Widget이 각자의 Presenter와 Model을 가질 필요는 없다.

<br><br>

## :fire: Widget은 Presenter를 모른다.
- Widget은 자신의 Presenter를 직접 들고 있지 않는다.
- Presenter는 Popup과 대화하고, Popup이 자신이 가진 Widget을 변경한다.