# 목차
- [목차](#목차)
- [개요](#개요)
- [Stack의 의문점](#stack의-의문점)
- [scope와 stack의 성질 (내 생각)](#scope와-stack의-성질-내-생각)
- [:star:메서드 내부의 지역변수와 객체의 멤버변수의 메모리 할당 위치 같은 건 중요하지 않아 -> Reference Type의 원리만 알면 된다.](#star메서드-내부의-지역변수와-객체의-멤버변수의-메모리-할당-위치-같은-건-중요하지-않아---reference-type의-원리만-알면-된다)
- [C# Stack Memory vs Heap Memory](#c-stack-memory-vs-heap-memory)
- [Boxing & Unboxing](#boxing--unboxing)
- [Garbage Collection](#garbage-collection)

# 개요
- 면접 단골 질문일 만큼 정말 중요한 내용이다.
- 깊게 물어볼수록 전혀 모르고 있음을 알 수 있다.
- 메모리를 자동으로 관리해주는 만큼 잘 이해하고 있어야 한다.
- :star:C#과 유니티에서 적은 코드가 메모리에 어떻게 생성되는지 이해하는 것은 정말 중요하다.
  - OS의 메모리 부분을 읽고 오면 이해하기 쉬울 것 이다.
  - 인턴 때 준 책 2권 중 1권이 관련 내용이었다.

# Stack의 의문점 
- :question: 어떤 함수에 지역변수를 순서대로 10개를 선언했다고 가정하자. 그러면 처음 선언한 변수는 스택의 하단에 쌓일 것 이다. 해당 변수가 필요해서 호출을 한다면 위에 9개를 pop해야 하는가?
  - 확실한 답변은 찾지 못했다. 하지만 정말 순수 스택이라면 O(9)의 복잡도를 갖게 될 것 이고 컴퓨터가 멍청할 것 이다. 어떠한 로직으로 이 부분은 O(1)일 것이며 접근이 될 것 이다. 
  - 구글의 대부분 그림들이 스택의 특정원소를 쉽게 접근하는 그림으로 표현했다.

# scope와 stack의 성질 (내 생각)
- scope 내부에 선언된 지역변수 및 지역함수는 scope가 끝나면 메모리에서 제거되어야 한다.
  - 이는 조금만 생각해보면 LIFO이고 이를 가장 쉽게 이용할 수 있는 자료구조는 스택이다.
- Example : Big Scope -> Small Scope라면 small scope 내부의 변수와 함수의 주소들이 스택의 탑 부터 쌓이며 small scope를 나가면 스택의 탑 부터 지워진다.
   
# :star:메서드 내부의 지역변수와 객체의 멤버변수의 메모리 할당 위치 같은 건 중요하지 않아 -> Reference Type의 원리만 알면 된다.
- 오랜 고민을 한 끝에 뭐가 스택에 가고 뭐가 힙에 가는 것은 크게 중요하지 않을지도 모른다고 생각했다. 결국에 중요한 것은 Reference Type의 원리다.
- :star:Reference Type으로 선언한 것은 주소를 스택이나 힙에 저장하고, 실제 데이터는 힙(아마 모두 힙일 것)에 저장된다. 저장된 주소를 통해 실제 데이터에 접근해서 **참조**한다.
  
# C# Stack Memory vs Heap Memory
- :link:[Link](https://www.c-sharpcorner.com/article/stack-vs-heap-memory-c-sharp/)
- ![image](https://user-images.githubusercontent.com/55792986/198195402-cb2a823d-2e2c-4c11-9c13-2927f7d03ccd.png)
- ![image](https://user-images.githubusercontent.com/55792986/198195894-a393214f-487b-426e-a1cd-4c6bb83dad66.png)
  - ![image](https://user-images.githubusercontent.com/55792986/198196438-e98544a7-4ee3-46bd-9e4d-3acd3af4cd73.png)
    - 스택 영역에 저장되는 변수는 해당 scope가 끝나면 LIFO 순서대로 스택 영역에서 제거 된다.
  - ![image](https://user-images.githubusercontent.com/55792986/198198711-5a7e2b0c-7bb2-4990-ac4d-7d839b47f7dd.png)
    - 힙 영역에 배열을 저장하는 부분을 보면 배열의 데이터는 힙에 저장되지만 배열의 주소는 스택에 저장된다.
    - new로 선언하는 모든 것(배열, 객체 등등)은 힙에 실제 값들을 저장하고 스택에 힙을 참조할 수 있는 주소를 저장한다.
- ![20221027_140253](https://user-images.githubusercontent.com/55792986/198196247-e87bb55c-a963-46fe-aa8c-bb334a59ac52.png)
  - ![image](https://user-images.githubusercontent.com/55792986/198208655-afc783e1-a655-4ce3-8e68-044a31c305a0.png)
  - 스택에는 Value_type이 저장되고 힙에는 Reference_type이 저장된다. 힙에 저장되는 reference_type을 가기 위한 주소는 스택에 저장된다.
  - 구조체는 스택에 저장되고 클래스는 힙에 저장되는 차이도 알 수 있다.
- 스택 접근 속도가 힙 접근 속도보다 빠르다.
- ![image](https://user-images.githubusercontent.com/55792986/198207907-76e38bc8-021c-4e05-8a26-bd340eaed4f6.png)
  - Garbage Collection은 Managed Heap의 영역이다.
- :link:[추가 링크](https://www.c-sharpcorner.com/article/C-Sharp-heaping-vs-stacking-in-net-part-i/)


# Boxing & Unboxing
- C#은 value_type 과 reference_type으로 메모리에 할당한다.
- 
# Garbage Collection
- :link:[친구 블로그](https://luv-n-interest.tistory.com/m/922)