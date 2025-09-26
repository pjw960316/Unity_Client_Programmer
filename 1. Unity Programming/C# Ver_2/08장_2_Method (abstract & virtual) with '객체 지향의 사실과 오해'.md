## :orange_book: 작가가 은유한 예시를 실제 Unity Programming 구현과 연관 짓는다. <br> :orange_book: 작가의 용어를 이해하고 :star:로 시작하는 문단을 이해한다.

<br><br>

## :fire: '요청' = '호출' = Method Call = Message Send
> 객체가 어떤 행동을 하는 유일한 이유는 다른 객체로부터 요청을 수신했기 때문이다.
- Method Call에는 적절한 argument와 함께 할 수 있다.

<br><br>

## :fire: '책임' = '행동' = Method Signature = Method Head = Interface의 존재 이유

<br><br>

## :fire: '책임 수행' = Method Body = Method 구현

<br><br>

## :fire: '역할' = Class = Type
> 어떤 객체가 수행하는 책임의 집합은 객체가 협력 안에서 수행하는 역할을 암시한다.

> 역할은 협력 안에서 구체적인 객체로 대체될 수 있는 추상적은 협력자다. 따라서 본질적으로 역할은 다른 객체에 의해 대체 가능함을 의미한다.
- 하나의 class를 만들면 해당 type으로 여러 instance를 생성 할 수 있다. (not singleton) 

<br><br>

## :fire: '협력' = Assembly = Unity Project 

<br><br>

## :fire: Interface 그리고 Abstract Method는 책임을 강제한다는 공통점이 있지만 <br> 미묘한 차이점 또한 존재한다. <br> :fire: Interface는 책임을 강제해서 외부에 제공하도록 한다. <br> :fire: Abstract Method는 책임을 강제함과 동시에 <br> 이 기능은 반드시 override해서 구현해야 한다는 책임까지 강제한다. <br> 수행 방향은 제공하지 않는다. <br>:fire: Virtual method는 책임 수행 방향을 조언 할 수 도 있다. (base에 body를 적으니) <br> :fire: 3가지 키워드 모두 책임을 강제하거나 책임의 수행 방향 까지 조언 할 수 있으나<br> Method Call ('요청')을 강요하지는 않는다.
> Interfaces are about exposing a contract. “You can use this thing this way”. Abstract classes are used for shared functionality. “Here’s a toolkit in building this class, implement one or two methods and you’re set.” They have a bit of overlap. Many abstract classes also implement interfaces. It’s not uncommon to see where something that takes in the interface, but there’s a base class that provides a lot of common functionality.
> 왕은 '재판을 수행해라'는 요청에 응답해야 하므로 '재판을 수행할' 책임을 지게 된다.
- 여기서 '재판을 수행'하는 것에만 집중해야 한다.
- '어떻게 재판을 수행'은 나중일이고, 이건 '책임 수행'에서 구현한다. 또한 이 것은 설계 단계에서 method 구현을 당장 고민하지 않음을 방증한다.
> 객체가 다른 객체로 부터 받은 요청을 처리하기 위해 객체가 수행하는 행동을 책임이라고 한다. 객체지향 설계의 핵심은 올바른 책임을 올바른 객체에게 할당하는 것이다.
- 직장에서 항상 고민하던 '이 Method(책임)는 어디에 넣어햐 하지'는 사실 객체지향 설계의 핵심이었다.

<br>

> 행동은 결국 객체가 협력에 참여하면서 완수해야 하는 책임을 의미한다.

> 크레이그 라만 : 객체지향 개발에서 가장 중요한 능력은 책임을 능숙하게 소프트웨어 객체에 할당하는 것

> A return type of a method isn't part of the signature of the method for the purposes of method overloading. However, it's part of the signature of the method when determining the compatibility between a delegate and the method that it points to.

<br><br>

## :fireworks: 두 독립 클래스 UIPopupBase와 PresenterBase 통해 <br> virtual & abstract로 OnAwake() 구조를 이해한다.
#### :one: BindEvent를 <ins>최상단</ins>에서 호출하기 <br> :fire:[장점] : 하위 타입에서 BindEvent를 매 번 Initialize()에 넣어 주지 않아도 된다. <br> 또한 abstract로 구현의 책임을 부여하기 때문에 구현도 반드시 하게 된다. <br> :fire:[단점] : base.Initialize()로 인해 부모의 Initialize()부터 타다 보니 <br> 자식의 Initialize()가 완료 되지 않아도 자식의 BindEvent()가 호출이 되어 버린다. <br> null-exception이 날 가능성이 매우 높다. 
~~~c#
// Class : UIPopupBase
private void Awake()
{
    OnAwake();
}

protected virtual void OnAwake()
{
    Initialize();

    CreatePresenterByManager();

    BindEvent(); // 여기서
}

protected virtual void Initialize()
{
    _uiManager = UIManager.Instance;
    _uiToastManager = UIToastManager.Instance;
}

protected abstract void BindEvent();
protected abstract void CreatePresenterByManager();

// AlarmPopup : UIPopupBase
protected override void OnAwake()
{
    base.OnAwake();
}

protected sealed override void Initialize()
{
    base.Initialize();

    InitializeWidgets();
}

private void InitializeWidgets()
{
    foreach (var widget in AlarmAudioClipButtons)
    {
        widget.Initialize();
    }

    foreach (var widget in AlarmTimeButtons)
    {
        widget.Initialize();
    }
}

protected sealed override void CreatePresenterByManager()
{
    _uiManager.CreatePresenter<AlarmPresenter>(this);
}

#endregion

protected sealed override void BindEvent()
{
    BindButtonMenuEvents(AlarmAudioClipButtons);
    BindButtonMenuEvents(AlarmTimeButtons);

    _confirmButton?.OnClick.AddListener(OnClickConfirmButton);
}
~~~
- virtual은 상위 타입의 기능도 필요한 경우가 있기 때문에 'base.부모 메서드' 콜을 항상 의식한다.
- Abstract는 책임에 대한 부여만 있기 때문에 'base.부모 메서드' 콜은 의식 하지 않는다.
- UIPopupBase에서 하위 Concrete Class에서 OnAwake()에서 호출될 메서드들의 실행 순서를 **강제**한다.
- abstract method를 이용해서 상위 타입에서 method의 호출 순서를 제어할 수 있다.

<br>

#### :two: BindEvent를 <ins>최하단(sealed 하면 좋음)</ins>에서 호출하기 <br> :fire:[장점] : Initialize()를 부모 ~ 자식 까지 모두 완료하고 나의 BindEvent를 안전하게 할 수 있다.<br> :fire:[단점] : 매 번 BindEvent()를 Initialize()에 넣는 걸 빼 먹으면 안 된다. <br> 이 방식이 더 좋다고 생각한다.
~~~c#

// PresenterBase (최상단)
public virtual void Initialize(IView view)
{
    _soundManager = SoundManager.Instance;
    _uiToastManager = UIToastManager.Instance;
    _uiManager = UIManager.Instance;
    _myCharacterManager = MyCharacterManager.Instance;
    _modelManager = ModelManager.Instance;
    _stringManager = StringManager.Instance;
    _presenterManager = PresenterManager.Instance;

    _view = view;
    ExceptionHelper.CheckNullException(_view, "PresenterBase's _view");
}

// FieldObjectPresenterBase : PresenterBase
public override void Initialize(IView view)
{
    base.Initialize(view);

    CastView();
    
    InitializeModel();
}

// SparrowPresenter : FieldObjectPresenterBase
public sealed override void Initialize(IView view)
{
    base.Initialize(view);

    // view
    if (_view is FieldObjectSparrow sparrow)
    {
        _fieldObjectSparrow = sparrow;
    }

    // model
    ExceptionHelper.CheckNullException(_fieldObjectSparrow, "_fieldObjectSparrow is null");

    if (_model is SparrowData sparrowData)
    {
        _sparrowData = sparrowData;
    }

    ExceptionHelper.CheckNullException(_sparrowData, "_sparrowData is null");
    
    BindEvent(); // sealed 한 놈에서 호출
}
~~~

<br>

#### :three: BindEvent()를 public으로 만들고 factory에서 호출하기 <br>:fire: 외부에서 Initialize() 호출 이후에, BindEvent()를 호출하면 <br> 1번과 2번의 장점을 모두 이용한다. <br> :fire: 그러나 BindEvent()를 public으로 빼는 건 캡슐화를 깨는 게 맞는가?에 대해서는 의문이다.

<br><br>

## :star::fire: 책임은 Interface로 구현한다. <br> :fire: 책임 수행의 중복은 Abstract Class로 구현한다. <br> :fire: 또한, Interface는 public으로 구현하기에 외부에서 호출될 책임을 구현하고 <br> Abstract는 protected와 private으로 내부의 책임을 구현하는 방향을 지향한다.  
- 예를 들어, 모든 Popup은 OnAwake()를 구현해서 Initialize()의 책임을 상속 구조로 수행하도록 할 것 이다. 그렇다고 OnAwake()를 Public으로 할 필요는 없다. 그러므로, OnAwake()의 책임은 유지하고, 내부에서 호출되어야 하기 때문에 Abstract Class에 구현한다.

#### [Manager class(=Concrete Class)의 Interface 와 Abstract Class 구분]
<details>
  <summary> :point_up_2: 눌러서 코드를 확인 합시다  </summary>

~~~c#
public interface IManager : IFactory
{
    public void Initialize();
    public void SetModel(IEnumerable<ScriptableObject> _list);
    public void ConnectInstanceByActivator(IManager instance);
}

// Note
// 공통로직을 담는 메서드가 굳이 IManager를 상속 받을 필요 없다.
public abstract class ManagerBase<T> where T : class, new()
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }

            return _instance;
        }
    }

    public virtual void ConnectInstanceByActivator(IManager instance)
    {
        if (_instance == null)
        {
            _instance = instance as T;
        }
    }
}
~~~

</details>

- Concrete Manager들을 구현하다 보니, ConnectInstanceByActivator의 책임 수행(=메서드 구현)이 같고, 코드의 중복이 발생했다. 이를 Generic으로 처리하면 중복을 줄일 수 있다고 판단했다. 그로 인해, Abstract Class를 구현했고, 메서드를 구현하여 책임 수행에 대한 기본 수행 로직을 제공하지만, 변경에도 자유로울 수 있도록 virtual로 선언했다.
- :link:[Abstract class or interface? Why not both?](https://www.youtube.com/watch?v=5aCUhnSN00k)

<br><br>

## :fire: Method Call('요청')은 Unirx의 Subject 와 Observable로 강제하거나 <br> Event(+Unity Event) System을 통해 강제 시킬 수 있다.
- 책임도 강제가 되고, 요청도 강제가 되면 설계자가 다른 프로그래머에게 내 의도를 강제 시킬 수 있다. 

<br><br>