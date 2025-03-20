using UnityEngine;
using XInputDotNetPure;

public abstract class GamepadInputHandler : MonoBehaviour
{
    protected GamePadState prevState;
    protected GamePadState state;
    public PlayerIndex playerIndex = PlayerIndex.One;

    protected virtual void Update()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);

        // Check for valid inputs (A button, Spacebar, or Left Click)
        if (IsButtonAPressed() || IsSpacebarPressed() || IsLeftClickPressed())
        {
            OnButtonAPressed();
        }
        else if (IsButtonAReleased() || IsSpacebarReleased() || IsLeftClickReleased())
        {
            OnButtonAReleased();
        }
    }

    // Check if the A button is pressed
    protected bool IsButtonAPressed()
    {
        return prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed;
    }

    // Check if the A button is released
    protected bool IsButtonAReleased()
    {
        return prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Released;
    }

    // Check if the Spacebar is pressed
    protected bool IsSpacebarPressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    // Check if the Spacebar is released
    protected bool IsSpacebarReleased()
    {
        return Input.GetKeyUp(KeyCode.Space);
    }

    // Check if the Left Mouse Button is pressed
    protected bool IsLeftClickPressed()
    {
        return Input.GetMouseButtonDown(0); // 0 is the left mouse button
    }

    // Check if the Left Mouse Button is released
    protected bool IsLeftClickReleased()
    {
        return Input.GetMouseButtonUp(0); // 0 is the left mouse button
    }

    protected abstract void OnButtonAPressed();
    protected virtual void OnButtonAReleased() { }
}