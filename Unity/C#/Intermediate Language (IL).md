# 목차
- [목차](#목차)

# 개요
- 나중에 쓰자

# Intermediate Language (IL)
- ![image](https://user-images.githubusercontent.com/55792986/206630306-c3e8c15f-bbb3-4650-8123-9a82ffe5d30f.png)
    - 개발자가 IL을 만들면 가상머신이 어떠한 OS에서도 동작하도록 변환해준다.
        - 가상머신을 CLR(=common language runtime)이라고 부른다.
        - :star개발자가 C#, C++, Visual Basic을 이용해서 코딩을 하면 C# 컴파일러가 이를 IL로 바꿔준다.
    - C# 개발자가 굳이 IL을 잘 알 필요는 없지만 IL을 간단하게 읽을 수 있으면 능력이 크게 향상된다.

# Rider에서 IL 코드 보는 방법
- :star:'Alt + tab위의 `'를 누르면 된다.
- 업데이트가 즉시 되지 않으므로 코드를 한 번 실행시켜 업데이트 시켜준다.

# 연습_1
~~~c#
using System;
using System.Collections.Generic;

namespace FirstConsoleApp
{
    struct Point
    {
        public int x;
        public int y;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Point pt1; //객체를 스택에 선언하고 객체의 실제 데이터를 힙에 올리지는 않음.
            Point pt2 = new Point(); //객체를 스택에 선언하고 객체의 실제 데이터를 힙에 올리고 참조
        }
    }
}
~~~
- ![image](https://user-images.githubusercontent.com/55792986/206637459-1a55725d-d9b2-4d86-a5bc-552c7bd6ab53.png)
    - pt1과 pt2의 IL 코드를 보면 차이를 느낄 수 있다. 

# 연습_2
- assembly를 선언해야 한다.
- .entrypoint는 반드시 1개여야 한다.
- 함수를 호출할 때 리턴 값과 매개변수까지 모두 선언해 주어야 한다.
- c#은 int를 알지만 IL은 int를 모르므로 int32로 작성한다.
- ldc는 스택에 넣는 것 이고 i4는 4바이트의 정수형을 의미한다.
    - 명령어는 구글에 검색하면 다 나온다.


# 생각
- 깊게 해당 코드를 파악하기 위해서는 해당 기능이 매우 중요하다.