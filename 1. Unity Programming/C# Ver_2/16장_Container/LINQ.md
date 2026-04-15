## :fire: LINQ는 IEnumerable 기반 반복 체인이다. <br> :fire: LINQ는 IEnumerable<T>을 구현한 객체에서만 사용 할 수 있다.
> which supports the generic IEnumerable<T> interface. This fact means it can be queried with LINQ.

> A query is executed in a foreach statement, and foreach requires IEnumerable or IEnumerable<T>.

> You can enable LINQ querying of in-memory data in two ways. If the data is of a type that implements IEnumerable<T>, query the data by using LINQ to Objects. If it doesn't make sense to enable enumeration by implementing the IEnumerable<T> interface, define LINQ standard query operator methods either in that type or as extension members for that type.
- IEnumerable을 구현하지 않은 클래스의 경우 직접 구현해서 사용하라고 한다. 직접 IEnumerable을 구현하는 경우는 언젠가 확인하면 내용을 추가하자.   

<br><br>

## :fire: Lazy(=deferred -> MSDN은 이걸 사용) Evaluation은 <br> LINQ의 실제 실행이 특정 구문을 만날 때 까지 지연 되는 것을 의미한다.<br> :one: LINQ의 본질인 순회를 담당하는 foreach 구문을 만나면 즉시 실행된다. <br> :two: .ToList() 같은 메서드를 만나면 즉시 실행된다. <br> :three: Count, Max, Average, First 같은 메서드를 만나면 즉시 실행된다.
> The actual execution only occurs when a materialization method is invoked, such as .ToList(), .ToArray(), or when iterating with foreach.

> The LINQ to Objects implementations of the standard query operator methods execute in one of two main ways: immediate or deferred. 
- IEnumerable<T>의 확장메서드로 구성된 Linq의 성질을 이용한다. 
- OrderBy와 ThenByDescending 모두 return Type이 IEnumerable<T>의 파생 타입이므로 FirstOrDefault를 사용할 수 있다.
- LINQ는 계속 IEnumerable<T> 상태로 평가를 미뤄두다가, ToList() / First() 같은 terminal operation이 호출되는 순간에 비로소 평가된다
- LINQ 과정에서 데이터 가공은 IEnumerable<T>에서 계속 진행하고, 최종 판단해서 Concrete Container로 사용하라는 의도다.
- :airplane:[MSDN LINQ 더 자세히](https://learn.microsoft.com/en-us/dotnet/csharp/linq/get-started/introduction-to-linq-queries)

<br><br>

## :fireworks: 왜 Lazy Evaluation을 하는가?
- select, where 같은 연산을 할 때 매 번 새로운 자료구조를 할당하지 않는다.
- 즉시 실행 구문을 만나기 전 까지는 자원을 사용하지 않는다. 그러므로, 불필요한 연산을 하지 않는다.
~~~c#
 var key = dict.OrderBy(kv => kv.Value.Item1)
                .ThenByDescending(kv => kv.Value.Item2)
                .FirstOrDefault().Key;

dict.Remove(key);
~~~

<br><br>

## :fire: ToList()처럼 새로운 컨테이너를 힙에 할당하는 LINQ 구분은 <br> 자주 콜 되는 구문에서 사용하면 메모리가 낭비된다. <br> :fire: 이런 경우에는 IEnumerable Generic 변수를 할당해서 참조시킨다. <br> :fireworks: 아래 코드를 읽어본다. 
~~~c#
var virusList = new List<(int r, int c, int virusNum)>();
IEnumerable <(int r, int c, int virusNum)> viruses;

// 엄청난 콜
for (int t = 0; t < 1,000,000; t++)
{
    UpdateVirusList();
    MoveVirus();
}

void UpdateVirusList()
{    
    virusList.Clear();
            
    for (int r = 1; r <= n; r++)
    {
        for (int c = 1; c <= n; c++)
        {
            for (int idx = 0; idx < 4; idx++)
            {
                if (arr[r + path[idx].r, c + path[idx].c] == 0)
                {
                    virusList.Add((r, c, arr[r, c]));
                    break;
                }
            }
        }
    }

    viruses = virusList
        .OrderBy(threePair => threePair.virusNum); // 버퍼로 메모리를 사용하긴 하나, 금방 해제된다.

    // 나쁜 코드
    /* virusList = virusList
        .OrderBy(threePair => threePair.virusNum)
        .ToList();
    */
}
        
void MoveVirus()
{
    foreach (var threePair in viruses) //foreach는 IEnumerable의 기능
    {
        var r = threePair.r;
        var c = threePair.c;
        var virusNum = threePair.virusNum;

        for (int idx = 0; idx < 4; idx++)
        {
            var newR = r + path[idx].r;
            var newC = c + path[idx].c;

            if (arr[newR, newC] == 0)
            {
                arr[newR, newC] = virusNum;
            }
        }
    }
}
~~~
- .ToList()를 했다면 1,000,000개의 List가 힙에 생성된다. 그러나 필요한 건 갱신된 리스트 뿐이다.
- 그러므로 IEnumerable로 변경된 컨테이너를 참조하고, 매 번 갱신 때마다 리스트를 clear()하면 된다.