## :sparkles: XML에서 사용하는 자료구조

#### :white_check_mark: 경험

- key를 기준으로 데이터를 관리하고 싶어서 Dictionary를 사용하고 싶다.
- 그런데 XmlSerializer로 저장하기 위해 List를 별도로 만들고 Dictionary와 동기화해야 하는 방식이 불만이다. 가독성이 떨어지고 메모리도 추가로 사용하는 것 같다고 느꼈다.
- 저장 형식에 맞추기 위해 별도의 자료구조와 코드를 만드는 것이 싫다. Dictionary를 쓰고 싶지만, 이를 유지하는 과정이 번거롭다면 꼭 고집하려는 것은 아니다.

<br>

#### :white_check_mark: 확인하고 싶은 질문

- XmlSerializer는 왜 Dictionary를 직접 지원하지 않는가?
- 대부분은 XmlSerializer로 처리하면서 Dictionary만 다른 API나 라이브러리로 저장·복원할 수 있는가? List를 별도로 유지하는 것보다 간단한가?
- Dictionary와 List를 함께 유지할 때 메모리와 동기화 부담은 실제로 어느 정도인가?
- List 하나를 사용한다면 원소를 class와 tuple 중 무엇으로 묶을 수 있고, 어떤 방식이 읽기 쉬운가?
