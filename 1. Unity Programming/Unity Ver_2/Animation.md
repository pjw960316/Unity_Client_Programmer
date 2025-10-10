## :fireworks: Animator 기초 정보
- Windows -> Animation -> Animator parameter setting은 Transition의 네임이고 통일해야 한다.
- Unity Insepctor에서 Asset으로 받아온 animation state 와 Transition을 연결하고 Condition으로 transition의 발동 조건을 세팅한다. 그 외에 모든 것은 코드 영역에서 구현한다.

<br><br>

## :fire: Animation Controller 마다 Animation Parameter를 고유 할당해주어야 한다.

<br><br>

## :fire: Animation Clip만 다르고 로직은 같을 때, Animation Override Controller를 사용한다. 
> your game has different characters such as a goblin, ogre, and an elf. Each character uses different animation clips for idling, turning, and jogging but the structure, parameters, and logic of each state machine is the same. In this case, you can create a base Animator Controller for all characters and create an Animator Override Controller asset for each character.
- 잘 만들어 놓은 Animation Controller를 넣어주면 transition만 이식된다. AudioClip은 override 하면 된다.
- Assets > Create > Animation > Animator Override Controller