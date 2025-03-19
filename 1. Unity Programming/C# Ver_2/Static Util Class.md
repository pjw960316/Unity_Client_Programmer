## :fire: 값 타입의 주소 얻기
~~~c#
static class ValueTypeAddressManager
{
	//unmanaged를 붙이지 않으면 generic 에서 warning이 발생
	public static unsafe string GetAddress<T>(ref T valueType) where T : unmanaged
	{
		if(valueType.GetType().IsValueType == false)
		{
			return null;
		}

		fixed (T* ptr = &valueType)
		{
			string address = Convert.ToString((long)ptr, 16);
			string ret = $"0x{address}";
			return ret;
		}
	}
}
~~~
- ref를 사용해서 값 복사를 하지 않고 원본의 메모리 주소를 메서드로 전달한다.

<br><br>

## :fire: 참조 타입의 주소 얻기
~~~c#
public static class ReferenceTypeAddressManager
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
