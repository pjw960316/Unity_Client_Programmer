이전에는 SoundData를 ScriptableObject, 이걸 SoundManager가 필드로 들고 있었는데
이제는 MycharcaterData를 Datamanager를 통해 XML을 읽어서 초기화하고, MYCharacterManager에서 MyCharcaterData를 필드로?

## :fire: Unity에서는 xml은 TextAsset에 포함되고, TextAsset으로 관리한다.
> Represents a raw text or binary file asset.
> Text assets are a format for imported text files. When you drop a text file into your Project folder, Unity converts it to a Text Asset. The supported text formats are: <ins>.bytes / .xml / .json / .txt / .md / </ins> 
- 포맷이 더 있지만 써보지 않은 것 이라 생략한다.

<br><br>

## :fire: XML을 C# Class로 변환하는 과정 (XmlDocument가 아닌 XmlSerializer를 사용하기로 했다.)
#### :one: [Load] (디스크에 있는 Xml을 런타임 메모리로 올리는 과정)
- Resources.Load()를 사용하고 있지만, Addressable을 이용하도록 한다.

<br>

#### :two: [Decode] (TextAsset인 XML의 Raw Byte[]를 String으로 변환하는 과정)
- ![alt text](./captures/20250805_1.png)

#### [Decode 예시]

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
~~~

<br>

#### :three: [Deserialize] (Xml의 String을 C#의 Class에 대응하는 과정)
- load -> decode -> deserialize
- :one: load
- :two: decode
- :three: deserialize


- [ ]  XMLSerializer
    - 상속 구조 X
    - https://learn.microsoft.com/ko-kr/dotnet/standard/serialization/xml-and-soap-serialization
    
    XML serialization의 중심 클래스는 [XmlSerializer](https://learn.microsoft.com/ko-kr/dotnet/api/system.xml.serialization.xmlserializer) 클래스이며, 이 클래스에서 가장 중요한 메서드는 **Serialize** 및 **Deserialize** 메서드입니다
    
    - Class(객체)를 XML로 바꾸어 저장하는 것을 XML Serializer이라 한다.
    - XML을 Class로 복원하는 것을 XML Deserialize이라 부른다.
- [ ]  오류
    - XmlSerializer는 **중첩 클래스(nested class)** 를 역직렬화할 수 없습니다.
    - 또, `FruitList.Fruit`와 XML `<Fruit>`이 **일치하지 않습니다.**
- xml Attribute를 사용해서 xml 파일의 element와 나의 class 이름을 맞춘다.