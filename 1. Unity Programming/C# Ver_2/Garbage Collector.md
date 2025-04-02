## MSDN Link
- [LINK](https://learn.microsoft.com/ko-kr/dotnet/standard/garbage-collection/)


<br><br>

## 핵심 개요 [흐름 파악]
- 1. 왜 GC가 필요한지를 Managed Heap에서 메모리를 관리하는 방식을 통해 이해한다.
- 2. GC 알고리즘을 가볍게 이해한다.
- GC에게 부담을 주는 코딩이 무엇이고 그걸 하지 않는 방식을 유니티와 연관해서
- GC가 관리하지 못할 것으로 생각되는 부분을 내가 강제로 해제하는 구현
- GC.Collect() 하면 순간 멈춘 경험

<br><br>

## :fire: Managed Heap을 CLR이 관리하는 기법을 알면 GC의 필요성을 이해할 수 있다. <br> :fire: 아래의 용어들을 숙지해야 한다.
- Reference Counting
- 순환 참조
- Root
- Mark
- nextobjPtr


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


