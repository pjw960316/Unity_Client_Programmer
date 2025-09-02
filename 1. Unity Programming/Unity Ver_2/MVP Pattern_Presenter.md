## :fire::one: Presenter의 역할 및 책임
#### 1. Model과 View 사이를 연결하는 **중재자(mediator)**입니다.
> View로부터 입력 이벤트를 받으면 Model을 업데이트하고, Model의 결과를 다시 View로 전달해 화면을 갱신하는 일을 맡습니다.
  - Presenter가 1대1로 Model과 View를 연결하는 Unit 단위 연결 통로라면, Presenter들 끼리의 소통은 Manager를 통해 한다.
#### 2. View의 Event를 구독할 책임이 있다.
> The presenter receives events from the view, retrieves data from the model and updates the view with the data.
  - Presenter는 Pub/Sub 구조에서 subscriber의 역할을 담당한다. IObservable이 View에서 public인 이유는 Presenter가 구독을 해야하기 때문이다.
  - View가 제공한 이벤트를 구독하여 이벤트가 발생했을 때 수행할 로직을 구현할 책임이 있다.
#### 3. Model 또는 Manager로 부터 Data를 Get하고, Get한 데이터를 View에 Set한다.
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
#### 1. View 마다 (특히 Widget) Presenter를 1대1로 대응 시킬 필요는 없다.
- Button의 경우 로직적으로 다양한 기능이 존재하지 않으므로 모든 Button에 대응하는 Presenter 1개만 있어도 된다고 생각한다.
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
