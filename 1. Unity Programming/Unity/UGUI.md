# 목차
- [목차](#목차)
- [과거 문서는 올바르게 연습하지 않고 정리한 느낌이 강하다. 이 문서를 이용해라.](#과거-문서는-올바르게-연습하지-않고-정리한-느낌이-강하다-이-문서를-이용해라)
- [UGUI](#ugui)
- [Canvas](#canvas)
- [Canvas Group](#canvas-group)
- [Canvas Scaler](#canvas-scaler)
- [:star::star::star:RectTransform:star::star::star:](#starstarstarrecttransformstarstarstar)
    - [1. 붉은 Anchor](#1-붉은-anchor)
    - [2. 파란 Anchor : Stretch](#2-파란-anchor--stretch)
    - [3. Pivot \& Anchor](#3-pivot--anchor)
- [Layout Group](#layout-group)
- [Layout Element](#layout-element)
- [Graphic Raycaster](#graphic-raycaster)
- [EvenetSystem](#evenetsystem)
- [:star::star::star:Layer \& Sorting Layer \& Order In Layer:star::star::star:](#starstarstarlayer--sorting-layer--order-in-layerstarstarstar)
    - [1. Layer](#1-layer)
    - [2. Sorting Layer](#2-sorting-layer)
    - [3. Order in (Sorting) Layer](#3-order-in-sorting-layer)
- [TextMeshPro (=TMP)](#textmeshpro-tmp)
- [Image](#image)
- [Content Size Fitter](#content-size-fitter)
- [RectTransform 변경하기](#recttransform-변경하기)
- [RectTransform](#recttransform)
- [AnchoredPosion Vs LocalPosition](#anchoredposion-vs-localposition)
- [Anchor와 Pivot을 잘 정해야 비율 계산이 쉽다.](#anchor와-pivot을-잘-정해야-비율-계산이-쉽다)
- [참조](#참조)

# 과거 문서는 올바르게 연습하지 않고 정리한 느낌이 강하다. 이 문서를 이용해라.

# UGUI
- UI는 이걸 사용하자.
- NGUI는 UGUI 이전의 기술
- ![image](https://user-images.githubusercontent.com/55792986/217409459-b9547e33-9899-4ab8-9fac-dcf42a7ecd02.png)

# Canvas
- 하나의 scene에 여러 개 배치가 가능하다.
  - UI를 만들 때 당연히 많은 캔버스를 만들 것 이고, 캔버스의 자식으로 캔버스를 놓을 수 있다.
    - 자식 캔버스는 render mode가 없다.
  - ![20230208_111641](https://user-images.githubusercontent.com/55792986/217411627-fb60e8ea-3409-48cd-ba05-814a0a9283b9.png)
  - 캔버스는 게임 오브젝트고 canvas component를 갖는 애다.
- 모든 UI는 캔버스의 자식이어야 한다.
- :star: **'UI == Canvas == UI 전문 GameObject == 객체'** 으로 이해해도 무방하다고 생각한다.
- Render Mode
  - Screen Space - Camera
    - UI를 전문적으로 다루는 카메라를 만들고 이를 적용한다.
    - 카메라와 UI의 거리에 따라 고정적인 거리를 갖고 움직인다.
- **캔버스 하위의 동일 계층 UI의 그려지는 순서**
  - 같은 계층이라면 맨 위에 있는 UI 부터 그리므로 가장 아래에 깔린다.
  
# Canvas Group
- 나의 하위 UI 객체들은 모두 나의 명령(alpha, interactable, blocks raycasts)을 거스르지 않고 수행한다.
- ![image](https://user-images.githubusercontent.com/55792986/217466215-ee730499-7f51-4971-b66e-a8c930579fe4.png)
    - Blocks Raycasts를 조금 더 제대로 이해해보자
      - 마우스의 클릭은 광선을 발사한다.
      - 이 광선이 어떤 객체와 부딪혀 충돌을 일으키면 클릭이 발생한다.
      - 하지만 이걸 블록시키면 canvas group 하위의 모든 UI 객체는 광선을 통과시킨다.
- 하나의 거대한 UI는 많은 캔버스로 구성되어 있다. 각각의 캔버스에 canvas group을 달아주면 해당 계층의 하위는 모두 컨트롤 할 수 있다.
- https://wergia.tistory.com/177
  
# Canvas Scaler
- ![20230208_120415](https://user-images.githubusercontent.com/55792986/217418316-584d25e6-4956-4791-bed0-2fc68ded69ea.png)
  - 핸드폰마다 화면크기가 다르기 때문에 이걸 많이 사용하게 된다.
  
# :star::star::star:RectTransform:star::star::star:
- UI에서 전문적으로 쓰이는 Transform 이다.
  - Transform을 상속받는다.
- Position과 Anchor 그리고 Pivot이 기존의 Transform과 다른 부분이다. 이를 제대로 이해해야 한다.
  - Anchor를 이해하면 나머지를 모두 이해하기 쉽다.
- :star:**기본적으로 이를 사용하는 이유는 제각각인 핸드폰의 해상도에 대응하기 위해서다.** 
  - 앵커를 이용하면 어떤 해상도에서도 개발자가 원하는 위치에 UI를 위치 시킬 수 있다. 
- [Youtube](https://www.youtube.com/watch?v=A0prWX3afwg)

### 1. 붉은 Anchor
- ![20230208_182432](https://user-images.githubusercontent.com/55792986/217488809-af2f57d2-6da1-4da8-b95d-342b9a442127.png)
- 앵커는 무조건 자신의 직속 부모의 UI에 맞춰진다.
- :star:**항상 잘못 생각했던 오류 : 나의 앵커를 이동해도 앵커 표시(4개 삼각형)는 움직이는데 내 UI 자체는 움직이지 않았다. 그건 유니티가 자동으로 PosX와 PosY값을 변경해서 움직이지 않도록 하기 때문이다. 그러므로 PosX와 PosY를 0으로 변경해서 확인해보자.**
- 앵커가 Left-Top 이라면 어떤 해상도로 변경해도 해당 UI는 부모 UI 기준 왼쪽 상단에 항상 고정된다.
- Width와 Height로 값을 조절할 수 있는데 이 크기를 키워서 부모 UI의 크기보다 커지면 당연히 넘어가서 화면에서 짤린다.
  
### 2. 파란 Anchor : Stretch
- ![20230208_183329](https://user-images.githubusercontent.com/55792986/217490889-274e94b8-1cb4-4c29-8d63-554e5de9d986.png)
- 상단 메뉴바 같은 거는 언제나 윗쪽에 위치하며 왼쪽 끝 ~ 오른쪽 끝을 모두 채워야 한다.
  - 이런 경우에 stretch를 이용한다.
  - 얘도 당연히 부모 UI를 기준으로 배치된다.
- Stretch로 변경하면 Position 부분이 Left, Top, Right, Bottom으로 변경되는데 이는 여백을 나타낸다.
  - 모두 0,0,0,0으로 하면 부모 UI를 꽉 채울 것 이다.

### 3. Pivot & Anchor  
- 어떤 사각형 UI를 Bottom-left로 붉은 Anchor를 설정하면 부모 캔버스의 좌측 하단으로 이동할 것 이다. 하지만 피벗이 중앙(0.5, 0.5)으로 설정되어 있어 아래의 문제가 생긴다.
  - ![20230208_184718](https://user-images.githubusercontent.com/55792986/217494620-2c76f33c-2d4f-4443-8108-08c2b6277233.png)
    - 짤린다.
- 피벗을 좌 상단(0,0)으로 맞추고 posX와 posY를 다시 (0,0)으로 바꿔주면 잘 나온다.
  - ![20230208_184907](https://user-images.githubusercontent.com/55792986/217494923-b8ca903a-be40-4608-81bf-da7464b1736e.png)
    - 유니티는 피벗을 바꾸면 또 posX와 posY를 바꿔 변경 사항이 없는 것 처럼 바꿔버린다. ㅠㅠ
- 결론적으로 피벗은 어떤 UI의 기준점이 되고, 이 기준점을 통해 앵커도 형성된다.

# Layout Group
- UI의 배치를 모두 수동으로 하면 상당히 많은 작업을 요구하며 시간을 많이 소모할 것 이다.
- **Layout Group으로 UI를 자동으로 정렬하여 배치할 수 있다.** 
- Horizontal, Vertical, Grid가 존재한다.
  - Grid는 바둑판이다.
- 세부 구성 요소
  - Spacing : 자식 요소들 간의 사이 거리
  - Child Alignment : 레이아웃 객체의 기준으로 자식들의 정렬 위치를 바꾼다.
  - Child Force Expand : 레이아웃(부모)에 자식의 정렬을 맞출 것 인가
- 각각의 자식은 Layout Element을 가질 수 있고, 이를 이용하여 자식 마다의 설정을 할 수 있다.
- https://wergia.tistory.com/178

# Layout Element
- Layout Group에서 자식들의 배치에 대한 툴을 잡는다. 하지만 자식들 개개인의 옵션에 따라 배치를 커스텀하고 싶을 때가 존재한다.
- 이 때 Layout Element를 자식들에게 각각 부여하여 개별의 배치를 설정한다.
- 이름 그대로 상위 타입에 대한 자식 위젯(=element)의 크기를 조절한다.
  - OSA에서 스크롤 뷰를 만들 때 Content Size Fitter의 Preferred를 적용하려면 Layout Element의 preferred 값을 조절한다.

# Graphic Raycaster
- ![20230209_104734](https://user-images.githubusercontent.com/55792986/217695613-2c842205-3854-4bad-8bdf-2958c8e7080c.png)
  - 얘 꺼버리면 해당 UI는 광선(raycast)에 충돌되지 않고 통과한다.
    - 그래서 마우스로 클릭해도 다 뚫는다.
  - 버튼이 눌리지 않으면 이게 존재하는 지 봐라.
    
# EvenetSystem
- ![20230208_112043](https://user-images.githubusercontent.com/55792986/217412194-5b7ca7ce-5109-401d-bfb6-664b3ab7da82.png)
- Scene에 1개만 존재
- 얘 덕분에 이벤트를 UI에서 감지하고 처리할 수 있나 보다.
- GraphicRaycaster의 충돌정보를 처리한다.

# :star::star::star:Layer & Sorting Layer & Order In Layer:star::star::star:
- 일단 Layer vs (Sorting Layer & Order In Layer다.)
- ![20230209_113748](https://user-images.githubusercontent.com/55792986/217703459-dc53fd92-b8ec-4a85-8eaf-8a3472e09ac8.png)
### 1. Layer
![image](https://user-images.githubusercontent.com/55792986/217703226-a83da77f-5310-44dc-ad4b-a5034329d564.png)
  - UI에서 필요한 그리기 우선순위와는 거리가 멀다고 생각한다.
  
### 2. Sorting Layer
- 화면에 그리는 우선순위.
- 위에 있을 수록 먼저 그린다.
- ![image](https://user-images.githubusercontent.com/55792986/217703939-922feec6-c30a-4834-b8ef-a747f526c865.png)

### 3. Order in (Sorting) Layer 
- **order in layer의 layer는 sorting layer를 의미한다.**
- **같은 Sorting Layer**내에서 Order in Layer가 값이 작으면 먼저 그린다.
  - 클수록 위에 그려진다.
  
# TextMeshPro (=TMP)
- Text를 표시하는 UI Component
  - Text와 관련되면 거의 무조건 이걸 사용한다고 본다.
  - 다양한 텍스트의 효과
- Text는 Legacy이므로 유니티에서도 TextMeshPro를 권장하고 있다.
- 꾸미는 부분은 개발자의 담당이 아니므로 패스한다.

# Image
- source image에 등록되는 이미지가 출력된다.
  - Sprite를 여기에 등록한다.

# Content Size Fitter
- UI의 내용물에 따라서 UI의 크기를 알아서 조절해 주는 기능.
- 보통 레이아웃과 함께 사용 된다.

# RectTransform 변경하기
- ![Alt text](./Capture/2023092601.png)
~~~c#
_rect.offsetMin = new Vector2(200, 0);
_rect.offsetMax = new Vector2(-300, 0);
~~~
  - 이러면 left값이 200, right 값이 300이 된다.
  - left는 parent에 대해서 왼쪽으로 200을 떨어뜨리고
  - right는 parent에 대해서 오른쪽으로 300을 떨어뜨린다.

# RectTransform
- ![Alt text](./Capture/20231010.png)

# AnchoredPosion Vs LocalPosition
- ![Alt text](./Capture/202310101.png)

# Anchor와 Pivot을 잘 정해야 비율 계산이 쉽다.
- 위치를 코드로 잡을 때 비율을 이용해서 rect 위치를 이동하는데
- 이 때 가운데가 pivot이면 가운데를 기준으로 이동하니까 어렵다.
- 그러므로 왼쪽으로 피봇을 박는게 맞다.
  - 실수 조심해라. 왼쪽부터 시작일 때 실제 객체가 왼쪽에 존재해서 계산 되는 지 정말 중요하다.

# 참조
- [Unity](https://docs.unity3d.com/kr/2018.4/Manual/UISystem.html)
- [고박사 Youtube](https://www.youtube.com/watch?v=QnJp45U_UEs&list=PLC2Tit6NyViewOPACJai5zNAfZuUW8aYq&index=56)
- [베르 Youtube](https://www.youtube.com/watch?v=YnOWWcZ35xw&list=PLYQHfkihy4Az5OFjO2hbY3AOO2EKPkiza&index=6)