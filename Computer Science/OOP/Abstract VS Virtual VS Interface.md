# 목차
- [목차](#목차)
- [정리 한 이유](#정리-한-이유)
- [Abstract (추상 클래스와 추상 메서드)](#abstract-추상-클래스와-추상-메서드)
- [Virtual (가상 함수)](#virtual-가상-함수)
- [Abstract vs Virtual](#abstract-vs-virtual)
- [Interface](#interface)

# 정리 한 이유
- 3개의 키워드는 모두 각자의 기능이 있고 명확하게 이해하고 구분해야 더 좋은 설계를 할 수 있을 것 같다.
  
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
- 
- virtual 함수는 구현을 해도 된다.


# Abstract vs Virtual
- 차이점
  - <img width="505" alt="20220810_173505" src="https://user-images.githubusercontent.com/55792986/183855202-8357de3f-f86e-42f1-a9b8-e1da73ef1ae4.png">
    

# Interface
- [Refrence](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/Books%20For%20Development/%EA%B0%9D%EC%B2%B4%EC%A7%80%ED%96%A5%EC%9D%98%20%EC%82%AC%EC%8B%A4%EA%B3%BC%20%EC%98%A4%ED%95%B4.md) (5장의 6번 항목에서 인터페이스를 자세하게 설명했다.)
