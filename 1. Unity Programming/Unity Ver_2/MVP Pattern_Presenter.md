## :fire: Presenter
#### :one: 역할 및 책임
> Presenter: Model과 View 사이를 연결하는 **중재자(mediator)**입니다. View로부터 입력 이벤트를 받으면 Model을 업데이트하고, Model의 결과를 다시 View로 전달해 화면을 갱신하는 일을 맡습니다.
  - Presenter가 1대1로 Model과 View를 연결하는 Unit 단위 연결 통로라면, Presenter들 끼리의 소통은 Manager를 통해 한다.
  - SoundManager가 필요한 정보를 UIManager에게 전달하려면 SoundManager가 Presenter를 참조해서 정보를 얻고, UIManager에게 전달한다.
> The presenter receives events from the view, retrieves data from the model and updates the view with the data.
- Presenter는 Pub/Sub 구조에서 subscriber의 역할을 갖고 있다.
  - View가 제공한 이벤트를 구독하여 이벤트가 발생했을 때 수행할 로직을 구현한다.
- Presenter에서 Model의 데이터 필드를 업데이트 한다. (property Set)
- View 마다 Presenter를 1대1 대응 시킬 필요는 없다. 예를 들어, Button의 경우 로직적으로 다양한 기능이 존재하지 않으므로 모든 Button에 대응하는 Presenter 1개만 있어도 된다고 생각한다.

<br>

#### :two: 멤버로 들고 있을 것
- **View 와 Model을 멤버로 갖는다.**
- **View에서 전달 받은 event의 <ins>Handle Method</ins>**
~~~c#
 _view.OnSoundButtonClicked.Subscribe(unit => OpenPopup()).AddTo(_disposable);
 ~~~
- **Manager**
  - Manager가 Presenter를 들고 있거나 Rx로 구현하는 방식은 복잡도가 매우 올라간다고 판단했다.
  - 그리고 Manager는 Singleton이기 때문에 어차피 의존성 높은 객체라.
- **Manager에게 Request 하는 Method**
- **Enum**
  - Class 외부에 선언하지 않도록 주의한다. (Scope)
  - ![alt text](./captures/20250715.png)

<br>

#### :three: 기타 사항
> Model does not know the View or the Presenter. Presenter knows both Models and Views, but only through their interfaces.
- :link:[Unity에서 MVP 패턴으로 UI를 깔끔하게 관리하기](https://wolstar.tistory.com/73)

<br><br>