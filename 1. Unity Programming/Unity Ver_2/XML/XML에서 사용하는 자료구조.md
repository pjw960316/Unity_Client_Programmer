## :sparkles: XML에서 사용하는 자료구조

#### :white_check_mark: 경험

- key를 기준으로 데이터를 관리하고 싶어서 Dictionary를 사용하고 싶다.
- 그런데 XmlSerializer로 저장하기 위해 List를 별도로 만들고 Dictionary와 동기화해야 하는 방식이 불만이다. 가독성이 떨어지고 메모리도 추가로 사용하는 것 같다고 느꼈다.
- 저장 형식에 맞추기 위해 별도의 자료구조와 코드를 만드는 것이 싫다. Dictionary를 쓰고 싶지만, 이를 유지하는 과정이 번거롭다면 꼭 고집하려는 것은 아니다.

<br>

#### :white_check_mark: 확인하고 싶은 질문

- XmlSerializer는 왜 Dictionary를 직접 지원하지 않는가?
- 대부분은 XmlSerializer로 처리하면서 Dictionary만 다른 API나 라이브러리로 저장·복원할 수 있는가? List를 별도로 유지하는 것보다 간단한가?
- Dictionary와 List를 함께 유지할 때 메모리와 동기화 부담은 실제로 어느 정도인가?
- List 하나를 사용한다면 원소를 class와 tuple 중 무엇으로 묶을 수 있고, 어떤 방식이 읽기 쉬운가?


## :sparkles: XML 저장에 사용할 자료구조 선택하기

#### :white_check_mark: 경험

- 물 보상을 받은 횟수를 연월별로 남기려고 했다. 202610과 3처럼 연월과 횟수를 묶고, Dictionary의 key를 연월, value를 횟수로 두면 된다고 생각했다.
- 월별 한 항목이면 10년치도 120개라서, 이번 달 값만 따로 보관하기보다 전체 기록에서 필요한 월을 조회하면 된다고 생각했다.
- 그런데 현재 사용하는 XmlSerializer는 Dictionary를 직접 지원하지 않았다. XML 저장용 List를 추가로 만들고 Dictionary와 맞춰야 한다니 번거롭고 낭비 같았으며, 코드도 예쁘지 않다고 느꼈다.
- 굳이 Dictionary가 필요하지 않다면 List 하나를 사용하는 게 좋아 보였다.
- List의 한 원소에 연월과 횟수를 함께 담으려니 class와 tuple 중 무엇을 사용할지, 둘 다 XML 저장을 지원하는지 궁금해졌다. .NET에서 왜 Dictionary를 직접 지원하지 않는지도 궁금했다.

<br>

#### :white_check_mark: Codex 보완 설명 — 자료구조와 직렬화 도구의 지원 범위를 구분한다

- XML은 데이터를 표현하는 형식이고, C# 자료구조를 XML로 변환하는 규칙은 사용하는 직렬화 도구가 정한다.
- Dictionary를 XML로 표현할 수 없는 것은 아니다. 현재 사용하는 XmlSerializer가 직접 지원하지 않는 것이다. DataContractSerializer는 Dictionary를 지원한다. :airplane: [Dictionary 컬렉션의 데이터 계약](https://learn.microsoft.com/en-us/dotnet/framework/wcf/feature-details/collection-types-in-data-contracts)
- 직렬화 도구를 바꾸면 기존 XML 형식과 Attribute 사용 방식의 호환성을 확인해야 한다. 자료구조 하나 때문에 바로 도구 전체를 교체할 필요가 있는지는 별도로 판단한다.

<br>

#### :white_check_mark: Codex 보완 설명 — List의 원소 타입도 지원 조건을 확인한다

| 원소를 묶는 방식 | XmlSerializer로 저장·복원 | 고려할 점 |
| --- | --- | --- |
| List에 직접 만든 기록 class를 담기 | public 타입, 매개변수 없는 생성자, 직렬화 가능한 public 멤버 등의 조건을 충족하면 가능 | YearMonth, RewardCount처럼 의미 있는 이름으로 저장할 수 있다. |
| `List<Tuple<string, int>>` | 기본 방식으로 불가 | System.Tuple은 매개변수 없는 생성자가 없고 Item1·Item2가 읽기 전용 프로퍼티다. |
| `List<(string YearMonth, int RewardCount)>` | 이 경우의 ValueTuple<string, int>는 로컬 .NET에서 저장·복원 확인 | XML에는 지정한 별칭 대신 Item1·Item2로 기록된다. |

- C#의 (string, int) 문법은 System.Tuple이 아니라 System.ValueTuple을 사용한다. ValueTuple의 원소는 public 필드다. 코드에서 붙인 YearMonth·RewardCount 같은 별칭이 런타임 필드 이름을 바꾸지는 않는다. :airplane: [C# 튜플 형식과 필드 이름](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-tuples)
- XmlSerializer는 클래스에 매개변수 없는 생성자를 요구하며, 기본적으로 public 필드와 읽기·쓰기가 가능한 public 프로퍼티를 처리한다. System.Tuple의 형태는 이 조건에 맞지 않는다. :airplane: [XML 직렬화 조건](https://learn.microsoft.com/en-us/dotnet/standard/serialization/introducing-xml-serialization), :airplane: [System.Tuple의 생성자와 프로퍼티](https://learn.microsoft.com/en-us/dotnet/api/system.tuple-2?view=net-10.0)
- 저장·복원 확인은 로컬 .NET 10.0.14에서 `List<ValueTuple<string, int>>`와 `List<Tuple<string, int>>`를 비교한 결과다. Unity 에디터와 Android 빌드에서 확인한 결과는 아니다.
- 보완 제안: 월별 보상처럼 이름이 있는 기록을 영구 저장한다면 작은 기록 class와 List 하나를 사용하는 방식부터 검토한다. tuple을 못 써서가 아니라, 코드와 XML에서 데이터의 의미를 읽기 쉽게 하려는 선택이다.

<br>

#### :white_check_mark: Codex 보완 설명 — 왜 Dictionary가 바로 처리되지 않는가

- 현재 XmlSerializer 구현은 IDictionary를 구현한 타입을 발견하면 명시적으로 NotSupportedException을 발생시킨다. public으로 바꾸거나 XmlElement를 붙이는 것만으로 이 제약이 없어지지는 않는다.
- 일반 컬렉션은 원소를 읽고 Add(원소)로 복원할 수 있는지 등의 규칙으로 처리한다. Dictionary의 키·값 쌍을 저장하고 복원하려면 그 형태에 맞는 처리가 필요하며, 현재 XmlSerializer는 이를 기본 지원하지 않는다. :airplane: [.NET XmlSerializer의 컬렉션 처리와 IDictionary 제외 코드](https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.Xml/src/System/Xml/Serialization/Types.cs)
- 위 내용은 현재 구현에서 확인한 제약이다. 최초 설계 때 왜 Dictionary 지원을 제외했는지에 대한 역사적 이유는 확인하지 못했다. XML로 표현할 수 없어서라거나 성능이 나빠서라고 단정하지 않는다.

<br>

#### :white_check_mark: Codex 보완 제안 — Dictionary가 필요한지부터 판단한다

- 이번처럼 기록 수가 적고 가끔 특정 월을 찾는 정도라면, List에서 YearMonth가 같은 항목을 조회하는 방법부터 검토한다. 같은 연월을 중복 추가하지 않도록 처리한다.
- List 하나를 런타임과 XML 저장의 원본으로 사용하면 Dictionary와 List의 동기화가 필요 없다.
- 기록이 많거나 키 조회를 자주 해야 해서 Dictionary의 이점이 필요해지면, 그때 XML 저장용 표현과의 변환 비용을 함께 검토한다.
- List의 기록 class는 한 원소에 연월과 횟수를 묶는 역할이다. Dictionary와 함께 쓰는 변환용인지, List 자체를 원본으로 쓰는 기록 타입인지는 전체 구조에 따라 달라진다.