## :fire: Input System 패키지를 사용한다. <br> :fire: InputActionAsset이 하드웨어(ex : 키보드 w키)와 유니티를 바인딩 하는 것 <br> :fire: PlayerInput Component는 Scene의 GameObject가 InputActionAsset을 사용할 때 필요한 컴포넌트
- action 설정은 :link:[Unity Official](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.19/manual/Actions.html) 이거 참고.
- 여러 기기 대응이 되니까 현대 게임 개발에서는 필수.

<br><br>

## :fireworks: 성능과 유지보수에 대해서 안전한가?
- 코드 보면 현재 리플렉션이 많이 보임. 즉, 리플렉션 쓰지 않고 PlayerInput Component를 읽어서 스크립트에서 관리하는 게 맞다고 생각. 