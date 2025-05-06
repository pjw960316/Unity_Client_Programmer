# 목차
- [목차](#목차)
- [Struct 값 타입](#struct-값-타입)
- [:star:Struct vs Class](#starstruct-vs-class)
    - [1. Struct](#1-struct)
    - [2. Class](#2-class)
    - [3. 차이점을 가장 잘 보여주는 코드 : LINQPAD 로 다시 정리](#3-차이점을-가장-잘-보여주는-코드--linqpad-로-다시-정리)

<br/><br/><br/>

# Struct 값 타입
- https://nowonbun.tistory.com/84

<br/><br/><br/>

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

<br/><br/><br/>

### 2. Class
- :star:클래스는 reference type이라 객체의 멤버들이 힙에 저장된다.
- ![image](https://user-images.githubusercontent.com/55792986/198620218-5459b1e0-af93-4122-88c3-655bea797c24.png)
  - 멤버 변수 Age의 실제 값은 힙에 저장된다.
- 상속이 가능하다.
- 생성자를 만들 수 있다.

<br/><br/><br/>

### 3. 차이점을 가장 잘 보여주는 코드 : LINQPAD 로 다시 정리
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