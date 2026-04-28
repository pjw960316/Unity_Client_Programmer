## :fireworks: Composition이 필요함을 느낀 순간
#### :one: 경위
- 기존에는 IManager → ManagerBase → ConcreteManager 들로 구현했었다.
- 일부 Manager에 Controller를 붙이고 연결해야 했다.
- ManagerBase를 상속받는 ControllerManagerBase를 만들었었다. -> 이것도 문제가 있다.

#### :two: ControllerManagerBase : ManagerBase 대신 두 가지 방식을 생각했었다.  ->  둘 다 좋지 않다.
1. ControllerManagerBase를 ManagerBase를 상속 받지 않는 독립 클래스로 만들었다. -> 다중상속 이슈 발생
2. ControllerManagerBase를 IControllerManager로 만들었다. -> 책임만 생기고 구현의 강제성이 없다. -> DRY (Do not Repeat Yourself)
~~~c#
public interface IControllerManagerBase 
{
    public void RegisterController<TController> (TController controller)
    {
    }
}
~~~
- 추상적으로 Manager와 Controller를 연결하는 코드는 모두 같기 때문에 구현을 강제 할 수 있다.

#### :three: ControllerManagerBase : ManagerBase의 문제점
- 메서드 기능 하나 때문에 계층을 분리시키면 Is-A가 깨진다.  
  - ControllerManagerBase는 “새로운 타입”이 아니라 그냥 “Controller를 가진 Manager”일 뿐이다.
- 계속 이런 구조를 유지하면 계층의 Depth가 10 이상이 나올 수도 있고 망가진 설계가 된다.
- 그렇다고 계층 분리를 피하기 위해 기존의 ManagerBase에 RegisterController 메서드를 넣는 건 절대 안 된다. 
  - ManagerBase는 “모든 Manager의 공통”
  - 공통에 넣으면 → 불필요한 책임 강제 + 구조 오염

#### :four: 해결책으로 Composition