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

## :fireworks: 일단 지금 아래 적은 'Controller와 Manager의 의존관계'에서 :three: 말고는 정답이 아니다. 일단 지우지는 않는다. 기나긴 삽질 과정을 적어보겠다. <br> 결론을 내리기 어렵다.
#### :one: 삽질의 의식의 흐름
- 우선 팀에서 우리 이렇게 합시다 + 주석은 절대로 지켜지지 않는다. 왜냐면 내가 0년차 때 회사에서 지키고 싶어도 실력 부족으로 지키지 못했음. 그리고 시니어분들도 문서 공유를 하지 않으면 이게 지켜지지 않는다고 했는데 문서 공유도 진짜 쉽지 않음.
- 그래서 결론은 최대한 코드레벨에서 막아야 한다고 생각한다. 이게 곧 좋은 설계라고 믿고 있다.
- 그러나 과설계도 너무 위험하다. 지금도 당장 매우 간단한 구존데 2~3일을 설계만 하고 있었다.
- 제일 큰 개념은. Controller를 통해 무거운 unity 작업을 처리하고, 그 처리 결과를 Manager에게 전달하고, Manager는 이걸 받아서 MVP 객체들에게 요청의 결과를 전달한다.
- 그래서 controller랑 manager의 분리는 매우 훌륭한 구조라고 생각한다.
- 그리고 manager는 자신과 연관된 controller를 들고 있는 것도 맞고.
- 근데 아직 실력이 부족해서 의존성을 줄이자로만 맹목적인 생각을 가지게 되었다. 그래서 controller는 manager를 들고 있지 말자!. 근데 이게 나쁘지는 않은게 controller가 manager를 들고 있어 버리면 controller는 보통 manager에게 상태 변경을 전달한다. 근데 상태변경을 public method로 호출하면 싱글턴 특성상 저기 있는 FieldObject인 참새가 FieldObjectManager를 통해 직접적으로 상태를 변경하는데 이게 정말 위험한 구조라고 생각한다.
- 전체 관리 데이터가 관리 받는 대상의 특정 행동으로 바뀌어 버리는게. 근데 또 생각해보면 이게 당연한 거 같기도. 다시 말해 지금 뭐 아는 거랑 경험은 많은데 그래서 나는 앞으로 어떻게 구현을 해야 할 지 감을 못 잡고 있다.
- 하나씩 정답을 내야 한다. 지금 :three:는 맞는 거 처럼. 
- 만약 controller manager의 참조 구조를 바꾸려면 uniRx나 Action인데 그러면 또 유지보수 개같고 디버깅 어렵다. 그러니 일단 계속 고민을 하되 하나씩 방향을 잡아가겠다.
- :link:[비슷한 고민 하신 분의 블로그](https://cyphen156.tistory.com/492)
  - 학교 다닐 때 friend class 누가 쓰나 했는데 너무 필요하군.

#### :two: 방향성
- Controller는 다른 Controller를 참조하지 않는다.
- Controller는 자기 짝 Manager 하나는 참조할 수 있다.
- Manager는 필요하면 자기 관련 Controller를 참조할 수 있다.
- Manager의 public API는 SetXXX()보다 Request/Handle/Try 형태로 만든다.
- Manager 내부 상태를 바꾸는 진짜 로직은 private으로 둔다.
- Manager가 관리하는 raw mutable 객체를 외부에 그대로 주지 않는다.
- View / Presenter가 Manager 상태를 직접 바꾸지 못하게 한다.

<br><br>

## :fireworks: :x::x::x: 이 문서는 현재 틀림 -> Controller와 Manager의 의존관계
#### :one::x: <ins>Manager는 단 한 개의 Controller(자신이 책임질)만 필드로 들고 있는다. </ins> <br> Manager는 다른 Controller를 절대로 들고 있지 않는다.
- 결론적으로, Controller는 Manager 하나 만이 들고 있게 되므로, public method 사용에도 안전하게 된다.
- 책임질 Controller가 같은 계층으로 여러 개가 존재한다면, 2개 이상의 controller도 가능하다. 헷갈리지 않게 일단 한 개를 기조로 잡았다.


#### :two::x: Manager는 다른 Manager를 들고 있는 게 가능하지만, 되도록 들고 있지 않도록 한다.
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

#### :four::x: Controller는 당연히 관련 없는 Manager를 들고 있지 않아야 하며, 자신과 연관된 Manager도 들고 있지 않는 구조까지도 고려한다.
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