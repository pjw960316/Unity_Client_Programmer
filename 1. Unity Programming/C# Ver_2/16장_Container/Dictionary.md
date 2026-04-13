## :fire: ContainsKey를 회피하기 위해 <br> value(int)를 0으로 초기화 하는 방식은 유용하다.
~~~c#
void Main()
{
    var dict = new Dictionary<char,int>();
    
    for(char tmp = 'a'; tmp<='z'; tmp++)
    {
        dict[tmp] = 0; 
    }
    
    dict['c'].Dump(); // result : 0
}
~~~
- 메모리가 낭비되고, Dictionary의 불연속성을 위배하기 때문에 좋은 방식은 아니다.
- 그러나, 가독성(ContainsKey 없음)과 안전성에서 매우 뛰어나다.

<br><br>

## :fire: Dictionary<key , Dictionary<k,v>>은 가독성 및 구현이 최악이므로 되도록 사용하지 않는다. <br> 자료구조를 재설계 하도록한다.
- 일단 동작하자 마인드로 설계할 때 이런 자료구조를 선택한다.
- dict에 dict는 변수 네이밍도 어렵고 순회 네이밍도 답이 없다.

<br><br>

## :fire: 해시 기반 자료구조다. <br> 그래서 조회한 Key의 이전 key 또는 다음 Key는 알 수 없다.
- 정렬의 개념도 없다.
- 정렬이 필요하다면 두 가지 방식이 있다.
    - SortedDictionary를 이용한다.
    - LINQ의 OrderBy 또는 OrderByDescending을 이용한다.

<br><br>

## :fire: TryGetValue()가 좋지만 ContainsKey()를 이용해서 값을 찾는다.  ⇒ O(1) * 2
~~~c#
if (dict.ContainsKey(key))
{
    dict[key] += 1;
}
else
{
    dict[key] = 1;
}
~~~
- 개인적으로 더 가독성이 좋다고 생각한다.
- Linq의 Any(kv ⇒ kv.Key == 2)처럼도 가능하다

<br><br>

## :fire: 모든 Value에 중복이 없다고 가정할 때, <br> 역방향 Dictionary를 추가로 생성하면 Value로 Key를 조회 할 수 있다.

<br><br>

## :fire: 문자열 LookUp의 경우 엄밀히 말하면 O(L) + O(1)이다.
- 문자열의 GetHashCode() 자체가 O(L)이기 때문이다.
- 그러나 문자열의 길이가 20 이상이 되지 않으면 성능의 큰 이슈는 없다.

<br><br>

## :fire: Key에 `float`, `double`, `decimal`을 Dictionary key로 쓰는 건 가능하지만 권장하지 않는다.
- float과 double은 부동소수점이고 이는 근사값이다.
- 그러므로 key의 같음을 비교할 때 정확하지 않을 수 있다.
- decimal이 128비트를 사용하므로 float과 double보다 정밀하지만 이 또한 100% key 비교에서 같음을 보장 할 수는 없다.
- :airplane:[Should I use Decimal type as keys in a Dictionary?](https://stackoverflow.com/questions/14693561/should-i-use-decimal-type-as-keys-in-a-dictionary)
