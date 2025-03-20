using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using XInputDotNetPure;
using UnityEngine.SceneManagement;

public class gamepadController : GamepadInputHandler // Inherit from GamepadInputHandler
{
    public cameraShake cameraShake;
    public float duration = 0.02f;
    public float magnitude = 0.02f;

    public float magDurPlusser = 0.02f;
    float magDurVal;
    public float resta;

    [Space]
    public float vibraDuration;
    public float vibraI;
    public float vibraD;

    public bool canVibrate;

    private bool buttonCanBeActivated;
    private int inputPlus;

    public GameObject stackController;

    private void Awake()
    {
        vibraD = .5f;
        vibraI = .5f;

        canVibrate = true;
        inputPlus = 1;
    }

    protected override void Update() // Override the base class Update method
    {
        base.Update(); // Call the base class Update method to ensure input handling works

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("goku");
        }

        if (cameraShake.GetComponent<cameraShake>().shaketrue && Time.timeScale != 0 && !stackController.GetComponent<stackController>().isSelling)
        {
            if (IsButtonAPressed())
            {
                StartCoroutine(cameraShake.Shake(duration * magDurVal, magnitude * magDurVal));
                magDurVal += magDurPlusser * inputPlus;
            }
        }
        else
        {
            StopAllCoroutines();
        }

        if (IsButtonAReleased())
        {
            StopVibracion();
        }

        if (magDurVal < 1.5)
        {
            resta = 0.08f;
        }
        if (magDurVal < 2 && magDurVal > 1.5)
        {
            resta = 0.1f;
        }
        if (magDurVal < 3 && magDurVal > 2.5)
        {
            resta = 0.12f;
        }
        if (magDurVal < 4 && magDurVal > 3.5)
        {
            resta = 0.14f;
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

    protected override void OnButtonAPressed() // Handle A button press
    {
        if (cameraShake.GetComponent<cameraShake>().shaketrue && Time.timeScale != 0 && !stackController.GetComponent<stackController>().isSelling)
        {
            StartCoroutine(cameraShake.Shake(duration * magDurVal, magnitude * magDurVal));
            magDurVal += magDurPlusser * inputPlus;
        }
    }

    protected override void OnButtonAReleased() // Handle A button release
    {
        StopVibracion();
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