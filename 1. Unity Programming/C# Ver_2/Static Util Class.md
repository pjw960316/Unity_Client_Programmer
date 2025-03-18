## :fire: 참조 타입의 주소 얻기
~~~c#
public static class AddressManager
{
	public static string GetAddress<T>(T referenceTypeInstance)
	{
		unsafe
		{
			TypedReference typedReference = __makeref(referenceTypeInstance);
			IntPtr ptr = **(IntPtr**)(&typedReference);
			string address = Convert.ToString((long)ptr);
			return address;
		}
	}
}
~~~

<br>

## :fire: 값 타입의 주소 얻기기