# 목차
- [목차](#목차)
- [개요](#개요)
- [Unity Inspector에서 private 데이터의 변경을 허용하는 방법](#unity-inspector에서-private-데이터의-변경을-허용하는-방법)
- [스크립트의 Serialized Field 실수](#스크립트의-serialized-field-실수)
- [게임 오브젝트와 프리팹](#게임-오브젝트와-프리팹)
- [Widget](#widget)
- [좋은 메서드](#좋은-메서드)

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

# 게임 오브젝트와 프리팹
- 게임을 실행하면 생기는 프리팹을 기반으로 생성되는 게임 오브젝트에 대해서 어려움을 겪었다. 게임이 진행 중이면 당연히 게임 오브젝트 들을 변경해도 게임을 끄면 저장되지 않는다.
  - 물론 무언가 있긴 한데 공식적인 방식은 아닌 것 같다. 
- :star:**프리팹이 많은 프리팹으로 이루어져 있을 수 있고, 프리팹은 엄청 많은 단계의 하위 객체들로 구성되어 있을 수 있다. 
  - 여기서 큰 오류를 범했었다.
  - **어떤 프리팹_A와 프리팹_B가 있다. 두 프리팹은 같은 하위 프리팹을 자식 객체로 갖고 있다. A에 연결된 하위 프리팹을 변경하고 B에서 해당 변경사항이 작동이 안 되고 있어서 당황을 했었다. 당연히 B에 연결된 하위 프리팹을 변경해야 한다.**

# Widget
- module 대신 widget이라는 언어를 이용한다.
- widget에 재사용이 가능한 메서드를 추가한다.

# 좋은 메서드
- 가독성이 좋으면
- 기능 별로 분리해서 메서드를 만들면 좋긴한데 자유임.
  - 잘 분리하면 가독성 좋음

# 부동 소수점
- ![image](https://user-images.githubusercontent.com/55792986/210028509-7347fdec-c85d-4319-91b8-5dda0b2b3d32.png)
- ![image](https://user-images.githubusercontent.com/55792986/210028723-e43b745a-42d6-4794-bce2-3d8346b249e3.png)
-  8bytes -> 4bytes 손실

# 위험한 float 비교
- ![image](https://user-images.githubusercontent.com/55792986/210466704-4d6ad080-365c-4d65-ba83-325c77d2770d.png)
  - 1인 줄 알았는데 0.99999999999다.
- ~~~c#
  unity mathf.approximately
  ~~~
  - ![image](https://user-images.githubusercontent.com/55792986/210472546-0e5480b3-bd88-4f38-8f2c-c78d23be8789.png)
  - ![image](https://user-images.githubusercontent.com/55792986/210472786-684ab4ff-8e5a-4b81-9c0b-bcaed265ecaa.png)
    - mathf.approximately가 입실론에 근거한 메서드이다.
  - ![image](https://user-images.githubusercontent.com/55792986/210472889-edef6194-b55d-4754-8087-5984f83ba2f7.png)-