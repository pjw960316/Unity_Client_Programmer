# 목차
- [목차](#목차)
- [옛날 부분 대신 이걸로 본다.](#옛날-부분-대신-이걸로-본다)
- [Unitask를 이해하려면 Async Await을 이해해야 한다.](#unitask를-이해하려면-async-await을-이해해야-한다)
- [동기 호출과 비동기 호출](#동기-호출과-비동기-호출)
    - [1. 동기 호출 (=Blocking Call)](#1-동기-호출-blocking-call)
    - [2. 비동기 호출](#2-비동기-호출)
- [Async/Await : 비동기 호출을 간단하게 하는 키워드](#asyncawait--비동기-호출을-간단하게-하는-키워드)
- [Async 함수의 리턴 타입](#async-함수의-리턴-타입)
- [Async 키워드와 Async를 붙이는 함수](#async-키워드와-async를-붙이는-함수)
- [Async/Await 정리](#asyncawait-정리)
- [Async vs Coroutine](#async-vs-coroutine)

# 옛날 부분 대신 이걸로 본다.

# Unitask를 이해하려면 Async Await을 이해해야 한다.
# 동기 호출과 비동기 호출
### 1. 동기 호출 (=Blocking Call)
- ![20230126_162358](https://user-images.githubusercontent.com/55792986/214779354-5e5015cf-65bd-4b5d-9b13-d737103d016a.png)
  - 디스크 I/O를 하느라 스레드가 멈춰 있는 현상을 blocking 이라고 한다.
  
### 2. 비동기 호출
- ![20230126_162313](https://user-images.githubusercontent.com/55792986/214779318-7824ee69-30e8-4710-a2c6-481dbf4ec677.png)
    - Blocking이 발생하지 않고 주 스레드는 계속 일을 한다.

# Async/Await : 비동기 호출을 간단하게 하는 키워드 
- await이 있는 메서드에서는 async 키워드를 붙여준다.
~~~c#
Main()
{
    PlayLongWork();
    CLog.Error("i never stop");
}
private async void PlayLongWork()
{
    int result = await PracticeAsync();
}
~~~
  - 메인 함수에서 PlayLongWork()는 비동기로 동작한다. 그러므로 메인 스레드에서는 블록이 발생하지 않고 "i never stop"을 출력한다.
  - 비동기로 동작하며 result에 값을 넣어 줄 때 까지 비동기로 도는 워크 스레드에서는 PracticeAsync()의 작업이 완료 될 때 까지 기다린다. 
- ![20230201_141055](https://user-images.githubusercontent.com/55792986/215956880-75105804-3c47-466b-a651-7018cee01135.png)
  - :star:**주 스레드 실행 흐름에서 Async로 만들어 진 메서드인 UpdateResult를 만났다. 실행 흐름에서 await를 만나면 우측 식인 SumAsync(1,200)을 호출만 하고(SumAsync(1,200)은 아직 끝나지 않았다.) 실행 흐름을 다시 주 스레드로 돌아온다.**
    - 디버그를 찍어보면 await 없이 SumAsync(1,200)을 부르면 SumAsync로 디버거가 이동하는데 await를 사용 하면 이동하지 않고 호출만 하고 바로 주 스레드의 다음 라인인 Console.WriteLine($"{ret}")으로 넘어간다.
- 워크 스레드로 빼는 이유는 당연히 오래걸리는 작업이거나, 기다려야 하는 메서드가 존재할 때. 

# Async 함수의 리턴 타입
- void면 그냥 void
- Task로 리턴할 수 있는데 신기한 것이 return을 적지 않아도 Task를 리턴한다.
  - ![image](https://user-images.githubusercontent.com/55792986/214784192-6923360d-11ba-4d40-a9ed-1880c3fc319c.png)
- 아예 리턴 값을 받고 싶으면 제네릭을 이용한다.
  - ![image](https://user-images.githubusercontent.com/55792986/214784401-0779a594-a6d0-4009-a852-f546b7a419b0.png)
    - 얘는 리턴 값을 명시해줘야 한다.
    - ret는 int지만 C#에서 알아서 Task<int>로 변경해서 리턴 해준다.

# Async 키워드와 Async를 붙이는 함수
- Async 키워드를 붙인 비동기 함수
  - 얘는 비동기로 동작시킬 것이며 이 함수는 만나도 주 스레드에서 블로킹이 되지 않음을 의미한다.
- 함수 이름 뒤에 Async를 붙여서 만든 함수 (ex : TestAsync)
  - 보통 Async 키워드를 붙인 비동기 함수 내부에서 호출되고 await 뒤에 있다.
  - TestAsync의 작업이 모두 진행 완료 될 때 까지 비동기 함수에서 결과를 기다리겠다.

# Async/Await 정리
- ![image](https://user-images.githubusercontent.com/55792986/214785317-a8da3b13-c1b9-4eff-a17a-d377af8d5db3.png)

# Async vs Coroutine
- ![20230126_171320](https://user-images.githubusercontent.com/55792986/214787579-622546cc-ed29-4cb3-9e4b-06d63e80012b.png)