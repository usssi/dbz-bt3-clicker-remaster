using UnityEngine;
using UnityEngine.InputSystem;

public abstract class GamepadInputHandler : MonoBehaviour
{
    private Gamepad gamepad;
    private Mouse mouse;
    private bool prevButtonAState;
    private bool prevLeftClickState;

    public int inputMultiplier = 1;

    protected virtual void Update()
    {
        gamepad = Gamepad.current;
        mouse = Mouse.current;

        bool buttonAPressed = IsButtonAPressed();
        bool buttonAReleased = IsButtonAReleased();
        bool leftClickPressed = IsLeftClickPressed();
        bool leftClickReleased = IsLeftClickReleased();

        if (buttonAPressed || leftClickPressed)
        {
            OnButtonAPressed();
        }
        else if (buttonAReleased || leftClickReleased)
        {
            OnButtonAReleased();
        }
    }

    protected bool IsButtonAPressed()
    {
        bool isPressed = gamepad?.buttonSouth.wasPressedThisFrame ?? false;
        return !prevButtonAState && isPressed;
    }

    protected bool IsButtonAReleased()
    {
        bool isReleased = gamepad?.buttonSouth.wasReleasedThisFrame ?? false;
        return prevButtonAState && isReleased;
    }

    protected bool IsLeftClickPressed()
    {
        bool isPressed = mouse?.leftButton.wasPressedThisFrame ?? false;
        return !prevLeftClickState && isPressed;
    }

    protected bool IsLeftClickReleased()
    {
        bool isReleased = mouse?.leftButton.wasReleasedThisFrame ?? false;
        return prevLeftClickState && isReleased;
    }

    protected abstract void OnButtonAPressed();
    protected virtual void OnButtonAReleased() { }
}
