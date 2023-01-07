# 목차
- [목차](#목차)
- [기본 개념](#기본-개념)
- [Ienumerator \& Coroutine](#ienumerator--coroutine)

# 기본 개념
- 추후에 이전 문서 가져 오기.
  
# Ienumerator & Coroutine
- 코루틴에 대해서 '왜' '원리'를 생각해본 적이 많이 없다.
- ![image](https://user-images.githubusercontent.com/55792986/209253145-09590e9a-ba88-4d3b-87f6-5a356f9a2af8.png)
- Ienumerator를 제대로 이해해야 한다.
  - foreach 사용을 권장한다.
  - ![image](https://user-images.githubusercontent.com/55792986/209254591-b4cab3ff-20e0-405b-ae5b-8cba3f4b77f9.png)
  - 열거형 데이터이므로 열거형에 코루틴의 실행흐름을 저장한다고 생각한다. 그러므로 이동할 때 마다 위치를 기억할 수 있는 것 이다.
- 내 생각 : 일단 유니티에서는 startcoroutine을 이용해서 코루틴을 조절하지만 유니티가 아니면 movenext 같은 함수로 열거형을 관리한다.
- yield return은 코루틴을 쓰겠다는 것 이다.
  - 특수하다!