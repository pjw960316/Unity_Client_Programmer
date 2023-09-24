# 개요
- [개요](#개요)
- [Object Type (=Object Class)](#object-type-object-class)
- [Boxing \& Unboxing](#boxing--unboxing)
    - [1. 개요](#1-개요)
    - [2. 박싱과 언박싱이 발생하지 않음 : Reference Type -\> Reference Type](#2-박싱과-언박싱이-발생하지-않음--reference-type---reference-type)
    - [3. 박싱과 언박싱이 발생함 : Value Type -\> Reference Type](#3-박싱과-언박싱이-발생함--value-type---reference-type)
- [Object Type \& Interface](#object-type--interface)
    - [1. 기본 사항](#1-기본-사항)
    - [2. 문제점 및 해결책\_1](#2-문제점-및-해결책_1)
    - [3. 문제점 및 해결책\_2](#3-문제점-및-해결책_2)

<br/><br/><br/>

# Object Type (=Object Class)
- ![image](https://user-images.githubusercontent.com/55792986/206975756-1cb91706-d2eb-4f04-bbc1-ccda509508f5.png)
- ![image](https://user-images.githubusercontent.com/55792986/206975791-06b3ba88-5633-4ccb-8a30-a3bcd7c8c834.png)
- 모든 클래스는 보이지 않게 System.Object를 상속 받고 있다.
  - ![image](https://user-images.githubusercontent.com/55792986/206975916-77b3e283-f53d-4481-8f84-ec80d837c4af.png)
  - 그러므로 C#의 모든 클래스는 Object의 메서드를 모두 갖고 있다.

<br/><br/><br/>

# Boxing & Unboxing
### 1. 개요
- 체감이 안 오던 개념인데 강의를 듣고 바로 이해를 했다.
- 박싱과 언박싱은 당연히 오버헤드가 발생한다.

<br/><br/>

### 2. 박싱과 언박싱이 발생하지 않음 : Reference Type -> Reference Type
- ![image](https://user-images.githubusercontent.com/55792986/206980097-757a9a48-1c09-43b0-a637-f384f26ca500.png)
- ![image](https://user-images.githubusercontent.com/55792986/206980006-e97b3387-6b4d-4c11-a74a-332b50676c92.png)
  - 배열로 선언하면 a1은 스택에서 힙의 1,2,3을 가리킨다.
  - object o1 또한 스택에서 a1이 참조하는 힙의 1,2,3을 동일하게 가리킨다. a2도 마찬가지다.
  - :star:참조 타입은 결국에 스택에 해당 메모리를 저장하는 변수가 힙의 데이터를 참조하는 것이므로 박싱과 언박싱이 발생하지 않는다.
- :star:Object Type과 참조 타입 사이에서는 박싱과 언박싱이 발생하지 않음을 알 수 있다.

<br/><br/>

### 3. 박싱과 언박싱이 발생함 : Value Type -> Reference Type
- ![image](https://user-images.githubusercontent.com/55792986/206982548-6163403f-1caa-47bf-98c3-fa252b736d03.png)
- ![image](https://user-images.githubusercontent.com/55792986/206982590-0459c1c2-96a5-4f9f-9943-232b017236e7.png)
- Reference type은 반드시 heap의 데이터를 참조해야 한다. 
  - 그런데 Reference type인 o2에서 value type인 n1을 참조하도록 하면 n1은 자체적으로 해당 값을 힙에 박싱한다.
  - 명시적 캐스팅을 하면 박싱한 데이터를 다시 스택으로 복사한다. 이게 언박싱이다.

<br/><br/><br/>

# Object Type & Interface
### 1. 기본 사항
- 인터페이스를 상속받으면 해당 인터페이스에서 명시한 메소드를 직접 구현해야 한다. 
  - 이 때 Object Type을 매개변수나 리턴 값으로 사용하면 편리하다.
  - 범용성 면에서 유리하다.
- ![image](https://user-images.githubusercontent.com/55792986/206985391-86ea2d46-c48a-4a5d-a291-e390b42e246d.png)
  - 매개변수를 object type으로 설정하여 모든 타입을 받을 수 있고, as를 이용하여 이를 Point 타입으로 캐스팅하여 쉽게 사용할 수 있다.

<br/><br/>

### 2. 문제점 및 해결책_1
- 문제점 : Object type의 매개변수가 만능은 아니다. 만약 해당 매개변수에 Value type을 받으면 박싱/언박싱이 발생하고 이는 오버헤드를 만든다.
- 해결책 : Generic을 이용한다!
  - Object type을 매개변수로 하지 말고 generic을 이용해서 명시적인 타입으로 받는다.
- ![image](https://user-images.githubusercontent.com/55792986/206988412-af1024ef-ca7c-4497-9f78-ea0dae79730b.png)
- 내 생각 : Object type은 박싱/언박싱이 발생할 우려가 있으므로 generic을 이용하여 다양한 타입을 받는 방식으로 우회하자.

<br/><br/>

### 3. 문제점 및 해결책_2
- 문제점 : Generic을 사용하여 구현을 해도 Object Type은 또 호환이 안 된다.
- 해결책 : object type을 구현한 인터페이스와 generic을 구현한 인터페이스를 모두 구현한다.
  - ![20221212_164300](https://user-images.githubusercontent.com/55792986/206989166-9b3cfe29-47a0-47a5-a85c-c6cea5765a2c.png)
    - 두 가지를 모두 구현한 int32다.