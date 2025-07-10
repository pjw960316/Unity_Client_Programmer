## :fire: GameObject는 UnityEngine.CoreModule에 존재하는 'Object' 클래스를 상속 받은 클래스다.
- :link:[GameObject](https://docs.unity3d.com/6000.0/Documentation/ScriptReference/GameObject.html)

<br><br>

## :fire: Prefab Reference를 Non-MonoBehaviour Script에서 사용하려면 <br> MonoBehaviour를 상속 받은 script에서 presenter나 Manager를 통해 <br> 주입 받아야 한다.
- Prefab 생성에는 Prefeb reference가 핵심이다. 
> To instantiate a prefab at runtime, your code needs a reference to the prefab. To make this reference, you can create a public field of type in your code, then assign the prefab you want to use to this field in the **Inspector**.
