# 목차
- [목차](#목차)
- [스택과 힙에 관해서는...](#스택과-힙에-관해서는)
- [:star:value type vs reference type](#starvalue-type-vs-reference-type)
- [Reference Type에 관한 회고](#reference-type에-관한-회고)

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