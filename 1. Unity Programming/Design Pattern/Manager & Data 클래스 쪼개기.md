## :fireworks: Manager Class - Data Class 쪼개는 규칙 
![image](./captures/Manager%20And%20Data.png)

<br>

#### :one: 큰 도메인(MyCharacter) 내부의 여러 내부 도메인(Routine, Siesta, EnglishLearning ...)이 존재한다. <br> Manager는 이걸 기능 관점 단위인 Handler로 쪼개고, <br> Data Class는 필드 관점 단위인 작은 Data Class들로 쪼갠다.
- 기능이 추가될수록 하나의 Manager, 하나의 Data에 모든 책임을 몰아넣으면 구조가 빠르게 비대해진다.
- 따라서 MyCharacter 내부 구조는 하위 도메인 단위로 분리한다.
  - 행동(처리 로직)은 Handler로 분리
  - 상태(보관 데이터)는 하위 Data로 분리

<br>

#### :two: Manager 내부의 Handler들은 데이터를 조회 및 가공해서 MVP / UI 객체들에게 전달한다. <br> 가공 과정이 Handler의 메서드로 표현된다.
- Manager는 외부 객체(MVP / UI)의 진입점이 된다.
- Handler는 외부 객체가 요구하는 데이터를 전달하기 위해 Data 클래스의 데이터를 가공한다. 
  - immutable 데이터를 받으므로 가공 과정에서 원본은 변하지 않는다.
- Manager와 Handler는 데이터를 저장하지 않고 Data Class를 통해 전달 받아 사용한다.
- 그러므로 새로운 도메인이 추가되면 새로운 Handler에게 위임하고 <br> 기존 도메인에 새로운 기능이 추가되면 Handler에 메서드를 추가하게 된다. 

<br>

#### :three: Data는 Data의 저장, 갱신만을 책임으로 갖는다. <br> Data Class는 Entity 영역이므로 Immutable Data 형식으로 Manager에게 조회 권한을 준다.
- 조회와 갱신은 프로퍼티와 매우 간단한 메서드로 구성된다. <br> 또한 Data Class를 쪼개는 건 결국 도메인 단위의 필드들을 구분하기 위함이다.
- 즉, Entity 영역이므로 메서드의 기능들이 단순하며 책임도 단순하다.
- **외부에 Data를 전달할 때 immutable 하게 전달하도록 주의한다.**