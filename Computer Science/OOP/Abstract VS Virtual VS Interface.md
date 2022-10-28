# 목차
- [목차](#목차)
- [정리 한 이유](#정리-한-이유)
- [Abstract (추상 클래스와 추상 메서드)](#abstract-추상-클래스와-추상-메서드)
- [Virtual (가상 함수)](#virtual-가상-함수)
    - [1. 특징](#1-특징)
    - [2. 사용 이유](#2-사용-이유)
    - [3. override를 붙이지 않는 다면?](#3-override를-붙이지-않는-다면)
- [Overhead](#overhead)
- [Abstract vs Virtual](#abstract-vs-virtual)
- [Sealed](#sealed)
- [Interface](#interface)

# 정리 한 이유
- 3개의 키워드는 모두 각자의 기능이 있고 명확하게 이해하고 구분해야 더 좋은 설계를 할 수 있을 것 같다.
- 인턴 때 인터페이스 개념도 제대로 모르고 실무를 했었다...
  
# Abstract (추상 클래스와 추상 메서드)
- [MSDN](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/abstract)
- Abstract는 클래스와 메서드 등에 붙일 수 있다.
- 구현은 반드시 상속 받는 추상 클래스가 아닌 클래스에서 해야한다.
  - 이 때 반드시 override를 붙인다.
- 추상 클래스에 존재하는 추상 메서드는 절대 구현을 하면 안 된다.
- 추상 클래스에는 최소한 1개의 추상 메서드가 존재해야 합니다. 아니면 에러가 납니다.
  - 추상 메서드 선언은 추상 클래스에서만 허용됩니다.
- 추상 메서드는 암시적으로 가상 메서드입니다.
  
# Virtual (가상 함수)
### 1. 특징
- 가상 함수는 부모에서 구현을 해도 된다.

### 2. 사용 이유
- 추상 메서드는 명확히 사용 이유가 존재한다. 하지만 가상 함수는 조금 모호하다. 
  - 객체 지향 설계 관점에서.
- 한정자 없이는 오버라이딩이 불가능하다. (추상함수도 마찬가지)
  - ![image](https://user-images.githubusercontent.com/55792986/185402871-dc79d0c2-3977-4efb-9135-4fc1da012b54.png)
  - ![image](https://user-images.githubusercontent.com/55792986/185402363-928218b0-f9bc-49a9-956c-f0495d157fc5.png)
    - 부모 함수 Test에 한정자(virtual, abstract, override)가 붙지 않으면 오버라이딩이 불가능 합니다.

### 3. override를 붙이지 않는 다면?
- override를 붙이지 않을 때 (관련 자료를 읽어봐도 잘 이해가 안 됨. 추후에 이해하면 필기하자)
  - 애당초 Warning만 나타남.
  - ![image](https://user-images.githubusercontent.com/55792986/185401824-0416f937-5556-47fd-95a0-0950cccdde67.png)

# Overhead
![image](https://user-images.githubusercontent.com/55792986/185398970-e72a3592-75e7-4635-a363-2fcb0e5ef069.png)
- 내 생각 : 추상 함수, 가상 함수 모두 테이블이 만들어 지기 때문에 기존 보다는 성능저하가 발생 할 것 이다. 그럼에도 불구하고 이점이 많으니 사용하겠지.
  
# Abstract vs Virtual
- 차이점
  - <img width="505" alt="20220810_173505" src="https://user-images.githubusercontent.com/55792986/183855202-8357de3f-f86e-42f1-a9b8-e1da73ef1ae4.png">
  - ![image](https://user-images.githubusercontent.com/55792986/185400348-0a62afea-598b-4b8b-9224-aafb3c44fa24.png)
    
# Sealed
- Virtual로 선언된 가상 메소드를 오버라이딩한 버전의 메소드가 오버라이딩 되지 않도록 봉인할 수 있다.
- ![image](https://user-images.githubusercontent.com/55792986/185403786-0f553666-5e3a-490c-bcd2-9c29afa5a538.png)
- ![image](https://user-images.githubusercontent.com/55792986/185403876-8345a38f-094d-4e42-867a-ccef624cd40b.png)

# Interface
- [Refrence](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/Books%20For%20Development/%EA%B0%9D%EC%B2%B4%EC%A7%80%ED%96%A5%EC%9D%98%20%EC%82%AC%EC%8B%A4%EA%B3%BC%20%EC%98%A4%ED%95%B4.md) (5장의 6번 항목에서 인터페이스를 자세하게 설명했다.)
- 인터페이스의 메소드(=메시지)를 선언할 때는 접근지정자를 붙이지 않는다.
  - 자동으로 public이 된다.
  - 누군가 상속받아서 구현을 해야 하기 때문에 당연한 것 이다.
    - 구현을 강제한다.
- 인터페이스를 상속받아 구현한 메서드는 반드시 public이어야 한다.
  - ![image](https://user-images.githubusercontent.com/55792986/184607435-7a5091ca-f08a-498d-abc4-f6dc140a7c72.png)
    - 다른 스크립트에서도 불려야 하므로 public이어야 한다.
    - [Link](https://stackoverflow.com/questions/7238575/why-must-an-c-sharp-interface-method-implemented-in-a-class-be-public)
- 어떤 클래스가 두 개의 인터페이스 또는 클래스를 상속 받았고 이 때 동일한 이름의 메서드가 구현되어 있다면 아래와 같이 진행하면 된다. 근데 애당초 이름이 겹치지 않도록 작성해야 할 것 이다.
~~~
void IMinion.getDamage(int a)
    {

    }
~~~ 
- 