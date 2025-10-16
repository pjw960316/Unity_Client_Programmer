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

## :fire::one: 음원 로드 방식_1 : Decompress On Load + PreLoad Audio Data ON
- 게임 Scene 열리기 전에 로드 하는 것 같다. (로드 씬 보다 먼저 동작해서 검은 화면이 이어진 현상 발견)
- 초기에 메모리에 로드 하니까 빠르지만 코드 영역을 넘어선 Native 영역이라 컨트롤이 불가능
> Decompress audio files as soon as they’re loaded. Use this option for smaller compressed sounds to avoid the performance overhead of decompressing during gameplay. Be aware that decompressing Vorbis-encoded sounds on load will use about ten times more memory than keeping them compressed (for ADPCM encoding it’s about 3.5 times), so don’t use this option for large files.

<br><br>

## :fire::two: 음원 로드 방식_2 : Compressed In Memory + PreLoad Audio Data OFF
- 큰 파일에서 권장한다.
- Unity는 ScriptableObject에 저장된 음원을 메모리에 로드 시키지 않는다.
> Keep audio compressed in memory and decompress while playing. This option has a slight performance overhead, especially for Ogg/Vorbis compressed files. Use it only for files that consume excess memory on Decompress on Load. The decompression happens on the mixer thread, which you can monitor in the DSP CPU section in the Audio module of the Profiler window.

#### [문제점 : ScriptableObject에서는 로드 되지 않는다. 그러므로 게임 음원을 재생시키는 그 순간 로드된다. 이 때 엄청난 렉이 발생한다. (5~10초 프리즈)]
~~~c#
// AlarmData : ScriptableObject
// 이건 메모리에 올라오지 않고, LoadAudioData()를 호출해야 올라온다. -> 렉 유발.
// 실제로 음원이 크니까 게임에서 음악 재생시에 5초 정도 렉이 발생했었다.
[SerializeField] private SerializedDictionary<EAlarmButtonType, AudioClip> _sleepingAudioClipDictionary = new();

// AudioClip.cs
/// <summary>
///   <para>Loads the asset data of an AudioClip into memory, so it will immediately be ready to play.</para>
/// </summary>
/// <returns>
///   <para>Returns true if the clip is loaded into memory.</para>
/// </returns>
public bool LoadAudioData()
{
    IntPtr _unity_self = Object.MarshalledUnityObject.MarshalNotNull<AudioClip>(this);
    if (_unity_self == IntPtr.Zero)
    ThrowHelper.ThrowNullReferenceException((object) this);
    return AudioClip.LoadAudioData_Injected(_unity_self);
}
~~~

#### [해결 방식 : Path만 추출해서 Resources.LoadASync로 비동기 로드 후 -> 연결]
~~~c#

// 비동기로 로드한다.
private async UniTaskVoid PreLoadAudioDataAsync()
{
    var alarmData = _modelList.OfType<AlarmData>().FirstOrDefault();
    
    if (alarmData == null)
    {
        throw new NullReferenceException("alarmData is null");
    }

    alarmData.Initialize();
    var sleepingAudioClipPathDictionary = alarmData.SleepingAudioClipPathDictionary;

    foreach (var element in sleepingAudioClipPathDictionary)
    {
        var key = element.Key;
        var relativePath = element.Value;

        var loadData = await Resources.LoadAsync<AudioClip>(relativePath);
        var memoryLoadedAudioClip = loadData as AudioClip;

        alarmData.SetSleepingAudioClipDictionary(key, memoryLoadedAudioClip);

        //log
        Debug.Log($"{relativePath}의 음원 파일 {memoryLoadedAudioClip?.name}이 비동기로 로드 되었습니다");
    }
}

// 기존의 _sleepingAudioClipDictionary의 value는 load가 되지 않았으나, 직접 비동기로 로드 후에 넣어준다.
// 렉을 해결했다.
public void SetSleepingAudioClipDictionary(EAlarmButtonType eAlarmButtonType, AudioClip memoryLoadedAudioClip)
{
    _sleepingAudioClipDictionary[eAlarmButtonType] = memoryLoadedAudioClip;
}
~~~

<br><br>

## :question: Addressable (아직 내용 X)

> I think addressables was meant to replace resources since it can manage memory better, pull from CCD, etc. I personally have never used resources so it’s easy for me to ignore, which is what i suggest you do in this case, is not to use resources anymore.

> Asset
> An asset is any item that you use in your Unity project to create your application, such as textures, 3D models, or sound files. Assets can include:
> **Visual elements**: 3D models, textures, or **sprites**.
> **Audio elements**: Sound effects or music.
> **Abstract items**: Color gradients, animation masks, arbitrary text, or numeric data. 
