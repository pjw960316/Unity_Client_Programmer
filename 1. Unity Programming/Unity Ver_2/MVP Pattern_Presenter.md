## :fire::one: Presenter의 역할 및 책임
#### 1. Model과 View 사이를 연결하는 **중재자(mediator)**입니다.
> View로부터 입력 이벤트를 받으면 Model을 업데이트하고, Model의 결과를 다시 View로 전달해 화면을 갱신하는 일을 맡습니다.
  - Presenter가 1대1로 Model과 View를 연결하는 Unit 단위 연결 통로라면, Presenter들 끼리의 소통은 Manager를 통해 한다.
#### 2. MVP Pattern의 Presenter는 MVC Pattern의 Controller + Presenter로 이해할 수 있다.
- View -> Controller -> Model
  - View에서 부터의 Input 정보를 Controller를 통해 Model로 전달한다. 이는 Mvp Pattern에서 Presenter가 Model에게 Data를 Request하는 부분과 유사하다.
- Model -> Presenter -> View
  - Model은 변화된 Data를 Presenter에게 전달하고 Presenter는 이를 View에게 Command하여 User가 보는 화면을 갱신시킨다.
  - 이는 현재 Presenter에서의 동작과 유사하다.
- Presenter가 2개의 책임이 있으므로 구분하는 것이 SRP에 맞지만, 나는 일단 Request를 접두어로 붙여 구분하기로 했다.
> 이렇게 MVP의 Presenter를 통해 Model 과 View의 소통을 구분하는 일은 어렵지 않으며, 향후에 겪을 수 많은 고통거리를 덜어줄 것 이다. 
  - 과거에 구분하지 못해서 View에서 모든 것을 동작시키고, 도저히 유지보수가 되지 않는 코드를 많이 생산해 봤다. 크게 다가오는 글이었다.
#### 3. View의 Event를 구독할 책임이 있다.
> The presenter receives events from the view, retrieves data from the model and updates the view with the data.
  - Presenter는 Pub/Sub 구조에서 subscriber의 역할을 담당한다. IObservable이 View에서 public인 이유는 Presenter가 구독을 해야하기 때문이다.
  - View가 제공한 이벤트를 구독하여 이벤트가 발생했을 때 수행할 로직을 구현할 책임이 있다.
#### 4. View에서 받은 Input으로 인한 변경사항을 Model에 전달하여 Model의 Data를 변경하고, Model로 부터 Data를 Get해서 View를 통해 보여지는 화면을 갱신한다.
> 단순히 Model 에서 값을 가져다가 View에 뿌리는 중계자가 아니라, 사용자의 입력을 받아 Model의 상태를 변경할 책임까지 Presenter는 갖고 있다.
- Model의 field는 public get; private set;으로 유지하고, public method를 통해 변경한다.
  - View의 경우 model을 필드로 들고 있지 않기 때문에 public method를 사용할 수 없어 캡슐화가 보장된다.
#### 5. Manager로 부터 데이터를 Get 한다.
~~~c#
// Model -> Presenter (GetData)
_latestSleepingAudioClip = _alarmData.GetAlarmAudioClip(eAlarmAudioClip);

// Presenter -> View (SetView)
protected override void SetView()
{
    _alarmPopup.SetButtonText(_alarmData.AlarmTimeDictionary);
}
~~~

<br><br>

## :fire::two: Presenter가 멤버로 들고 있을 것
#### 1. View 와 Model을 멤버로 갖는다.
> Model does not know the View or the Presenter. Presenter knows both Models and Views, but only through their interfaces.
- :link:[Unity에서 MVP 패턴으로 UI를 깔끔하게 관리하기](https://wolstar.tistory.com/73)
#### 2. View에서 전달 받은 event의 <ins>Handle Method</ins>
~~~c#
 _view.OnSoundButtonClicked.Subscribe(unit => OpenPopup()).AddTo(_disposable);
~~~

#### 3. Manager
- Manager가 Presenter를 들고 있거나 Rx로 구현하는 방식은 복잡도가 매우 올라간다고 판단했다.
- 그리고 Manager는 Singleton이기 때문에 어차피 의존성 높은 객체라.
#### 4. Model 또는 Manager에게 Request 하는 Method

<br><br>

## :fire::three: Presenter의 특징
#### :star:1. View와 1대1로 대응하고, View와 Presenter가 묶인 것을 VP라고 할 때 <br> 여러 개의 VP가 1개의 model을 참조하는 구조로 재사용을 구현하도록 한다.
- 2 views - 1 presenter로 구현을 해보았다. 의존성이 매우 높게 된다.
  - 1번 view를 close할 때 2번 view 동작시에 1번 view의 동작에 여전히 접근할 수 있지만 null이다.
  - 생명 주기를 presenter가 담당하기에 벅차다.
  - presenter의 기능이 늘어나고, 책임이 1개가 아니게 된다.
- 되도록 1view - 1presenter로 하고, 로직 중복은 서로 다른 popup이 어지간 하면 존재하지 않을 것 이고, 존재하더라도 static util class로 빼자.
#### 2. Presenter -> View는 올바르다. 그러나 Presenter와 Connect된 View가 Popup이라고 가정할 때, Presenter는 그 Popup이 들고 있는 Widget(small view)에는 직접 접근하면 안 된다. 다시 말해, Presenter가 직접 Widget을 Setting하면 안 된다.
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
