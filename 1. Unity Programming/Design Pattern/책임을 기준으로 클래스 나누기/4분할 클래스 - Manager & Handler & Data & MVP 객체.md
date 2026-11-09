## :fireworks: 문서를 작성하게 된 이유

- Data , Manager , Handler (Manager의 nested) , MVP 객체 (UI & FieldObject)의 핵심 책임의 경계가 뚜렷하지 않음을 느꼈다.
- 물 섭취 앱 푸쉬 기능을 만들다가 생긴 고민이었다.
  - 안드로이드 자바 (여기선 서버 느낌을 준다)가 물 마신 날짜를 전달해준다. 이걸 가공해서 UI에서 표시해야 한다.
  - 대체 가공하는 코드를 어디에 넣어야할까? MVP의 Presenter? 아니면 Handler를 하나 만들어야 하는가?
  - Manager는 이러다보면 컨텐츠가 커질 때 마다 매우 복잡해질텐데?
  - 근데 물 섭취 정보가 싱글턴에 존재해야 하는가?
- 이런 질문들에 대해서 출발했고 아래 4 가지 항목에 대해 나름의 정답을 만들었다.

<br><br>

## :fire: Data Class — 게임 전체에서 공유할 상태 (게임 전체에서 같은 값을 갖는다.)

- 이 문서의 Data는 Manager에 연결된 공용 Data 클래스를 의미한다. MVP 객체의 Model과 구분한다.
- 게임 전체에서 하나의 기준으로 공유해야 하는 상태와 기록을 보관한다.
- 현재 사용하는 객체가 하나여도, 게임 전체에서 같은 기준값으로 참조해야 한다면 공용 Data에 둘 수 있다.
- 서버에서 받거나 로컬에서 읽어올 수 있다. 데이터의 출처와 공용 데이터인지에 대한 판단을 구분한다.
- 각 MVP에서 필요하다는 이유로 공용 원본을 각각 따로 관리하지 않는다.

<br>

## :fire: Manager — 모든 클래스가 편하게 공용 Data를 읽도록 열어준 통로

#### :white_check_mark: 경험

- 처음에는 캐릭터 데이터를 외부에서 자주 참조하므로 MyCharacterManager에 모아두자고 생각했다. 
- 이후에는 캐릭터와 조금이라도 관련되면 넣기 시작했다. <br> 그러다보니 데이터 참조뿐만 아니라 가공까지 책임이 생겨버렸다. <br> 클래스가 너무 커졌다.
- 거대해진 Manager를 쪼개고 책임을 단순화하기 시작했다.
- 편하게 참조하려면 가공이 필요했고 그 가공을 Handler가 담당하도록 분리했다.
- 이 때 Handler의 가공과 MVP 객체의 Presenter의 가공이 겹치게 되었고 판단 기준을 고민했다.

<br>

#### :white_check_mark: 책임과 범위

- 내 프로젝트에서 Manager는 싱글턴으로 사용한다. 외부 객체가 해당 공용 Data를 일관된 창구로 조회하고 갱신할 수 있게 한다.
- 데이터 자체는 Data 클래스에 두고, Manager는 Data와 외부 객체 사이의 접근 통로 역할에 집중한다.

<br>

- 게임 전체에서 읽어야 하는 데이터를 아름답게 전달하기 위한 가공은 Handler에서 담당한다.
  - 회사에서 Handler 구분 없이 모든 가공 메서드를 Manager에 박았던 기억이 있다.
  - Manager가 커지게 되는 요인과 가독성을 깨는 주범이었다. AI 시대여도 이는 동일하다고 생각한다.
- 특정 UI나 FieldObject의 사용 목적에 맞춘 세부 가공은 그들의 Presenter가 담당하면 된다.

<br>

#### :white_check_mark: Facade Pattern
> Facade는 건물의 ‘정면’이라는 뜻입니다. 회사 비유에서는 외부인이 마주하는 대표 '창구'

- MVP객체가 Handler나 Data의 내부 구조를 몰라도 Manager에 필요한 일을 요청할 수 있다면, 그 창구 역할만으로 Facade 패턴이 성립한다.
  - private nested class의 구현은 Facade Pattern에 엄밀히 말하면 표현되지 않는다.

<br>

- 내 프로젝트에서는 Handler를 Manager의 `private` 중첩 클래스로 둔다. 
- MVP 객체는 Manager의 `public` 메서드로 요청하고, 어느 Handler에게 일을 맡길지는 Manager가 정한다.
- MVP 객체는 Manager가 공개한 메서드의 사용법과 동작에 의존한다. 그 약속을 지키면 Handler를 추가하거나 직원끼리 일을 다시 나누는 변경을 Manager 내부에서 처리할 수 있다.

<br>

## :fire: Handler Class — 공용 데이터에 대한 공통 가공 처리 메서드의 집합

#### :white_check_mark: 경험

- MyCharacterManager가 커지는 것을 경계해서 Handler 직원으로 나눴다. 
- 그런데 이번 대화에서 Manager의 책임을 너무 크게 잡아 Handler도 그 범위를 이어받고 있다는 생각이 들었다.
- private nested class로 두는 게 가장 아름답다고 생각한다.

<br>

#### :white_check_mark: 책임과 범위

- Manager에게 요청을 받아 해당 영역의 데이터를 조회하거나 가공하는 직원이다.
- 기능 영역을 기준으로 책임을 나누고, 그 영역에서 공통으로 적용할 처리와 규칙을 담당한다.
- 특정 UI가 원하는 표현을 만드는 것보다, 사용처들이 동일한 기준으로 활용할 수 있는 값을 만드는 데 집중한다.
- 가공 결과가 필요하다고 해서 별도의 공용 원본을 중복해서 보관할 필요는 없다.
- Manager 아래에 Handler를 추가하기 전에, 그 업무가 해당 Manager의 공용 책임에 속하는지 먼저 판단한다.

<br>

#### :white_check_mark: 장점
이 구조의 장점은 책임을 쪼개는 것. <br> 그리고 책임의 계층이 있는 것. 
> I typically nest a class if the nested class doesn't serve a purpose outside the context of the outer class.
> It could be a parameter class or something.
> Nesting it helps keeping the namespace cleaner ¯\_(ツ)_/¯
- :airplane:[reddit](https://www.reddit.com/r/csharp/comments/1lgfa6i/purpose_of_nested_classes/)

<br>

## :sparkles: MVP 객체 — 자신의 목적에 맞게 데이터 활용

#### :white_check_mark: 경험

- 과거에는 UI에서 필요한 데이터와 갱신 로직을 View에 함께 두고 처리한 적이 있다.
- FieldObject는 이미 MVP로 구성해서 데이터를 관리하고 있었다. 
- 이번에는 UI와 FieldObject 모두 필요한 공용 값을 받아 각자의 목적에 맞게 활용하는 객체로 정리했다.

<br>

#### :white_check_mark: 책임과 범위

- UI와 FieldObject 모두 MVP 객체에 포함한다.
- 필요한 공용 값은 Manager를 통해 전달받고, 각 객체의 목적에 맞게 활용한다.
- 화면에서 보여주는 값이라고 해서 그 값의 공용 원본까지 해당 MVP가 소유해야 하는 것은 아니다.

<br>

## :sparkles: 새로운 데이터와 기능을 배치할 때의 판단 기준

1. **어떤 범위의 데이터인가?** 게임 전체가 같은 기준으로 공유할 상태인지, 특정 MVP 객체가 소유할 상태인지 구분한다.
2. **어떤 범위의 처리인가?** 공용 데이터에 적용할 공통 처리인지, 특정 MVP의 목적에 맞춘 처리인지 구분한다.
3. **기존 책임 안의 기능인가?** 공용 기능이어도 관련 있다는 이유만으로 특정 Manager와 Handler에 계속 추가하지 않는다.

<br>

## :sparkles: Codex 보완 제안 — 계산의 배치 기준 -> 복잡도 보다는 데이터의 Scope

- **단순한 계산은 Handler, 복잡한 계산은 Presenter라는 기준으로 나누지 않는다.** 복잡도보다 그 계산이 담당하는 규칙과 사용 범위를 확인한다.
- 사용처마다 활용 방식이 다르면 각각 가공할 수 있다. 다만 동일하게 따라야 하는 게임 규칙을 여러 Presenter에 복제하지 않는다.
- 다른 곳에서도 사용할 수 있다는 가능성만으로 공통 처리를 늘리기보다, 여러 사용처가 반드시 같은 기준을 따라야 하는지 확인한다.

<br>

## :sparkles: 관련 문서

- :airplane: [Manager & Data 클래스 쪼개기](./Manager%20%26%20Data%20클래스%20쪼개기.md)
- :airplane: [클래스 쪼개기는 회사-직원들의 관계와 같다](./클래스%20쪼개기는%20회사-직원들의%20관계와%20같다.md)

<br><br>

## :sparkles: 물 섭취 기능에서 정리한 조회·가공·보관의 책임

#### :white_check_mark: 경험

- 물 섭취 기능을 만들면서 Android에서 받은 시각을 어디에서 가공하고 보관할지 고민했다.
- Handler가 가공한 날짜를 자신의 필드에 보관할 필요는 없다고 판단했다. Handler는 MyCharacterData의 Update 메서드를 호출해 값을 갱신하도록 했다.
- 외부에서 값을 읽을 때도 Manager → Handler → Data로 연결하려고 한다. 한 단계를 더 거치더라도 조회·가공 코드를 Handler에서 찾을 수 있게 하려는 규칙이다.
- 이 구분이 의존성을 줄이고 응집도를 높이는 데 중요하다고 느꼈다. Manager는 필요한 데이터를 MVP에 전달하는 창구에 집중하도록 하고 싶다.

<br>

#### :white_check_mark: 내가 유지하려는 흐름

```text
조회 요청: MVP → Manager → Handler → Data
값 반환:   Data → Handler → Manager → MVP
```

- Data에 원본 상태를 보관하고, 같은 날짜 값을 Manager와 Handler의 필드에 따로 중복 보관하지 않는다.
- Handler는 해당 기능의 데이터 조회와 공통 가공을 담당한다. 물 섭취 시각을 날짜 정수로 변환한 뒤 Data를 갱신하고, 조회 시에는 보관된 날짜를 읽는다.
- Manager는 Handler에 요청을 전달하고 결과를 MVP에 제공한다.
- MVP는 Manager에서 받은 값을 자신의 목적에 맞게 활용하고 표시한다.

<br>

#### :white_check_mark: Codex 보완 제안 — 의존성과 응집도를 확인하는 기준

- 계층을 추가하는 것만으로 의존성이 자동으로 줄지는 않는다. MVP가 Android 저장소의 Key나 Handler 내부 구현을 몰라도 필요한 값을 사용할 수 있는지 확인한다.
- Handler에는 같은 기능의 조회·가공을 모은다. 관련 없는 기능까지 모으지 않아야 책임과 응집도를 유지할 수 있다.
- Manager가 Data 객체의 참조를 갖는 것은 같은 값을 중복 보관하는 것과 다르다. 중복을 피하려는 대상은 여러 필드에 각각 보관한 동일한 상태다.
- 단순 조회에도 Handler를 거치면 따라갈 메서드가 늘어난다. 이 비용을 감수하고 데이터 접근 위치의 일관성을 얻겠다는 프로젝트의 선택으로 기록한다.
