## :fire::one: Model의 역할 및 책임
#### 1. Presenter 또는 Manager가 데이터를 요청 할 때 get, set, update를 담당할 책임이 있다.
- :star: Get, Set, Update는 매우 단순한 동작을 처리해야 한다. 
- 그러므로, Presenter 또는 Manager에서 Get, Set, Update를 단순하게 처리 할 수 있게 가공하는 로직을 구현해야 한다.

<br><br>

## :fire::two: Model이 멤버로 들고 있을 것
#### 1. private으로 캡슐화 시킨 일반 타입의 데이터 필드
- 외부에서 접근을 public getter property 또는 public getter method로 구현해서 Property에게 제공한다.
#### 2.Container 
- private 형태의 외부 접근 불가한 기본 Container(List, Dictionary) 
- public 형태의 외부 접근 가능한 ImmutableContainer의 Getter Property
- :link:[06장_Type and Member Basics (=Class).md](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/C%23%20Ver_2/06%EC%9E%A5_Type%20and%20Member%20Basics%20(%3DClass).md)
#### 3. Enum
- Class 외부에 선언하지 않도록 주의한다. (Scope)
- ![alt text](./captures/20250715.png)
#### 4. ReactiveProperty (Unirx)
- State field를 private reactiveProperty로 구현하고 public Iobservable을 제공한다. 
- 그러면 상태 변경에 따라 호출되는 method를 구독 시킬 수 있다.

<br><br>

## :fire::three: Model의 특징 및 주의사항
#### 1. 절대로 View를 멤버로 갖지 않는다.

<br>

#### 2. ScriptableObject, Xml, MVP 구조에서 presenter와 붙는 스크립트 모두 모델이다. <br> 그러나 ScriptableObject와 Xml은 Manager Class로 관리되기 때문에 조금은 일반 모델과 다르게 바라본다.
~~~c#
public class AlarmData : ScriptableObject, IModel

public class ScriptableObjectManager : ManagerBase<ScriptableObjectManager>
{
    #region 1. Fields

    private List<ScriptableObject> _scriptableObjectList = new();
}
~~~
- AlarmData의 경우 Model이므로 IModel을 상속 받지만, ScriptableObject도 상속받는다.
- ScriptableObjectManager에서는 List<ScriptableObject> _scriptableObjectList 타입으로 생성시킨다. 그로 인해 IModel 보다는 더 특정하게 ScriptableObject로 관리한다.

<br>

#### 3. :bangbang: ScriptableObject로 구현된 Model의 경우, Model Instance가 생성되면 Field들이 메모리에 로드된다. 그러나 AudioClip은 설정을 해줘야 메모리에 로드된다.
- ![alt text](./captures/20250902.png)
~~~c#
public class AlarmData : ScriptableObject, IModel
{
    #region 1. Fields

    [SerializeField] private SerializedDictionary<EAlarmButtonType, AudioClip> _alarmAudioClipDictionary = new();
    [SerializeField] private SerializedDictionary<EAlarmButtonType, float> _alarmTimeDictionary = new();
    [SerializeField] private AudioClip _alarmChickenAudioClip;

    #endregion
}
~~~
- 여기 존재하는 필드는 메모리에 올라가지만 AudioClip의 경우 완전히 메모리에 올라가지는 않는다. 
- 그러므로, 높은 용량의 audioClip을 참조할 때 disk-load로 인한 2~3초 정도의 렉이 발생한다. 
> When Preload Audio Data is enabled, the audio data is loaded into memory along with the object when the scene loads or the asset is referenced. If disabled, Unity will not load audio data into memory until you explicitly call AudioClip.LoadAudioData() or call Play(), which implicitly does so.
- ![alt text](./captures/20250902_2.png)
  - Preload Audio Data를 하면 Memory에 미리 로드해서 렉은 없어지지만 memory에 상주한다는 단점이 있다. (Trade-Off)