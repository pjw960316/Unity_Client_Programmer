## :fireworks: View를 멍청하게, Presenter는 책임있게. <br> View를 신입 직원이라 생각하고, 본인은 할 줄 아는 게 없음. <br> Presenter가 파트장님, Manager가 팀장님이라 생각하자.

#### :one: 모든 View는 멍청해서 자신의 Close 조차 자신이 책임지지 못하고, Presenter에게 책임을 넘긴다. <br> 최상단에 존재한다. 
~~~c#
public abstract class UIPopupBase : MonoBehaviour, IView
{
    public void ClosePopup()
    {
        _disposables?.Dispose();

        Destroy(gameObject);
    }
}
~~~
- 우선 모든 팝업은 자신을 Scene에서 Destroy 하는 기능을 갖고, View에 존재하는 IObservable을 Dispose하고 정리한다.
- public으로 외부에 노출시켜 Presenter에게 자신을 끄도록 한다. 

<br>

#### :two: 모든 UIPresenter는 View를 Close()해야 할 책임이 있다. <br> 코드의 주석을 확인한다.
~~~c#
public abstract class UIPresenterBase : PresenterBase
{
    public override void Initialize(IView view)
    {
        base.Initialize(view);

        _popupBase = _view as UIPopupBase;
        ExceptionHelper.CheckNullException(_popupBase, "_popupBase is null");
    }

    // presenter는 자신이 close 될 때 View도 Close 한다.
    protected void Close()
    {
        _popupBase.ClosePopup(); // View를 끈다. // Presenter는 View를 들고 있기에 접근이 가능하다.

        RequestUpdateLivedPopup(_popupBase.EPopupKey); // UIManager에게 현재 켜져 있는 Popup의 목록을 갱신해달라고 요청한다.

        TerminatePresenter(); // Presenter를 제거한다.
    }

    private void RequestUpdateLivedPopup(EPopupKey ePopupKey)
    {
        _uiManager.RemoveOpenedPopup(ePopupKey);
    }

    protected void TerminatePresenter()
    {
        DisposeCompositeDisposables();
        
        _presenterManager.TerminatePresenter(this);
    }
}
~~~
- 즉, Presenter로 하여금 View의 Close를 결정한다.

<br>

#### :three: concrete Presenter에서 Popup과 자신(presenter)을 종료 시켜야 하는 시점에 Close()를 호출한다. 
~~~c#
public class AlarmPresenter : UIPresenterBase
{
    private void OnStartAlarmSystem()
    {
        RequestPlaySleepingMusic();

        RequestOpenAlarmTimerPopup();

        Close(); // Presenter에서 Close() 시점을 정하고, View는 아무것도 모르고 Presenter가 죽으라고 하면 죽는다.
    }
}
~~~
- 상위 클래스에서 구현한 Close()를 동일하게 하위 concrete Presenter에서 사용한다.
- 알람 음악을 재생시키고 -> 알람 타이머 팝업을 켜고 -> view와 presenter를 정리하고 리소스를 해제한다.

<br>

#### :bangbang: 이전에는 화면을 닫는 X 버튼을 누르는 순간 User의 Close Input을 <br> View에서 인지하고 Presenter에 요청을 시켰다. <br> 이 방식도 나쁘지 않지만, 하나의 버튼에 두 개 이상의 IObservable이 걸리게 되고, <br> 실행 흐름을 알 수 없게 되어 방식을 수정했다.
- :link:[1개의 이벤트에 2개의 IObservable의 Subscribe가 걸려 있다면
실행 흐름을 알 수 없기 때문에 지양한다.](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/Unity%20Ver_2/UniRx.md#fire-1%EA%B0%9C%EC%9D%98-%EC%9D%B4%EB%B2%A4%ED%8A%B8%EC%97%90-2%EA%B0%9C%EC%9D%98-iobservable%EC%9D%98-subscribe%EA%B0%80-%EA%B1%B8%EB%A0%A4-%EC%9E%88%EB%8B%A4%EB%A9%B4--%EC%8B%A4%ED%96%89-%ED%9D%90%EB%A6%84%EC%9D%84-%EC%95%8C-%EC%88%98-%EC%97%86%EA%B8%B0-%EB%95%8C%EB%AC%B8%EC%97%90-%EC%A7%80%EC%96%91%ED%95%9C%EB%8B%A4)