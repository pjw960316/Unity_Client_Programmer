## :fire: 메서드는 추상적으로 n개의 인풋 데이터를 넣으면, m개의 아웃풋 데이터가 나오는 것 <br> 조회가 실패할 수 있고, 성공 여부와 여러 결과값을 함께 받아야 할 때 Try... + out을 사용한다.

#### :white_check_mark: 경험

- 돌아보면 이런 조회 메서드를 자주 쓰다 보니, 비슷한 상황에서 일관되게 쓸 나만의 패턴을 정해두고 싶었던 것 같다.
- 하나의 메서드에서 조회와 초기화의 책임을 모두 담당하게 하고 싶었다.
  - 필요한 데이터를 넣어주고, out으로 초기화까지 책임진다.
  - 그렇게 되면 외부에서 추가로 변수를 만들지 않아도 되며 메서드 자체의 책임이 깔끔하게 떨어진다.

<br>

#### :white_check_mark: `Try... + out`을 쓰는 기준

- 조회가 실패할 수 있고, 호출부에서 성공한 경우에만 결과를 써야 할 때 이 형태를 사용한다.
- 메서드 이름 앞에 `Try`를 붙이면 호출부에서 성공 여부를 확인해야 한다는 점이 드러난다. 
- `bool`로 성공 여부를 확인한 뒤, 성공했다면 `out`으로 값을 초기화한다.

<br>

#### :white_check_mark: 스폰 위치와 회전 조회

- 스폰 인덱스 하나를 넣으면 해당 위치와 회전을 얻는다. 메서드 안에서 인덱스가 범위를 벗어났는지 확인하고, 조회할 수 없으면 `false`를 반환한다.

~~~c#
public bool TryGetSpawnPositionAndRotation(int index, out Vector3 position, out Vector3 rotation)
{
    if (index < 0 || index >= _spawnPositionCount ||
        index >= _spawnPositionList.Count || index >= _spawnRotationList.Count)
    {
        position = default;
        rotation = default;
        return false;
    }

    position = _spawnPositionList[index];
    rotation = _spawnRotationList[index];
    
    return true;
}
~~~