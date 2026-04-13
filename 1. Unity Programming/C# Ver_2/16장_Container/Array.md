## :fire: Array 개념 (📔제프리 16장)
- **모든 배열은 System.Array class로 부터 상속을 받기 때문에 참조 타입이고 힙에 저장된다.**
  - System.Array → IList → ICollection → IEnumerable(최상단) (📔P444)
  - :airplane:[MSDN](https://learn.microsoft.com/ko-kr/dotnet/api/system.array?view=net-8.0)
- **배열은 Add나 Remove 같은 메서드를 사용 할 수 없다. (고정 크기)**
   > IList.Add(Object) => Calling this method always throws a NotSupportedException exceition.
- **메서드의 매개변수로 배열을 전달할 때에는 실제로는 배열의 참조를 전달하게 된다.**
    - 이차원 배열에서 하나의 Row(일차원 배열)만 분리해서 메서드로 전달할 수 없다. 그러므로 메서드에서는 이차원 배열 전체를 전달한다.
- **var arr = new int[0]는 빈 배열이고 var arr = null은 배열을 할당 하지 않은 것 이다. 엄연히 다르다.**
    - 크기가 0인 배열은 예외처리 및 안정성에서 쓸모 있는 개념이 된다.

<br><br>

## :fire: 이차원 배열은 두 가지 타입이 있다.
#### :one: int[ , ] 
- **C# 기본 이차원 배열이고 코딩 테스트에서 대부분 이걸 사용한다.**
- 크기가 n으로 주어지면 다음과 같이 만들고 1-based indexing을 한다.
~~~c#
var arr = new int[n+1,n+1];

for(int i=1; i<=n; i++)
{
	for(int j=1; j<=n; j++) 
	{
        arr[i,j] = i+j;
	}
}
~~~ 
- GetLength(0) = 행 길이.
- GetLength(1) = 열 길이.
- 내부의 일차원 배열을 뽑고 싶다면, **일차원 배열을 새로 할당하고 순회+초기화 방법밖에 없다.
- bool 타입으로 만들지 말자.  →  로그 찍으면 가독성이 떨어진다.

#### :two: int[ ][ ]  (=Jagged Array =가변 배열)
- int[,]은 특정 Row만 가져온 일차원 배열을 참조할 수 없다.  그러나 int[][]는 가능하다!
- Row의 index를 1부터 해야 한다면 int[][]을 사용하고, 0부터 사용하면 List<int[]>를 사용한다.
~~~c#
void Main()
{
    var arr = new int[10][];

    for (int i = 1; i <= 9; i++)
    {
        arr[i] = new int[51];
    }
    arr[3][0] = 77;

    // 1. jaggedArray의 Row_3 일차원 배열의 주소를 새로운 일차원 배열 참조 변수에 초기화한다.
    int[] oneDimensionArr = arr[3];
    oneDimensionArr[0].Dump(); // 77
    
    // 2. 서로 같은 일차원 배열을 참조하기 때문에 원본이 변경된다.
    oneDimensionArr[0] = 88;
    arr[3][0].Dump(); // 88
}
~~~

<br><br>

## :fire: 큰 배열을 자주 복사할 때는 재사용 기반 for문을 사용한다.
- :teacher: int[,] originArr 와 int[,] newArr로 설명한다.

<br>

- **Array.Clone()  →  빠르지만 메모리를 추가로 더 사용한다.**
  - Array.Clone()은 새로운 배열을 할당하고 이는 당연히 콜 마다 메모리를 사용해서 메모리 초과 위험이 있다.
    - 내부 원소에 대해서는 잘 구분한다.
      - 내부 원소가 valueType이라면 originArr와 newArr가 완전히 독립적이다.
      - 내부 원소가 referenceType이라면 originArr의 2번째 원소를 수정하면 newArr의 2번째 원소도 변경된다.

<br>

- **Local Function에서 Array.Clone()을 사용해서 newArr를 생성했다.**
  - Local Function의 리턴 타입이 int[,]일 때
    - newArr는 외부에서 참조되므로 메모리에 계속 존재한다.
  - Local Function의 리턴 타입이 void일 때
    - newArr는 local Function이 종료되면 GC 대상이 된다.
    - 그러나 GC 대상이 된다고 해도 코딩 테스트에서는 메모리 초과에서 안전하지 않으며 GC의 잦은 콜은 성능이슈를 만든다.
  - 그럼에도 불구하고, Array.Clone()은 For문 순회 직접 복사 보다 빠르다.
    - Clone()은 **런타임 내부 최적화 (IL → native)**
    - for는 **managed 코드 루프**

<br>

- **재사용 기반 for문 복사  →  느리지만 메모리를 추가로 사용하지 않는다.**
  - int[,] originArr랑 int[,] newArr를 한 번만 할당한다. for문을 통해 값을 직접 복사하며 재사용한다.
  - 재사용 방식은 새로운 배열을 추가로 할당하지 않아서 메모리초과 이슈에서 안전하다.
  - 하지만 Array.Clone()보다 느리다.
  - Array.Clear(newArr)와 같이 사용하도록 한다.

<br>

- **알고리즘 문제 → Clone() 이용.**
  - :airplane:[BOJ_2573번 문제](https://github.com/pjw960316/Algorithm-Habit/blob/main/%EB%B0%B1%EC%A4%80/Gold/2573.%E2%80%85%EB%B9%99%EC%82%B0/%EB%B9%99%EC%82%B0.cs)
