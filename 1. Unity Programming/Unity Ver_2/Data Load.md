## :fire: Resources.Load는 빌드 시점의 Resources 폴더 상태를 기준으로 <br> 해당 파일을 read-only 형태로 메모리에 로드한다. <br> :fire: 따라서 XML을 Serialize하여 디스크의 파일을 변경하더라도 <br> 이미 메모리에 올라간 TextAsset에는 반영되지 않으며 <br> 게임을 종료하거나 다시 시작하지 않는 이상 변경 사항은 게임에 반영되지 않는다. <br> :fire: 그러므로 Serialize를 런타임에 반영하고 싶으면 <br> File 클래스를 사용해서 직접 디스크로 부터 읽는다. (느림)
> The Resources system is convenient to use, especially for rapid prototyping and small projects. But it does not scale well and overall use of this feature is discouraged. For this reason AssetBundles and the Addressables package are the recommended alternative.

> >The resources folder can be appropriate for small Assets that are required throughout the project’s lifetime, that <ins>**do not require updates**</ins>.

- File 클래스는 Application.dataPath와 combine을 이용해서 path를 절대경로로 넣는다. 이 때 /와 \는 OS가 알아서 처리해주니 걱정하지 않는다.

<br><br>

## :fire: Resources.Load는 항상 슬래시(/)로 구성된 상대 경로를 이용한다. <br> :fire: 확장자를 붙이지 않는다. <br> :fire: 절대 경로(full-path)를 사용하지 않는다. 
> Note that the path is case insensitive and must not contain a file extension. All asset names and paths in Unity use forward slashes, so using backslashes in the path will not work.

#### [제목]
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

## :question: Addressable (아직 내용 X)

> I think addressables was meant to replace resources since it can manage memory better, pull from CCD, etc. I personally have never used resources so it’s easy for me to ignore, which is what i suggest you do in this case, is not to use resources anymore.

> Asset
> An asset is any item that you use in your Unity project to create your application, such as textures, 3D models, or sound files. Assets can include:
> **Visual elements**: 3D models, textures, or **sprites**.
> **Audio elements**: Sound effects or music.
> **Abstract items**: Color gradients, animation masks, arbitrary text, or numeric data. 
