## :fireworks: 아래는 CODEX 제작

<br><br>

## :fireworks: Resources 경로와 실제 저장 경로는 역할이 다르다.
- `Assets/Resources`는 게임에 포함할 기본 데이터를 두는 곳이다.
- `Resources.Load()`를 사용하려면 파일을 반드시 `Resources` 폴더 아래에 둬야 한다.
- `Assets/Resources/XML/MyCharacterData.xml`은 `"XML/MyCharacterData"`로 읽는다.
- `Resources.Load()`에서는 `Resources` 이전 경로와 파일 확장자를 적지 않는다.

<br><br>

## :fire: Resources의 데이터는 최초 실행용 원본이다.
- `Resources`에 있는 XML은 사용자의 변경 데이터를 계속 저장하는 파일이 아니다.
- 빌드에 포함된 기본 데이터를 최초 한 번 불러오기 위해 사용한다.
- 사용자 데이터가 아직 없다면 기본 XML을 읽어서 `Application.persistentDataPath`에 복사한다.
- 이후에는 복사된 파일을 읽고 갱신한다.

<br><br>

## :fire: Application.persistentDataPath는 플랫폼 공통 저장 진입점이다.
- Windows, Android, iOS의 실제 저장 위치는 서로 다르다.
- Unity가 플랫폼에 맞는 쓰기 가능한 경로를 `Application.persistentDataPath`로 반환해 준다.
- 모든 플랫폼의 실제 경로 문자열이 같은 것은 아니다.
- 같은 코드를 사용해도 Unity가 각 플랫폼의 올바른 저장 위치를 찾아준다는 의미다.

<br><br>

## :fire: 기본 데이터와 사용자 데이터는 파일이 두 개다.
- 기본 데이터
  - `Assets/Resources/XML/MyCharacterData.xml`
- 실제 사용자 저장 데이터
  - `Application.persistentDataPath/MyCharacterData.xml`
- 최초 실행
  - Persistent 경로에 파일이 없으면 Resources의 XML을 읽어서 복사한다.
- 이후 실행
  - Persistent 경로에 만들어진 XML을 읽는다.
- 데이터 변경
  - Persistent 경로에 있는 XML만 갱신한다.

<br><br>

## :fire: 경로 변수는 역할이 보이도록 이름을 짓는다.
- `RelativePath`만으로는 무엇을 기준으로 한 상대 경로인지 알기 어렵다.
- `ResourcesRelativePath`는 `Resources` 폴더 하위의 로드 경로라는 뜻이다.
- `AbsolutePath`보다 `PersistentFilePath`가 실제 사용자 저장 파일이라는 의미를 잘 전달한다.

## :fireworks: 아래는 과거 문서

<br><br>

## :fire: Resources.Load는 빌드 시점의 Resources 폴더 상태를 기준으로 <br> 해당 파일을 read-only 형태로 메모리에 로드한다. <br> :fire: 따라서 XML을 Serialize하여 디스크의 파일을 변경 하더라도 <br> 이미 메모리에 올라간 TextAsset에는 반영되지 않으며 <br> 게임을 종료하거나 다시 시작하지 않는 이상 변경 사항은 게임에 반영되지 않는다. <br> :fire: 그러므로 Serialize를 런타임에 반영하고 싶으면 <br> File 클래스를 사용해서 직접 디스크로 부터 읽는다. (느림)
> The Resources system is convenient to use, especially for rapid prototyping and small projects. But it does not scale well and overall use of this feature is discouraged. For this reason AssetBundles and the Addressables package are the recommended alternative.

> >The resources folder can be appropriate for small Assets that are required throughout the project’s lifetime, that <ins>**do not require updates**</ins>.

- File 클래스는 Application.dataPath와 combine을 이용해서 path를 절대경로로 넣는다. 이 때 /와 \는 OS가 알아서 처리해주니 걱정하지 않는다.

<br><br>

## :fireworks: Resources.Load 주의사항 <br> :fire: 절대 경로를 사용하지 않고, <br> 항상 슬래시(/)로 구성된 상대 경로를 이용한다. <br> :fire: 확장자를 붙이지 않는다.
> Note that the path is case insensitive and must not contain a file extension. All asset names and paths in Unity use forward slashes, so using backslashes in the path will not work.

#### [예시]
<details>
  <summary> :point_up_2: 누르면 코드가 나옵니다.  </summary>

~~~c#
public class XmlFileData
{
    public Type DataType;

    // note : Resources.Load<>
    public string RelativePath;

    // note : File Read I/O
    public string AbsolutePath;
}

private void InitializeXmlFileDataList()
{
    _xmlFileDataList.Add(new XmlFileData
    {
        DataType = typeof(MyCharacterData),
        RelativePath = "MyCharacterData",
        AbsolutePath = Application.persistentDataPath + "/MyCharacterData.xml"
    });
}
~~~
</details>

<br><br>