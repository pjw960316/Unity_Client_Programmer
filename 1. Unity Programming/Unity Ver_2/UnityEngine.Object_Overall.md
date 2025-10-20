## :fire: GameObject는 UnityEngine.CoreModule에 존재하는 'Object' 클래스를 상속 받은 클래스다.
- :link:[GameObject](https://docs.unity3d.com/6000.0/Documentation/ScriptReference/GameObject.html)

#### [Object 아래에는 Component도 있고, GameObject도 있고, Monobehaviour도 있다.]

<details>
  <summary> :point_up_2: 눌러서 이미지를 확인 합시다  </summary>

![alt text](./captures/20250717.png)

![alt text](./captures/20250717_1.png)

![alt text](./captures/20250717_2.png)

</details>

<br><br>

## :fire: GetComponent<T>는 T와 완전히 매치되는 컴포넌트 or <ins>T의 Derived Type</ins>의 컴포넌트를 찾아준다.

<br><br>

## :fire: Prefab Reference를 Non-MonoBehaviour Script에서 사용하려면 <br> MonoBehaviour를 상속 받은 script에서 presenter나 Manager를 통해 <br> 주입 받아야 한다.
- Prefab 생성에는 Prefeb reference가 핵심이다. 
> To instantiate a prefab at runtime, your code needs a reference to the prefab. To make this reference, you can create a public field of type in your code, then assign the prefab you want to use to this field in the **Inspector**.

<br><br>