# 목차
- [목차](#목차)
- [Awake vs 생성자](#awake-vs-생성자)
- [Awake , Start는 왜 override를 붙이지 않는가? (검색의 한계를 느낀다...)](#awake--start는-왜-override를-붙이지-않는가-검색의-한계를-느낀다)
- [FixedUpdate vs Update의 프레임 관점](#fixedupdate-vs-update의-프레임-관점)

# Awake vs 생성자
- ![20220817_131012](https://user-images.githubusercontent.com/55792986/185032968-bbd8461a-92cc-4c6e-9e7c-a20aa6947d65.png)
  - 어떤 스크립트에서 다른 스크립트(클래스)의 객체를 생성하면 당연히 awake와 start가 호출될 것 이라 생각했다. 하지만 위의 설명과 같이 awake()는 스크립트를 컴포넌트로 갖고 있는 게임오브젝트 객체가 씬에 로드 될 때 최초로 한 번 호출되는 것이기 때문에 애당초 생성자와 다른 개념이다. 다시 말해 객체가 생성될 때 호출되는 생성자와는 다르게 씬에 로드 되어 생성 될 때 한 번 호출된다.
~~~c#
void Start() //Awake()도 동일할 것
    {
        CannonMinion cannon = new CannonMinion();
        Debug.Log(cannon.test_value);
    }
~~~
- 유니티는 생성자 대신 Awake()를 권장한다.

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

# FixedUpdate vs Update의 프레임 관점
- 60frame이면 1/60초 마다 한 번 진행하기 때문에 한 번의 프레임이 1000/60 ms다. 100frame이면 1/100초 마다 한 번 진행하기 때문에 한번의 프레임이 10ms가 된다.
- FixedUpdate의 경우 고정 된 시간(Fixed Timestep)마다 해당 코드를 검사한다.
  - 보통 0.02초 마다 검사한다.
  - 모든 사용자마다 동일한 연산 결과가 나올 것 이다.
    - 이즈리얼의 q 속도는 모든 게임에서 동일해야 한다.
    - fixedupdate에 발사한 방향으로 0.02초마다 계산해서 모두 동일하게 한다.

   