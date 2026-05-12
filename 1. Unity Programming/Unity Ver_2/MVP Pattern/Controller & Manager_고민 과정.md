## :fireworks: 고민의 의식의 흐름
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

<br><br>

## :fireworks: 방향성을 적어놓고 계속 연구한다.
- 확정
  - Controller는 다른 Controller를 참조하지 않는다.
  - Manager는 MVP 객체들의 소통창구 역할과 게임전체의상태 (ex:음악전체, 필드오브젝트 전체)를 관리하고, Controller는 상태를 들고 있지 않는다. 얘는 유니티 연산 전문가임.
    - 즉, 일단 MVP는 3계층. Manager-controller는 2계층. Manager는 Model에 가깝고, Controller는 View에 가깝다. 근데 Presenter에서. Presenter의 View 개입기능을 Controller에서 하고, 일반 객체(ex : 참새 오브젝트)들이 요청 받아서 처리하고 전달해 주는 Presenter의 기능은 Manager에서 함.
  - MVP는 객체단위, Manager - Controller는 시스템 단위 구조. 
- 연구
  - 누구든 상태 변경을 호출할 수 있지만 상태 변경의 주체는 하나다 이게 핵심이구나. (얘가 거의 정답임) -> 이 방향으로 가도록
    - Manager가 상태를 관리하는데 외부에서 변경 불가하는 시스템
      - 그니까 상태를 private으로 만들고 public을 통해 그 상태 변경을 요청받고, 상태 변경 메서드는 private으로 하는 거지. 근데 이게 결국은 저 public 콜 하면 private 상태 바꾸는 건데. 이게 어쨌든 처리를 manager에서 하니까 안전한거?
      - 그러하다. 상태: private / 외부 접근: public API / 실제 변경 로직: private
      - 근본 캡슐화
    - Manager가 관리하는 raw mutable 객체를 외부에 그대로 주지 않는다.
    - View / Presenter가 Manager 상태를 직접 바꾸지 못하게 한다.
- 구현 해보고 2차 메모
  - 이벤트를 “발행하는 주체”와 “구독해서 반응하는 주체”는 달라도 됩니다. `UIManager`가 `OnOpenPopup`, `OnClosePopup`을 발행하고, `CameraManager`가 그것을 구독하는 구조는 설계적으로 문제 없습니다.
  - Manager는 어딘가에서 요청을 받지만 직접 계산 및 유니티 세상의 연관하는 게 아니라 controller라는 대리자를 통해 유니티 계산을 해서 본인이 관리하고 있는 state를 본인이 변경하는 거다. 
  - controller가 계산하고 그걸 controller에게 주입 받지. 이게 의존성이 있지만 manager - controller 의존은 나쁘지 않아. 
  - 그래서 manager에서 터치 좌표 관리 -> controller가 터치 감지 -> 유니티 관련 계산 -> 이 과정에서 인풋이라는 개념이 들어오니 전략 패턴으로 인풋들을 쪼갬 -> 그러면 쪼갠 결과를 manager에게 전달. -> manager는 변경 사항을 받고 (의존성 생성) 자신의 상태를 갱신 -> 갱신된 상태를 통해 요청한 주체에게 반환
  - :star: 정답이라고 생각 -> Manager는 필드로 상태를 들고 있고, Controller를 통해 “가공된 형태”로 외부에서 변경사항을 주입받는다. 그리고 Manager가 주체로 그 상태를 갱신한다. 만약 unity를 통하지 않는다면 manager 내부에서 직접 갱신을 해도 무방하지 않을까?
  - 값 타입은 복사라서 외부로 빼도 안전 참조 타입은 조심해야 한다
  - 누구든 상태 변경을 요청할 수는 있지만, 상태 변경의 주체는 하나다.
