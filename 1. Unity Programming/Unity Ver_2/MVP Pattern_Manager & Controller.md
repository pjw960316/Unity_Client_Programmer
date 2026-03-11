## :fireworks: Controller Script와 Manager Script의 기본
#### :one: Controller는 Unity Scene에 존재하는 Component를 이용하기 위한 MonoBehaviour 상속 Script다.

#### :two: Manager는 Controller를 관리하기 위한 C# Script다.
- Manager는 MonoBehaviour를 상속받지 않는다. 단, Unity Type을 참조하는 필드를 가질 수는 있다.
- Manager도 필드로 Camera _camera가 가능하다.

<br><br>

## :fireworks: Controller와 Manager의 책임
#### :one: Controller는 Unity 타입의 데이터를 가공에서 C# 타입으로 만들고 <br> 그  데이터를 Manager의 필드에 초기화 한다.
- 그러므로, controller와 manager의 의존 관계에서 controller가 manager를 들고 있어야 한다.

#### :two: Manager는 Controller가 가공해 준 데이터를 관리하고, 외부에서 이용 할 수 있도록 한다.

#### :three: 코드 예시
~~~c#
// Controller
public void OnHandleMove(InputAction.CallbackContext context)
{
  var pathPair = context.ReadValue<Vector2>();
  
  _inputManager.UpdateMoveVector(pathPair);
}

// Manager
public void UpdateMoveVector(Vector2 vector)
{
  _moveVector = vector;
}
~~~
  - Controller가 Unity 타입인 InputAction.CallbackContext을 가공해서 Vector2를 생성했다. 그리고 이를 Manager의 메서드를 이용해서 초기화 한다.
  - Manager는 _moveVector를 통해 FieldObject에게 Rx를 이용해서 이벤트를 발생시킨다.

<br><br>

## :fireworks: Manager와 Controller의 의존관계
#### :one: <ins>Manager는 절대로 Controller를 들고 있지 않는다.</ins> <br> Controller는 절대로 다른 Controller를 들고 있지 않는다. <br> Controller는 Manager를 들고 있는다. (다른 Manager도 가능하다.) 
- InputController는 당연히 InputManager를 들고 있는다.
- InputController가 CameraManager를 들고 있을 수 있고 필요한 데이터를 요청 할 수 있다.

#### :two: Manager에서 Controller를 들고 있지 않은지, Request Method가 있지 않은지 검토한다.