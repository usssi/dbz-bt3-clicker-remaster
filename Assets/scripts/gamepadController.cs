using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using XInputDotNetPure;
using UnityEngine.SceneManagement;



public class gamepadController : MonoBehaviour
{

    InputMaster controls;

    public cameraShake cameraShake;
    public float duration = 0.02f;
    public float magnitude = 0.02f;

    public float magDurPlusser = 0.02f;
    float magDurVal;
    public float resta;


    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    [Space]
    public float vibraDuration;
    public float vibraI;
    public float vibraD;

    public bool canVibrate;

    private bool buttonCanBeActivated;
    private int inputPlus;

    private void Awake()
    {
        controls = new InputMaster();
        controls.player.action.performed += PadActionA;

        vibraD = .5f;
        vibraI = .5f;

        canVibrate = true;
        inputPlus = 1;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("goku");
        }


        prevState = state;
        state = GamePad.GetState(playerIndex);

        if (canVibrate)
        {
            if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
            {
                StartCoroutine(cameraShake.Shake(duration * magDurVal, magnitude * magDurVal));
                magDurVal += magDurPlusser*inputPlus;

            }
        }
        else if (!canVibrate)
        {
            StopAllCoroutines();
        }
        

        if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released)
        {
            StopVibracion();
        }


        if (magDurVal < 1.5)
        {
            resta = 0.08f;
        }
        if (magDurVal < 2)
        {
            if (magDurVal > 1.5)
            {
                resta = 0.1f;
            }
        }
        if (magDurVal < 3)
        {
            if (magDurVal > 2.5)
            {
                resta = 0.12f;
            }
        }
        if (magDurVal < 4)
        {
            if (magDurVal > 3.5)
            {
                resta = 0.14f;

            }
        }
        if (magDurVal > 4)
        {
            magDurVal = 4;

        }

    

    }

    private void FixedUpdate()
    {       
        if (magDurVal > 1)
        {
            magDurVal -= resta * inputPlus * Time.deltaTime;
        }
        else if (magDurVal < 1)
        {
            magDurVal = 1;
        }

    }



    public void PadActionA(InputAction.CallbackContext context)
    {
        Vibracion();
    }

    private void OnEnable()
    {
        controls.player.Enable();
    }

    private void OnDisable()
    {
        controls.player.Disable();
    }

    private void Vibracion()
    {
        if (canVibrate)
        {
            GamePad.SetVibration(playerIndex, vibraI, vibraD);
            Invoke("StopVibracion", vibraDuration);
        }
        else if (!canVibrate)
        {
            StopVibracion();
        }
        
    }

    private void StopVibracion()
    {
        GamePad.SetVibration(playerIndex, 0f, 0f);
    }

    public void OnButtonActivatePowerUpShaker(int duracion, int intensidad)
    {
        if (buttonCanBeActivated)
        {
            inputPlus = intensidad;
            Invoke("PowerUpDisable", duracion);
            buttonCanBeActivated = false;
        }
        else
        {
            return;
        }

    }

    private void PowerUpDisable()
    {
        inputPlus = 1;
        buttonCanBeActivated = true;

    }
}
