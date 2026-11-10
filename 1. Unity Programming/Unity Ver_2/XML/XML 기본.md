## :fire: <br> 나는 유니티에서 XmlSerializer 클래스로 Xml을 다룬다. <br> XmlSerializer는 public 데이터만 직렬화한다.

| 멤버                            | XML 직렬화 |
| ----------------------------- | ------- |
| `public` 필드                   | O       |
| `public get/set` 프로퍼티         | O       |
| `private` 필드·프로퍼티             | X       |
| `[XmlIgnore]`가 붙은 `public` 멤버 | X       |

- Container도 동일한 기준을 적용한다.
- Dictionary를 런타임 조회용으로 함께 사용한다면 <br> private으로 관리하거나 [XmlIgnore]로 제외하고, XML 저장용 List를 별도로 둔다. Dictionary가 필요하지 않다면 List 하나를 원본으로 사용할 수도 있다.
- [XmlIgnore]는 에러를 무시하는 Attribute가 아니라, 해당 public 멤버를 **XML 저장 대상에서 제외**한다.

~~~c#
public List<RoutineRecordData> RoutineRecordList = new();

private Dictionary<string, List<bool>> _routineRecordDictionary = new();

[XmlIgnore]
public ImmutableSortedDictionary<string, ImmutableList<bool>> RoutineRecordDictionary
{
    get { /* Dictionary를 읽기 전용으로 제공 */ }
}
~~~

<br><br>

## :fireworks: XML의 기본 특징에 대해 공부한다.

#### :one: Xml은 대소문자에 민감하다. 

<br>

#### :two: XML은 get과 set이 public인 property와 함께한다. 
> XmlSerializer only looks at public fields and properties.

<br>

#### :three: XML의 Root Element가 C#의 [XmlRoot] attribute고, XML의 Child Element가 C#의 [XmlElement] attribute다.

~~~XML
<MyCharacterData>
<Name>지원</Name>
<Age>30</Age>
</MyCharacterData>
~~~
- MyCharacterData = Root Element
- Name & Age = Child Element 
- :link:[formatting XML](https://dontpaniclabs.com/blog/post/2025/05/06/formatting-xml-when-serializing-c-objects/)

<br>

#### :four: Unity에서 xml은 TextAsset에 포함되고, TextAsset으로 관리된다.
> Represents a raw text or binary file asset.

> Text assets are a format for imported text files. When you drop a text file into your Project folder, Unity converts it to a Text Asset. The supported text formats are: <ins>.bytes / .xml / .json / .txt / .md / </ins> 
- 더 많은 format이 존재하지만 생략했다.

<br><br>

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
