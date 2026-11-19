## :fireworks: Manager Class - Data Class 쪼개는 규칙 
<p align="center">
  <img src="../captures/Manager And Data.png" width="700">
</p>

<br>

#### :one: 큰 도메인(MyCharacter) 내부의 여러 내부 도메인(Routine, Siesta, EnglishLearning ...)이 존재한다. <br> Manager는 이걸 기능 관점 단위인 Handler로 쪼개고, <br> Data Class는 필드 관점 단위인 작은 Data Class들로 쪼갠다.
- 기능이 추가될수록 하나의 Manager, 하나의 Data에 모든 책임을 몰아넣으면 구조가 빠르게 비대해진다.
- 따라서 MyCharacter 내부 구조는 하위 도메인 단위로 분리한다.
  - 행동(처리 로직)은 Handler로 분리
  - 상태(보관 데이터)는 하위 Data로 분리

<br>

#### :two: Manager 내부의 Handler들은 데이터를 조회 및 가공해서 MVP / UI 객체들에게 전달한다. <br> 가공 과정이 Handler의 메서드로 표현된다.
- Manager는 외부 객체(MVP / UI)의 진입점이 된다.
- Manager는 Handler를 필드로 가지고 있어야 한다. 그래야 MVP / UI 객체의 요청을 이벤트뿐만 아니라 메서드 호출(Getter) 방식으로도 Handler에 위임할 수 있다.
- Handler는 외부 객체가 요구하는 데이터를 전달하기 위해 Data 클래스의 데이터를 가공한다. 
  - immutable 데이터를 받으므로 가공 과정에서 원본은 변하지 않는다.
- Manager와 Handler는 데이터를 저장하지 않고 Data Class를 통해 전달 받아 사용한다.
- 그러므로 새로운 도메인이 추가되면 새로운 Handler에게 위임하고 <br> 기존 도메인에 새로운 기능이 추가되면 Handler에 메서드를 추가하게 된다. 

<br>

#### :three: Data는 Data의 저장, 갱신만을 책임으로 갖는다. <br> Data Class는 Entity 영역이므로 Immutable Data 형식으로 Manager에게 조회 권한을 준다.
- 조회와 갱신은 프로퍼티와 매우 간단한 메서드로 구성된다. <br> 또한 Data Class를 쪼개는 건 결국 도메인 단위의 필드들을 구분하기 위함이다.
- 즉, Entity 영역이므로 메서드의 기능들이 단순하며 책임도 단순하다.
- **외부에 Data를 전달할 때 immutable 하게 전달하도록 주의한다.**

<br><br>

## :fireworks: MVP -> Manager -> Handler -> Data 구조에서 <br> Getter를 구현하는 방식

#### :white_check_mark: 경험

- 나는 Data Class의 Setter를 외부로 여는 걸 싫어한다. <br> 그래서 public get; private set;을 이용한다.
- 나는 MVP 객체가 필요한 데이터를 싱글턴에 요청할 때 반드시 Manager를 통해야 한다고 설계했다.
- 그러다 보니 중복된 코드가 발견되기 시작했다. 

~~~c#
// MyCharacterManager
public int GetMonthlyRoutineSuccessMoney()
{
    return _routineHandler.GetMonthlyRoutineSuccessMoney();
}

// RoutineHandler
public int GetMonthlyRoutineSuccessMoney()
{
    return _myCharacterData.MonthlyRoutineSuccessMoney;
}

// MyCharacterData
public int MonthlyRoutineSuccessMoney { get; private set; }
~~~

<br>

#### :white_check_mark: 각각의 클래스의 Getter 관점 책임

- Data는 공용 상태를 보관한다. Manager와 Handler에는 같은 상태를 별도 필드로 중복 보관하지 않는다.
- Handler는 해당 기능의 데이터를 Data에서 조회하고, 필요한 공통 가공을 담당한다.
- Manager는 외부에 최종 Data를 제공한다.

<br>

#### :pushpin: 단순 조회는 Manager에서 Data를 바로 읽는다.

- MVP가 필요한 값을 Data 프로퍼티에서 그대로 얻을 수 있으면 <br> Manager가 직접 읽는 것을 기본으로 삼는다.
- 이전 예시의 월간 루틴 보상액은 Handler에서도 같은 값을 전달하기 때문에, Manager에서 바로 읽을 수 있다.

  ~~~c#
  // MyCharacterManager
  public int GetMonthlyRoutineSuccessMoney()
  {
      return _myCharacterData.MonthlyRoutineSuccessMoney;
  }
  ~~~


<br>

#### :pushpin: const는 Data에 모으고 클래스 이름으로 접근한다

- Manager와 Data 구조에서 사용하는 `const`는 Data에 모아 정의하고, 외부에서도 Data 클래스 이름으로 직접 접근한다.