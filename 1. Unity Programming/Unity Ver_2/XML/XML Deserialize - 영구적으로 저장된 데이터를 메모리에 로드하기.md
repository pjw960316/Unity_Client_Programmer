## :fire: [Deserialize] <br> :fire: '영구 저장'된 XML 파일을 읽어 런타임 메모리의 C# 객체로 불러온다. <br> 서버 데이터를 읽어오는 느낌.

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

## :fireworks: XML을 C# Class로 변환시키는 4단계 과정 <br> (XmlDocument가 아닌 XmlSerializer를 사용하기로 했다.)

#### :one: [<ins>C# Class</ins>] (Xml에 대응하는 C#의 Class를 만들어 준다.) 

<br>

#### :two: [<ins>Load</ins>] (디스크에 있는 Xml을 런타임 메모리로 올리는 과정)
- Resources.Load()를 사용하고 있지만, Addressable을 이용하도록 한다.

<br>

#### :three: [<ins>Decode</ins>] (TextAsset인 XML의 Raw Byte[]를 String으로 변환하는 과정)
- ![alt text](../captures/20250805_1.png)

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
- ![alt text](../captures/20250805_3.png)
- ![alt text](../captures/20250805_2.png)
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

