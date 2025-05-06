# Garbage Collection (=GC)
### 1. 개요
- ![image](https://user-images.githubusercontent.com/55792986/198259528-bd68a268-1b8a-4da3-b8c2-53655e9258f7.png)

### 2. GC를 하는 조건
- ![image](https://user-images.githubusercontent.com/55792986/198262004-650aae6c-f23e-4daa-b93b-7c74ee8219cc.png)
  - 간단하게 생각하면 메모리가 부족하면 수행할 것 이다.

### 3. Unity의 Heap 분류
- ![image](https://user-images.githubusercontent.com/55792986/198262261-4ea44f09-eaa9-4b34-b6a5-121f7b25479a.png)

### 4. GC에서 살아남는 메모리
- ![image](https://user-images.githubusercontent.com/55792986/198262729-a0ddbdf2-b3bc-4511-b693-ed90d15b26d2.png)
  - A,C,D의 경우 해당 힙의 주소 및 정보를 스택이나 힙에 저장하고 있기 때문에 올바른 참조관계로 이루어져 있다. 그러므로 살아남는다.
  - F의 경우 D가 참조하고 있기 때문에 살아남는다.
  - B와 E의 경우 힙에 저장되어 있지만 아무도 참조하고 있지 않기 때문에 제거한다.
    - 제거한 빈 공간으로 인해 Fragmentation(단편화)이 발생하므로 Compaction(압축)으로 해결한다. 

### 5. 세대를 이용하는 GC
- 참고문헌을 읽는 것이 좋아 보인다.