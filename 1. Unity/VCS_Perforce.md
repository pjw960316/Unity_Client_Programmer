# 목차
- [목차](#목차)
- [개요](#개요)
- [P4](#p4)

# 개요
- 성능이 좋은 Version Control System인 Perforce를 공부해보자.

# P4
- ![image](https://user-images.githubusercontent.com/55792986/210712287-aeb62aa7-38d6-4c75-aad0-f09b61940c02.png)
    - 왼쪽은 현재 파일의 로컬 버전 / 오른쪽은 현재 파일의 서버 버전
    - 숫자가 다른 것은 아직 최신화가 되지 않음을 의미
- 개발자가 prefab을 건드는 일(스크립트 붙이는)도 존재하므로 prefab도 변경 사항이 될 수 있다.
- 자주 자주 최신을 받아주자.
- 변경 사항의 파일중에 ?가 붙은 거는 충돌을 의미한다.
  - resolve -> Auto resolve multiple files -> Automatic Resolve
    - 기본적으로 Automatic resolve를 한다.
    - 여기서 성공하면 완료
    - 여기서 실패하면 accept source(server에 올라간 걸로 해당 파일을 적용하겠음) 또는 accept target (local에 저장된 내 걸로 해당 파일을 적용하겠음)
      - 별 일 없으면 accept source를 한다.
    - Interactively resolve
      - 개발자가 수동으로 비교하면서 머지를 하는 것
      - 코드는 이게 가능하나 프리팹은 알아 보기 어려우므로 불가능.
- pending
  - pending을 구분할 수 있다.