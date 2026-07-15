## :fireworks: Manager - Controller 구조 규칙
#### :one: Manager는 Controller를 들고 있는다. <br> Controller는 Manager를 들고 있지 않는다. <br> 단방향 의존성을 구현한다. <br> 구현은 Manager와 Controller를 연결해주는 연결 전문 Singleton Class를 만든다.
- Manager는 Controller의 메서드를 직접 호출, Controller는 CallBack으로 구현한다.
- [핵심] Controller 내부에 2가지 Interface가 존재한다.
  - 1. unity 데이터 가공이란 추상적 개념은 같음 -> 인터페이스 -> 전략 패턴으로 여러 가지 데이터 가공 책임을 concrete method로 구현 
  - 2. 가공된 데이터가 여러 타입임. 그러나 추상적 개념은 manager에게 전달해야 할 데이터인 건 동일 -> 그러니 인터페이스
- :airplane:[Service Locator로 구현](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/Design%20Pattern/Service%20Locator.md)

<br><br>

## :fireworks: Controller의 책임
#### :one: Controller는 Unity Component를 다루는 Unity 세상과 가장 가까운 계산기다.
- Unity에 의존적인 기능은 Controller에서 진행한다.
- 무거운 기능 및 계산을 담당한다.

#### :two: Controller는 manager가 상태를 변경하기 위해 필요한 context를 생산하고 전달한다. <br> Manager는 context를 조합해서 자신이 요청받은 데이터를 MVP에게 전달한다. <br> 그러므로 Controller는 Manager에게 작은 단위의 context를 전달하기 위해 메서드를 작게 만들어야 한다.
- 예를 들어, Manager가 화면 터치 이벤트를 처리해야 한다.
- Manager는 Controller를 통해 터치가 되었는지, 그리고 터치의 좌표를 알아야 한다. 그리고 두 기능 모두 Unity Component에 종속된다.
- 그러므로 Controller는 터치의 여부와 터치의 좌표를 각각 Manager에게 전달만 할 책임이 있다. 그 외에 Controller는 책임 지지 않는다.
- Manager는 두 데이터를 통해 터치관련 가공을 한다.

~~~c#
// 1. InputController의 TouchHandler -> 터치 여부만 검사하고 콜백으로 넘긴다.
public class TouchHandler : IInputHandler
{
    private readonly Action _onResult;

    public TouchHandler(Action action)
    {
        _onResult = action;
    }
    
    public void HandleInput(InputAction.CallbackContext context)
    {
        if (!context.canceled)
        {
            return;
        }

        _onResult.Invoke();
    }
}

// 2. InputController의 TouchPosHandler -> 마우스의 경우 계속 현재 포인터의 좌표를 갱신한다.
// Handler는 InputController에게 계산한 좌표를 전달하여 갱신하도록 한다.
public class TouchPosHandler : IInputHandler
{
    private readonly InputController _inputController;

    public TouchPosHandler(InputController inputController)
    {
        _inputController = inputController;
    }
    
    public void HandleInput(InputAction.CallbackContext context)
    {
        var pos = context.ReadValue<Vector2>();

        _inputController.UpdateCurMousePosition(pos);
    }
}

// 3. InputManager 내부의 두가지 메서드
// 단방향 의존성이므로 InputController를 Manager는 들고 있는다.
// Controller가 제공한 Event에 Manager의 OnTouchScreen을 등록한다.
// 터치 여부가 콜백의 호출로 구현된다.
// 컨트롤러는 콜백이 호출될 때 curTouchPos context를 Manager에게 전달한다.
private void BindInputControllerEvent()
{
    _inputController.OnTouchEvent += OnTouchScreen;
}

private void OnTouchScreen(Vector2 curTouchPos)
{
  var ray = CameraManager.Instance.GetRay(curTouchPos);

  if (Physics.Raycast(ray, out var hit))
  {
      if (hit.collider.TryGetComponent<FieldObjectSparrow>(out var sparrow))
      {
          _touchedFieldObject.Value = sparrow;
      }
  }
}
~~~

#### :three: Controller도 Manager에게 전달하기 위해 context를 필드로 들고 있을 수 있다. <br> 그러나 controller의 필드 자체를 Manager가 아닌 외부 객체에서 이용하지는 않는다.
- Controller는 MonoBehaviour기 때문에 new로 선언하지 않는다. 이는 manager가 아닌 객체에서 쉽게 접근하기 어렵게 한다.
- MVP 객체들은 외부에서 필요한 데이터는 반드시 Manager를 통해 조회되도록 한다.

#### :four: Controller는 다른 Controller를 참조하지 않는다.

#### :five: Controller에서 다른 Manager를 호출하지 않는다. <br> 그 요청도 자신의 Manager에게 부탁한다.

<br><br>

## :fireworks: Manager의 책임
#### :one: Manager는 게임의 거시적인 기능을 '관리'한다. <br> MVP 객체들은 책임 영역이 개인에만 있다. 그러므로 서로를 모른다. <br> Manager를 통해 MVP 객체들이 상호 작용 할 수 있다.
  - MVP 객체에서 Manager는 Model에 가까우며 Presenter의 소통 창구 역할을 한다.
- Manager는 MonoBehaviour를 상속받지 않는다. 단, Unity Type을 참조하는 필드를 가질 수는 있다.
- Manager도 필드로 Camera _camera가 가능하다. 단, Camera를 통한 Unity API는 절대 Manager에서 구현하지 않는다. 이는 Controller의 책임이다.

#### :two: Manager는 Controller의 처리 결과를 주입 받아서 자신의 상태를 변경시킨다. <br> :star: 누구나 Manager에게 상태 변경을 요청할 수 있지만, 상태 변경의 주체는 Manager 하나다. 
- Controller에서 Manager의 상태 변경 메서드 (public)를 호출하지만 상태 변경의 주체는 Manager 단 하나임을 보장한다.
  - 예시 :  manager 터치 좌표를 필드로 관리 -> controller 터치 감지 -> Unity 관련 계산 -> 전략 패턴을 통해 인풋을 쪼갠다 -> 쪼갠 결과를 manager에게 주입 -> manager는 주입 받은 context를 이용해서 상태 변경 -> 상태 변경과 연동된 이벤트가 발행되어 MVP 객체들이 메서드 수행 

#### :three: Manager는 MVP 객체들의 요청을 받고 Controller에게 해당 요청을 수행시킨다.
- MVP 객체 -> Request -> Manager -> Command -> Controller -> Handle

#### :four: Manager는 자신이 관리하고 있는 거시적인 기능의 상태를 외부에 전달한다.
- ValueType은 값 복사이므로 안전할 수 있으나 ReferenceType은 읽기 전용으로 전달하도록 구현한다.

<br><br>

## :fireworks: Controller와 Manager의 책임
#### :one: Controller는 Unity Component로부터 의존된 기능을 구현한다. <br> 구현 결과를 Manager에게 리턴하거나 이벤트로 처리한다.

#### :two: Manager는 Controller가 가공해 준 데이터를 관리하고, 외부에서 이용 할 수 있도록 한다. <br> Manager는 Unity Component로부터 의존된 기능을 절대 구현하지 않는다.
- Manager는 자신의 데이터를 캡슐화 할 수 있다. (외부에서 Get은 가능하나 Set은 불가능!)
  - 외부는 FieldObjectPresenter 같은 계층을 의미한다.  
- Manager에서 관리할 private 필드를 업데이트 할 때 private으로 할 수 있다.
- Controller에서 변경한 데이터를 리턴하면 그걸 지역변수로 사용해서 사용 범위 및 생명주기를 줄일 수 있고, 필드에 저장해도 private 하다.

#### :three: 코드 예시
~~~c#
// manager
private void RequestFollowSparrow(FieldObjectSparrow sparrow)
{
    _cameraController.StartFollowFieldObject(sparrow.transform);
}

//controller
public void StartFollowFieldObject(Transform fieldObjectTransform)
{
  _mainCamera.fieldOfView = FOLLOWING_CAMERA_FOV;

  _followFieldObjectObservable?.Dispose();
  _followFieldObjectObservable = Observable
    .Interval(TimeSpan.FromMilliseconds(FOLLOWING_CAMERA_UPDATE_MILLISECONDS))
    .Subscribe(_ =>
    {
      if (_mainCameraTransform == null)
      {
          return;
      }

      var direction = fieldObjectTransform.position - _mainCameraTransform.position -
                      FOLLOWING_CAMERA_ROTATE_ADJUST_VECTOR;
      _mainCameraTransform.rotation = Quaternion.LookRotation(direction.normalized);
      _mainCameraTransform.position = fieldObjectTransform.position + FOLLOWING_CAMERA_POSITION_ADJUST_VECTOR;
    });
}
~~~
- Manager는 Controller에게 Unity 세상에서 할 수 있는 동작을 요청한다.
- Controller는 요청 받은 데이터를 기반으로 unity 동작을 처리한다.

<br><br>

## :fire: MVP 계층에서, Presenter만이 Manager와 소통이 가능하도록 설계한다.
~~~c#
public class FieldObjectManager : ManagerBase<FieldObjectManager>
{
  // note : key = InstanceID (UnityEngine.Object)
  private readonly Dictionary<int, FieldObjectPresenterBase> _fieldObjectPresenterDict = new();
}
~~~
- FieldObject MVP 구조를 예로 들면, 여러 FieldObject의 MVP 구조를 관리해 줄 Manager가 필요하다.
  - ex : 1번 FieldObject가 사라지면 2번 ~ 8번 FieldObject를 생성시키게 하는 기능
- View는 멍청해야 하고, Model은 Presenter 단 하나의 주체로 변경이 되어야 한다.
- 그러므로 Manager는 Presenter를 등록하고, Presenter를 통해 요청을 받고, 핸들한 걸 전달해야 한다.

<br><br>

## :fire: Controller가 Manager를 들고 있는 방식을 채택하지 않은 이유 : Rx 지옥
- 기존에는 Controller의 Rx-Pattern으로 Controller가 핸들링 한 방식을 Manager에게 전달하였다.
- 그러나, 이 방식은 무수히 많은 uniRx 코드를 생성하며 코드 흐름 파악이 쉽지 않았다.
- uniRx를 통해 값을 변경하고 그걸 Controller에서 Manager의 public UpdateXXXX() Method로 갱신하는 것도 문제다. Manager의 경우 외부에 열려있기 때문에 property의 private Setter가 의미가 없으며, public UpdateXXXX() method는 어디서든 호출이 가능하다. 다시 말해, 어디서든 Set이 가능한 위험한 코드가 된다.