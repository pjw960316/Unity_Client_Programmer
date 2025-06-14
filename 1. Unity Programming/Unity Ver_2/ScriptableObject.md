## :book: SO == ScriptableObject

<br><br>

## :fire: 언제써?

<br><br>

## :fire: ScriptableObject는 Manager Class로 관리한다. 
> While Scriptable Objects don't have a dedicated manager, a **manager class** might be used to access or manage multiple instances of a Scriptable Object or to coordinate their usage with other parts of the game. A presenter class might also use Scriptable Objects to provide data to UI elements
- 현재는 ScriptableObject의 범위를 크게 설정하여 특정 Sound Data만 모으지 않고, 모든 Sound Data를 모으고 있기 때문에 이런 방향으로 진행한다.

<br><br>

ScriptableObject가 singleton이나 static이 아닌데도 copy 없이 1개를 참조 하는 거 공부해서 적어
> A ScriptableObject is a data container that you can use to save large amounts of data, independent of class instances. One of the main use cases for ScriptableObjects is to reduce your project’s memory usage by avoiding copies of values.
> This means that there is one copy of the data in memory.

## Scriptable
