## :fireworks: 가장 중요한 생각
- 자료를 담는 자료구조가 어렵고 복잡하면 안 된다. 
- 차라리 자료구조를 많이 쪼개서 메모리를 많이 쓰더라도.
- 적어도 serializable value와 non-serializable value 정도는 구분시킨다.


## :fireworks: 아래 부터는 codex

<br><br>

## SerializedDictionary의 복합 Value 분할

처음에는 하나의 `EFieldObject`에 여러 데이터를 묶어서 저장하려고 했다.
```text
EFieldObject → AnimalSpawnSetting
                 ├─ Prefab
                 └─ Weight
```
이를 코드로 표현하면 다음과 같다.
```csharp
SerializedDictionary<EFieldObject, AnimalSpawnSetting>
```
하지만 `SerializedDictionary`의 Value에 여러 데이터를 가진 클래스를 넣으려면 해당 클래스를 Unity가 직렬화할 수 있도록 만들어야 한다.

이 과정에서 다음 문제가 발생했다.

- Value 클래스를 `[Serializable]`로 만들어야 한다.
- Dictionary 구현에 따라 구체적인 파생 타입이 필요할 수 있다.
- 커스텀 PropertyDrawer가 복합 Value를 제대로 처리하지 못할 수 있다.
- Inspector에서 데이터가 표시되지 않거나 예외가 발생할 수 있다.
- 단순한 설정을 저장하기 위해 직렬화 코드가 지나치게 복잡해진다.

따라서 하나의 Dictionary에 복합 Value를 넣지 않고, Unity가 확실하게 직렬화할 수 있는 단순 데이터별로 Dictionary를 분리한다.
```text
EFieldObject → Prefab
EFieldObject → Weight
```

```csharp
[SerializeField]
private SerializedDictionary<EFieldObject, FieldObjectAnimalBase>
    _animalPrefabDict = new();

[SerializeField]
private SerializedDictionary<EFieldObject, int>
    _animalSpawnWeightDict = new();
```
두 Dictionary는 동일한 `EFieldObject`를 연결 기준으로 사용한다.
```csharp
var prefab = _animalPrefabDict[eFieldObject];
var weight = _animalSpawnWeightDict[eFieldObject];
```
즉, Inspector에서는 직렬화하기 쉬운 단순 데이터들을 각각 저장하고, 코드 내부에서 동일한 Key를 이용해 필요한 데이터들을 조합해서 사용한다.

### 장점

- 복합 Value 직렬화를 피할 수 있다.
- Inspector 에러 가능성이 줄어든다.
- 각 Dictionary의 데이터 의미가 명확하다.
- Prefab이나 `int`처럼 Unity가 확실하게 처리하는 타입만 직렬화한다.
- 새로운 런타임 객체를 반드시 만들지 않아도 동일한 Key로 데이터를 함께 사용할 수 있다.

### 주의점

Dictionary를 분리했기 때문에 Key 구성이 서로 일치해야 한다.

실행 전에 다음 조건을 검증해야 한다.

- 두 Dictionary의 Key 개수가 같은가?
- 한쪽에 존재하는 `EFieldObject`가 다른 쪽에도 존재하는가?
- Prefab이 `null`이 아닌가?
- Weight가 유효한 값인가?

이번 설계의 핵심은 복합 데이터를 하나의 직렬화 Value로 억지로 묶는 것이 아니라, 직렬화하기 쉬운 단순 데이터 단위로 분할한 뒤 코드에서 같은 Key를 기준으로 조합해서 사용하는 것이다.
