using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class gamepadController : GamepadInputHandler
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

    protected override void Update()
    {
        base.Update();

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("goku");
        }

        if (cameraShake.shaketrue && Time.timeScale != 0 && !stackController.GetComponent<stackController>().isSelling)
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

        AdjustMagDurVal();
    }

    private void FixedUpdate()
    {
        if (magDurVal > 1)
        {
            magDurVal -= resta * inputPlus * Time.deltaTime;
        }
        else
        {
            magDurVal = 1;
        }
    }

    protected override void OnButtonAPressed()
    {
        if (cameraShake.shaketrue && Time.timeScale != 0 && !stackController.GetComponent<stackController>().isSelling)
        {
            StartCoroutine(cameraShake.Shake(duration * magDurVal, magnitude * magDurVal));
            magDurVal += magDurPlusser * inputPlus;
        }
    }

    protected override void OnButtonAReleased()
    {
        StopVibracion();
    }

    private void Vibracion()
    {
        if (canVibrate && Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(vibraI, vibraD);
            Invoke("StopVibracion", vibraDuration);
        }
        else
        {
            StopVibracion();
        }
    }

    private void StopVibracion()
    {
        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(0f, 0f);
        }
    }

    public void OnButtonActivatePowerUpShaker(int duracion, int intensidad)
    {
        if (buttonCanBeActivated)
        {
            inputPlus = intensidad;
            Invoke("PowerUpDisable", duracion);
            buttonCanBeActivated = false;
        }
    }

    private void PowerUpDisable()
    {
        inputPlus = 1;
        buttonCanBeActivated = true;
    }

    private void AdjustMagDurVal()
    {
        if (magDurVal < 1.5) resta = 0.08f;
        else if (magDurVal < 2) resta = 0.1f;
        else if (magDurVal < 3) resta = 0.12f;
        else if (magDurVal < 4) resta = 0.14f;
        else magDurVal = 4;
    }
}
