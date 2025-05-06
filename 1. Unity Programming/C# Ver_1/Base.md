# 목차
- [목차](#목차)
- [Base는 바로 직속 부모를 가르킨다.](#base는-바로-직속-부모를-가르킨다)
- [예외\_1 : C2에 Test()가 없다면?](#예외_1--c2에-test가-없다면)
- [예외\_2 : C1,C2에 Test()가 없다면?](#예외_2--c1c2에-test가-없다면)

<br/><br/><br/>

# Base는 바로 직속 부모를 가르킨다.
~~~c#
public class C1
{
    public virtual void Test()
    {
        Console.WriteLine("1");
    }
}
public class C2 : C1
{
    public override void Test()
    {
        Console.WriteLine("2");
    }
}
public class C3 : C2
{
    public override void Test()
    {
        base.Test();
		
        Console.WriteLine("3");
    }
}

void Main()
{
	C3 obj = new C3();
	obj.Test();
}

/*Result
2
3
*/
~~~
<br/><br/><br/>

# 예외_1 : C2에 Test()가 없다면?
~~~c#
public class C1
{
    public virtual void Test()
    {
        Console.WriteLine("1");
    }
}
public class C2 : C1
{
    //None
}
public class C3 : C2
{
    public override void Test()
    {
        base.Test();
        Console.WriteLine("3");
    }
}

void Main()
{
	C3 obj = new C3();
	obj.Test();
}

/*Result
1
3
*/
~~~
- 직속 부모가 없으면 그 위로 간다.
<br/><br/><br/>

# 예외_2 : C1,C2에 Test()가 없다면?
~~~c#
public class C1
{
    //None
}
public class C2 : C1
{
    //None
}
public class C3 : C2
{
    public override void Test()
    {
        base.Test(); //컴파일 에러
        Console.WriteLine("3");
    }
}

void Main()
{
	C3 obj = new C3();
	obj.Test();
}
~~~
- C3의 모든 부모 (C2,C1)에 대해서 Test()가 없기 때문에 컴파일 에러가 난다.
- CS0115 'UserQuery.C3.Test()': no suitable method found to override



  