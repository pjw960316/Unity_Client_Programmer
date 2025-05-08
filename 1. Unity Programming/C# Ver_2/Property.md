## :fire: Property 사용 규칙
#### [Auto-Implemented Property : Validation과 복잡한 로직이 필요 없을 시에 사용한다.]
~~~c#
public int Capacity { get; private set; }
~~~
- Rider에서 Reformat & Clean-Up 하면 알아서 Auto-Property로 변경해준다.
- Backing-field가 없기 때문에 코드가 간결해 진다.
- 컴파일러가 backing-field를 암시적으로 생성한다.

<br>

#### [Basic Property : **Validation** 또는 복잡한 로직이 필요할 때 사용한다.]
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
  <summary>커피 마시기</summary>

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

## :fire: Property는 Field가 아닌 Method다. <br> :fire: Field 보다 조금이지만 overhead가 있을 수 밖에 없다. <br> :fire: 그러므로 property를 남용하지 말고 field로 충분하면 field로 사용한다.
> 저자는 생각보다 많은 사람들이 property를 필요 이상으로 남용한다는 것에 개인적으로 많이 놀랐다.

> Property는 메서드를 호출하는 것과 비교했을 때 성능상의 이점이 있는 것도 아니다.

<br><br>

## :fire: Field 대신 Property를 사용할 때 얻는 이점

## link
- [link](https://medium.com/@vsiromin/understanding-auto-implemented-properties-in-c-ed1b01620548)