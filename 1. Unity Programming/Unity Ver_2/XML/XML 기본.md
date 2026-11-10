## :fire: <br> 나는 유니티에서 XmlSerializer 클래스로 Xml을 다룬다. <br> XmlSerializer는 public 데이터만 직렬화한다.

| 멤버                            | XML 직렬화 |
| ----------------------------- | ------- |
| `public` 필드                   | O       |
| `public get/set` 프로퍼티         | O       |
| `private` 필드·프로퍼티             | X       |
| `[XmlIgnore]`가 붙은 `public` 멤버 | X       |

- Container도 동일한 기준을 적용한다.
- XmlSerializer가 지원하지 않는 Dictionary는 <br> private으로 관리하거나 [XmlIgnore]로 제외하고, XML 저장용 List를 별도로 둔다.
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