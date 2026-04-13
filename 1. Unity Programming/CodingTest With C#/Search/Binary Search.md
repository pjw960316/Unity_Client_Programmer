## :fireworks: Binary Search
- **이진 탐색은 큰 범위에서 큰 범위(Half)를 가지치는 방식**
  - 가지치는 범위가 타당한지 검사하는 게 이진 탐색의 핵심이다.
  - 이 때 숫자 몇 개를 넣어본다.
    - P(1)이 성립함을 확인하고
    - P(n) → P(n+1)이 ‘반드시’ 성립할 수밖에 없음을 증명하면
    - 모든 n에 대해 P(n)이 성립합니다.

<br>

- **탐색 범위는 <ins>mid를 제외</ins>하고 계산한다.**
  - left  =  mid - 1
  - right  =  mid + 1
  - DFS / BFS 에서 자기 자신은 제외하고 탐색을 진행하는 것과 동일하다.

<br><br>

## :fire: Binary Search API 내부 구조
#### [MSDN 코드]
~~~c#
while (num <= num2)
{
	int median2 = GetMedian(num, num2);
	int num4;
	try
	{
		num4 = comparer.Compare(array.GetValue(median2), value);
	}
	catch (Exception innerException2)
	{
		throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_IComparerFailed"), innerException2);
	}
	if (num4 == 0)
	{
		return median2;
	}
	if (num4 < 0)
	{
		num = median2 + 1;
	}
	else
	{
		num2 = median2 - 1;
	}
}
~~~