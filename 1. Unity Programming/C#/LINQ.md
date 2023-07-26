# 목차
- [목차](#목차)
- [LINQ 결과물의 참조 여부](#linq-결과물의-참조-여부)
- [Lazy Evaluation](#lazy-evaluation)

# LINQ 결과물의 참조 여부
~~~c#
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<int> originalList = new List<int> { 1, 2, 3, 4, 5 };

        List<int> evenNumbersList = originalList.Where(x => x % 2 == 0).ToList(); 
        // origianlList = originalList.Where(x => x % 2 == 0).ToList(); 이렇게 해서 재사용을 해도 된다!

        originalList[0] = 10;

        foreach (int num in originalList)
        {
            Console.Write(num + " "); // Output: 10 2 3 4 5
        }

        foreach (int num in evenNumbersList)
        {
            Console.Write(num + " "); // Output: 2 4
        }
    }
}
~~~
- ![image](https://github.com/pjw960316/Unity_Client_Programmer/assets/55792986/90da06b1-4536-4e89-bbdf-d5d9b2bec0c4)
- ![20230726_220209](https://github.com/pjw960316/Unity_Client_Programmer/assets/55792986/a675609d-5dbf-4e93-a9c2-6bf356c40235)
- 결론적으로 새로운 리스트가 할당 되는 것.
- 아래의 Lazy Evaluation과 차이점은 .toList()를 하는 것!
  - 아직 Lazy Evaluation의 특성을 활용해서 최적화를 해 본 경험은 없다.

# Lazy Evaluation
~~~c#
class Program
{
    static void Main()
    {
        List<int> originalList = new List<int> { 1, 2, 3, 4, 5 };

        IEnumerable<int> evenNumbersQuery = originalList.Where(x => x % 2 == 0);

        foreach (int num in evenNumbersQuery)
        {
            Console.Write(num + " "); // Output: 2 4
        }
        Console.Write("\n");

        originalList[0] = 10;

        foreach (int num in originalList)
        {
            Console.Write(num + " "); // Output: 10 2 3 4 5
        }
        Console.Write("\n");

        foreach (int num in evenNumbersQuery)
        {
            Console.Write(num + " "); // Output: 10 2 4
        }
    }
}
~~~
- 마지막 Output이 10 2 4가 나온 이유는 evenNumbersQuery는 실제로 연산을 해야 할 순간까지 계산을 하지 않는 Linq의 성질 때문이다.
- 마지막 output을 구하는 순간에 IEnumerable<int> evenNumbersQuery = originalList.Where(x => x % 2 == 0);의 연산을 제대로 수행한다.
  - 제대로 라는 말이 모호하지만 이게 이해하기 제일 쉽다.
  - 그러므로 10,2,3,4,5에 대해서 LINQ를 수행한다.
- 프로그래머가 실수하기 정말 좋은 특성이라고 생각한다. 최적화에는 용이하지만.