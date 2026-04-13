## :fire: LINQ는 IEnumerable 기반 반복 체인이다.
> which supports the generic IEnumerable<T> interface. This fact means it can be queried with LINQ. A query is executed in a foreach statement, and foreach requires IEnumerable or IEnumerable<T>.

<br><br>

## :fire: Lazy(=deferred -> MSDN은 이걸 사용) Evaluation은 LINQ의 실제 실행이 특정 구문을 만날 때 까지 지연 되는 것을 의미한다.<br> :one: LINQ의 본질인 순회를 담당하는 foreach 구문을 만나면 즉시 실행된다. <br> :two: .ToList() 같은 메서드를 만나면 즉시 실행된다. <br> :three: Count, Max, Average, First 같은 메서드를 만나면 즉시 실행된다.
> The actual execution only occurs when a materialization method is invoked, such as .ToList(), .ToArray(), or when iterating with foreach.

> The LINQ to Objects implementations of the standard query operator methods execute in one of two main ways: immediate or deferred. 
- LINQ Chain 과정에서 ToList 또는 ToDictionary로 변환할 필요가 없다.
~~~c#
 var key = dict.OrderBy(kv => kv.Value.Item1)
                .ThenByDescending(kv => kv.Value.Item2)
                .FirstOrDefault().Key;

dict.Remove(key);
~~~
- IEnumerable<T>의 확장메서드로 구성된 Linq의 성질을 이용한다. 
- OrderBy와 ThenByDescending 모두 return Type이 IEnumerable<T>의 파생 타입이므로 FirstOrDefault를 사용할 수 있다.
- LINQ는 계속 IEnumerable<T> 상태로 평가를 미뤄두다가, ToList() / First() 같은 terminal operation이 호출되는 순간에 비로소 평가된다
- LINQ 과정에서 데이터 가공은 IEnumerable<T>에서 계속 진행하고, 최종 판단해서 Concrete Container로 사용하라는 의도다.
- :airplane:[MSDN LINQ 더 자세히](https://learn.microsoft.com/en-us/dotnet/csharp/linq/get-started/introduction-to-linq-queries)

<br><br>

## :fireworks: 왜 Lazy Evaluation을 하는가?
- select, where 같은 연산을 할 때 매 번 새로운 자료구조를 할당하지 않는다.
- 즉시 실행 구문을 만나기 전 까지는 자원을 사용하지 않는다. 그러므로, 불필요한 연산을 하지 않는다.

<br><br>

## :fire: foreach 구문 내부에서 단순 순회를 하는 경우 .ToList() 대신, IEnumerable<T>로 이용한다. <br> :fire: .ToList()를 통한 새로운 리스트 할당을 막기에 메모리 관점에서 성능 이득이 있다.