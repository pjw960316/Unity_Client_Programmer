# 목차
- [목차](#목차)
- [History](#history)
- [:star::star::star:그려지는 순서:star::star::star:](#starstarstar그려지는-순서starstarstar)
    - [1. Sorting Layer 비교](#1-sorting-layer-비교)
    - [2. Order In Layer 비교](#2-order-in-layer-비교)
    - [3. Unity Hierarchy에서의 위치 비교](#3-unity-hierarchy에서의-위치-비교)
- [Sorting Layer](#sorting-layer)
- [Order In Layer](#order-in-layer)
- [Sorting Layer vs Layer](#sorting-layer-vs-layer)

<br/><br/><br/>

# History
- UGUI의 오브젝트가 코드에서 SetActive(true)인 상태임에도 불구하고 보이지 않는 경우가 있다.
- 그럴 때는 UGUI의 Rendering System에 따라 그려지는 순서를 확인해 본다.

<br/><br/><br/>

# :star::star::star:그려지는 순서:star::star::star:
### 1. Sorting Layer 비교
  - **Sorting Layer가 높을 수록 위에 그려진다. (위에 그려지는 것이 보인다.)**

<br/>

### 2. Order In Layer 비교
  - **Order In Layer가 높을 수록 위에 그려진다.**

<br/>

### 3. Unity Hierarchy에서의 위치 비교
- ![Alt text](./Capture/20240204_185901.png)
  
<br/><br/><br/>

# Sorting Layer
- ![image](https://user-images.githubusercontent.com/55792986/217703939-922feec6-c30a-4834-b8ef-a747f526c865.png)

<br/><br/><br/>

# Order In Layer

<br/><br/><br/>

# Sorting Layer vs Layer
- ![20230209_113748](https://user-images.githubusercontent.com/55792986/217703459-dc53fd92-b8ec-4a85-8eaf-8a3472e09ac8.png)