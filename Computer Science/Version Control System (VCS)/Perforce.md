# 목차
- [목차](#목차)
- [개요](#개요)
- [기본 용어](#기본-용어)
- [과거의 기억](#과거의-기억)
- [Pending \& Revert](#pending--revert)
- [Get Latest](#get-latest)

# 개요
- 자주 사용하는 퍼포스를 이해하고 실수 하지 않는다.

# 기본 용어
- Checkout
  - 이걸 걸어줘야 해당 문서를 변경할 수 있었던 것 같다.
  - 내가 무언가를 코드에서 변경하면 checkout을 눌러주어야 한다.
    - 근데 이걸 Rider에서 자동으로 해서 checkout이 걸리게 한다.
    - 바로 변경 되는 것 같지는 않고 refresh를 눌러줘야 하는 것 같다.
  
- Revert
  - ![20221215_092746](https://user-images.githubusercontent.com/55792986/207744237-b1fe63dd-27e5-462c-994d-a6e52545ef9a.png)

- 파일을 추가할 때
  - 자동으로 추가 할 것 인지 Rider에서 물어본다.
    - 승락하면 자동으로 Perforce에 Add가 된다.
    - 이것도 자동화의 영향인가?

- 파일의 내용 변경
  - 파일의 내용을 변경하면 알아서 퍼포스와 연결해서 관리해준다.
  - 90 bytes였던 파일에 코드를 추가하니까 255 bytes로 증가 함을 확인했다.

- 파일 지우기
  - ![20221215_093641](https://user-images.githubusercontent.com/55792986/207745106-22e2e0c5-1e52-4498-ae6f-c6de1404c7a3.png)
  - 파일을 삭제하기 위해서는 Mark For Delete를 신청해야 Submit에 적용된다.
  - 이것도 자동화가 되는가?

- 로컬에서 막 작업하고 Get Latest를 땡겨버리면 충돌이 일어날까?
  - 당연한건가?

- Workspace
  - 이거를 내 로컬과 연결하는 작업이 기억이 살짝 나지 않는다.
  - 당시에 workspace를 5개 이상 만들어도 보았다.

- Depot
  - 아마 서버 컴퓨터의 데이터이다.
  - depot (server) <-> Workspace (client-local)

# 과거의 기억
- 과거에는 CLI를 이용해서 퍼포스를 관리했었다.

# Pending & Revert
- 최근에 진행한 나의 변경사항 이다.
- ![image](https://user-images.githubusercontent.com/55792986/207748042-4d3a36b7-3856-498b-9819-f14dfeb30252.png)
- ![image](https://user-images.githubusercontent.com/55792986/207748154-38f40644-2995-4300-9c98-43d7c804bcb2.png)
  - pending에서 revert files를 진행하고 옵션을 체크하고 revert를 진행하면 변경사항이 모두 삭제되고 Rider에서 변경 사항이 갱신된다.
  - 그리고 파란색 default가 뜬다.
  
# Get Latest
- Get Latest를 받을 때 그 다음에 뭐 이상한 팝업이 뜬다.
  - continue 누르면 된다.
  
