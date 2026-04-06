## :one: LINQ에서 단순 순회 목적이면 .ToList() .ToDictionary()같은 즉시 실행 메서드 사용하지 않는다. <br> IEnumerator로 사용한다.
- .ToList() .ToDictionary() 새로운 객체를 힙에 할당한다.
- LINQ는 지연 실행을 전제로 한다.
  - :link:[Lazy Evaluation](https://persistent-hoverfly-e3c.notion.site/LINQ-2e629daafd6680b597b1c692199d4ffd?pvs=74)