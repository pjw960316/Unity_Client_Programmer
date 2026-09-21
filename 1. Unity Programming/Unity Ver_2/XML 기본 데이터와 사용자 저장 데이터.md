## :fireworks: XML 기본 데이터와 사용자 저장 데이터
- HabitGame의 `MyCharacterData.xml`을 통해 `Resources`와 `Application.persistentDataPath`의 역할을 구분한다.
- XML은 같은 형식이어도 어느 경로에 있는지에 따라 책임이 달라진다.

<br><br>

## :fire: 두 XML의 역할은 다르다.

| 위치 | 역할 | 런타임 접근 |
| --- | --- | --- |
| `Assets/Resources/XML/MyCharacterData.xml` | 최초 사용자 데이터를 만들기 위한 기본 원본 | 읽기 |
| `Application.persistentDataPath/MyCharacterData.xml` | 각 디바이스에서 변경되는 실제 사용자 저장 데이터 | 읽기·쓰기 |

- `Resources`의 XML은 Unity 빌드에 포함된다.
- `Application.persistentDataPath`는 Unity가 Windows, Android, iOS 등 현재 플랫폼에 맞는 쓰기 가능한 경로를 반환한다.
- 플랫폼마다 실제 경로 문자열은 다르지만 같은 코드를 사용하여 접근할 수 있다.

<br><br>

## :fire: Resources를 읽는 시점은 Persistent XML이 없을 때다.
- 게임 설치 자체가 아니라, 게임 실행 중 저장 XML이 없는 상태에서 데이터를 처음 로드할 때 Resources를 읽는다.
- 저장 XML이 없으면 Resources의 기본 XML을 읽어서 Persistent 경로에 복사한다.
- 이후에는 Persistent XML을 읽고, 게임 진행으로 데이터가 변경될 때 Persistent XML만 갱신한다.

```text
최초 실행
Persistent XML 없음
→ Resources XML 읽기
→ Persistent 경로에 XML 생성
→ 생성된 사용자 데이터 사용

이후 실행
Persistent XML 있음
→ Persistent XML 읽기
→ 사용자 행동으로 데이터 변경
→ Persistent XML 저장
```

- 앱 데이터 삭제, 저장 파일 삭제, 재설치 등으로 Persistent XML이 다시 없어지면 Resources를 다시 읽을 수 있다.

<br><br>

## :fire: Resources XML을 수정해도 기존 사용자 데이터에는 자동으로 반영되지 않는다.
- 기존 Persistent XML이 있으면 현재 `GetXmlText()`는 Resources XML을 읽지 않는다.
- 개발자가 Resources의 기본값을 변경하고 새 앱을 배포해도 기존 사용자는 자신의 Persistent XML 값을 계속 읽는다.

```text
Resources XML의 MoneyPerSiestaMinute
100 → 10

신규 사용자
→ Resources의 10으로 저장 파일 생성

기존 사용자
→ Persistent XML의 기존 값 사용
```

- Resources와 Persistent 사이에 자동 동기화는 없다.
- Unity가 게임 종료 시 두 파일을 동기화해 주는 것도 아니다.
- HabitGame에서는 코드가 사용자 데이터 변경 시 Persistent XML을 직접 Serialize한다.

<br><br>

## :fire: 런타임에서 변경되는 사용자 XML은 Persistent 경로에서 읽고 쓴다.
- 루틴 기록과 낮잠 기록은 앱을 종료한 뒤에도 유지되어야 한다.
- Resources만 다시 읽으면 매 실행마다 기본 상태로 돌아간다.
- 따라서 런타임에서 XML을 읽고 쓰는 사용자 저장 파일은 `Application.persistentDataPath`에 둔다.

```text
Resources
→ 최초 생성에 사용할 기본 데이터
→ 런타임에는 읽기

persistentDataPath
→ 디바이스별 사용자 데이터
→ 런타임에 읽기·쓰기
```

<br><br>

## :fire: 게임 공통 설정과 사용자 저장 데이터는 구분한다.
- 기존 `MyCharacterData`에는 게임 전체 설정과 사용자마다 달라지는 데이터가 함께 있었다.

```text
MyCharacterData
├─ 게임 전체 설정
│  ├─ MoneyPerRoutineSuccess
│  └─ MoneyPerSiestaMinute
│
└─ 사용자 저장 데이터
   ├─ RoutineRecordList
   ├─ SiestaTimeRecordList
   ├─ MonthlyRoutineSuccessMoney
   ├─ Name
   └─ Age
```

- `MoneyPerRoutineSuccess`, `MoneyPerSiestaMinute`처럼 모든 사용자에게 같은 규칙을 적용할 값은 사용자 저장 XML에 넣지 않는다.
- HabitGame에서는 현재 값의 개수와 프로젝트 규모를 고려하여 C#의 `public const`로 관리할 수 있다.
- 루틴 기록, 낮잠 기록, 누적 재화처럼 플레이 결과로 달라지는 값만 Persistent XML에 저장한다.

<br><br>

## :fire: 파일 존재 여부에 따른 기본 흐름

| Resources XML | Persistent XML | 의미 | 처리 방향 |
| --- | --- | --- | --- |
| 있음 | 없음 | 정상적인 최초 실행 | Resources를 읽어 사용자 저장 데이터 생성 |
| 있음 | 있음 | 정상적인 재실행 | Persistent 사용자 데이터 읽기 |
| 없음 | 있음 | 기본 원본이 빌드에서 누락됨 | 오류를 기록하고 기존 저장 사용 여부 판단 |
| 없음 | 없음 | 시작할 데이터가 없음 | 오류 처리 후 데이터 초기화 중단 |

- Persistent XML이 없는 것은 최초 실행에서 정상적인 상황이다.
- Resources XML까지 없다면 기본 데이터를 만들 수 없으므로 단순 Warning만 남기고 계속 진행할 수 없다.
- 빈 XML, 손상된 XML, 읽기 실패는 파일 존재 여부와 별도의 예외 상황으로 다룬다.

<br><br>

## :fireworks: 이번 경험에서 정한 기준
> Resources XML은 사용자 저장 파일의 첫 상태를 만드는 원본이고, Persistent XML은 각 디바이스의 실제 사용자 상태다. <br> 게임 공통 설정은 사용자 저장 데이터와 분리하고, 사용자 행동으로 달라지는 데이터만 Persistent XML에 저장한다.
