## :fireworks: Presenter의 역할 및 책임
### :one: View 와 Model 사이를 연결하는 **중재자**다.
> View로부터 입력 이벤트를 받으면 Model을 업데이트하고, Model의 결과를 다시 View로 전달해 화면을 갱신하는 일을 맡습니다.
  - View에서 받은 Input으로 인한 변경사항을 Model에 전달하여 Model의 Data를 변경하고, Model로 부터 Data를 Get해서 View를 통해 보여지는 화면을 갱신한다.
- Presenter가 1대1로 Model과 View를 연결하는 Unit 단위 연결 통로라면, Presenter들 끼리의 소통은 Manager를 통해 한다.
> 단순히 Model 에서 값을 가져다가 View에 뿌리는 중계자가 아니라, 사용자의 입력을 받아 Model의 상태를 변경할 책임까지 Presenter는 갖고 있다.
  - Model의 필드는 캡슐화를 반드시 해야하고, Presenter는 Model에게 데이터 변경을 요청한다. (그러나 같은 layer므로, Request는 붙이지 않는다.)
> Model does not know the View or the Presenter. Presenter knows both Models and Views, but only through their interfaces.
- :link:[Unity에서 MVP 패턴으로 UI를 깔끔하게 관리하기](https://wolstar.tistory.com/73)

<br>

### :two: MVP Pattern의 Presenter는 MVC Pattern의 Controller + Presenter로 이해할 수 있다.
- View -> Controller -> Model
  - View에서 부터의 Input 정보를 Controller를 통해 Model로 전달한다. 이는 Mvp Pattern에서 Presenter가 Model에게 Data를 Request하는 부분과 유사하다.
- Model -> Presenter -> View
  - Model은 변화된 Data를 Presenter에게 전달하고 Presenter는 이를 View에게 Command하여 User가 보는 화면을 갱신시킨다.
  - 이는 현재 Presenter에서의 동작과 유사하다.

<br>

### :three: View의 Event를 구독할 책임이 있다.
> The presenter receives events from the view, retrieves data from the model and updates the view with the data.
  - Presenter는 Pub/Sub 구조에서 subscriber의 역할을 담당한다. 
  - IObservable이 View에서 public인 이유는 Presenter가 구독을 해야하기 때문이다.
  - View가 제공한 이벤트를 구독하여 이벤트가 발생했을 때 수행할 로직을 구현할 책임이 있다.

~~~c#
 _view.OnSoundButtonClicked.Subscribe(unit => OpenPopup()).AddTo(_disposable);
~~~

<br>

### :four: View를 Close()하는 주체는 Presenter가 되어야 한다. 그래야 생명주기가 관리된다.
~~~c#
private void OnStartAlarmSystem()
{
    _uiManager.AddPendingPopup(EPopupKey.AlarmTimerPopup);

    //AlarmPresenter에서 View를 끈다.  
    _alarmPopup.ClosePopup();

    RequestPlaySleepingMusic();

    RequestOpenAlarmTimerPopup();
}
~~~

<br>

### :five::question: MVP 중 그나마 Manager에 대한 접근의 자유를 갖고 있다.

<br><br>

## :fireworks: Presenter의 특징
### :one: View와 Presenter는 1:1로 대응되어야 한다. <br>:fire: View와 Presenter가 묶인 것을 V-P라고 할 때 <br> 여러 개의 V-P가 1개의 model을 참조하는 구조를 채택하도록 한다.
- two views - one presenter로 구현을 해보았다. 의존성이 매우 높아서 고칠 때 답이 없었다.
  - 1번 view를 close할 때 2번 view 동작시에 1번 view의 동작에 여전히 접근할 수 있지만 null이다.
  - 생명 주기를 presenter가 담당하기에 벅차다.
  - presenter의 기능이 늘어나고, 책임이 1개가 아니게 된다.
- one view - one presenter 구조로 구현하도록 한다.
  - 로직 중복은 서로 다른 popup이면 존재하지 않을 것 이다. 
  - 존재하면 상속이나, static util class로 분할하면 된다.

<br>

### :two: Presenter -> View는 올바르다. <br> :fire: 그러나 Presenter와 Connect된 View가 Popup이라고 가정할 때 <br> Presenter는 그 Popup이 들고 있는 Widget(small view)에는 직접 접근하면 안 된다. <br> :fire: 다시 말해, Presenter가 직접 Widget을 Setting하면 안 된다.
- 캡슐화 관점에서 Popup(big-view)은 자신의 field은 Widget들(small-view)를 private이나 protected로 숨겨야 한다.
- 그러므로, Presenter의 SetView로 Widget의 데이터를 세팅할 때 Presenter -> Popup(big-view) -> Widgets(small-view)를 구조로 구현해야 한다.
- Presenter가 widget의 세부 구현에 관여하면 SRP 위반이다.

~~~c#
// Presenter에서 호출 될 Popup의 public method
// widget의 text를 popup을 통해 세팅한다.
public void SetButtonText(ImmutableDictionary<EAlarmButtonType, float> immutableDictionary)
{
    foreach (var widget in _alarmTimeButtons)
    {
        if (immutableDictionary.TryGetValue(widget.AlarmButtonType, out var time))
        {
            widget.UpdateAlarmButtonText(time);
        }
        else
        {
            Debug.Log($"{widget.AlarmButtonType} 의 알람 버튼의 텍스트가 세팅되지 않았습니다.");
        }
    }
}
~~~

<br>

### :three: Presenter는 View 와 Model과 동등한 위치기 때문에 <br> Request 접두어를 메서드 이름 앞에 붙이지 않는다. 