using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using XInputDotNetPure;

public class stackControllerPrueba : GamepadInputHandler // Inherit from GamepadInputHandler
{
    public GameObject stack;
    public GameObject stackDorado;
    private GameObject stackSpawner;
    public GameObject stackParent;
    private GameObject instantiatedStack;

    public float positionOffsetX = 0f;
    public float positionOffsetY = 0f;

    private bool stackOne;

    public int numeroStack;

    private float pitch = 0;
    private float pitchPlus = .05f;

    private int inputPlus = 1;

    private bool buttonCanBeActivated = true;

    public int minChanceGold;
    private bool shouldBeGolden;

    private void Start()
    {
        stackOne = false;
    }

    protected override void OnButtonAPressed() // Handle A button press
    {
        if (numeroStack >= 0 && numeroStack <= 58)
        {
            numeroStack += inputPlus;
        }
        else if (numeroStack >= 59)
        {
            pitch += pitchPlus;
            StackOne();
            numeroStack = 0;
        }
    }

    private void StackOne()
    {
        GetRandomNumber();

        FindObjectOfType<stackController>().Stackeador(shouldBeGolden);

        FindObjectOfType<AudioManager>().Play("stack", .5f + pitch);
    }

    // Calculate chance of gold stack
    void GetRandomNumber()
    {
        int randomValue = Random.Range(1, 100);

        if (randomValue >= 1 && randomValue <= minChanceGold)
        {
            shouldBeGolden = false;
            FindObjectOfType<AudioManager>().Play("platosComplete", 1);
        }
        else if (randomValue > minChanceGold && randomValue <= 100)
        {
            shouldBeGolden = true;
            FindObjectOfType<AudioManager>().Play("platosComplete", 7);
            FindObjectOfType<AudioManager>().Play("platosCompleteGold", 1);
        }
    }

    public void DefaultSpawnerPosition()
    {
        pitch = 0;
    }

    #region PowerUp Logic
    public void OnButtonActivatePowerUpStack(int duracion, int intensidad)
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
    #endregion
}