## :fire: 게임을 구현하다보면 보통 3~5개의 분기가 나온다. <br> 과연 1,000개의 분기가 생기면 내 코드는 괜찮은가?
#### :one: 삽질 과정 <br> 1,000개의 객체가 추상화 단계에서 동일한 기능을 할 때 1,000개의 메서드를 만들어야 하는가?
- 데이터 마다 호출되는 메서드를 연결하면 되겠다.
- 3개 까지는 OnXXXX로 콜백을 만들었다.
- 4개 즈음 그럼 1,000개면 1,000개의 메서드인가? 이건 아닌데. InputSystem은 확장성에 대해 매우 열려있어야 한다.
- 데이터도 그러면 private int a; 같이 1,000개의 필드를 만들건가?
- 데이터를 구조화 해야겠다. 그 구조화한 데이터 마다 key-value로 콜백을 달아주어야 겠다.
- 구조화 해도 1,000개의 메서드인데 이걸 깔끔하게 하는 방법이 없을까? 인터페이스!
- 그러면 데이터 -> 데이터와 데이터의 타입 매핑 -> 타입 별로 concrete 객체생성 달라진다. -> 같은 메서드를 호출해도 자동으로 분기
- 찾아보니 전략패턴 발견

#### :two: 장점
- 그러면, HandleInput이라는 interface method로 선언하고, 1,000개의 concrete method를 구현하면 되겠구나!
  - 1,000개의 이름 안 지어도 된다.
  - 유지보수가 쉽다.
  - 중복되는 기능은 부모 메서드로 선언하고나 abstract class의 virtual method로 변경해도 된다.
- 머릿속에서 1,000개의 분기가 if-else로 있는 것과는 다르게 클래스로 분리되어 있으니 구분이 쉽다. 함수의 길이 문제가 아니라 함수의 기능이 간단해진다.
- 메서드와 클래스는 작아야 한다는 기본 원칙을 지킨다.
- 데이터를 구조화할 때 enum을 사용하여 가독성도 늘릴 수 있고, 기획자가 inspector 사용할 때 편리하게 쓸 수 있다.

#### :three: 단점
- 어떤 데이터가 들어올 때 어떤 concrete instance를 생성해야 하는지 매핑을 해주긴 해야 한다. 다시 말해, 분기는 사라지지 않는다!
  > 클라이언트 클래스는 선택해야 하는 전략을 알기 위해 클라이언트가 구현한 알고리즘의 변형과 개별 전략을 알고 있어야 한다. 클라이언트는 객체가 살아 있는 동안 예상대로 동작하는지 확인해야 한다. (:book: 유니티로 배우는 게임 디자인패턴 2판 P123)
- 객체 수가 증가된다 -> 힙 new 할당이 많아진다 -> GC 생성 
  - 결국 trade-off다.

## :fire: 코드
#### :one: 나의 코드
~~~c#
private readonly SerializedDictionary<EInput, InputActionReference> _inspectorInputDict = new();
private readonly Dictionary<InputAction, EInput> _inputActionDict = new();
private readonly Dictionary<EInput, IInputHandler> _handlerDict = new();
~~~

#### :two: 책 코드
~~~c#
using UnityEngine;
using System.Collections.Generic;

namespace Chapter.Strategy {
    public class ClientStrategy : MonoBehaviour {
        
        private GameObject _drone;
        private List<IManeuverBehaviour> 
            _components = new List<IManeuverBehaviour>();
        
        private void SpawnDrone() {
            _drone = 
                GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            _drone.AddComponent<Drone>();
            
            _drone.transform.position = 
                Random.insideUnitSphere * 10;
            
            ApplyRandomStrategies();
        }

        private void ApplyRandomStrategies() {
            _components.Add(
                _drone.AddComponent<WeavingManeuver>());
            _components.Add(
                _drone.AddComponent<BoppingManeuver>());
            _components.Add(
                _drone.AddComponent<FallbackManeuver>());
            
            int index = Random.Range(0, _components.Count);
            
            _drone.GetComponent<Drone>().
                ApplyStrategy(_components[index]);
        }
        
        void OnGUI() {
            if (GUILayout.Button("Spawn Drone")) {
                SpawnDrone();
            }
        }
    }
}
~~~ 
- 데이비드 바론도 List로 데이터 분기처리하고, IManeuverBehaviour을 상속 받는 concrete 타입을 생성했다.