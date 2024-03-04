# 목차
- [목차](#목차)
- [코드](#코드)

<br/><br/><br/>

# 코드
~~~c#
public interface ISubject
{
	void Request();
}

// The RealSubject contains some core business logic. Usually, RealSubjects
// are capable of doing some useful work which may also be very slow or
// sensitive - e.g. correcting input data. A Proxy can solve these issues
// without any changes to the RealSubject's code.
class RealSubject : ISubject
{
	public void Request()
	{
		Console.WriteLine("RealSubject: Handling Request.");
	}
}

// The Proxy has an interface identical to the RealSubject.
class Proxy : ISubject
{
	private RealSubject _realSubject;

	public Proxy(RealSubject realSubject)
	{
		this._realSubject = realSubject;
	}

	// The most common applications of the Proxy pattern are lazy loading,
	// caching, controlling the access, logging, etc. A Proxy can perform
	// one of these things and then, depending on the result, pass the
	// execution to the same method in a linked RealSubject object.
	public void Request()
	{
		if (this.CheckAccess())
		{
			this._realSubject.Request();

			this.LogAccess();
		}
	}

	public bool CheckAccess()
	{
		// Some real checks should go here.
		Console.WriteLine("Proxy: Checking access prior to firing a real request.");

		return true;
	}

	public void LogAccess()
	{
		Console.WriteLine("Proxy: Logging the time of request.");
	}
}

public class Client
{
	// The client code is supposed to work with all objects (both subjects
	// and proxies) via the Subject interface in order to support both real
	// subjects and proxies. In real life, however, clients mostly work with
	// their real subjects directly. In this case, to implement the pattern
	// more easily, you can extend your proxy from the real subject's class.
	public void ClientCode(ISubject subject)
	{
		subject.Request();
	}
}

class Program
{
	static void Main(string[] args)
	{
		Client client = new Client();

		RealSubject realSubject = new RealSubject();
		client.ClientCode(realSubject);

		"\n".Dump();

		Proxy proxy = new Proxy(realSubject);
		client.ClientCode(proxy);
	}
}
~~~
- 코드를 보면 대충은 알겠지만 명확히 왜 써야 하는 지 느낌이 오지는 않는다.