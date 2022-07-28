# 목차
- [목차](#목차)
- [1. Framework](#1-framework)
- [2. Library](#2-library)
- [3. Module](#3-module)
# 1. Framework
- ### 정의
  - ![image](https://user-images.githubusercontent.com/55792986/181395656-4d21c2f0-627a-4d00-acad-1ce0f9d4f1ae.png)
  - 어떤 프로그램을 만들 때 기반이 되는 클래스.
    - 여러 클래스와 컴포넌트로 구성되어 있다. (UI.lua, Dragon.lua)
    - 회사에서는 프레임워크가 회사의 가장 큰 재산이라고 했다.
  - skeleton과 같은 개념.
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
- ![image](https://user-images.githubusercontent.com/55792986/181397941-1ccaed10-6282-4f5d-b161-3516ad8fe12d.png)
  - 용어 그 자체의 뜻은 **구성 단위** 이다.
  - Unity에서 하나의 스크립트 파일이 모듈일 수도 있다.
  - 모듈 간에 종속성을 최대한 줄이는 방식의 코딩을 인턴 때 진행했었다.