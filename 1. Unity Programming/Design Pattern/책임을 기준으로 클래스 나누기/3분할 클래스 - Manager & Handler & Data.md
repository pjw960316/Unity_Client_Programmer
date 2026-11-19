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

<br>

#### :white_check_mark: 이상적인 Manager는 Getter로만 이루어질까?

- 조회 흐름을 정리하다 보니, 이상적인 Manager의 공개 메서드에는 Getter만 남지 않을까 하는 생각이 들었다. <br>
  월간 루틴 보상액은 Data에서 읽고, 월간 물 보상 횟수는 Handler에서 받아오면 MVP는 Manager에 필요한 값을 요청할 수 있다.

- 실제로 낮잠을 기록할 때는 MVP가 `UpdateSiestaRecord()`를 호출한다. Manager는 기록 갱신을 Handler에 맡기고, 저장한 뒤 변경된 보상액을 알린다. <br>
  조회만 제공하는 Manager라면 Getter로 충분하다. 상태 변경 요청도 받는 Manager라면 그 작업의 순서를 조율하는 메서드가 필요하다.

<br><br>

## :fire: 데이터를 가공하는 주체는 상황에 따라 두 개로 나뉜다. <br> :fire: 하나는 Handler Class <br> :fire: 다른 하나는 Data Class

#### :white_check_mark: 경험

- **Getter의 가공은 위에서 공부한 바와 같이 handler의 영역이다.**
  - Data Class가 결국 entity가 되는 거고, 우리는 거기서 온전히 읽어온다. 
  - 그 후, MVP의 요청대로 전달해주려면 적절한 가공이 필요하고 그걸 Handler가 한다.
- **하지만 반대의 경우도 있다. MVP의 어떤 행위로 데이터 갱신이 필요하다면?**
  - 그건 어떤 클래스에서 해야할까? 의 질문으로 출발했다.

<br>

#### :one: 조회에서는 Handler Class가 데이터를 가공한다.

- Data → Handler → Manager → MVP <br>
  MVP가 요청한 값에 가공이 필요하면 Handler가 Data를 읽어 결과를 만든다. Manager가 그 결과를 MVP에 전달한다.

<br>

#### :two: 갱신에서는 Data Class가 데이터를 가공한다.

- MVP → Manager → Handler → Data <br>
  Manager가 작업을 맡기면 Handler는 필요한 인자를 준비해 Data의 갱신 메서드를 호출한다. 
  - 예를 들어 Handler가 `confirmedDates`를 넘기면 Data가 자기 컬렉션에 맞게 가공해 상태를 갱신한다.
- Data Class에 private set을 지향하는 이유.
- Data는 컬렉션의 갱신을 맡고, 외부 조회에는 `ImmutableDictionary`나 `ImmutableHashSet` 같은 불변 컬렉션을 제공한다.