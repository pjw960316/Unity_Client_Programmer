# 목차
- [목차](#목차)
- [스택과 힙에 관해서는...](#스택과-힙에-관해서는)
- [:star:value type vs reference type](#starvalue-type-vs-reference-type)
- [Reference Type에 관한 회고](#reference-type에-관한-회고)
- [리스트에서 조심해야 하는 개념](#리스트에서-조심해야-하는-개념)
  - [1. 참조에 대한 주의](#1-참조에-대한-주의)
  - [2. ToList()의 함정](#2-tolist의-함정)

# 스택과 힙에 관해서는...
- :link:[Link](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/Unity/Manage%20Memory.md#c-stack-memory-vs-heap-memory)

# :star:value type vs reference type
~~~c#
public class Move : MonoBehaviour
{
    private Vector3 cattle_r_position_value_type; //value type
    private Transform cattle_r_transform_reference_type; //reference type
    private const float MAGNITUDE = 0.2f;

    void Start()
    {
        cattle_r_position_value_type = transform.position; //value type / independence
        cattle_r_transform_reference_type = transform; //this is reference type... two pointer one heap memory
    }

    private void FixedUpdate()
    {
        //testValueType();
        testReferenceType();
    }

    private void testValueType()
    {
        cattle_r_position_value_type += new Vector3(1, 0, 0) * MAGNITUDE; //1,0,0 = normalized vector / magnitude - const
        Debug.Log("value type" + cattle_r_position_value_type + transform.position);
    }

    private void testReferenceType()
    {
        cattle_r_transform_reference_type.position += new Vector3(1, 0, 0) * MAGNITUDE; //1,0,0 = normalized vector / magnitude - const
        Debug.Log("reference type" + cattle_r_transform_reference_type.position + transform.position);
    }
}
~~~
- Value type 결과
  - ![image](https://user-images.githubusercontent.com/55792986/200546748-5bfa3d80-1b4f-4459-8fa2-22c538f46235.png)
    - 참조 형식이 아니기 때문에 cattle_r_position_value_type의 이동은 transform.position에 영향을 주지 않는다.
- Reference type 결과
  - ![image](https://user-images.githubusercontent.com/55792986/200546607-5c56eb4b-ec9c-45df-b096-00b6034ab2c2.png)
    - 참조 형식이므로 cattle_r_transform_reference_type.position의 이동은 transform.position에 영향을 준다.
    - 두 변수는 같은 힙의 메모리를 가리키고 있다.
- :star: Vector3도 reference type이라고 생각했으나 이는 struct라 힙에 할당하지 않고 스택에 할당하는 타입이다.
  - :link:[블로그](http://batmask.net/index.php/2020/04/17/414/)
- :star: 아래의 코드를 통해 참조를 반드시 해주어야 null reference가 나타나지 않는다.
~~~c#
cattle_r_transform_reference_type = transform;
~~~~

# Reference Type에 관한 회고
~~~c#
public class ReferenceExample
{
	public int number;
}
ReferenceExample a = new ReferenceExample(); 
var b = a;
~~~
  - 당연히 a의 멤버 변수의 값이 변경되면 b의 멤버 변수의 값도 변경된다.
    - 참조 타입이니까.


# 리스트에서 조심해야 하는 개념
## 1. 참조에 대한 주의
~~~ c#
void Main()
{
	AA obj = new AA(1,2,"hi");
	AA obj2 = obj;
	
	obj.a = 3; //value change
	obj.c = "hihihihi"; //reference change
	obj.d.Add(77);
	obj.d.Add(88);
	obj.d.Add(99);
	
	AA obj3 = new AA();
	
	//deep copy
	obj3.a = obj.a;
	obj3.b = obj.b;
	obj3.c = obj.c;
	obj3.d = obj.d;
	
	obj3.a = 1;
	obj3.d.Add(100);
	
	obj3.Dump();
	obj.Dump();
	
}

public class AA
{
	public int a;
	public int b;
	public string c;
	public List<int> d = new List<int>();
	
	public AA()
	{
	}
	public AA(int aa, int bb, string cc)
	{
		a = aa;
		b = bb;
		c = cc;
	}
}
~~~
- ![image](https://github.com/pjw960316/Unity_Client_Programmer/assets/55792986/cb66d839-9cdd-46c9-96f8-dd2c6ac6b3a9)
- obj3.d = obj.d와 같이 리스트를 복사할 때는 실제로 리스트의 참조만 복사되기 때문에 obj3와 obj가 같은 리스트를 공유하게 됩니다. 따라서 obj3.d.Add(100)을 호출하면 obj3의 리스트에 100이 추가되면서, obj의 리스트도 동일한 리스트를 참조하고 있으므로 obj의 리스트에도 100이 추가되는 결과가 발생합니다.


## 2. ToList()의 함정
- ![image](https://github.com/pjw960316/Unity_Client_Programmer/assets/55792986/3cb7f8ab-b7c6-4fc9-a2a0-a9ce3e6f4c62)
- ![image](https://github.com/pjw960316/Unity_Client_Programmer/assets/55792986/16436480-5ab8-4d31-9068-505451fc58d0)
- ![image](https://github.com/pjw960316/Unity_Client_Programmer/assets/55792986/2beb37d0-1d35-41ea-aea4-c07ce7c5b0f8)
- 결론
  - ![20230821_150339](https://github.com/pjw960316/Unity_Client_Programmer/assets/55792986/47cf3deb-2b3d-458d-a344-403f7358aac1)
  - 리스트 객체는 다른 힙 메모리지만, 리스트의 요소는 같은 힙 메모리를 참조.