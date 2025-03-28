- [text](https://learn.microsoft.com/ko-kr/dotnet/standard/garbage-collection/)
- 지울 것 : 내용이 어렵고 복잡하므로 메모 형식으로 일단 쓰고 계속 고치자.

## 용어
- Managed Heap (관리되는 힙)
  - GC가 자동으로 관리하는 힙 메모리 영역


## 핵심 개요
- Managed Heap -> GC가 필요
- GC 알고리즘 가볍게
- GC에게 부담을 주는 코딩이 무엇이고 그걸 하지 않는 방식을 유니티와 연관해서
- GC가 관리하지 못할 것으로 생각되는 부분을 내가 강제로 해제하는 구현
- GC.Collect() 하면 순간 멈춘 경험

<br><br>

## :fire: Managed Heap
- 메모리 해제 -> 내가 굳이 하지 않으면 GC 가 함
- 내가 명령할 것 = 객체 할당 및 생성자로 초기화
- NextObjPtr이 관리 힙에서 할당할 시에 0으로 채우는 것 
- Managed Heap이 무한대가 아니기 때문에 GC를 쓴다.

<br><br>

## :fire: 아마 GC.Collect()만이 개발자인 내가 담당할 부분일 것 이고 이걸 잘 쓰는 걸 하나 공부 해야 해.
- GC가 수행되는 순간
  - GC 알고리즘
  - System.GC.Collect() -> 핵심으로 볼 것 -> 이걸 
  - 운영체제의 메모리 부족 보고 -> 내가 제어 못하지 않는가?
  - 게임이 종료되어도 메모리에 남아있는 것이 있었던 기억이 난다.
  
  <br><br>

## :fire: unsafe 코드 블록 안에서는 C#의 안전한 메모리 관리 환경을 벗어나 <br> C++과 비슷하게 포인터를 사용하여 메모리 주소를 직접 다룰 수 있다. <br> :fire: fixed 키워드를 이용하면 GC에 의해 인스턴스가 이동되지 않도록 고정한다.
- > unsafe 컨텍스트에서 코드는 포인터를 사용하고, 메모리 블록을 할당 및 해제하고, 함수 포인터를 사용하여 메서드를 호출할 수 있습니다.
- Static Utill Class에서 valueType의 주소를 찾을 때 두 키워드를 사용했다. 


