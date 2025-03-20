using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class zoomController : GamepadInputHandler // Inherit from GamepadInputHandler
{
    private Vector3 camPositionDefault; // Default camera position
    private Vector3 camPositionFinal; // Final camera position

    private float camSizeDefault; // Default camera size
    private float camSizeFinal; // Final camera size

    private float time; // Lerp progress
    public float resta; // Speed of returning to default

    private bool buttonCanBeActivated;
    private int multiCombo;

    public bool isInStore;

    public Camera mainCamera;

    void Start()
    {
        camPositionDefault = new Vector3(-2.5f, -1, 0);
        camPositionFinal = new Vector3(0, 0, 0);

        camSizeDefault = 3.5f;
        camSizeFinal = 5f;

        time = 0;
        resta = .05f;

        buttonCanBeActivated = true;
        multiCombo = 1;
    }

    protected override void OnButtonAPressed() // Handle A button press
    {
        Debug.Log("button a");

        if (!isInStore)
        {
            time += .01f * multiCombo;
        }
    }

    private void FixedUpdate()
    {
        // Gradually reduce the time value to return the camera to its default state
        if (time > 0)
        {
            time -= resta * multiCombo * .7f * Time.deltaTime;
        }
        else if (time < 0)
        {
            time = 0;
        }
        else if (time > 1)
        {
            time = 1;
        }

        // Adjust the speed of returning based on the current time value
        if (time < 0.25f)
        {
            resta = 0.05f;
        }
        if (time < 0.5f && time > 0.35f)
        {
            resta = 0.07f;
        }
        if (time < 0.75f && time > 0.65f)
        {
            resta = 0.09f;
        }
        if (time < 0.9f && time > 0.85f)
        {
            resta = 0.11f;
        }

        // Lerp the camera's position and size
        transform.localPosition = Vector3.Lerp(camPositionDefault, camPositionFinal, time);
        mainCamera.orthographicSize = Mathf.Lerp(camSizeDefault, camSizeFinal, time);
    }

    public void ZoomerController()
    {
        camPositionDefault = new Vector3(camPositionDefault.x + .13f, camPositionDefault.y + .05f, camPositionDefault.z);
        camSizeDefault += .08f;
        Debug.Log("get zoomed");
    }

    public void ButtonPressMultiComboZoom(int duracion, int intensidad)
    {
        if (buttonCanBeActivated)
        {
            if (intensidad <= 6)
            {
                multiCombo = intensidad / 2;
            }
            if (intensidad > 6)
            {
                multiCombo = intensidad / 3;
            }
            Invoke("PowerUpDisable", duracion);
            buttonCanBeActivated = false;
        }
    }

    private void PowerUpDisable()
    {
        multiCombo = 1;
        buttonCanBeActivated = true;
    }

    public void CamDefault()
    {
        camPositionDefault = new Vector3(-2.5f, -1, 0);
        camSizeDefault = 3.5f;
    }
}