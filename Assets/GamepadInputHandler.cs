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

        // Verificar inputs v�lidos (bot�n A o clic izquierdo)
        if (IsButtonAPressed() || IsLeftClickPressed())
        {
            OnButtonAPressed();
        }
        else if (IsButtonAReleased() || IsLeftClickReleased())
        {
            OnButtonAReleased();
        }
    }

    // Verificar si el bot�n A est� presionado
    protected bool IsButtonAPressed()
    {
        return prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed;
    }

    // Verificar si el bot�n A est� liberado
    protected bool IsButtonAReleased()
    {
        return prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Released;
    }

    // Verificar si el clic izquierdo est� presionado
    protected bool IsLeftClickPressed()
    {
        return Input.GetMouseButtonDown(0); // 0 es el clic izquierdo
    }

    // Verificar si el clic izquierdo est� liberado
    protected bool IsLeftClickReleased()
    {
        return Input.GetMouseButtonUp(0); // 0 es el clic izquierdo
    }

    // M�todo abstracto para manejar la pulsaci�n del bot�n
    protected abstract void OnButtonAPressed();

    // M�todo virtual para manejar la liberaci�n del bot�n (opcional)
    protected virtual void OnButtonAReleased() { }
}