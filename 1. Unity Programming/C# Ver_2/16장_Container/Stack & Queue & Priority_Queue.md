## :fireworks: Stack & Queue와 List를 선택하는 기준
- **Stack & Queue가 시간복잡도 관점에서는 List보다 빠르다.**
  - Stack & Queue는 Enqueue 와 Dequeue가 O(1)로 매우 빠르다.
  - List는 Add는 O(1)이지만, Insert와 RemoveAt이 O(n)으로 느리다.

<br>

- **하지만 Stack & Queue는 유연하게 중간 원소 조회가 쉽지 않기 때문에 복잡한 기능이 필요하면 List를 이용한다.**

<br><br>

## :bangbang: Stack & Queue 주의사항
- Count가 0일 때를 항상 주의하고 검사한다.

<br><br>

## Priority Queue