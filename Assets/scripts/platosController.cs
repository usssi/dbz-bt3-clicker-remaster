using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class platosController : GamepadInputHandler // Inherit from GamepadInputHandler
{
    private float numeroPlato = 0f; // Cambio a float para interpolación suave
    public SpriteRenderer platoDefinitivo;
    public Sprite[] platosArray;
    public int platoArrayCount;

    private float pitchPlus = .1f;
    public int inputPlus = 1;
    private int inputPlusOriginal = 1;

    private bool buttonCanBeActivated = true;
    public int inputcount;

    private bool bool1;
    private bool bool2;

    private stackControllerPrueba stackController;
    private float smoothSpeed = 5f; // Velocidad de interpolación

    private int triggerCount = 1; // Counter to track the number of times the condition is met

    private void Start()
    {
        bool1 = true;
        stackController = FindObjectOfType<stackControllerPrueba>();
    }

    protected override void OnButtonAPressed() // Handle A button press
    {
        if (stackController != null)
        {
            int targetNumeroPlato = stackController.numeroStack;
            if (targetNumeroPlato >= 0 && targetNumeroPlato <= 58)
            {
                inputcount += inputPlus;
            }
            else if (targetNumeroPlato >= 59)
            {
                numeroPlato = 54; // Mantener en el último valor en lugar de resetear a 0
            }
        }
    }

    protected override void Update()
    {
        base.Update(); // Ensure base input handling works
        if (stackController != null)
        {
            if (stackController.numeroStack == 0)
            {
                numeroPlato = 0; // Sin interpolación si es 0
            }
            else if (stackController.numeroStack >= 59)
            {
                numeroPlato = 54; // Evita que parpadee volviendo a 0
            }
            else
            {
                numeroPlato = Mathf.Lerp(numeroPlato, stackController.numeroStack, Time.deltaTime * smoothSpeed);
            }
        }

        UpdatePlateSprite();
    }

    void UpdatePlateSprite()
    {
        int newPlatoArrayCount = Mathf.Clamp((int)(numeroPlato / 6), 0, platosArray.Length - 1);
        if (newPlatoArrayCount != platoArrayCount)
        {
            platoArrayCount = newPlatoArrayCount;
            platoDefinitivo.sprite = platosArray[platoArrayCount];
            PlaySoundPitched(pitchPlus * platoArrayCount);
        }
    }

    void PlaySoundPitched(float pitch)
    {
        FindObjectOfType<AudioManager>().Play("plato", 1 + pitch);
    }

    public void OnButtonActivatePowerUpPlatos(int duracion, int intensidad)
    {
        if (buttonCanBeActivated)
        {
            inputPlusOriginal = inputPlus;
            inputPlus = intensidad;
            Invoke("PowerUpDisable", duracion + .1f);
            buttonCanBeActivated = false;
            inputcount = 0;
        }
    }

    private void PowerUpDisable()
    {
        inputPlus = inputPlusOriginal;
        buttonCanBeActivated = true;
        inputcount = 0;
    }
    public void InputPlusLogicMulti()
    {
        if (inputPlus < 10)
        {
            triggerCount++; // Increase the counter

            if (triggerCount == 2) // Every two triggers
            {
                inputPlus++; // Increment inputPlus
                triggerCount = 0; // Reset counter
            }
        }
    }
}
