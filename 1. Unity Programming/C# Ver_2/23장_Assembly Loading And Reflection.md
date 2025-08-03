## :fire: Activator.CreateInstance로 Type을 Reflection으로 알게 되면 <br> Type에 맞는 Instance를 생성할 수 있다. <br> :fire: 이 때 생성된 Instance는 내부적으로 생성자를 호출한다. <br> :fire: 하지만 Instance Property를 호출하지 않기 때문에 반드시 명시적으로 연결해준다.  
> 'Activator.CreateInstance' is a standard .NET reflection method used to create instances of types dynamically
- ![alt text](./capture/20250803.png)
- ![alt text](./capture/20250803_2.png)

~~~c#
// Note
// 공통로직을 담는 메서드가 굳이 IManager를 상속 받을 필요 없다.
public abstract class ManagerBase<T> where T : class, new()
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }

            return _instance;
        }
    }

    // Note
    // Activator를 통해 만든 Instance를 Singleton Instance에 초기화 시켜준다.
    public void ConnectInstanceByActivator(IManager instance)
    {
        _instance = instance as T;
    }
}
~~~