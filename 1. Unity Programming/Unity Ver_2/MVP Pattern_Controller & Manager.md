## :fireworks: Controller Script와 Manager Script의 기본
#### :one: Controller는 Unity Scene에 존재하는 Component를 이용하기 위한 MonoBehaviour 상속 Script다.

#### :two: Manager는 Controller를 관리하기 위한 C# Script다.
- Manager는 MonoBehaviour를 상속받지 않는다. 단, Unity Type을 참조하는 필드를 가질 수는 있다.
- Manager도 필드로 Camera _camera가 가능하다. 단, Camera를 통한 Unity API는 절대 Manager에서 구현하지 않는다. 이는 Controller의 책임이다.

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

## :fireworks: Controller와 Manager의 의존관계
#### :one: <ins>Manager는 단 한 개의 Controller(자신이 책임질)만 필드로 들고 있는다. </ins> <br> Manager는 다른 Controller를 절대로 들고 있지 않는다. <br> :x::x::x: 수정이 필요하다. 이게 지금 계속 생각해봐도 구조적으로 좀 어려운데. (실력 부족 ㅠ) <br> controller가 자기랑 관련된 manager를 들고 있지 않으면 소통이 또 너무 복잡해진다. <br> controller에서 unity 관련 구현만 하고 Manager에서 controller에서 처리한 데이터를 들고 있거나 가공할 때 소통이 너무 복잡해진다. <br> 또 편하자고 오픈하면 어디서든 호출이 되고... 
- 결론적으로, Controller는 Manager 하나 만이 들고 있게 되므로, public method 사용에도 안전하게 된다.
- 책임질 Controller가 같은 계층으로 여러 개가 존재한다면, 2개 이상의 controller도 가능하다. 헷갈리지 않게 일단 한 개를 기조로 잡았다.
- :link:[비슷한 고민 하신 분](https://cyphen156.tistory.com/492)

#### :two: Manager는 다른 Manager를 들고 있는 게 가능하지만, 되도록 들고 있지 않도록 한다.
- A_Manager의 필드로 B_Manager의 필드를 들고 있으면 Manager의 범위가 방대해진다.
- 매니저와 소통하는 presenter가 다른 Manager를 통해 필요한 데이터를 받은 후, 인자로 넘겨주도록 하자.
~~~c#
private void RequestFollowSparrow()
{
    var randomSparrow = _fieldObjectManager.GetRandomSparrow();

    _cameraController.StartFollowFieldObject(randomSparrow.transform);
}
~~~
- _fieldObjectManager.GetRandomSparrow()를 호출부에서 전달하고 함수 시그니처를 RequestFollowSparrow(FieldObjectSparrow)로 변경한다.

#### :three: Controller는 다른 Controller를 절대 들고 있지 않는다.

#### :four: Controller는 당연히 관련 없는 Manager를 들고 있지 않아야 하며, 자신과 연관된 Manager도 들고 있지 않는 구조까지도 고려한다.
- 진행 중

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