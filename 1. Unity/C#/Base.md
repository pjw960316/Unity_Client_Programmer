# 1. 베이직 한 테스트
~~~c#
public class C1
{
    public virtual void TestTest()
    {
        CLog.Error("1");
    }
}
public class C2 : C1
{
    public override void TestTest()
    {
        CLog.Error("2");
    }
}
public class C3 : C2
{
    public override void TestTest()
    {
        base.TestTest();
        CLog.Error("3");
    }
}

Main()
{
    C3 obj = new C3();
    obj.TestTest();
}
~~~
- 3과 2가 출력된다.
  - 일단 base는 조상이 아닌 직속 부모를 호출하는 게 맞다.

# 2. 중간을 건너 뛰면
~~~c#
public class C1
{
    public virtual void TestTest()
    {
        CLog.Error("1");
    }
}
public class C2 : C1
{
    /*public override void TestTest()
    {
        CLog.Error("2");
    }*/
}
public class C3 : C2
{
    public override void TestTest()
    {
        base.TestTest();
        CLog.Error("3");
    }
}

Main()
{
    C3 obj = new C3();
    obj.TestTest();
}
~~~
- 3과 1이 출력된다.
  - 직속 부모가 해당 메서드를 갖지 않으면 더 올라가서 찾아본다.
- 만약 C1까지 없다면?
  - C1이 없으면 애당초 C3의 고유 메서드다.


  