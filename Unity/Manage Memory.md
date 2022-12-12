# 목차
- [목차](#목차)
- [개요](#개요)
- [Stack의 의문점](#stack의-의문점)
- [scope와 stack의 성질 (내 생각)](#scope와-stack의-성질-내-생각)
- [:star: 메서드 내부의 지역변수와 객체의 멤버변수의 메모리 할당 위치 같은 건 중요하지 않아 -\> Reference Type의 원리만 알면 된다.](#star-메서드-내부의-지역변수와-객체의-멤버변수의-메모리-할당-위치-같은-건-중요하지-않아---reference-type의-원리만-알면-된다)
- [C# Stack Memory vs Heap Memory](#c-stack-memory-vs-heap-memory)
- [:star:Struct vs Class](#starstruct-vs-class)
    - [1. Struct](#1-struct)
    - [2. Class](#2-class)
    - [3. 차이점을 가장 잘 보여주는 코드](#3-차이점을-가장-잘-보여주는-코드)
- [Garbage Collection (=GC)](#garbage-collection-gc)
    - [1. 개요](#1-개요)
    - [2. GC를 하는 조건](#2-gc를-하는-조건)
    - [3. Unity의 Heap 분류](#3-unity의-heap-분류)
    - [4. GC에서 살아남는 메모리](#4-gc에서-살아남는-메모리)
    - [5. 세대를 이용하는 GC](#5-세대를-이용하는-gc)
    - [6. Memory Compaction (메모리 빈 공간 없애기)](#6-memory-compaction-메모리-빈-공간-없애기)
    - [7. 참고문헌](#7-참고문헌)

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
   
# :star: 메서드 내부의 지역변수와 객체의 멤버변수의 메모리 할당 위치 같은 건 중요하지 않아 -> Reference Type의 원리만 알면 된다.
- 오랜 고민을 한 끝에 뭐가 스택에 가고 뭐가 힙에 가는 것은 크게 중요하지 않을지도 모른다고 생각했다. 결국에 중요한 것은 Reference Type의 원리다.
- :star:Reference Type으로 선언한 것은 주소를 스택이나 힙에 저장하고, 실제 데이터는 힙(아마 모두 힙일 것)에 저장된다. 저장된 주소를 통해 실제 데이터에 접근해서 **참조**한다.
  
# C# Stack Memory vs Heap Memory
- ![image](https://user-images.githubusercontent.com/55792986/198195402-cb2a823d-2e2c-4c11-9c13-2927f7d03ccd.png)
- ![image](https://user-images.githubusercontent.com/55792986/198195894-a393214f-487b-426e-a1cd-4c6bb83dad66.png)
  - ![image](https://user-images.githubusercontent.com/55792986/198196438-e98544a7-4ee3-46bd-9e4d-3acd3af4cd73.png)
    - 스택 영역에 저장되는 변수는 해당 scope가 끝나면 LIFO 순서대로 스택 영역에서 제거 된다.
  - ![image](https://user-images.githubusercontent.com/55792986/198198711-5a7e2b0c-7bb2-4990-ac4d-7d839b47f7dd.png)
    - 힙 영역에 배열을 저장하는 부분을 보면 배열의 데이터는 힙에 저장되지만 배열의 주소는 스택에 저장된다.
    - new로 선언하는 모든 것(배열, 객체 등등)은 힙에 실제 값들을 저장하고 스택에 힙을 참조할 수 있는 주소를 저장한다.
    - 참조의 경우 힙에 저장된 데이터를 바꾸면 참조하고 있는 모든 애들도 해당 데이터가 변경된다.
- ![20221027_140253](https://user-images.githubusercontent.com/55792986/198196247-e87bb55c-a963-46fe-aa8c-bb334a59ac52.png)
  - ![image](https://user-images.githubusercontent.com/55792986/198208655-afc783e1-a655-4ce3-8e68-044a31c305a0.png)
  - 스택에는 Value_type이 저장되고 힙에는 Reference_type이 저장된다. 힙에 저장되는 reference_type을 가기 위한 주소는 스택에 저장된다.
  - 구조체는 스택에 저장되고 클래스는 힙에 저장되는 차이도 알 수 있다.
- 스택 접근 속도가 힙 접근 속도보다 빠르다.
- ![image](https://user-images.githubusercontent.com/55792986/198207907-76e38bc8-021c-4e05-8a26-bd340eaed4f6.png)
  - Garbage Collection은 Managed Heap의 영역이다.
- :link:[Link_1](https://www.c-sharpcorner.com/article/stack-vs-heap-memory-c-sharp/)
- :link:[Link_2](https://www.c-sharpcorner.com/article/C-Sharp-heaping-vs-stacking-in-net-part-i/)

# :star:Struct vs Class
### 1. Struct
- :star:구조체는 value type이라 객체의 멤버들이 스택에 저장된다.
  - ![image](https://user-images.githubusercontent.com/55792986/198582083-7e623816-1680-432e-a86a-069ef981186d.png)
    - :star:구조체에 대한 객체 ss를 만들면 ss 자체가 스택에 올라가며 ss를 이루고 있는 멤버 변수 + 멤버 함수(이건 아닐 수도)만큼 스택의 메모리를 차지합니다.
    - 구조체의 메모리 차지는 대학 1때 배운 로직이다.
- 구조체의 멤버 변수중에 참조 타입인 배열이 있다면?
  - 배열의 주소만 스택에 존재하고 배열 자체는 힙에 할당된다.
  - 근데 이런 경우 그냥 힙이 좋지 않을까?
- 상속을 할 수 없다.
- 생성자를 만들 수 없다.
- :link:[link](https://www.sysnet.pe.kr/2/0/12624)

### 2. Class
- :star:클래스는 reference type이라 객체의 멤버들이 힙에 저장된다.
- ![image](https://user-images.githubusercontent.com/55792986/198620218-5459b1e0-af93-4122-88c3-655bea797c24.png)
  - 멤버 변수 Age의 실제 값은 힙에 저장된다.
- 상속이 가능하다.
- 생성자를 만들 수 있다.

### 3. 차이점을 가장 잘 보여주는 코드
~~~c#
        private void testStruct()
        {
            str_data obj_1 = new str_data();
            obj_1.a = 10;
            str_data obj_2 = new str_data();
            obj_2.a = 20;
            str_data obj_3 = obj_2;
            obj_3.a = 30; //obj_3은 obj_2를 복사한 독립적인 객체기 때문에 obj_3의 값을 변화시킨다고 해서 obj_2에 영향을 미치지 않는다.

            Console.WriteLine(obj_1.a + " " + obj_2.a + " " + obj_3.a); //10 20 30
        }

        private void testClass()
        {
            data obj_1 = new data(10);
            data obj_2 = new data(10);
            data obj_3 = obj_2; //참조

            obj_2.a = 20;
            obj_3.a = 30; //obj_2와 obj_3은 같은 힙 메모리를 참조하고 있으므로 obj_2.a의 값도 30으로 변할 것 이다.
     
            Console.WriteLine(obj_1.a + " " + obj_2.a + " " + obj_3.a); //10 30 30
        }
~~~

# Garbage Collection (=GC)
### 1. 개요
- ![image](https://user-images.githubusercontent.com/55792986/198259528-bd68a268-1b8a-4da3-b8c2-53655e9258f7.png)

### 2. GC를 하는 조건
- ![image](https://user-images.githubusercontent.com/55792986/198262004-650aae6c-f23e-4daa-b93b-7c74ee8219cc.png)
  - 간단하게 생각하면 메모리가 부족하면 수행할 것 이다.

### 3. Unity의 Heap 분류
- ![image](https://user-images.githubusercontent.com/55792986/198262261-4ea44f09-eaa9-4b34-b6a5-121f7b25479a.png)

### 4. GC에서 살아남는 메모리
- ![image](https://user-images.githubusercontent.com/55792986/198262729-a0ddbdf2-b3bc-4511-b693-ed90d15b26d2.png)
  - A,C,D의 경우 해당 힙의 주소 및 정보를 스택이나 힙에 저장하고 있기 때문에 올바른 참조관계로 이루어져 있다. 그러므로 살아남는다.
  - F의 경우 D가 참조하고 있기 때문에 살아남는다.
  - B와 E의 경우 힙에 저장되어 있지만 아무도 참조하고 있지 않기 때문에 제거한다.
    - 제거한 빈 공간으로 인해 Fragmentation(단편화)이 발생하므로 Compaction(압축)으로 해결한다. 

### 5. 세대를 이용하는 GC
- 참고문헌을 읽는 것이 좋아 보인다.

### 6. Memory Compaction (메모리 빈 공간 없애기)
- ![image](https://user-images.githubusercontent.com/55792986/198264662-9ea66084-5667-43cf-ade0-50c096459566.png)
  - 외부 단편화가 발생하는 것을 해결하기 위해 memory compaction을 수행한다.
- ![image](https://user-images.githubusercontent.com/55792986/198264817-c97c0faa-2dbb-4c42-8b87-12ff4608bfab.png)
  - 외부 단편화를 제거한다. 그로 인해 메모리의 누수가 줄어들고 효율적으로 사용할 수 있다.
  - 시간이 많이 걸리고 효율적으로 compaction을 할 수 없는 경우도 존재한다.

### 7. 참고문헌
- :link:[UNITY](https://docs.unity3d.com/kr/current/Manual/performance-garbage-collector.html)
- :link:[MSDN](https://learn.microsoft.com/ko-kr/dotnet/standard/garbage-collection/fundamentals)
- :link:[친구 블로그](https://luv-n-interest.tistory.com/m/922)