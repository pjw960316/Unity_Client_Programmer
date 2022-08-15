# 목차
- [목차](#목차)
- [상속과 monobehaviour](#상속과-monobehaviour)


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
   