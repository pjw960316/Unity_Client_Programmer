## :fire: Enum Type은 Class 밖에 선언하는 것이 일반적이다.
> Enums are types, just like classes. When you declare an enum inside a class, it's just a nested type. A nested enum just hides other enums with the same name that are declared in outer scopes, but you can still refer to the hidden enum through its fully qualified name (using the namespace prefix, in your example).

> The decision whether to declare a top level enum or a nested enum depends on your design and whether those enums will be used by anything other than the class. You can also make a nested enum private or protected to its enclosing type. :star:<ins>**But, top level enums are far more common.**</ins>
- Top level enums가 class 밖에 선언하는 것을 의미한다.

<br><br>

## :fire: enum은 int 기반의 value Type이다. 
~~~c#
private ESparrowState _currentSparrowState;
_currentSparrowState = _sparrowData.GetSparrowState();
~~~
- 여기선 deep-copy가 일어나서 원본을 변경하지 않는다.
- :link:[물론 이걸 다시 한 번 더 확인한다.](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/C%23%20Ver_2/05%EC%9E%A5_Primitive%2C%20Reference%2C%20and%20Value%20Type.md#fireworks-params%EB%A1%9C-%EC%A0%84%EB%8B%AC%ED%95%98%EB%8A%94-call-by-value-%EC%99%80-call-by-ref%EB%A5%BC-7%EA%B0%80%EC%A7%80-%EC%BC%80%EC%9D%B4%EC%8A%A4%EB%A1%9C-%EC%A6%9D%EB%AA%85%ED%95%B4-%EB%B3%B4%EC%95%98%EB%8B%A4)

<br><br>

## :fire: Enum은 Static처럼 사용된다. <br> :fire: IL로 까보면 알 수 있다.
- ![alt text](./capture/20250724_1.png)
- ![alt text](./capture/20250724_2.png)

<br><br>

## :question: Enum은 상속이 불가능하니, 많이 쪼개지말고 한 곳에 많이 모아서 저장하는 게 코드 분할을 막기 쉽다.
