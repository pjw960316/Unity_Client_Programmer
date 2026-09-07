## :fireworks: virtual은 필요한 곳에만 사용한다.

#### :one: 설명
- 예전에는 Awake, OnAwake, Initialize, BindEvent의 상속 관계를 복잡하게 생각했다.
- 실제로 상속 흐름이 필요한 메서드는 OnAwake 하나였다.
- 하위 클래스는 OnAwake를 override하고 base.OnAwake()를 호출한다.
- 각 클래스의 private Initialize와 BindEvent는 이름이 같아도 서로 다른 메서드다.
- 따라서 Initialize와 BindEvent까지 virtual로 만들 필요가 없다.

<br>

#### :two: 코드
~~~c#

//상위 타입
public abstract class UIWidgetBase : MonoBehaviour, IView
{
    private void Awake()
    {
        OnAwake();
    }

    protected virtual void OnAwake()
    {
        Initialize(); // UIWidgetBase.Initialize()
        BindEvent();  // UIWidgetBase.BindEvent()
    }

    private void Initialize()
    {
        // 모든 Widget에 필요한 공통 초기화
    }

    private void BindEvent()
    {
        // 모든 Widget에 필요한 공통 이벤트 연결
    }
}
~~~

~~~C#

// 하위 타입
public class UIImageBase : UIWidgetBase
{
    protected override void OnAwake()
    {
        base.OnAwake();

        Initialize(); // UIImageBase.Initialize()
        BindEvent();  // UIImageBase.BindEvent()
    }

    private void Initialize()
    {
        // Image에 필요한 컴포넌트와 표시 데이터 초기화
    }

    private void BindEvent()
    {
        // Image에 필요한 이벤트만 연결
    }
}
~~~
- 두 클래스의 `Initialize()`와 `BindEvent()`는 이름만 같다.
- 모두 `private`이므로 override 관계가 아니며 완전히 다른 메서드다.
- `UIWidgetBase.OnAwake()`에서는 상위 타입의 메서드가 호출된다.
- `UIImageBase.OnAwake()`에서는 하위 타입의 메서드가 호출된다.
- 상속 실행 순서를 연결하는 메서드는 `OnAwake()` 하나다.