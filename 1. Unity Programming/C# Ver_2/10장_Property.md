## :fire: Property 사용 규칙
#### :large_blue_diamond: [Auto-Implemented Property : Validation과 복잡한 로직이 필요 없을 시에 사용한다.]
~~~c#
public int Capacity { get; private set; }
~~~
- Rider에서 Reformat & Clean-Up 하면 알아서 Auto-Property로 변경해준다.
- Backing-field가 없기 때문에 코드가 간결해 진다.
- 컴파일러가 backing-field를 암시적으로 생성한다.
- Backing-field를 직접 변경하지 말고, Property를 변경해서 Set이 돌게 한다.

<br>

#### :large_blue_diamond: [Basic Property : **Validation** 또는 복잡한 로직이 필요할 때 사용한다.]
~~~c#
private int capacity; 
public int Capacity
{
    get
    {
        ShowToastMessage();
        
        return capacity;
    }
    private set
    {
        if (capacity < 100)
        {
            "drink all".Dump();
            capacity = 0;
        }
        else
        {
            capacity = value;
        }
    }
}
~~~

<br>

#### [Property 사용 예제 - 커피 마시기]
<details>
  <summary>:point_up_2: 커피 마시기 코드 보기</summary>

~~~c#
public class Coffee
{
	private const int DEFAULT_COFFEE_CAPACITY = 300;
	
	public Coffee()
	{
		capacity = DEFAULT_COFFEE_CAPACITY;
	}
	
	private int capacity; 
	public int Capacity
	{
		get
		{
			ShowToastMessage();
			
			return capacity;
		}
		private set
		{
			if (capacity < 100)
			{
				"drink all".Dump();
				capacity = 0;
			}
			else
			{
				capacity = value;
			}
		}
	}
	
	private void ShowToastMessage()
	{
		"GetCapacity".Dump();
	}
	
	public void DrinkedCoffee()
	{
		Capacity -= 100; //여기서도 Get이 호출된다.
	}
}

void Main()
{
	Coffee americano = new Coffee();
	americano.DrinkedCoffee();

	int myAmericanCapacity = americano.Capacity;
	myAmericanCapacity.Dump(); 
}
/*
RESULT

GetCapacity
GetCapacity
200
*/
~~~
</details>

> 만약 속성을 정의한다면, get과 set 접근자를 모두 정의하는 것이 좋다.
- DrinkedCoffee() 메서드에서도 Get이 호출되는 것을 주의한다.

<br><br>

## :fire: property를 남용하지 말고 필요할 때만 사용한다. <br> Property는 Field가 아닌 Method다. <br> Field 보다 조금이지만 overhead가 있을 수 밖에 없다.
> 저자는 생각보다 많은 사람들이 property를 필요 이상으로 남용한다는 것에 개인적으로 많이 놀랐다.

> Property는 메서드를 호출하는 것과 비교했을 때 성능상의 이점이 있는 것도 아니다.

<br><br>

## :question::question: 그러면 언제 Property 써요? <br> 1. get은 외부에서도 가능하지만 set은 내부에서만 가능하게 하고 싶을 때 <br> 2. 값에 대한 변경사항과 유지보수가 필요할 만큼 중요한 멤버일 때
![alt text](./capture/20250512.png) 

<br><br>

## Reference
- [link](https://medium.com/@vsiromin/understanding-auto-implemented-properties-in-c-ed1b01620548)