# 목차
- [목차](#목차)
- [1. Framework](#1-framework)
- [2. Library](#2-library)
- [3. Module](#3-module)
- [4. 모듈 간의 관계](#4-모듈-간의-관계)
    - [4-1. Coupling (=결합도 =연관 정도)](#4-1-coupling-결합도-연관-정도)
      - [4-1-1. Tight Coupling (=강한 결합)](#4-1-1-tight-coupling-강한-결합)
      - [4-1-2. Loose Coupling (=느슨한 결합)](#4-1-2-loose-coupling-느슨한-결합)
    - [4-2. Cohesion (=응집도)](#4-2-cohesion-응집도)
    - [4-3. 결론 : Loose Coupling의 관점으로 설계하고 그 방법에 대해 정리해본다.](#4-3-결론--loose-coupling의-관점으로-설계하고-그-방법에-대해-정리해본다)
- [5. MVC Pattern](#5-mvc-pattern)
- [6. Binding](#6-binding)
- [7. Wrapping](#7-wrapping)
# 1. Framework
- ### 정의
  - ![image](https://user-images.githubusercontent.com/55792986/181395656-4d21c2f0-627a-4d00-acad-1ce0f9d4f1ae.png)
  - 어떤 프로그램을 만들 때 기반이 되는 클래스.
    - 여러 클래스와 컴포넌트로 구성되어 있다. (UI.lua, Dragon.lua)
    - 회사에서는 프레임워크가 회사의 가장 큰 재산이라고 했다.
  - skeleton과 같은 개념.
  - 내부가 여러개의 모듈로 이루어져 있다.
- ### 집 짓기 예시
  - ![20220728_090831](https://user-images.githubusercontent.com/55792986/181393199-0a972c48-e636-41e8-9395-0e68f7c8b26a.png)
  - 집을 지을 때 뼈대를 구성하는 작업이 프레임워크다..
  - 잘 만들어진 프레임워크가 있다면 많은 시간을 절약 할 수 있다.
  - 시니어 엔지니어들이 프레임워크를 제작한다.
    - 회사에서도 PM이상 급들이 프레임워크를 만들었고 모든 게임에 적용했다.
- ### 왜 프레임워크를 이용하는가?
  - ![20220728_090918](https://user-images.githubusercontent.com/55792986/181394801-2ea25a68-690a-4e05-b147-a810aa512d80.png)
  - 모든 코드를 작성하지 않아도 된다.
  - 중복을 피할 수 있다.
    - 결국 프레임워크는 타인이 만든 것이므로 완벽한 이해가 어렵다. 특히 주니어개발자가 시니어개발자의 코드를 완벽히 해석하는 것은 어렵다. 그러므로 프레임워크에 존재하는 함수지만 주니어개발자가 이를 찾지 못해 직접 만드는 경우도 발생함을 경험했다.
  - 확장도 가능하고, 안전한 코드다.
    - 확실히 상위 개발자가 작성한 코드라서 그런지 훌륭했다. 

# 2. Library
- ### 정의
  - ![20220728_094307](https://user-images.githubusercontent.com/55792986/181396304-08cacd7a-f21d-49c8-be67-facddeeb018b.png)


- ### Framework vs Library
  - 자동차의 뼈대는 프레임워크고, 와이퍼나 전조등 같은 것들이 라이브러리다.
  - 내가 이해한 것은 프레임워크는 전체적인 코드의 기반이다. 그 프레임워크 위에서 코드를 개발하여 프로그램을 완성시킬 때 필요한 도구 및 기능이 라이브러리다.

- ### 예시
  - C++ STL

- ### Library vs API (Application Programming Interface)
  - ![20220728_095628](https://user-images.githubusercontent.com/55792986/181397593-5c291b47-8231-47c6-b6da-9d68e18bdb1e.png)
    - API와 Library는 포함관계라고 생각한다.

# 3. Module
- 소프트웨어를 **기능별로** 나누는 것을 말한다.
- 조영호님의 오브젝트에서 내리는 정의 : 크기와 상관 없이 클래스나 패키지, 라이브러리와 같이 프로그램을 구성하는 임의의 요소
- ![image](https://user-images.githubusercontent.com/55792986/181397941-1ccaed10-6282-4f5d-b161-3516ad8fe12d.png)
- 용어 그 자체의 뜻은 **구성 단위** 이다.
- Unity에서 하나의 스크립트 파일이 모듈일 수도 있다.
- 모듈 간에 종속성을 최대한 줄이는 방식의 코딩을 인턴 때 진행했었다.
  - 독립성이 높은 모듈일수록 좋다.
  - 독립성이 높으면 해당 모듈을 수정하더라도 다른 모듈에 끼치는 영향이 적으며 오류가 발생하더라도 쉽게 문제를 발견하고 해결할 수 있다.
# 4. 모듈 간의 관계
### 4-1. Coupling (=결합도 =연관 정도)
- ![image](https://user-images.githubusercontent.com/55792986/184568974-e3937691-1ca3-4a6c-ae23-d2292d0bcfa2.png)
  - 다른 모듈과의 의존성(=dependency)정도 
- **유니티에서 모듈을 스크립트**라고 생각하면 스크립트간에 연관된 정도가 많으면 결합도가 높다고 한다.
- 응집도와는 반대 되는 개념이다. 
- 결합도가 높으면 다른 모듈을 찾아가며 유지 보수해야 하기 때문에 유지 보수 측면에서 좋지 않기 때문에 

#### 4-1-1. Tight Coupling (=강한 결합)
-![image](https://user-images.githubusercontent.com/55792986/184570254-c0c64600-8688-483d-96f5-fa08551de7f4.png)
  - 두 객체가 강하게 결합되어 있다면 하나의 객체에서 다른 객체를 알고 있어야 한다.
  - 작은 프로그램에서는 괜찮지만 큰 프로그램이라면 문제가 된다. 다른 개발자가 작성한 코드를 완벽하게 이해하지 않은 상태에서 모두 검사해 보아야 한다.
- 팀장님이 하셨던 말 중에 '드래곤이 없다면 이 코드는 동작하지 않을 텐데?' 라는 것이 기억난다.
  - 모듈이나 객체가 없더라도 코드가 동작해야 한다.
  
<br/>

- 아래의 예시를 확인해 본다.
~~~
class A {
   public int a = 0;
   public int getA() {
      System.out.println("getA() method");
      return a;
   }
   public void setA(int aa) {
      if(!(aa > 10))
         a = aa;
   }
}
public class B {
   public static void main(String[] args) {
      A aObject = new A();
      aObject.a = 100; // Not suppose to happen as defined by class A, this causes tight coupling.
      System.out.println("aObject.a value is: " + aObject.a);
   }
}
~~~
  - 만약 개발자가 클래스 A의 변수 a를 private로 변경한다면 클래스 B는 정상 동작하지 않게 된다.
#### 4-1-2. Loose Coupling (=느슨한 결합)
- 일단 이게 좋음
### 4-2. Cohesion (=응집도)
- 응집도가 높으면 하나의 모듈에 많은 기능들이 응집되어 모여있다.
  
### 4-3. 결론 : Loose Coupling의 관점으로 설계하고 그 방법에 대해 정리해본다.
- 인터페이스를 조금 더 이해하고...
- 
# 5. MVC Pattern



# 6. Binding

# 7. Wrapping