## :fireworks: 하얀 마네킹의 3D 참새가 실제 참새가 되는 방법 <br> :fire: Mesh == 흰 참새 마네킹 <br> :fire: Texture2D == 흰 참새 마네킹을 덮을 2D 참새 조각 그림 <br> :fire: Shader == 2D의 참새 조각 그림을 3D Mesh에 붙이는 알고리즘 <br> :fire: Material == 기본적인 3D Mesh를 완전한 참새 3D Mesh로 보이게 하는 것 <br> :fire: Material = Texture + Shader + @ 


<br>

## :fire: GLB는 “모델 + 머티리얼 + 텍스처 + 애니메이션 + 본/스킨 정보”를 한 파일에 묶을 수 있는 3D 패키지 파일
- .glb = glTF의 바이너리 버전
- 반드시 라이선스를 확인한다

<br>

## :fire: 특정 부분 material 안 보이게 하기
- none 처리 = 핑크색 깨짐
- default - skybox 처리 = 안 보임