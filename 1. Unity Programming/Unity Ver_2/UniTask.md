## :fireworks: 우선 Unity는 싱글 스레드이므로 메인 스레드와 서브 스레드는 사실 구분되지 않는다. <br> 하지만 설명을 위해 구분했다. <br> :fire: 메인  스레드는 await 키워드를 만날시에 해당 메서드를 **즉시 탈출하고** 호출부로 돌아간 후, 이어서 진행한다. <br> 그리고 서브 스레드 1개가 이 시점부터 일을 시작한다. <br> :fire: 서브 스레드는 await 키워드에 걸린 Unitask를 비동기적으로 수행하고, 이를 완료하면 주 스레드에게 알린다. <br> :fire: 주 스레드는 완료 시점에 await 이후로 돌아와서 결과 값을 가지고 일을 수행한다.
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
- **UniTask.Delay(1000) 부분에서 탈출해도 또 await SumAsync(100,200)이 존재한다. 그러면 당연히 또 탈출해서 Test()로 제어가 넘어간다.**
> await 연산자의 피연산자는 일반적으로 .NET 형식인 Task, Task<TResult>, ValueTask 또는 ValueTask<TResult> 중 하나에 해당합니다. 그러나 대기 가능한 모든 식은 await 연산자의 피연산자일 수 있습니다. 

<br><br>

## :fire: await 키워드가 있는 메서드는 <br> return type을 반드시 UniTask 계열(혹은 .NET의 Task 계열)로 해야 한다.

<br><br>

## :question: 비동기를 구현하려면 async UniTask 타입을 리턴하는 method가 2개 필요하다.
- 추후 더 내용을 적자.

<br><br>

## :link: 과거 문서
- [Async & Await](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/C%23%20Ver_1/%EB%B9%84%EB%8F%99%EA%B8%B0_Async%20%26%20Await.md)
- [UniTask](https://github.com/pjw960316/Unity_Client_Programmer/blob/main/1.%20Unity%20Programming/C%23%20Ver_1/%EB%B9%84%EB%8F%99%EA%B8%B0_Unitask.md)