using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class pauseController : MonoBehaviour
{
    public GameObject playerObj;
    public GameObject gamepadControllerObj;
    public GameObject cameraObject;
    public GameObject comboController;
    public GameObject ereele;
    public GameObject storeController;

    public Button firstSelectedButton;

    public bool isCanvasOn;

    public GameObject panelController;

    private bool canBePaused;

    Color customGrey = new Color(.85f, .85f, .85f, 1);

    public GameObject titulo;

    private float timePingponged;
    private bool canGoDown;
    private bool canGoUp;

    Vector3 pene2 = new Vector3(.76f, .76f, .76f);
    Vector3 pene1;

    public GameObject buttonController;

    void Start()
    {
        isCanvasOn = false;
        canBePaused = true;
        canGoUp = true;
        timePingponged = 0;

        pene1 = titulo.transform.localScale;
    }

    private void Update()
    {
        if (canGoUp)
        {
            if (timePingponged < 1)
            {
                timePingponged += Time.fixedDeltaTime * 0.1f;
            }

            if (timePingponged >= 1)
            {
                canGoDown = true;
                timePingponged = 1;
                canGoUp = false;
            }
        }

        if (canGoDown)
        {
            if (timePingponged > 0)
            {
                timePingponged -= Time.fixedDeltaTime * 0.1f;
            }

            if (timePingponged <= 0)
            {
                timePingponged = 0;
                canGoUp = true;
                canGoDown = false;
            }
        }

        titulo.GetComponent<Image>().color = Color.Lerp(customGrey, Color.white, timePingponged);
        titulo.transform.localScale = Vector3.Lerp(pene1, pene2, timePingponged);

        // Pausar con ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (buttonController.GetComponent<buttonController>().optionsToggle)
            {
                ButtonEnableDisableCanvas();
            }
            else
            {
                buttonController.GetComponent<buttonController>().ButtonOptions();
                ButtonEnableDisableCanvas();
            }
        }

        // Pausar con Start del gamepad
        if (Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame)
        {
            if (buttonController.GetComponent<buttonController>().optionsToggle)
            {
                ButtonEnableDisableCanvas();
            }
            else
            {
                buttonController.GetComponent<buttonController>().ButtonOptions();
                ButtonEnableDisableCanvas();
            }
        }

        // Cerrar el menú con B
        if (isCanvasOn && Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            if (buttonController.GetComponent<buttonController>().optionsToggle)
            {
                ButtonEnableDisableCanvas();
            }
            else
            {
                buttonController.GetComponent<buttonController>().ButtonOptions();
            }
        }

        cameraObject.GetComponent<cameraShake>().enabled = !isCanvasOn;
    }

    public void ButtonEnableDisableCanvas()
    {
        if (!isCanvasOn)
        {
            if (canBePaused)
            {
                if (storeController.GetComponent<StoreController>().storeOn)
                {
                    storeController.GetComponent<StoreController>().StoreToggle();
                }

                panelController.GetComponent<controllerPauseCanvasSize>().gameObject.SetActive(true);

                FindObjectOfType<AudioManager>().Play("pauseOpen", 1.5f);

                firstSelectedButton.Select();
                isCanvasOn = true;

                Time.timeScale = 0;

                playerObj.GetComponent<platosController>().enabled = false;
                playerObj.GetComponent<stackControllerPrueba>().enabled = false;
                playerObj.GetComponent<gokuPrueba>().enabled = false;

                gamepadControllerObj.GetComponent<gamepadController>().vibraD = 0;
                gamepadControllerObj.GetComponent<gamepadController>().vibraI = 0;

                comboController.GetComponent<comboController>().isPaused = true;

                ereele.GetComponent<shakerScript>().magnitudeX = 0;
                ereele.GetComponent<shakerScript>().magnitudeY = 0;

                cameraObject.GetComponent<cameraShake>().enabled = false;
                cameraObject.GetComponent<changeBG>().enabled = false;
                cameraObject.GetComponent<zoomController>().enabled = false;
                storeController.GetComponent<StoreController>().gamePaused = true;

                canBePaused = false;
            }
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("pauseClose", 1.5f);

            panelController.GetComponent<controllerPauseCanvasSize>().CanvasAnimationExit();

            isCanvasOn = false;
            Time.timeScale = 1;

            playerObj.GetComponent<platosController>().enabled = true;
            playerObj.GetComponent<stackControllerPrueba>().enabled = true;
            playerObj.GetComponent<gokuPrueba>().enabled = true;

            gamepadControllerObj.GetComponent<gamepadController>().vibraD = 0.5f;
            gamepadControllerObj.GetComponent<gamepadController>().vibraI = 0.5f;

            comboController.GetComponent<comboController>().isPaused = false;

            ereele.GetComponent<shakerScript>().magnitudeX = 2;
            ereele.GetComponent<shakerScript>().magnitudeY = 2;

            cameraObject.GetComponent<cameraShake>().enabled = true;
            cameraObject.GetComponent<changeBG>().enabled = true;
            cameraObject.GetComponent<zoomController>().enabled = true;

            storeController.GetComponent<StoreController>().gamePaused = false;

            Invoke("CanBePausedResumed", .4f);
        }
    }

    private void CanBePausedResumed()
    {
        canBePaused = true;
    }
}
