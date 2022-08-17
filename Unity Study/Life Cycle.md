# 목차
- [목차](#목차)
- [상속과 monobehaviour](#상속과-monobehaviour)
- [Awake vs 생성자](#awake-vs-생성자)


# 상속과 monobehaviour
~~~
public class Structure : MonoBehaviour
{
    public int money;

    private void Awake()
    {
        Debug.Log("call awake");
    }
    private void Start()
    {
        Debug.Log("call start");
        money = 30;
    }
    protected void onDie()
    {
        Debug.Log(money);
        //죽었을 때 호출된다.
        //죽인 객체에게 돈을 준다.
        //다른 객체의 멤버 변수 값을 변경한다.
    }
}
~~~
~~~
public class CannonMinion : Structure
{
    private void Start()
    {
        money = 60;
        onDie();
    }
}
~~~
- CannonMinion만 GameObject에 할당한다. 당연히 Structure는 어떠한 객체에도 할당하지 않는다.
- CannonMinion은 Awake()와 Start()를 모두 Structure를 통해 호출된다. Awake()는 Structure의 것으로 호출되지만 Start()는 오버라이딩 했기 때문에 CannonMinion의 것으로 호출된다.

# Awake vs 생성자
- ![20220817_131012](https://user-images.githubusercontent.com/55792986/185032968-bbd8461a-92cc-4c6e-9e7c-a20aa6947d65.png)
  - 어떤 스크립트에서 다른 스크립트(클래스)의 객체를 생성하면 당연히 awake와 start가 호출될 것 이라 생각했다. 하지만 위의 설명과 같이 awake()는 스크립트를 컴포넌트로 갖고 있는 게임오브젝트 객체가 씬에 로드 될 때 최초로 한 번 호출되는 것이기 때문에 애당초 생성자와 다른 개념이다. 다시 말해 객체가 생성될 때 호출되는 생성자와는 다르게 씬에 로드 되어 생성 될 때 한 번 호출된다.
~~~
void Start() //Awake()도 동일할 것
    {
        CannonMinion cannon = new CannonMinion();
        Debug.Log(cannon.test_value);
    }
~~~
- 유니티는 생성자 대신 Awake()를 권장한다.
   