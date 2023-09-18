- 참조
  - https://www.smashingmagazine.com/2012/10/why-coding-style-matters/
  - MSDN 코드 룰 : https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/
  - ![20230918_120124](https://github.com/pjw960316/Unity_Client_Programmer/assets/55792986/10718ec5-17b6-47ac-9712-0498c3ef8014)
- 클린코드 보다는 리팩터링 2판에 대해서 읽으면서 분석한 내용을 위주로.
- 갑자기 번뜩인 것.
  - 업무 시간에서 줄일 수 있는 시간이 있다. 이런 스타일 적 고민을 하는 것. 왜 할까? 스타일이 없으니까.
  - 스타일은 곧 개인의 법칙
  - 법칙을 만들면 고민의 오버헤드를 줄인다.
- 타인에게 가독성이 좋아야 하니까 고민하는 것도 맞지만 결국 내 시간을 줄이기 위한, 내가 내 코드를 잘 읽기 위함이 크다.


- if-else는 몇개 switch-case는 몇개일때 이것도 분석하고 본인의 스타일로.
- ![20230918_114858](https://github.com/pjw960316/Unity_Client_Programmer/assets/55792986/9e58540e-3f3b-49d0-b204-f50fc528c1d5)


# 법칙
### 1. 복잡한 메서드는 Summary 주석을 달아주자.
- ///를 쓰면 알아서 자동 완성이 된다.
- 한글로 적는 게 더 가독성이 좋다.

### 2. 매개변수로 전달할 때 클래스의 인스턴스 전체보다는 필요한 인스턴스 필드만 전달하자.
- 매개변수가 많아질 것을 우려했지만 이 방법이 더 괜찮다고 판단했다.
- ![20230918_135148](https://github.com/pjw960316/Unity_Client_Programmer/assets/55792986/4c1100a2-d2f7-4257-acb8-4eeaade21a03)
> 인용 테스트
>> 인용 테스트2
>>> 인용 테스트3