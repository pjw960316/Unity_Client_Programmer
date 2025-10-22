## :fire: View에서는 Awake()가 있기 때문에 BindEvent를 <ins>최상단</ins>에서 호출한다. <br> :fire:[장점] : 하위 타입에서 초기 세팅 함수에 대한 호출 순서에 대한 고민을 하지 않아도 된다. <br> View(내부 Widget 포함)의 Initialize() 세팅이 모두 완료된 후에 <br> Presenter에서 View에 주입할 데이터를 안전하게 SetView() 가능하다.  <br> 또한 abstract로 구현의 책임을 부여하기 때문에 구현도 반드시 하게 된다. <br> :fire:[단점] : 하위 타입 어딘가에서 Initialize() 내부에 BindEvent()를 넣었을 때 발생하는 예외에 대해 무방비 할 수 있다. <br> Initialize()가 끝나지 않았는데 어디선가 BindEvent를 하여 null-exception이 발생 할 수 있다.
~~~c#

// 1. UIPopupBase : 최상단
private void Awake()
{
    InitializeEPopupKey();
    
    Initialize();
    
    CreatePresenterByManager();
    
    // note : view에서는 최상단 호출
    BindEvent(); 
}

protected virtual void Initialize()
{
    _uiToastManager = UIToastManager.Instance;
    _presenterManager = PresenterManager.Instance;
}
protected abstract void InitializeEPopupKey();
protected abstract void CreatePresenterByManager();
protected abstract void BindEvent();

// 2. UIAlarmPopup : UIPopupBase
protected sealed override void Initialize()
{
    base.Initialize();

    // 큰 View인 Popup이 들고 있는 작은 View들 초기화
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

// 3. AlarmPresenter : PresenterBase
public sealed override void Initialize(IView view)
{
    base.Initialize(view);

    _alarmPopup = _view as UIAlarmPopup;
    _alarmData = _modelManager.GetModel<AlarmData>();

    ExceptionHelper.CheckNullException(_alarmPopup, "_alarmPopup");
    if (_alarmData == null)
    {
        throw new NullReferenceException("_alarmData");
    }

    SetView(); // View에 DI

    BindEvent();
}

protected sealed override void SetView()
{
    _alarmPopup.SetButtonText(_alarmData.SleepingAudioPlayTimeDictionary);
}
~~~
- View Initialize() 흐름
  - :one: 최상단 View의 Awake() 호출
  - :two: virtual로 상위의 Initialize()를 수행 후, 자신의 Initialize()를 통해 내부에서 초기화 할 수 있는 필드를 모두 초기화한다.
  - :three: Presenter를 생성하고, Presenter와 View를 Presenter에서 연결시킨다.
  - :four: Presenter에서 SetView()를 통해 View에 DI를 수행한다.
  - :five: BindEvent()를 통해 View에서 RX를 구현한다.
- virtual은 상위 타입의 기능도 필요한 경우가 있기 때문에 'base.부모 메서드' 콜을 항상 의식한다.
- Abstract는 책임에 대한 부여만 있기 때문에 'base.부모 메서드' 콜은 의식 하지 않는다.
- UIPopupBase에서 하위 Concrete Class에서 Awake()에서 호출될 메서드들의 실행 순서를 **강제**한다.
- abstract method를 이용해서 상위 타입에서 method의 호출 순서를 제어할 수 있다.

<br><br>

## :fire: Presenter에서는 Awake()가 없기 때문에 BindEvent를 <ins>최하단(sealed 하면 좋음) </ins>에서 호출한다. <br> :fire:[장점] : Initialize()를 부모 ~ 자식 까지 모두 완료하고 나의 BindEvent를 안전하게 할 수 있다.<br> :fire:[단점] : 하위 타입에서 매 번 BindEvent()를 Initialize()에 넣는 걸 빼 먹으면 안 된다. 
~~~c#
// 1. View -> Presenter 생성 요청
protected sealed override void CreatePresenterByManager()
{
    _presenterManager.CreatePresenter<FieldObjectSparrowPresenter>(this);
}

// 2. PresenterManager (Factory) -> Presenter 생성
public void CreatePresenter<TPresenter>(IView view) where TPresenter : PresenterBase, new()
{
    var presenter = new TPresenter();
    
    presenter.Initialize(view);

    _livedPresenterHashSet.Add(presenter);
}

// 3. PresenterBase (최상단)
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

// 4. FieldObjectPresenterBase (중간 계층) : PresenterBase
public override void Initialize(IView view)
{
    base.Initialize(view);

    CastView();
    
    InitializeModel();

    // BindEvent를 여기서 호출 하지 않는다.
}

private void CastView()
{
    _fieldObjectBase = _view as FieldObjectBase;

    if (_fieldObjectBase == null)
    {
        throw new InvalidCastException("_fieldObjectBase");
    }
}

private void InitializeModel()
{
    var modelType = _presenterManager.GetModelTypeUsingMatchDictionary(_view.GetType());
    var model = Activator.CreateInstance(modelType) as IModel;

    _model = model;
}

protected virtual void BindEvent()
{
    _fieldObjectBase.OnDestroyFieldObject.Subscribe(_ => { OnOnDestroyFieldObject(); });
}

// 5. SparrowPresenter : FieldObjectPresenterBase
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

protected sealed override void BindEvent()
{
    base.BindEvent();

    _myCharacterManager.OnUpdateRoutineSuccess.Subscribe(OnChangeSparrowSpinState).AddTo(_disposable);
}
~~~

<br><br>

## :fire: BindEvent()를 public으로 만들고 factory에서 호출하기 <br>:fire: 외부에서 Initialize() 호출 이후에, BindEvent()를 호출하면 <br> 1번과 2번의 장점을 모두 이용한다. <br> :fire: 그러나 BindEvent()를 public으로 빼는 건 캡슐화를 깨는 게 맞는가?에 대해서는 의문이다.

<br><br>