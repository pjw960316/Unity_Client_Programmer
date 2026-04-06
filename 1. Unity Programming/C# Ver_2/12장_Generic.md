## :fireworks: 기본 개념 <br> :fire: 지금까지는 Interface, Base Type을 통해 코드의 유연성을 만들었다. <br> :fire: 그러나 호출하는 쪽에서 명시적인 타입을 알고 있다면 <br>굳이 추상화 하지말고 컴파일 타임에 확정시키면 된다. <br> 그게 Generic이다.
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

<br><br>

## :fireworks: Generic의 장점
## :fire: 타입의 분기를 호출부에서 책임 질 수 있으면 Generic Method로 구현한다.
- 타입을 코드가 아니라 “타입 파라미터”로 추상화해서 컴파일 타임에 확정시키는 것
- 불필요한 박싱과 캐스팅이 없다. 
- 타입이 매우 안정적이고 재사용이 되므로 코드의 중복도 없애준다.
~~~c#
public T GetController<T>() where T : IController, new()
{
    return new T();
}
~~~

<br><br>

## :fireworks: 실전 개념 <br> :fire: 인터페이스를 매개변수로 하면 해당 인터페이스를 상속받는 모든 클래스를 받을 수 있다. <br> 그러나 인터페이스에 정의된 기능만 사용할 수 있기 때문에 concrete 타입으로 캐스팅 해줘야 한다. <br> :fire: Generic을 사용하지 않으면, concrete Type 마다 분기를 만들어줘야 하고 매우 귀찮다. <br> :fire: Generic을 사용하면, 개발자가 컴파일 타임에 직접 concrete type을 명시하게 된다. <br> 그 덕분에 <ins>불필요한 분기를 만들지 않게 된다.</ins> 

#### :one: Generic을 사용한 코드
~~~c#
// 코드_1
// TController는 IController을 상속 받는 타입이면 된다.
// 개발자는 RegisterController를 호출할 때 Concrete Type을 전달하면 된다.
public abstract class ControllerManagerBase<TManager, TController>
    : ManagerBase<TManager>
    where TManager : class, new()
    where TController : IController
{
    protected TController _controller;

    public void RegisterController(TController controller)
    {
        _controller = controller;
    }
}

// 코드_2
// InputController는 IController의 Concrete Type이다.
public class InputController : MonoBehaviour, IController

// 코드_3
// 여기서 this는 InputController Type이다.
InputManager.Instance.RegisterController(this);
~~~

#### :two: Generic을 사용하지 않은 코드
~~~c#

// 코드_1
// CastController 메서드를 선언해야 한다.
public abstract class ControllerManagerBase<TManager>
    : ManagerBase<TManager>
    where TManager : class, new()
{
    protected IController _controller;

    public void RegisterController(IController controller)
    {
        _controller = controller;
    }

    protected abstract void CastController(IController controller);
}

// 코드_2
// ControllerManagerBase을 상속받는 모든 Manager Class가 아래와 같이 캐스팅 코드를 구현해야 한다.
protected override void CastController(IController controller)
{
    _inputController = controller as InputController;

    if (_inputController == null)
    {
        Debug.LogError("controller cast fail");
    }
}
~~~

<br><br>

## :fire: Interface 상속 + Generic Constraint를 같이 쓸 때 문법.
~~~c#
public abstract class ManagerBase<T> : IManager
    where T : class, new()
{}
~~~