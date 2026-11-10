## :fire: [Serialize] <br> :fire: 런타임에 변경된 C# 객체의 데이터를 XML 파일에 영구 저장한다. <br> 서버에 데이터 업데이트를 요청하는 느낌.

#### :white_check_mark: 경험

- 회사에서 UpdateData()라는 메서드로 XML 전체를 갱신했던 기억이 있다. 파일 I/O라면 필요한 데이터만 갱신해야 하는 것은 아닌지 예전에도 고민했다.
- 이번에 물 섭취 보상 기록을 영구 저장하려고 하니, 런타임에서 변경된 데이터를 언제 저장해야 할지 다시 고민하게 됐다.
- 변경할 때마다 저장하면 게임 성능이 떨어지지 않을까? 반대로 저장을 미루다가 게임이 갑자기 종료되면 변경한 내용이 날아가지 않을까?
- XML 파일의 일부만 업데이트할 수 있을까? 일부든 전체든 파일 I/O에 접근하므로 시간이 비슷한 것일까?
- 단순히 Serialize API를 사용하는 방법보다, 저장 시점과 범위를 판단할 기준이 필요하다고 느꼈다.

<br>

#### :white_check_mark: 현재 프로젝트에서 데이터를 저장하는 흐름

- 런타임에서 C# Data Class의 인스턴스의 필드는 계속 변경된다. 
- 코드 레벨에서 데이터가 변경되면 Serialize를 통해 XML 파일에 데이터를 저장해야 한다.
  - 캐릭터의 재화, 루틴 기록처럼 게임 중 데이터가 변경된 뒤 저장할 때 실행한다.
  - 타입을 잘 맞추는 게 중요하다.
- Deserialize가 저장 데이터를 **불러오는 것**이라면, Serialize는 변경된 데이터를 **저장하는 것**이다.

<br>

#### :white_check_mark: 예시
~~~c#
public void SerializeXmlData<T>(T data)
{
    var xmlFileData = GetXmlFileData(typeof(T));
    var serializer = new XmlSerializer(typeof(T));

    using var writer = new StreamWriter(xmlFileData.PersistentFilePath);

    serializer.Serialize(writer, data);
}
~~~

~~~c#
myCharacterData.MonthlyRoutineSuccessMoney += 100;

XmlDataManager.Instance.SerializeXmlData(myCharacterData);
~~~

- 예를 들어 `MyCharacterData`의 값이 런타임에서 변경되었다면 <br> 객체의 현재 상태가 XML 파일에 다시 기록된다.

<br><br>

## :sparkles: 저장 시점과 범위, 데이터 유실을 판단하기

아래는 Codex의 보완 제안이다. 저장 주기와 파일 분리 방식은 아직 HabitGame의 정책으로 확정하지 않았다.

#### :white_check_mark: 저장 시점은 성능 비용과 허용할 유실량을 함께 판단한다

- 자주 저장한다고 반드시 체감 성능이 떨어지는 것은 아니다. 직렬화할 데이터의 양, 파일 크기, 호출 빈도, 실제 기기에서 저장에 걸리는 시간을 함께 확인한다.
- 저장을 미루면 마지막으로 정상 저장한 이후의 변경분을 잃을 수 있다. 먼저 어떤 변경을 얼마나 잃어도 되는지 정하고 저장 시점을 선택한다.

| 상황 | 검토할 저장 시점 |
| --- | --- |
| 잃으면 안 되는 보상·진행 결과가 확정됨 | 해당 작업이 끝났을 때 저장 |
| 작은 변경이 짧은 시간에 반복됨 | 여러 변경을 모아 저장하거나 주기적으로 저장 |
| 앱이 백그라운드로 전환됨 | 변경분을 저장하는 보조 시점으로 활용 |

- 실제 변경이 있을 때만 저장한다. 현재 물 보상 처리도 Handler가 false를 반환하면 Manager가 XML 저장을 건너뛴다.
- 종료 시점에만 저장하는 방식에 의존하지 않는다. 모바일에서는 운영체제가 앱을 종료하면서 종료 콜백을 호출하지 못할 수 있다. 백그라운드 전환 시 저장도 갑작스러운 종료에 대한 완전한 보장은 아니다. :airplane: [Unity OnApplicationQuit](https://docs.unity3d.com/6000.0/Documentation/ScriptReference/MonoBehaviour.OnApplicationQuit.html)

<br>

#### :white_check_mark: XML 값의 부분 수정과 파일의 부분 쓰기는 구분한다

- 메모리에서 물 보상 날짜 하나만 바꾸는 것과, 파일에서 그 날짜에 해당하는 바이트만 쓰는 것은 다른 작업이다.
- 현재 SerializeXmlData()는 전달받은 객체의 XML 저장 대상 전체를 직렬화하고 해당 파일을 덮어쓴다. MyCharacterData를 전달하면 물 기록만 바뀌어도 같은 파일의 다른 저장 대상 데이터까지 다시 기록한다. :airplane: [StreamWriter의 덮어쓰기 동작](https://learn.microsoft.com/en-us/dotnet/api/system.io.streamwriter.-ctor?view=net-10.0#system-io-streamwriter-ctor(system-string))
- XML 문서의 특정 노드만 수정해도 일반적인 문서 저장에서는 문서 전체를 다시 기록한다. :airplane: [XDocument.Save](https://learn.microsoft.com/en-us/dotnet/api/system.xml.linq.xdocument.save?view=net-10.0)
- 파일의 특정 위치에 일부 바이트만 쓰는 방식도 가능하다. 다만 XML 값이 500에서 1000으로 바뀌면 길이가 늘어나므로 뒤쪽 내용까지 처리해야 한다. 일반적인 XML 직렬화가 변경된 필드의 바이트 위치를 찾아 부분 갱신해 주는 것은 아니다. :airplane: [파일 위치 지정](https://learn.microsoft.com/en-us/dotnet/api/system.io.filestream.seek?view=net-10.0)
- 둘 다 I/O라는 이유로 시간이 같아지지는 않는다. 파일 열기·닫기 비용 외에 직렬화 작업량과 읽고 쓰는 양도 영향을 준다. 작은 파일에서는 차이가 작을 수 있고, 큰 파일에서는 전체 재작성 비용이 커질 수 있으므로 실제 기기에서 측정한다.
- 파일이 커지고 기능마다 저장 빈도가 달라지면 기능별·기간별 파일 분리를 검토할 수 있다. Data 클래스를 나누는 것만으로 저장 파일까지 자동으로 나뉘지는 않는다.

<br>

#### :white_check_mark: 저장 전 종료와 저장 도중 종료는 다른 문제다

| 종료된 시점 | 발생할 수 있는 문제 |
| --- | --- |
| 변경분을 아직 파일에 저장하지 않음 | 기존 저장 파일은 남아 있지만 마지막 저장 이후의 변경분을 잃음 |
| 기존 파일을 덮어쓰던 중 종료됨 | 파일이 일부만 기록되어 기존 저장 데이터까지 읽지 못할 수 있음 |

- 현재 코드는 기존 파일에 직접 덮어쓰므로, 저장 시점을 자주 잡는 것과 별개로 저장 도중 중단되는 상황도 고려해야 한다.
- 보완 방향으로 임시 파일에 새 내용을 완성한 뒤 기존 파일을 교체하고, 이전 정상 파일을 백업으로 유지하는 방식을 검토할 수 있다.

~~~text
변경된 상태를 임시 파일에 기록
→ 기록 완료 확인
→ 기존 저장 파일 교체 및 이전 정상본 보관
→ 다음 로드에서 저장 파일이 손상됐다면 백업 복구 검토
~~~

- 임시 파일과 백업만으로 모든 종료·전원 손실에서 무손실이 보장되는 것은 아니다. 대상 플랫폼에서 파일 교체와 버퍼의 디스크 반영, 실패 시 복구 동작을 확인해야 한다. :airplane: [파일 교체와 백업](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.replace?view=net-10.0), :airplane: [파일 버퍼 반영](https://learn.microsoft.com/en-us/dotnet/api/system.io.filestream.flush?view=net-10.0)

<br>

#### :white_check_mark: 함께 맞아야 하는 데이터는 저장 단위도 함께 검토한다

- 물 보상 기록을 추가할 때는 실제 지급 기록·금액과 중복 처리 판단용 날짜가 함께 맞아야 한다. 날짜만 저장되고 보상 기록이 저장되지 않으면, 다음 실행에서 지급된 것으로 판단하면서 실제 기록은 없는 상태가 될 수 있다.
- 현재 코드는 지급 날짜를 저장하고 UI에 500을 표시하는 단계다. 누적 지급 기록을 추가할 때 무엇을 하나의 완료된 작업으로 저장할지 정해야 한다.
- 파일을 분리하면 일부 파일만 저장되는 상황도 생길 수 있다. 성능을 위해 범위를 나누기 전에 함께 유지되어야 하는 상태인지 먼저 확인한다.
