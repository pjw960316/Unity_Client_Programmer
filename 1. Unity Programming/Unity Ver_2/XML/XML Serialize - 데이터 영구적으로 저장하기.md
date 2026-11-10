## :fire: [Serialize] <br> :fire: 런타임에 변경된 C# 객체의 데이터를 XML 파일에 영구 저장한다. <br> 서버에 데이터 업데이트를 요청하는 느낌.

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

