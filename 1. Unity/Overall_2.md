# 목차
- [목차](#목차)
- [개요](#개요)
- [Unity Inspector에서 private 데이터의 변경을 허용하는 방법](#unity-inspector에서-private-데이터의-변경을-허용하는-방법)
- [스크립트의 Serialized Field 실수](#스크립트의-serialized-field-실수)

# 개요
- 일단 강의를 들으면서 분류 하기 귀찮은 것 들을 이 곳에 모아둔다.
- 추후에 천천히 정리한다.

# Unity Inspector에서 private 데이터의 변경을 허용하는 방법
- ![image](https://user-images.githubusercontent.com/55792986/207480785-50ecb462-65df-4850-8472-53565580e3ed.png)
- ![image](https://user-images.githubusercontent.com/55792986/207481214-37a50665-bf38-4735-8f45-300e02bd4bbc.png)
- :link:[Reference](https://m.blog.naver.com/PostView.naver?isHttpsRedirect=true&blogId=pxkey&logNo=221307184650)

# 스크립트의 Serialized Field 실수
- 어떤 스크립트에서 serialized field를 만들면 해당 스크립트가 연결된 프리팹이나 게임오브젝트에서 Serialized Field의 값을 설정할 수 있다.
- 스크립트가 연결된 A 프리팹에서 Serialized Field 값을 변경하고 B 프리팹에서 왜 Serialized Field 값이 설정되지 않았는지 고민한다.
  - :star:반드시 원하는 프리팹에서 Serialized Field 값을 설정해야 한다.