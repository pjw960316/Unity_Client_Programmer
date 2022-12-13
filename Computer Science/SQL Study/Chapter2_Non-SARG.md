# 목차
- [목차](#목차)
- 

# 개요
- 가장 중요한 챕터

# 팁
- 불 필요한 열 참조 하지 않는다.
  - SELECT *을 지양한다.

# 용어
- SARG = Search Argument = 검색 인수
- Non-SARG = Non-Search Argument = 비 검색 인수
- where절과 join의 on절에는 조건식이 오며 이를 predicate라고 부릅니다.
  - :star:다 검색하지 말도록 predicate를 적는 규칙이 있다. 이게 SARG다.
  - optimizer가 최적화를 하는 과정에서 방해가 있으면 안 된다.

# :star: :star: :star:오늘 강의에서 가장 중요한 내용 : 쿼리의 금기 사항 : NON-SARG
- 부정형을 사용하지 않는다.
  - 부정형을 쓰면 optimizer가 긍정형으로 바꿔 버린다.
  - 조건은 '='이 가장 빠르고 효율적이다.