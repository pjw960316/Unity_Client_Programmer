# 목차
- [목차](#목차)
- [개요](#개요)
- [예제](#예제)

# 개요
- 자료구조에서 가장 쉽게 원하는 데이터를 뽑는 기법.

# 예제
~~~c#
public class Program
{
    // 예시로 사용할 데이터 클래스
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public static void Main()
    {
        List<Person> people = new List<Person>
        {
            new Person { Name = "Alice", Age = 30 },
            new Person { Name = "Bob", Age = 25 },
            new Person { Name = "Charlie", Age = 35 },
            new Person { Name = "David", Age = 28 }
        };

        // 원본 리스트를 변경하지 않고 자기 자신에게 할당하고 싶다면 ToList()를 사용합니다.
        people = people.Where(p => p.Age > 30).ToList();

        foreach (var person in people)
        {
            Console.WriteLine($"Name: {person.Name}, Age: {person.Age}"); 
        }
        /* Result
        Name: Charlie, Age: 35
        */
    }
}
~~~
- 
