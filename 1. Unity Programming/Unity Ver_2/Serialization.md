## :fire: Rider에서 사용하는 C#은 한국어에 비유할 수 있다. <br> :fire: Unity 내부에서 사용하는 YAML은 일본어에 비유할 수 있다. <br> :fire: 두 언어가 서로 대화하려면 <br> 서로가 이해할 수 있는 공통 언어(=binary)가 필요하다. <br> :fire: 공통 언어로 번역하는 과정을 Serialization(직렬화) 이라고 한다.
- [SerializeField] , [Serializable] = 통역하기 = Serializable 한 객체로 변경
> When you apply a Serializefield attribute, it means that you are making the current object 'readable' by the inspector. 

> When you apply a Serializable attribute, it means you are saying that Objects of this type can be 'readed' by the unity's inpector.

> Interfaces are currently not serializable by Unity and therefore don’t show in the inspector.

<br><br>

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