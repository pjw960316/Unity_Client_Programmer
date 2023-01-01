# 목차
- [목차](#목차)
- [22/12/14 용어 학습](#221214-용어-학습)
- [개인적인 생각](#개인적인-생각)

# 22/12/14 용어 학습
- Shader
  - 과거 필기 P117
  - GPU의 연산
- Shader 최적화
  - 중요한 것
  - 얘도 결국 코드니까 이 코드를 잘 짜서 최적화를 하는 거 같다.
  - 어떤 기법이 있을 지 정도 배워보자.
- Transparent Shader
  - https://docs.unity3d.com/Manual/shader-TransparentFamily.html
  - 투명하게 물체가 나타나는 걸 표현하는 셰이더?
- 실시간 light
  - ![image](https://user-images.githubusercontent.com/55792986/207489511-9b2c60f3-a5e2-48d2-8d6f-fed5f6ead573.png)
- Z-test
  - Z-buffer가 해당 객체의 depth를 저장한다.
    - depth는 눈 과의 거리
  - 원근감?
  - depth를 계산해서 굳이 보이지 않아도 되는 걸 없애려고 하는 건가
  - 
- SRP (=Scriptable Render Pipeline)
  - ![20221214_112954](https://user-images.githubusercontent.com/55792986/207490641-feb9193c-5697-4818-9527-ea2e923cac0c.png)
- 히칭
  - CPU <-> GPU가 로딩 하다가 렉 걸리는 것 처럼 보이는 현상.
- Particle
  - ![image](https://user-images.githubusercontent.com/55792986/207490966-c54e10f4-922f-4cee-865d-b81b93517e08.png)
- Post Processing
  - ![image](https://user-images.githubusercontent.com/55792986/207491083-517480e5-d25b-4c6d-b6fc-7e716b667314.png)
    - 게임할 때 화면 그래픽 설정에서 본 것 같다.
# 개인적인 생각
- 수업 때 셰이더를 최적화 하는 걸 많이 듣지 않을까?
