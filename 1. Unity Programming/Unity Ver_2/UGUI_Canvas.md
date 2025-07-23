## :fire: Canvas
> The Canvas is a Game Object with a Canvas component on it, and all UI elements must be children of such a Canvas.

> The first child is drawn first, the second child next

<br><br>

## :fire: Canvas의 Render Mode는 Screen Space - Overlay를 사용한다. <br> UI를 Field의 GameObject와 독립적으로 존재해 Z-Buffer 경쟁을 피하고, 항상 위로 오도록 한다.
> This render mode places UI elements on the screen rendered on top of the scene. If the screen is resized or changes resolution, the Canvas will automatically change size to match this.