# 아래는 과거에 한 이론 공부니까 나중에 검토해서 살릴 내용만 살리자.

# Inheritance
### 1. C#은 다중상속을 지원하지 않는다. 그러므로 Monobehaviour를 상속받는 클래스는 다른 클래스를 상속 받지 못한다.
- 다이아몬드 문제로 인해 지원하지 않는다.
- <img width="698" alt="20220803_184517" src="https://user-images.githubusercontent.com/55792986/182578386-f32409a8-0b77-4a67-9bc6-867b10ece4cf.png">
### 2. Base Class
- 멤버 변수를 넣음 (ex : name, health)
- 상속 받을 클래스들이 필요한 변수와 메소드를 저장
- Character -> NPC -> Merchant
  - merchant에 필요 없는 함수라도 NPC에 있을 수 있다. 그러면 문제가 발생한다.
  - ![20220809_120654](https://user-images.githubusercontent.com/55792986/183558031-02168329-35bf-4b51-b7d9-354d36808739.png)
- **크기가 커질수록 상속은 관리하기 어려워진다.**
  - 하위 클래스는 Base의 모든 것을 상속받지만 필요 없는 기능도 많아지기 때문이다.

### 3. 함수의 위치
- 어떤 함수를 구현할 때 부모 클래스에서 구현할지 자식 클래스에서 구현할지에 대한 구분이 어렵다.
  - 회사에서도 이 부분이 매우 어려웠다.

### 4. 기타
- 자식이 부모에게서 어떤 메서드를 상속받았다고 가정하자. 자식의 메서드에서 사용되는 멤버 변수는 자식의 것이다.
- base를 이용하여 부모의 멤버를 호출할 수 있다.


# Composition
- ![20220809_121908](https://user-images.githubusercontent.com/55792986/183558147-7066d537-c330-4ad1-a179-2956d0a78ceb.png)
- 부품을 추가하는 방식
- 복잡해 보이지만 유지 보수에는 훌륭한 방법이다.
- character movement class, health class, ai movement class 같이 기능을 나누어서 개발한다.
- ![20220809_123635](https://user-images.githubusercontent.com/55792986/183558827-42f878f2-f0ed-4fcb-a6fc-a5786ca1e4cc.png)

# Inheritance vs Composition 연습
### Practice : [Link](https://github.com/pjw960316/Better_Unity_Skill)
### Reference : [Link](https://www.youtube.com/watch?v=8TIkManpEu4)

### 1. 이전 경험
- 회사에서는 상속만 이용했다.
- 부모의 멤버변수, 메소드를 모두 상속 받았고, 적당한 계층의 클래스에 필요한 함수를 구현했다.
- 인터페이스나 오버라이딩도 거의 제대로 이용되지 않았던 걸로 기억한다. 

### 2. 설계
- 미니언의 고유 기능과, 미니언/정글몹이 모두 갖는 기능에 대해 생각해보자.
- (1) 최상단 공통기능 (Structure Class)
  - 상태 (hp,mp,ad,ap,armor,magic_resistance)
  - 데미지를 받는 것.
  - 죽음 이벤트가 발생했을 때 죽인 객체에게 골드를 전달하는 것
  - 죽음
- (2) 미니언 끼리 공통기능 (Minions Class)
  - 자동 이동 (속도는 다르지만 이는 상태로 조절)
  - 일정 거리에 들어왔을 때 공격
  - 이걸 인터페이스로 구현해본다.
- (3) 근거리 미니언, 원거리 미니언, 대포미니언, 슈퍼미니언 독립기능 (각각의 Class)
  - 각각의 공격은 다를 것 이다?
    - 공격이란 기능을 수행하는 것은 똑같고, 애니메이션과 상태만 다르게 구현할 수도 있을 것.
    - 공격이라는 기능은 같으니.


### 3. 상속 (Inheritance)으로 구현해보기
- 최상단 structure는 인터페이스로 구현해야할까? 클래스로 구현해야할까?
- ad,ap 같은 최상단 멤버의 접근지정자는 어떻게 해야할까?


### 4. 컴포넌트로 구현해보기
- StructureState Class, MinionsMove Class 처럼 세부적으로 나누는 클래스.

# 멤버 변수에 대해서 고민해보자.
### 1. 비슷한 고민
- 질문
  - ![image](https://user-images.githubusercontent.com/55792986/186606975-2fbea4d7-c3b1-4932-b8ec-3af020e6de67.png)
- 답변
  - ![20220825_165029](https://user-images.githubusercontent.com/55792986/186607109-144d13fa-6374-4553-b9c8-33832e19eadc.png)
  - 객체의 멤버 변수가 모두 상태를 나타내지는 않지만(당연함) 상태를 나타내는 멤버 변수가 존재한다.
  - 이런 경우 '객체지향의 사실과 오해'를 참고하면 숨겨주어야 한다. 상태를 참고하려면 메서드를 이용해야 한다.
  
### 2. 어떻게 구현할까?
- 프로퍼티로 구현한다.
  - 인터페이스에?

