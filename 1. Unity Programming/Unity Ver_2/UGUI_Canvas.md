## :fire: UI Elements는 모두 Canvas 아래에 존재해야 한다.
> The Canvas is a Game Object with a Canvas component on it, and all UI elements must be children of such a Canvas.

> The first child is drawn first, the second child next

<br><br>

## :fire: Canvas의 Render Mode는 대부분 Screen Space - Overlay를 사용한다. <br> :fire: UI는 FieldObject와 독립적으로 존재시켜 Z-Buffer 경쟁을 피하고 <br> 항상 위로 오도록 한다.
> This render mode places UI elements on the screen rendered on top of the scene. If the screen is resized or changes resolution, the Canvas will automatically change size to match this.

<br><br>

## :fire: Canvas Scaler에서는 대부분 Scale With Screen Size을 사용한다 <br> 이를 통해 다양한 해상도에 대응한다.
> Makes UI elements bigger the bigger the screen is.
- TextMeshPro의 글자 크기, Image들의 Anchor만 제대로 설정했다면 해상도가 달라져도 Unity가 알아서 비율에 맞게 크기를 조절해준다.