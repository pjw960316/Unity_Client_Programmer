## :fireworks: Generic의 궁극의 장점 <br> :fire: Interface와 상위클래스(특히 abstract)로 타입을 받으면 많은 타입을 받을 수 있다. <br> 하지만 추후에 캐스팅을 해야 한다. <br> :fire: 그러나, 컴파일 타임에 명시적인 타입을 알고 있다면 <br> Generic Method + where 조건으로 타입을 명시할 수 있다. <br> :fire: 결국 훨씬 명시적이고 좁은 범위를 잡게 되면서 캐스팅을 없앨 수 있다. 
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
public TPresenter GetPresenterAfterCreate<TPresenter>(IView view) 
where TPresenter : IPresenter, new()
{
    TPresenter presenter = new TPresenter();
    presenter.Initialize(view);
    
    return presenter;
}
~~~
- UIAlarmPopup은 AlarmPresenter와 자신이 비슷한 추상화 정도 인 걸 알고, 자신이 AlarmPresenter가 갖고 있는 책임은 수행할 필요가 있다고 판단한다.
- Generic으로 자신이 원하는 Presenter의 explicit Type을 전달해서 얻어낸다.

<br><br>

## :fireworks: Generic Method의 단점 <br> :fire: 상위 타입에 구현한 메서드를 강제로 하위 타입도 강제로 호출 시키고 싶다. <br> 그러면 상위 타입에서 호출을 시키면 되지만 Generic Method는 불가능하다. <br> :fire: 강제 호출을 하고 싶다면 Generic Class로 만들어야 하는데 <br> 그러면 과한 설계가 된다고 생각한다.

~~~c#
public abstract class ControllerBase : MonoBehaviour, IController
{
    private ControllerConnectionManager _controllerConnectionManager;

    protected virtual void Awake()
    {
        _controllerConnectionManager = ControllerConnectionManager.Instance;
        
        Initialize();
    }

    protected virtual void Initialize()
    {
    }

    // NOTE : 하위 타입에서 항상 호출시키세요.
    // ControllerBase를 비제네릭 클래스로 유지하고 싶다.
    // 그러므로, 해당 메서드를 하위 타입에서 강제 할 수 없다.
    protected void RequestConnectManager<TManager, TController>(TController controller)
        where TManager : class, IManager, IHasController<TController>
        where TController : ControllerBase
    {
        _controllerConnectionManager.ConnectManager<TManager, TController>(controller);
    }
}

<br><br>

## fire: 인터페이스를 매개변수로 하면 해당 인터페이스를 상속받는 모든 클래스를 받을 수 있다. <br> 그러나 인터페이스에 정의된 기능만 사용할 수 있기 때문에 concrete 타입으로 캐스팅 해줘야 한다. <br> :fire: Generic을 사용하지 않으면, concrete Type 마다 분기를 만들어줘야 하고 매우 귀찮다. <br> :fire: Generic을 사용하면, 개발자가 컴파일 타임에 직접 concrete type을 명시하게 된다. <br> 그 덕분에 <ins>불필요한 분기를 만들지 않게 된다.</ins> 

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