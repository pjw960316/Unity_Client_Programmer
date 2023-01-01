# 목차
- [목차](#목차)
- [Stack과 Queue는 왜 순회를 할 수 없을까?](#stack과-queue는-왜-순회를-할-수-없을까)
- [Deque은 Queue와 Stack의 상위호환 이다.](#deque은-queue와-stack의-상위호환-이다)
  
# Stack과 Queue는 왜 순회를 할 수 없을까?
- **Iterator를 제공하지 않는다.**
- 원소를 pop()하면서 출력하면 순회를 할 수 있으나 원소들을 제거하는 작업이다.
  - 다시 말해 두 자료구조는 순회에 최적화 된 자료가 아니다.
  - 그래서 Deque을 사용한다.


# Deque은 Queue와 Stack의 상위호환 이다.
- [Reference](https://blockdmask.tistory.com/73)
- queue나 stack은 순회가 불가능 하기 때문에 순회가 필요한 경우 Deque으로 대체해서 푼다.