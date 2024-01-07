# 목차
- [목차](#목차)
- [개요](#개요)
- [자식 객체를 연결할 때 더 이상 Getchild(), Getchildren()을 사용하지 말고 SerializeField로 직접 넣어주자.](#자식-객체를-연결할-때-더-이상-getchild-getchildren을-사용하지-말고-serializefield로-직접-넣어주자)
- [GetComponent로 부모 스크립트만 가져오기](#getcomponent로-부모-스크립트만-가져오기)
- [스크립트의 본질을 잊지마라. 어딘 가에는 붙어있을 것 이다.](#스크립트의-본질을-잊지마라-어딘-가에는-붙어있을-것-이다)
- [부모가 SetActive(false)면 자식도 SetActive(false)가 되나?](#부모가-setactivefalse면-자식도-setactivefalse가-되나)
- [유니티에서 변경 되지 않는 이미지는 연결할 필요가 없다.](#유니티에서-변경-되지-않는-이미지는-연결할-필요가-없다)

# 개요
- 게임 오브젝트와 프리팹과 관련된 대다수의 내용을 이 곳에 정리한다.

 

# 자식 객체를 연결할 때 더 이상 Getchild(), Getchildren()을 사용하지 말고 SerializeField로 직접 넣어주자.
- 이전에는 게임 오브젝트에 붙어있는 자식 게임 오브젝트를 찾을 때 GetChildren()을 사용했었다. 일반적인 컴포넌트를 찾아주려면 넣어줘야 하지만 자식은 Unity의 함수로 찾을 수 있기 때문이다.
  - 하지만 필연적으로 인덱스나 이름을 parameter로 넣어줘야 하며 이는 하드코딩에 가깝다. 또한 성능도 좋지 않다.
  - ![image](https://user-images.githubusercontent.com/55792986/213980039-c7bfcc31-cc0e-4548-ab86-7bd3cb73f5db.png)
    - 혼자 허접한 게임을 만들 때는 자식의 개수가 적었지만, 큰 구조라면 자식도 많아지고 자주 호출된다. 
- 게임 오브젝트에 자식 게임 오브젝트의 스크립트를 Serialize Field로 만들어서 추가해 버리면 매우 안전하고 이 방식이 현재는 정답이라고 생각한다.
- **주의 : Serialize Field로 선언한 멤버의 이름을 바꾸면 null exception이 날 것 이다. 다시 연결해줘야 한다.**
  - 하지만 해당 이름으로 유지 시켜주는 FormerlySerializedAsAttribute 라는 것도 있다.
  - ![image](https://user-images.githubusercontent.com/55792986/213980653-8c91f3e3-8aa1-487d-9577-78295b920674.png)
  - https://docs.unity3d.com/ScriptReference/Serialization.FormerlySerializedAsAttribute.html

# GetComponent로 부모 스크립트만 가져오기
- ![image](https://user-images.githubusercontent.com/55792986/212243908-0a881976-ef90-41ed-80c6-675810b25f3a.png)
  - 상속 관계에서의 GetComponent

# 스크립트의 본질을 잊지마라. 어딘 가에는 붙어있을 것 이다.
- 스크립트에 적은 것은 결국 어떤 객체의 컴포넌트로 쓰기 위함이다. 
- 프리팹으로 만든 복잡한 게임오브젝트 내부를 파고들어 하위의 하위의 하위의 계층을 파고들면 어딘가에는 연결되어 있을 것 이다!
- 물론 아닌 것도 있지만 그런 애들은 정말 특별한 목적의 스크립트다. 

# 부모가 SetActive(false)면 자식도 SetActive(false)가 되나?
- 일단 부모에서 SetActive(false)를 하면 게임에서 자식도 모두 꺼진다.
- 그러나 inspector에서는 여전히 살아있는 걸로 나온다.
- ![20230125_200147](https://user-images.githubusercontent.com/55792986/214546848-79705ba6-7f09-4cd5-8596-b8d239de0606.png)
  - 꺼지는 게 맞다.
  - 하지만 Script는 꺼지지 않는다. 스크립트를 끄고 키는 것은 enable로 담당한다.
- 여러 테스트를 해봤지만 정확한 답은 사실 찾지 못했다. 개발을 하다가 완벽한 예시를 찾으면 다시 테스트 해본다.

# 유니티에서 변경 되지 않는 이미지는 연결할 필요가 없다.
- 조금만 생각해보면 너무 당연한 이야기다.
- 어떤 이미지에 대해 변경사항이 존재한다면 게임 오브젝트에 해당 이미지를 스크립트에 연결해야 하지만 변경 사항이 없다면 그럴 필요 없이 게임 오브젝트에만 연결해주면 된다. 

