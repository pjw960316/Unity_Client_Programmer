## :fire: await 키워드를 만나면 메서드는 즉시 반환 되고, 그 시점을 저장하고 있는다. <br> :fire: await 걸린 비동기 호출이 완료되면 풀에 있던 스레드가 해당 부분을 실행한다.
#### [await 예제]
~~~c#
private void Test()
{
    UpdateResult().Forget();

    Debug.Log("Main : Run Event Loop");
}

private async UniTaskVoid UpdateResult()
{
    Debug.Log("UpdateResult");

    var result = await SumAsync(100, 200);

    Debug.Log($"{result}");
}

private async UniTask<int> SumAsync(int n1, int n2)
{
    await UniTask.Delay(1000);

    var ret = n1 + n2;

    return ret;
}
/*
UpdateResult
Main : Run Event Loop
300
*/
~~~
- ![alt text](./captures/20250808_1.png)
> await 연산자의 피연산자는 일반적으로 .NET 형식인 Task, Task<TResult>, ValueTask 또는 ValueTask<TResult> 중 하나에 해당합니다. 그러나 대기 가능한 모든 식은 await 연산자의 피연산자일 수 있습니다. 

<br><br>

## :fire: await가 있는 메서드에서는 그 메서드의 return type은 반드시 UniTask 계열(혹은 .NET의 Task 계열)이어야 한다.

<br><br>

## :link: 과거 문서
- [Async & Await](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/C%23%20Ver_1/%EB%B9%84%EB%8F%99%EA%B8%B0_Async%20%26%20Await.md)
- [UniTask](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/C%23%20Ver_1/%EB%B9%84%EB%8F%99%EA%B8%B0_Unitask.md)