## :fire: 동기 호출과 비동기 호출
#### :one: 동기 호출 (=Blocking Call)
- ![20230126_162358](https://user-images.githubusercontent.com/55792986/214779354-5e5015cf-65bd-4b5d-9b13-d737103d016a.png)
  - 디스크 I/O를 하느라 스레드가 멈춰 있는 현상을 blocking 이라고 한다.

#### :two: 비동기 호출
- ![20230126_162313](https://user-images.githubusercontent.com/55792986/214779318-7824ee69-30e8-4710-a2c6-481dbf4ec677.png)
  - Blocking이 발생하지 않고 주 스레드는 계속 일을 한다.

<br><br>

## :fire: Async/Await 예제_1
~~~c#
public static void Main()
{
    Test();
    
    Console.WriteLine("Main Don't Block");
    Console.ReadLine();
}

public static async void Test()
{
    Console.WriteLine("Test Start");
    
  //await으로 인해 Test()를 탈출하고, SumAsync를 대기한다.
  //그러므로 Result에서 Test Start 후에 즉시 Main Don't Block이 적힌다.
    int ret = await SumAsync(1,2); 

    Console.WriteLine($"Test End : {ret}");
    
    return;
}

public static async Task<int> SumAsync(int a, int b)
{
    // 3초 동안 대기
    await Task.Delay(3000);

    return a + b;
}

/* Result
  Test Start
  Main Don't Block
  Test End : 3
*/
~~~

<br><br>

## :fire: Async/Await 예제_2
- ![20230206_164222](https://user-images.githubusercontent.com/55792986/216913244-155031a6-3c96-4128-a9b9-7c215adcd124.png)
- ![20230201_141055](https://user-images.githubusercontent.com/55792986/215956880-75105804-3c47-466b-a651-7018cee01135.png)
  - '메서드'의 즉시 반환이 핵심이다. 해당 메서드를 탈출하는 것을 의미한다. 
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

<br><br>

## :fire: async 함수의 리턴 타입
- void면 그냥 void
- Task로 리턴할 수 있는데 신기한 것이 return을 적지 않아도 Task를 리턴한다.
  - ![image](https://user-images.githubusercontent.com/55792986/214784192-6923360d-11ba-4d40-a9ed-1880c3fc319c.png)
- 아예 리턴 값을 받고 싶으면 제네릭을 이용한다.
  - ![image](https://user-images.githubusercontent.com/55792986/214784401-0779a594-a6d0-4009-a852-f546b7a419b0.png)
    - 얘는 리턴 값을 명시해줘야 한다.
    - ret는 int지만 C#에서 알아서 Task<int>로 변경해서 리턴 해준다.

<br><br>

## :fire: async 키워드와 async를 붙이는 함수
- async 키워드를 붙인 비동기 함수
  - 얘는 비동기로 동작시킬 것이며 이 함수는 만나도 주 스레드에서 블로킹이 되지 않음을 의미한다.
- 함수 이름 뒤에 async를 붙여서 만든 함수 (ex : TestAsync)
  - 보통 async 키워드를 붙인 비동기 함수 내부에서 호출되고 await 뒤에 있다.
  - TestAsync의 작업이 모두 진행 완료 될 때 까지 비동기 함수에서 결과를 기다리겠다.

<br><br>

## :fire: await이 걸리는 순간 this도 null일 수 있다.
- 실행 흐름이 언제인지 알 수 없기 때문에 나 자체가 꺼졌을 때 해당 task가 수행 될 수 있다.
- cancellationToken을 항상 await과 함께 쓴다고 생각해서 이벤트도 종료 시키고, 인스턴스의 필드에 대해서도 null 검사를 해서 안정성을 확보한다.

<br><br>

## :fire: async vs Coroutine
- ![20230126_171320](https://user-images.githubusercontent.com/55792986/214787579-622546cc-ed29-4cb3-9e4b-06d63e80012b.png)