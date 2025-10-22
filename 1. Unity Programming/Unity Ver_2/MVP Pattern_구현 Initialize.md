## :fire: View에서는 Awake()가 있기 때문에 BindEvent를 <ins>최상단</ins>에서 호출한다. <br> :fire:[장점] : 하위 타입에서 초기 세팅 함수에 대한 호출 순서에 대한 고민을 하지 않아도 된다. <br> View(내부 Widget 포함)의 Initialize() 세팅이 모두 완료된 후에 <br> Presenter에서 View에 주입할 데이터를 안전하게 SetView() 가능하다.  <br> 또한 abstract로 구현의 책임을 부여하기 때문에 구현도 반드시 하게 된다. <br> :fire:[단점] : 하위 타입 어딘가에서 Initialize() 내부에 BindEvent()를 넣었을 때 발생하는 예외에 대해 무방비 할 수 있다. <br> Initialize()가 끝나지 않았는데 어디선가 BindEvent를 하여 null-exception이 발생 할 수 있다.
~~~c#
// Class : UIPopupBase
private void Awake()
{
    Initialize(); // Popup + Widget Initialize 완료

    CreatePresenterByManager(); // Presenter 생성 및 Presenter 내부 로직 동작 가능 == Presenter에서 View에 접근함에 문제가 없다.

    BindEvent(); // 최상단에서 호출합니다.
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