# Generic
- T는 일단 해당 타입만 이다. 자식 까지 포함하지는 않는다.
- 그러나 유니티의 GetComponent를 이용하면 T에서 받고 GetComponent로 해당 스크립트에 접근하여 component를 가져오면 하위 타입으로 변환이 가능하다.

# where T : 클래스 이름
- Generic 타입이 해당 클래스의 자식이면 컴파일 에러를 남기지 않고 정상 동작하지만 그렇지 않으면 컴파일 에러를 낸다.
- Assert의 느낌.