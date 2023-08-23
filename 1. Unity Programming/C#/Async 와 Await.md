# 목차
- [목차](#목차)
- [동기 호출과 비동기 호출](#동기-호출과-비동기-호출)
    - [1. 동기 호출 (=Blocking Call)](#1-동기-호출-blocking-call)
    - [2. 비동기 호출](#2-비동기-호출)
- [.Net Async/Await : 비동기 호출을 간단하게 하는 키워드](#net-asyncawait--비동기-호출을-간단하게-하는-키워드)
- [Async 함수의 리턴 타입](#async-함수의-리턴-타입)
- [Async 키워드와 Async를 붙이는 함수](#async-키워드와-async를-붙이는-함수)
- [Async/Await 정리](#asyncawait-정리)
- [Async vs Coroutine](#async-vs-coroutine)


# 동기 호출과 비동기 호출
### 1. 동기 호출 (=Blocking Call)
- ![20230126_162358](https://user-images.githubusercontent.com/55792986/214779354-5e5015cf-65bd-4b5d-9b13-d737103d016a.png)
  - 디스크 I/O를 하느라 스레드가 멈춰 있는 현상을 blocking 이라고 한다.
  
### 2. 비동기 호출
- ![20230126_162313](https://user-images.githubusercontent.com/55792986/214779318-7824ee69-30e8-4710-a2c6-481dbf4ec677.png)
    - Blocking이 발생하지 않고 주 스레드는 계속 일을 한다.

# .Net Async/Await : 비동기 호출을 간단하게 하는 키워드 
- ![20230206_164222](https://user-images.githubusercontent.com/55792986/216913244-155031a6-3c96-4128-a9b9-7c215adcd124.png)
- ![20230201_141055](https://user-images.githubusercontent.com/55792986/215956880-75105804-3c47-466b-a651-7018cee01135.png)
- ![image](https://user-images.githubusercontent.com/55792986/216913024-2a1018bc-93e0-4ddb-b5a3-5cc2c116468d.png)
  - :star:**실행 흐름**
    - 1) 주 스레드는 Main에서 UpdateResult를 호출한다.
    - 2) 주 스레드는 UpdateResult()에 진입해서 Console.WriteLine("UpdateResult")를 실행한다.
    - 3) await을 만나고 SumAsync(1,200)을 호출해놓고(풀에 있던 다른 스레드가 SumAsync를 수행) 다시 주 스레드는 Main으로 돌아간다.
    - 4) 주 스레드는 Main의 Console.WriteLine("Main : Run Event loop")를 실행하고 이 때 비동기로 다른 스레드에서 SumAsync()의 내부를 수행한다.
    - 5) **:star:주 스레드가 아닌 다른 스레드**에서 SumAsync(1,200)을 완료하면 UpdateResult()의 ret에 그 값을 초기화 하고 Console.WriteLine($"{ret}")까지 진행하고 종료한다. 
- 주 스레드는 SumAsync의 결과를 기다리지 않고 Main 함수를 수행할 수 있다.
- async 메서드의 await을 만나면 풀의 스레드에서 해당 부분을 관리하고 주 스레드는 관심을 갖지 않고 자신의 흐름을 유지한다.
  - Blocking이 발생하지 않는다!
<br>


# Async 함수의 리턴 타입
- void면 그냥 void
- Task로 리턴할 수 있는데 신기한 것이 return을 적지 않아도 Task를 리턴한다.
  - ![image](https://user-images.githubusercontent.com/55792986/214784192-6923360d-11ba-4d40-a9ed-1880c3fc319c.png)
- 아예 리턴 값을 받고 싶으면 제네릭을 이용한다.
  - ![image](https://user-images.githubusercontent.com/55792986/214784401-0779a594-a6d0-4009-a852-f546b7a419b0.png)
    - 얘는 리턴 값을 명시해줘야 한다.
    - ret는 int지만 C#에서 알아서 Task<int>로 변경해서 리턴 해준다.
<br/><br/><br/><br/><br/>

# Async 키워드와 Async를 붙이는 함수
- Async 키워드를 붙인 비동기 함수
  - 얘는 비동기로 동작시킬 것이며 이 함수는 만나도 주 스레드에서 블로킹이 되지 않음을 의미한다.
- 함수 이름 뒤에 Async를 붙여서 만든 함수 (ex : TestAsync)
  - 보통 Async 키워드를 붙인 비동기 함수 내부에서 호출되고 await 뒤에 있다.
  - TestAsync의 작업이 모두 진행 완료 될 때 까지 비동기 함수에서 결과를 기다리겠다.
<br>

# Async/Await 정리
- ![image](https://user-images.githubusercontent.com/55792986/214785317-a8da3b13-c1b9-4eff-a17a-d377af8d5db3.png)
<br>

# Async vs Coroutine
- ![20230126_171320](https://user-images.githubusercontent.com/55792986/214787579-622546cc-ed29-4cb3-9e4b-06d63e80012b.png)