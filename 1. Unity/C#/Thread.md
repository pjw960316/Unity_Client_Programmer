# 목차
- [목차](#목차)
- [생각](#생각)
- [Thread](#thread)
- [ThreadPool](#threadpool)
- [Task](#task)
- [async / await](#async--await)
- [Unitask vs task](#unitask-vs-task)
- [UniTask](#unitask)

# 생각
- 이 문서에 대한 정리를 어떻게 해야 할까?
- thread, task, unitask 는 큰 개념이지만 연관 되었다.
  
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
- 

# async / await
- 
# Unitask vs task
- ![image](https://user-images.githubusercontent.com/55792986/209285354-feaaec6e-f4a9-40f1-9af4-45aa1478889d.png)

# UniTask
- ![image](https://user-images.githubusercontent.com/55792986/209274876-0b387bbd-c442-4fd4-a1e4-58949be5d8a7.png)
- link : https://neuecc.medium.com/unitask-a-new-async-await-library-for-unity-a1ff0766029
- async/await for 비동기, unirx for event