using UnityEngine;
using XInputDotNetPure;

public abstract class GamepadInputHandler : MonoBehaviour
{
    protected GamePadState prevState;
    protected GamePadState state;
    public PlayerIndex playerIndex = PlayerIndex.One;

    // Multiplicador de inputs (puede ser aumentado con mejoras)
    public int inputMultiplier = 1;

    protected virtual void Update()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);

        // Verificar inputs válidos (botón A o clic izquierdo)
        if (IsButtonAPressed() || IsLeftClickPressed())
        {
            OnButtonAPressed();
        }
        else if (IsButtonAReleased() || IsLeftClickReleased())
        {
            OnButtonAReleased();
        }
    }

    // Verificar si el botón A está presionado
    protected bool IsButtonAPressed()
    {
        return prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed;
    }

    // Verificar si el botón A está liberado
    protected bool IsButtonAReleased()
    {
        return prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Released;
    }

    // Verificar si el clic izquierdo está presionado
    protected bool IsLeftClickPressed()
    {
        return Input.GetMouseButtonDown(0); // 0 es el clic izquierdo
    }

    // Verificar si el clic izquierdo está liberado
    protected bool IsLeftClickReleased()
    {
        return Input.GetMouseButtonUp(0); // 0 es el clic izquierdo
    }

    // Método abstracto para manejar la pulsación del botón
    protected abstract void OnButtonAPressed();

    // Método virtual para manejar la liberación del botón (opcional)
    protected virtual void OnButtonAReleased() { }
}