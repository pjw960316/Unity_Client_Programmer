## :fire: 모든 C# Container는 Enumerator를 들고 있기 때문에 단방향 순회는 가능하다.

<br><br>

## :fire: LINQ가 성능 이슈는 있지만 <br> 리팩터링 전에는 LINQ로 일단 구현을 해서 동작을 확인한다. <br> :fire: 물론, call 횟수가 적어 성능이 중요하지 않은 메서드에서는 <br> 가독성을 위해 사용해도 된다고 생각한다. 

<br><br>

## :fire: Private Container를 외부에서 접근하도록 property를 만들 때 <br> ImmutableDictionary<Tkey , TValue> 형식으로 만들자.
~~~c#
public ImmutableDictionary<EAlarmButtonType, float> AlarmTimeDictionary => _alarmTimeDictionary.ToImmutableDictionary();
~~~
- 외부에서의 Add,Remove 방어
- :link:[[예제_2 : Immutable한 readonly Container는 Immutable로 만들어 준다.](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/C%23%20Ver_2/06%EC%9E%A5_Type%20and%20Member%20Basics%20(%3DClass).md#%EC%98%88%EC%A0%9C_2--immutable%ED%95%9C-readonly-container%EB%8A%94-immutable%EB%A1%9C-%EB%A7%8C%EB%93%A4%EC%96%B4-%EC%A4%80%EB%8B%A4)

<br><br>

## :fire: Dictionary 에서 기본적으로는 dict[key] = value로 element를 추가한다. <br> :fire: 덮어 쓰기가 싫을 때는 TryAdd()를 이용한다.

<br><br>

## :fire: Container에서 Type에 맞는 element 찾기 : OfType
~~~c#
var alarmData = _modelList.OfType<AlarmData>().FirstOrDefault();
~~~