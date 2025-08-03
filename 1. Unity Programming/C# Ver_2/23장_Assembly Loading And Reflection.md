## :fire: Type을 Reflection으로 알게 되면 Activator.CreateInstance을 사용해서 <br> Type에 맞는 객체를 생성할 수 있다. <br> :fire: 이 때 생성된 객체는 내부적으로 생성자를 호출한다. <br> :fire: 하지만 Instance Property를 호출하지 않기 때문에 반드시 명시적으로 연결해준다.  
> 'Activator.CreateInstance' is a standard .NET reflection method used to create instances of types dynamically
- ![alt text](./capture/20250803.png)
- ![alt text](./capture/20250803_2.png)

#### [CreateInstance 사용 예제]
<details>
  <summary> :point_up_2: 눌러서 코드를 확인 합시다  </summary>

~~~c#
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

    // Activator를 통해 만든 Instance를 Singleton Instance에 초기화 시켜준다.
    public void ConnectInstanceByActivator(IManager instance)
    {
        _instance = instance as T;
    }
}

// 호출부
private void CreateSingletonManager()
{
    var cSharpAssembly = AppDomain.CurrentDomain.GetAssemblies()
        .FirstOrDefault(asm => asm.GetName().Name == MAIN_ASSEMBLY);

    var managerTypes = cSharpAssembly?.GetTypes()
        .Where(type => typeof(IManager).IsAssignableFrom(type) && type.IsClass)
        .ToList();

    if (managerTypes != null)
    {
        foreach (var type in managerTypes)
        {
            // Note
            // 여기서 각각의 생성자를 호출하며 Type에 맞는 Instance를 생성한다.
            // 그리고 연결을 ConnectInstanceByActivator
            var objectTypeInstance = Activator.CreateInstance(type);

            if (objectTypeInstance is IManager manager)
            {
                manager.ConnectInstanceByActivator(manager);
                manager.SetModel(_allModels);
                manager.Initialize();
            }
        }
    }
}
~~~

</details>