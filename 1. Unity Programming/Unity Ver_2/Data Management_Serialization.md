## :fire: 데이터를 저장하고 전달하기 위해 xml로 Serialization을 한다.
> Serialization은 가독성과 전달성을 위해 데이터를 **구조화**하는 과정.
#### [C#의 인스턴스를 XML로 serialization을 시킨 예시]
~~~c#
public class PlayerData
{
    public int level;
    public int gold;
}
~~~
~~~xml
<?xml version="1.0" encoding="utf-16"?>
<PlayerData xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
            xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <level>3</level>
  <gold>100</gold>
</PlayerData>
~~~
- XML 타입으로 serialization을 하면 기획자도 데이터를 쉽게 읽을 수 있다.

<br><br>

## :fire: 클라이언트가 서버에 패킷으로 보낼 때에는 :star:byte[]로 Serialization을 해야 보낼 수 있다.
#### [packet에 보낼 때 이렇게 보낼 수 없음]
~~~c#
class LoginRequest
{
    public string username;
    public string password;
}
~~~
> MessagePack is a compact binary serialization format, resulting in smaller message sizes compared to JSON and XML.
  - Binary Serialization은 Json과 XML 보다 compact한 Serialization.
- 방식 코드?

<br><br>

## :fire: [SerializeField]는 Unity가 해당 필드를 “에디터에 노출하고 파일에 저장”할 수 있게 <br> Serialization 대상으로 지정하는 키워드.
- 기본 : private 필드를 inspector에 노출 시키기 위해 사용한다.
- 심화 : Unity Editor(+inspector)와 코드의 파일 저장 구조를 **잇는** 기능
  - YAML 변환?????
- prefab을 까보면 YAML로 되어 있다.
- 그래서 왜? 모르겠어...