## :fire: Rider에서 사용하는 C#이 한국어라 가정하고, Unity에서 사용하는 언어(YAML)가 일본어라고 가정한다. <br> :fire: 둘이 대화를 하기 위해서는 공통된 언어(binary?)가 필요하고 그걸 영어라고 가정한다. <br> :fire: 영어로 변환하는 것이 Serialization(=직렬화)이다. 
- 영어가 반드시 binary로 1대1 비유가 되지는 않는다.
- [SerializeField] , [Serializable] = 통역하기 = Serializable 한 객체로 변경
> When you apply a Serializefield attribute, it means that you are making the current object 'readable' by the inspector. 

> When you apply a Serializable attribute, it means you are saying that Objects of this type can be 'readed' by the unity's inpector.

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