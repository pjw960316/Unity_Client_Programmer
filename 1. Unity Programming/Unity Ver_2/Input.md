## :fire: Input System 패키지를 사용한다. <br> :fire: InputActionAsset이 하드웨어(ex : 키보드 w키)와 유니티를 바인딩 하는 것 <br> :fire: PlayerInput Component는 Scene의 GameObject가 InputActionAsset을 사용할 때 필요한 컴포넌트
- action 설정은 :link:[Unity Official](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.19/manual/Actions.html) 이거 참고.
- 여러 기기 대응이 되니까 현대 게임 개발에서는 필수.

<br><br>

## :fire: Input Routing으로 Input에 대한 분기를 처리한다. <br> :fire: string binding 보다는 inspector binding이 훨씬 안전하다. <br> 그러므로 Dictionary를 만들고, <br> key를 string 대신 InputAction으로 세팅한다.
~~~c#
[SerializeField] private PlayerInput _playerInput;
    
// InputActionReference 추가하면 Dictionary에 추가하세요
[SerializeField] private InputActionReference _moveInput;

private readonly Dictionary<InputAction, Action<InputAction.CallbackContext>> _actionDict = new ();

// 예외처리
if (_playerInput.currentActionMap.Count() != _actionDict.Count)
{
    Debug.LogError("PlayerInput Component의 액션 개수와 _actionDict의 액션 개수가 다르다.");
}
~~~
- Player Input Component의 Behaviour를 Invoke C Sharp Event로 하기 때문에 직접 바인딩을 해줘야 한다.
- 이렇게 되면 구현과 데이터 바인딩이 분리된다. 
  - 데이터 바인딩을 Insector를 통해 진행하고, Awake()에서 초기화 한다.
  - 구현은 우선 모든 Input을 받고, dictionary에서 key로 조회해서 Handler Action을 통해 처리한다.
> Using a Dictionary, I map specific keys to actions that invoke the "ProcessEmailByStoreParam" method with the appropriate parameters. This methodology simplifies the workflow by eliminating verbose if-else or switch statements. Additionally, adding a new action is as simple as adding a new key-value pair to the "actionMap" dictionary.
- :link: [공식 문서 추가 내용](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.19/manual/PlayerInput.html)

<br><br>

## :fireworks: 몇 가지 주의사항
- Input System에서 사용할 하드웨어(device)는 직접 추가해줘야 한다. Control Scheme에 가서 키보드랑 마우스를 추가한다.
- Input은 action property랑 binding property로 나눈다.
  - Action property는 어떤 input인지 설정한다.
  - Binding property는 어떤 device의 어떤 key와 연결할 지 설정한다. (Input System에 device가 추가되어 있지 않으면 동작하지 않는다.)
- Touch의 경우 마우스 포인터의 좌표를 실시간으로 입력 받는 기능과 포인터의 클릭 여부를 감지하는 두 가지 action을 모두 등록한다.