# 목차
- [목차](#목차)
- [해당 문서에 대한 계획 (02/07)](#해당-문서에-대한-계획-0207)
- [Thread](#thread)
- [ThreadPool](#threadpool)
- [Task](#task)
- [ThreadSafe](#threadsafe)
- [ConcurrentQueue를 왜 쓸까?](#concurrentqueue를-왜-쓸까)

# 해당 문서에 대한 계획 (02/07)
- 스레드랑 unitask가 연결이 되어있지만 unitask는 따로 빼서 관리하자.
- 최근에 산 책을 여기서 정리하자.
  
# Thread
- 과거 회사 경험 + 취준 때 공부한 자료 + 지금을 섞어서 훌륭한 융합 공부를 해보자.
- 코드를 실행하는 실행 흐름
- 오랜 시간이 걸리는 작업은 보조 스레드로
- cpu의 개수만큼 스레드를 만들자.
- 오버헤드
  - context switch(cpu의 정보)가 발생한다.
    - thread끼리 교환할 때 발생함.
  - 그러므로 최적의 숫자는 cpu 개수와 스레드의 수를 일치한다.
- 아는 내용 : 동시에 동작하는 것

# ThreadPool
- 스레드 생성과 파괴에는 오버헤드가 있다.
- 생성 파괴를 줄이고 하나의 스레드를 대기/실행 하도록 하는 것이 좋다.
- 스레드를 몇개를 만들까?
  - 다양한 환경(컴퓨터 마다 다양한 cpu)
  - 이를 위해 threadpool을 쓴다.
- Threadpool에 thread를 넣는다.
  - 작업이 들어오면 thread가 없으면 생성하고 해당 thread를 실행하고, 노는 thread있으면 거기에 씀.
- Threadpool은 background thread다. 기본적으로 만드는 thread는 front thread다. 
- threadpool의 thread에는 이름을 지정할 수 없다.
- threadpool은 종료에 대한 대기가 없다.

# Task
- Thread, Threadpool보다 좋다.
- Unitask도 Task겠지.
- 강의에서는 Task도 좋다고 하지만 이건 유니티 친화가 아닐 것
- 종료 대기는 태스크 객체.wait()
- 자세한 메서드는 f12에서 확인

# ThreadSafe
- ![20230206_115020](https://user-images.githubusercontent.com/55792986/216872480-4fd5712e-84df-445b-8ab9-a917b1de0dd8.png)
- https://gompangs.tistory.com/entry/OS-Thread-Safe%EB%9E%80
- https://learn.microsoft.com/ko-kr/dotnet/standard/collections/thread-safe/
  
# ConcurrentQueue를 왜 쓸까?
- [C-Sharp](https://www.csharpstudy.com/DS/queue.aspx)
  - 일반적인 큐와 다른 것은 스레드에 대해서 안전한지
