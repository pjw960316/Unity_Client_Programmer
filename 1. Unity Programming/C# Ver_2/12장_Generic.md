## :fire: 지금까지는 Interface, Base Type을 통해 코드의 유연성을 만들었다. <br> 그러나 호출하는 쪽에서 명시적인 타입을 알고 있다면 <br>굳이 추상화 하지말고 전해주면 된다! <br> 그게 Generic이다.

#### [호출부에서 명시적인 타입을 알면 Generic을 써서 전달하자]
~~~c#

//1. View_1 : UIAlarmPopup Script의 코드
protected void Awake()
{
    
    _uiManager = UIManager.Instance;
    _soundManager = SoundManager.Instance;

    _alarmPresenter = _soundManager.GetPresenterAfterCreate<AlarmPresenter>(this);
    
    BindEvent();
}

//2. View_2 : UIButtonBase Script의 코드
private void Initialize()
{
    _buttonPresenter = SoundManager.Instance.GetPresenterAfterCreate<ButtonPresenterBase>(this);
    
    BindEvent();
}

//3. SoundManager의 코드
public TPresenter GetPresenterAfterCreate<TPresenter>(IView view) where TPresenter : IPresenter, new()
{
    TPresenter presenter = new TPresenter();
    presenter.Initialize(view);
    
    return presenter;
}
~~~
- UIAlarmPopup은 AlarmPresenter와 자신이 비슷한 추상화 정도 인 걸 알고, 자신이 AlarmPresenter가 갖고 있는 책임은 수행할 필요가 있다고 판단한다.
- Generic으로 자신이 원하는 Presenter의 explicit Type을 전달해서 얻어낸다.