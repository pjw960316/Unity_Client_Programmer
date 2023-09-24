# 목차
- [목차](#목차)
- [개요](#개요)
- [2023년 3월에 대충 쓴 건데 일단 캐싱](#2023년-3월에-대충-쓴-건데-일단-캐싱)

<br/><br/><br/>

# 개요
- Enum은 정리 할 내용이 많기 때문에 일단 페이지를 만들었다.

# 2023년 3월에 대충 쓴 건데 일단 캐싱
- Enum.GetValues(해당 enum 클래스)
  - enum에 상수를 등록하고, 여기서 찾으면 된다.
  - 그리고 해당 클래스에서 enum으로 찾으려면 enum
- ![image](https://user-images.githubusercontent.com/55792986/218911608-edc7af67-6cd2-4a37-829b-e7dbe4e4ad79.png)
- ![image](https://user-images.githubusercontent.com/55792986/218911639-73e118d0-41e5-43d5-955c-9c593ea319fe.png)
  - 이 enum은 클래스에 있어야 한다!
~~~c#
EJiwonType.Power1.ToString()
~~~
- 이거 Power1으로 나옴. enum의 Tostring()하면 그 문자열 나옴.