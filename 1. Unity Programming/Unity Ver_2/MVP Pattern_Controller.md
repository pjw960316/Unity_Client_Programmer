## :fireworks: MVP pattern에서 View는 Input_View를 의미한다. <br> 하지만 Output을 담당하는 View도 존재하며 이를 Controller로 명명했다.

<br><br>

## :fire: Manager는 MonoBehaviour를 상속 받지 않는다. <br> Manager는 자신의 책임을 수행해 줄 Scene에 상주하는 MonoBehaviour 객체가 필요하다. <br> 이를 Controller로 이용한다. 
~~~c#
using System.Collections.Generic;
using UniRx;

public class CameraManager : ManagerBase<CameraManager>, IManager
{
    private MainCameraMono _mainCamera;

    public void RequestMainCameraDispose()
    {
        _mainCamera.DisposeFollowFieldObjectInterval();
    }

    public void ExecuteMainCameraToFollowFieldObject(FieldObjectBase fieldObjectBase)
    {
        var fieldObjectTransform = fieldObjectBase.transform;

        _mainCamera.FollowFieldObject(fieldObjectTransform);
    }

    // refactor
    // _mainCamera는 null 일 수 있지 않은가.
    public void SetMainCamera(MainCameraMono mainCameraMono)
    {
        _mainCamera = mainCameraMono;
    }
}
~~~
- CameraManager를 통해 다른 클래스에서 카메라 동작을 요청하고, CameraManager는 MainCameraMono를 통해 요청을 실제 Scene에서 수행한다.
- 다른 클래스는 _mainCamera에 직접 접근 할 수 없다.