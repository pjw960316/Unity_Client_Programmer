# 목차
- [목차](#목차)
- [Awake vs 생성자](#awake-vs-생성자)
    - [1. 의문점이 발생한 코드](#1-의문점이-발생한-코드)
    - [2. 해답 (:exclamation:확실하지는 않다.)](#2-해답-exclamation확실하지는-않다)
- [Awake , Start는 왜 override를 붙이지 않는가? (검색의 한계를 느낀다...)](#awake--start는-왜-override를-붙이지-않는가-검색의-한계를-느낀다)
- [Awake는 메시지다.](#awake는-메시지다)
- [FixedUpdate vs Update의 프레임 관점](#fixedupdate-vs-update의-프레임-관점)
    - [1. 프레임](#1-프레임)
    - [2. Time.deltatime으로 update에서도 언제나 동일한 결과값이 나오도록 한다.](#2-timedeltatime으로-update에서도-언제나-동일한-결과값이-나오도록-한다)

# Awake vs 생성자
### 1. 의문점이 발생한 코드
~~~c#
private void Start()
{
  AwakeTest obj = new AwakeTest(); 
}
~~~
  - 생성자 개념이라면 AwakeTest 클래스의 객체를 만들면 awake()와 start()가 호출되어야 하지만 호출되지 않는다.
    - AwakeTest script의 awake와 start는 동작하지 않는다.

### 2. 해답 (:exclamation:확실하지는 않다.)
- ![image](https://user-images.githubusercontent.com/55792986/197757984-d0ae28ec-9820-4b81-8f43-8f56f9b49986.png)
- 어떤 스크립트에서 다른 스크립트(클래스)의 객체를 생성하면 당연히 awake와 start가 호출될 것 이라 생각했다. 하지만 위의 설명과 같이 awake()는 스크립트를 컴포넌트로 갖고 있는 게임오브젝트 객체가 씬에 로드 될 때 최초로 한 번 호출되는 것이기 때문에 애당초 생성자와 다른 개념이다. 다시 말해 객체가 생성될 때 호출되는 생성자와는 다르게 씬에 로드 되어 생성 될 때 한 번 호출된다.
- 게임 오브젝트와 컴포넌트(스크립트)를 다르게 생각하면 될 것 같다.
  - :star:게임 오브젝트가 생성되면 그 안에 컴포넌트(스크립트)의 awake를 찾아서 실행시키고 start를 실행시킨다. 하지만 스크립트의 객체를 선언한다고 해서 발생하지는 않을 것 이다.


# Awake , Start는 왜 override를 붙이지 않는가? (검색의 한계를 느낀다...)
- 보통 우리는 Monobehaviour를 상속받는다. 그리고 start와 awake를 구현하는데 이 때 어떻게 override를 붙이지 않고 편하게 사용할 수 있을까?
- 비슷한 질문
  - 질문 : ![image](https://user-images.githubusercontent.com/55792986/185576507-b053df6d-e857-4961-a171-f42c3fc8f5e7.png)
  - 답변 : [Answer](https://forum.unity.com/threads/solved-start-and-update-methods-do-we-override-them-hide-them-or-what.404044/)
    - 외국 형님들의 답변을 봐도 잘 모르겠다.
  - 답변_2 : ![20220819_172858](https://user-images.githubusercontent.com/55792986/185578134-0aeceeed-175b-438f-85df-db0cdd2c0ead.png)
    - monobehaviour에서 가상함수로 구현하지 않았다.
      - 내 생각 : 그럼에도 불구하고 구현도 하지 않았을 것. 
    - 이해한 것 (틀릴 가능성이 높음) : 게임오브젝트에 달은 스크립트의 awake만 호출되고, 부모의 awake는 무시됨.
      - 만약 부모 것을 호출하고 싶다면 virtual과 override 그리고 base를 이용하여 상속받아 base로 호출해야 할 듯.

# Awake는 메시지다.
- ![image](https://user-images.githubusercontent.com/55792986/213359255-b399363f-a5f0-4155-88e2-4e79e29729f2.png)
  - Awake는 메시지고, 이는 메서드와 유사한 형태다.
  
# FixedUpdate vs Update의 프레임 관점
### 1. 프레임
- 60frame이면 1/60초 마다 한 번 진행하기 때문에 한 번의 프레임이 1000/60 ms다. 100frame이면 1/100초 마다 한 번 진행하기 때문에 한번의 프레임이 10ms가 된다.
- fixedupdate의 경우 고정된 프레임을 이용하지만 update의 경우 사용자의 환경에 따라 다른 프레임이 나온다.

### 2. Time.deltatime으로 update에서도 언제나 동일한 결과값이 나오도록 한다.
- Time.deltatime은 전 프레임 종료 ~ 현 프레임 종료 까지 걸린 시간이다. 그러므로 10프레임이면 1/10 seconds가 된다.
- 그러므로 사용자의 프레임이 얼마든 간에 time.deltatime을 곱하면 올바르게 값이 나온다.
- 코드로 증명하는 것이 빠를 것 이다.
~~~c#
    float my_time = 0f;
    Vector3 v1 = new Vector3(0, 0, 0);
    Vector3 v2 = new Vector3(0, 0, 0);

    private void FixedUpdate()
    {
        my_time += Time.deltaTime;
        v1 += new Vector3(1, 0, 0) * Time.deltaTime; 
        if(my_time >= 3f)
        {
            Debug.Log("v1" + v1); // (3,0,0)
            Debug.Log("v2" + v2); // (3,0,0)
            my_time = -123123f; //출력을 위한 초기화일뿐
        }

    }

    private void Update()
    {
        v2 += new Vector3(1, 0, 0) * Time.deltaTime;
    }
~~~


   