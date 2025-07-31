## :fire: Canvas
> The Canvas is a Game Object with a Canvas component on it, and all UI elements must be children of such a Canvas.

> The first child is drawn first, the second child next

<br><br>

## :fire: Canvas의 Render Mode는 Screen Space - Overlay를 사용한다. <br> UI를 Field의 GameObject와 독립적으로 존재해 Z-Buffer 경쟁을 피하고, 항상 위로 오도록 한다.
> This render mode places UI elements on the screen rendered on top of the scene. If the screen is resized or changes resolution, the Canvas will automatically change size to match this.


<br><br>

## :Fire: Scale With Screen Size을 이용해서 다양한 해상도에 대응한다.

<br><br>

## :fire: 어떤 UI Object에게 할당할 수 있는 공간이 최대 50이라고 가정한다. <br> Min이 30이고, preferred가 80일 때 Layout Element는 50을 할당한다. <br> min을 만족시키지만 preferred는 50의 한계가 있어서 80을 할당해 줄 수 없다. <br> :fire: preferred를 flexible이 0일 때 Max로 봐도 무방하다.
> Their heights are determined by their respective minimum, preferred, and flexible heights according to the following model

> Preferred : this layout element should have before additional **available value** is allocated. 