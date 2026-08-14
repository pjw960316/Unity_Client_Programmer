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

## :fire: Deserialize는 XML 데이터를 읽어서 C# Data Class에 초기화하는 과정이다. <br> 일단은 게임 플레이에서 한 번 실행된다고 대충 이해한다.

> XML → C# Object
- 저장되어 있는 XML 데이터를 읽어서 C#에서 사용할 수 있는 객체로 변환한다.
- 현재 프로젝트에서는 게임 데이터를 처음 로드할 때 실행한다.
- `XmlSerializer.Deserialize()`의 반환값은 `object`이므로 실제 Data Class 타입으로 사용한다.

~~~c#
var serializer = new XmlSerializer(typeof(MyCharacterData));

using var reader = new StringReader(xmlText);

var myCharacterData =
    (MyCharacterData)serializer.Deserialize(reader);
~~~

- 현재 `XmlDataManager`에서는 XML 타입별로 Deserialize한 객체를 Dictionary에 보관한다.

~~~c#
_deserializedXmlDictionary.Add(
    xmlType,
    xmlSerializer.Deserialize(stringReader)
);
~~~

<br><br>

## :fire: Serialize는 C# Data Class를 XML 데이터로 저장하는 과정이다. <br> 일단은 게임 플레이에서 자주 실행된다고 대충 이해한다.

> C# Object → XML

- 런타임에서 C# Data Class의 인스턴스의 필드는 계속 변경된다. <br> 코드 레벨에서 데이터가 변경되면 Serialize를 통해 XML 파일에 데이터를 저장해야 한다.
  - 캐릭터의 재화, 루틴 기록처럼 게임 중 데이터가 변경된 뒤 저장할 때 실행한다.
  - 타입을 잘 맞추는 게 중요하다.
- Deserialize가 저장 데이터를 **불러오는 것**이라면, Serialize는 변경된 데이터를 **저장하는 것**이다.

~~~c#
public void SerializeXmlData<T>(T data)
{
    var xmlFileData = GetXmlFileData(typeof(T));
    var serializer = new XmlSerializer(typeof(T));

    using var writer = new StreamWriter(xmlFileData.PersistentFilePath);

    serializer.Serialize(writer, data);
}
~~~

예를 들어 `MyCharacterData`의 값이 런타임에서 변경되었다면,

~~~c#
myCharacterData.MonthlyRoutineSuccessMoney += 100;

XmlDataManager.Instance.SerializeXmlData(myCharacterData);
~~~

C# 객체의 현재 상태가 XML 파일에 다시 기록된다.


<br><br>

## :fireworks: XML을 C# Class로 변환시키는 4단계 과정 <br> (XmlDocument가 아닌 XmlSerializer를 사용하기로 했다.)

#### :one: [<ins>C# Class</ins>] (Xml에 대응하는 C#의 Class를 만들어 준다.) 

<br>

#### :two: [<ins>Load</ins>] (디스크에 있는 Xml을 런타임 메모리로 올리는 과정)
- Resources.Load()를 사용하고 있지만, Addressable을 이용하도록 한다.

<br>

#### :three: [<ins>Decode</ins>] (TextAsset인 XML의 Raw Byte[]를 String으로 변환하는 과정)
- ![alt text](./captures/20250805_1.png)

~~~c#
public string text
{
    get
    {
        byte[] bytes = this.bytes;
        return bytes.Length == 0 ? string.Empty : TextAsset.DecodeString(bytes);
    }
}
~~~
- TextAsset 클래스가 알아서 해준다.

<br>

#### :four: [<ins>Deserialize</ins>] (Xml의 String을 C#의 Class에 대응하는 과정)
- :link:[MSDN XmlSerializer](https://learn.microsoft.com/ko-kr/dotnet/standard/serialization/xml-and-soap-serialization)
- ![alt text](./captures/20250805_3.png)
- ![alt text](./captures/20250805_2.png)
  - 내부적으로 reflection을 사용하고 있다.
  - StringReader -> TextReader로 사용한다.

<br>

#### [전체 예시]

~~~xml
<?xml version="1.0" encoding="utf-8"?>
<MyCharacterData>
    <name>ParkJiWon</name>
    <routineOneSuccessTime>99999</routineOneSuccessTime>
</MyCharacterData>
~~~

~~~c#
//xml load
TextAsset textAsset = Resources.Load<TextAsset>(resourcePath);

//test
byte[] bytes = textAsset.bytes;
string text = textAsset.text;

// 1번 : raw bytes
Debug.Log(string.Join(", ", bytes));

// 2번 : hex
Debug.Log($"{bytes.ToHexString()}");

// 3번 : string
Debug.Log($"{text}");


/* 1번 결과
60, 63, 120, 109, 108, 32, 118, 101, 114, 115, 105, 111, 110, 61, 34, 49, 46, 48, 34, 32, 101, 110, 99, 111, 100, 105, 110, 103, 61, 34, 117, 116, 102, 45, 56, 34, 63, 62, 13, 10, 60, 77, 121, 67, 104, 97, 114, 97, 99, 116, 101, 114, 68, 97, 116, 97, 62, 13, 10, 32, 32, 32, 32, 60, 110, 97, 109, 101, 62, 80, 97, 114, 107, 74, 105, 87, 111, 110, 60, 47, 110, 97, 109, 101, 62, 13, 10, 32, 32, 32, 32, 60, 114, 111, 117, 116, 105, 110, 101, 79, 110, 101, 83, 117, 99, 99, 101, 115, 115, 84, 105, 109, 101, 62, 57, 57, 57, 57, 57, 60, 47, 114, 111, 117, 116, 105, 110, 101, 79, 110, 101, 83, 117, 99, 99, 101, 115, 115, 84, 105, 109, 101, 62, 13, 10, 60, 47, 77, 121, 67, 104, 97, 114, 97, 99, 116, 101, 114, 68, 97, 116, 97, 62

1번 결과에서 '60, 63, 120, 109, 108, 32, 118, 101, 114, 115, 105'
부분이 '<?xml versi' 이다.

2번 결과
3C3F786D6C2076657273696F6E3D22312E302220656E636F64696E673D227574662D38223F3E0D0A3C4D79436861726163746572446174613E0D0A202020203C6E616D653E5061726B4A69576F6E3C2F6E616D653E0D0A202020203C726F7574696E654F6E655375636365737354696D653E39393939393C2F726F7574696E654F6E655375636365737354696D653E0D0A3C2F4D79436861726163746572446174613E

3번 결과
<?xml version="1.0" encoding="utf-8"?>
<MyCharacterData>
    <name>ParkJiWon</name>
    <routineOneSuccessTime>99999</routineOneSuccessTime>
</MyCharacterData>
*/
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