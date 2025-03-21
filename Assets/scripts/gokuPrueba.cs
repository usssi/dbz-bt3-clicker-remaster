using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using XInputDotNetPure;

public class gokuPrueba : GamepadInputHandler // Inherit from GamepadInputHandler
{
    public GameObject goku1;
    public GameObject goku2;

    private int numeroGoku = 0;

    private bool esperaPaClickear = true;

    public GameObject combocontroller;
    public bool boostIsOn;

    private bool hasPlayedReleaseSound = false; // Flag to track if the release sound has been played

    private void Awake()
    {
        Invoke("Esperaculiado", 0.1f);

        boostIsOn = combocontroller.GetComponent<comboController>().comboCanBeActivated;
    }

    protected override void Update() // Override the base class Update method
    {
        base.Update(); // Call the base class Update method to ensure input handling works

        boostIsOn = combocontroller.GetComponent<comboController>().comboCanBeActivated;
    }

    protected override void OnButtonAPressed() // Handle A button press
    {
        if (esperaPaClickear == false)
        {
            if (numeroGoku == 0)
            {
                numeroGoku = 1;
                goku1.SetActive(true);
                goku2.SetActive(false);
            }
            else if (numeroGoku == 1)
            {
                numeroGoku = 0;
                goku1.SetActive(false);
                goku2.SetActive(true);
            }

            FindObjectOfType<AudioManager>().Play("click_down", Random.Range(0.7f, 1.3f));
            if (!boostIsOn)
            {
                FindObjectOfType<AudioManager>().Play("boostedDOWN", 1);
            }

            hasPlayedReleaseSound = false; // Reset the flag when the button is pressed
        }
    }

    protected override void OnButtonAReleased() // Handle A button release
    {
        if (esperaPaClickear == false && !hasPlayedReleaseSound) // Check if the sound hasn't been played yet
        {
            FindObjectOfType<AudioManager>().Play("click_up", Random.Range(0.7f, 1.3f));
            if (!boostIsOn)
            {
                FindObjectOfType<AudioManager>().Play("boostedUP", 1);
            }

            hasPlayedReleaseSound = true; // Set the flag to prevent the sound from playing again
        }
    }

    void Esperaculiado()
    {
        esperaPaClickear = false;
    }
}