## :fire: GameObject는 UnityEngine.CoreModule에 존재하는 'Object' 클래스를 상속 받은 클래스다.
- :link:[GameObject](https://docs.unity3d.com/6000.0/Documentation/ScriptReference/GameObject.html)

#### [Object 아래에는 GameObject도 있고, Component도 있고, Monobehaviour도 있다.]

- <details>
  <summary> :point_up_2: 눌러서 이미지를 확인 합시다  </summary>

![alt text](./captures/20250717.png)

![alt text](./captures/20250717_1.png)

![alt text](./captures/20250717_2.png)

</details>

<br><br>

## :fire: Prefab Reference를 Non-MonoBehaviour Script에서 사용하려면 <br> MonoBehaviour를 상속 받은 script에서 presenter나 Manager를 통해 <br> 주입 받아야 한다.
- Prefab 생성에는 Prefeb reference가 핵심이다. 
> To instantiate a prefab at runtime, your code needs a reference to the prefab. To make this reference, you can create a public field of type in your code, then assign the prefab you want to use to this field in the **Inspector**.

<br><br>

## :fire: 직접 Destroy()로 파괴하는 것과 Scene이 변경되어 자동으로 파괴되는 것은 <br> '파괴'의 관점에서는 거의 비슷하다. <br> :fire: 둘 다 UnityEngine.Object(!= C# Object)를 상속 받는 Scene에 존재하는 모든 Instance들을 파괴한다.
- Scene이 변경될 때 DontDestroyOnLoad를 사용하지 않으면 Scene의 Object 들은 파괴된다.
  - > Do not destroy the target Object when loading a new Scene. (DontDestroyOnLoad의 정의)
  - 이걸 반대로 생각하면 Do Destroy가 되고, GameObject가 아닌 unityengine.Object를 모두 파괴시킨다.
> OnDestroy occurs when a Scene or game ends. Stopping the Play mode when running from inside the Editor will end the application. As this end happens an OnDestroy will be executed. Also, <ins>if a Scene is closed and a new Scene is loaded</ins> the OnDestroy call will be made.

<br><br>

## :fire: Object가 Destroy되면 <br> Object에 Component로 존재하는 Monobehaviour 상속 스크립트의 필드들은 <br> 모두 unity null(=fake null) 상태가 된다. 
- 정확히 <ins>필드</ins>들이 fake null이 됨을 강조하기 위해 따로 적었다. 

<br><br>

## :fire: unity null은 GC의 대상이다.