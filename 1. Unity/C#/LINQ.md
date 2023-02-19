# Ienumerable을 이해해서 왜 List 같은 애들에 대해 .Where(조건)을 사용할 수 있는지 이해해 보자
- ![20230217_112320](https://user-images.githubusercontent.com/55792986/219533485-59abf09a-638b-4f94-b6bf-66f26178eff9.png)
- ![image](https://user-images.githubusercontent.com/55792986/219533654-abf151bf-d3ea-4a86-a766-6c15b44c2613.png)
- ![20230217_112532](https://user-images.githubusercontent.com/55792986/219533793-efa6f62d-4c50-4142-8e3a-21646d57e211.png)
- ![image](https://user-images.githubusercontent.com/55792986/219533895-ad6ca1c8-6b1d-480f-b1ea-39f7fd89dac8.png)
  - C#의 Collection은 모두 Ienumerable을 구현하기 때문에 LINQ를 사용할 수 있다.
- ![image](https://user-images.githubusercontent.com/55792986/219535334-80b09158-667e-4d62-948b-620f699173e0.png)
  - 이건 다른 인터페이슨데 MoveNext()로 다른 열거자만으로 이동한다.

# Where
- https://ibocon.tistory.com/96