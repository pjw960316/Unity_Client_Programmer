# 목차
- [목차](#목차)
- [:star::star::star:Layer \& Sorting Layer \& Order In Layer:star::star::star:](#starstarstarlayer--sorting-layer--order-in-layerstarstarstar)
    - [1. Layer](#1-layer)
    - [2. Sorting Layer](#2-sorting-layer)
    - [3. Order in (Sorting) Layer](#3-order-in-sorting-layer)


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