using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;




public class buttonController : MonoBehaviour
{
    public GameObject cameraObject; 
    public GameObject gamepadController;

    private bool juiceToogleBool;

    public AudioMixer masterMixer;

    private bool soundOnOff;

    public bool optionsToggle = true;

    private bool boostToggle;

    private bool vibrateBool;



    public GameObject jugoText;
    public GameObject sonidoText;
    public GameObject autocomboText;
    public GameObject vibrateText;


    public GameObject comboController;
    public GameObject botonesOptions;
    public GameObject mainOptions;

    public Button firstSelectedButton;
    public Button secondSelectedButton;

    public GameObject panelController;


    // Start is called before the first frame update
    void Awake()
    {
        soundOnOff = true;
        juiceToogleBool = true;
        boostToggle = comboController.GetComponent<comboController>().autoCombo;
        botonesOptions.SetActive(false);
        vibrateBool = gamepadController.GetComponent<gamepadController>().canVibrate;
    }

    private void Update()
    {
        if (juiceToogleBool)
        {
            jugoText.GetComponent<TextMeshProUGUI>().color = Color.green;
        }
        else
        {
            jugoText.GetComponent<TextMeshProUGUI>().color = Color.red;
        }

        if (soundOnOff)
        {
            sonidoText.GetComponent<TextMeshProUGUI>().color = Color.green;
        }
        else
        {
            sonidoText.GetComponent<TextMeshProUGUI>().color = Color.red;
        }

        if (boostToggle)
        {
            autocomboText.GetComponent<TextMeshProUGUI>().color = Color.green;
        }
        else
        {
            autocomboText.GetComponent<TextMeshProUGUI>().color = Color.red;
        }

        if (vibrateBool)
        {
            vibrateText.GetComponent<TextMeshProUGUI>().color = Color.green;
            gamepadController.GetComponent<gamepadController>().canVibrate = true;
        }
        else
        {
            vibrateText.GetComponent<TextMeshProUGUI>().color = Color.red;
            gamepadController.GetComponent<gamepadController>().canVibrate = false;

        }

    }



    public void ButtonOptions()
    {
        if (optionsToggle)
        {
            FindObjectOfType<AudioManager>().Play("pauseOpen", 1);

            optionsToggle = false;

            mainOptions.SetActive(false);
            botonesOptions.SetActive(true);

            firstSelectedButton.Select();

            panelController.GetComponent<controllerPauseCanvasSize>().GrowUI();


        }
        else if (optionsToggle == false)
        {

            optionsToggle = true;
            FindObjectOfType<AudioManager>().Play("pauseClose", 1);

            mainOptions.SetActive(true);
            botonesOptions.SetActive(false);

            secondSelectedButton.Select();

            panelController.GetComponent<controllerPauseCanvasSize>().DownUI();

            Invoke("OgUI", .2f);

        }

    }

    public void OgUI()
    {
        panelController.GetComponent<controllerPauseCanvasSize>().OriginalUI();
    }

    public void ButtonToggleJuice()
    {
        if (juiceToogleBool)
        {
            cameraObject.layer = 0;
            cameraObject.GetComponent<cameraShake>().shaketrue = false;

            juiceToogleBool = false;
            FindObjectOfType<AudioManager>().Play("genericButtonOff", 1);

            jugoText.GetComponent<TextMeshProUGUI>().text = "JUGON'T";



        }
        else if (!juiceToogleBool)
        {
            cameraObject.layer = 6;

            cameraObject.GetComponent<cameraShake>().shaketrue = true;

            juiceToogleBool = true;
            FindObjectOfType<AudioManager>().Play("genericButtonOn", 1);
            jugoText.GetComponent<TextMeshProUGUI>().text = "JUGO";


        }
    }
    public void ButtonToggleBoost()
    {
        if (boostToggle)
        {
            FindObjectOfType<AudioManager>().Play("genericButtonOff", 1);


            boostToggle = false;
            comboController.GetComponent<comboController>().autoCombo = false;

            autocomboText.GetComponent<TextMeshProUGUI>().text = "MANUAL-BOOST!";

        }
        else if (boostToggle == false)
        {

            boostToggle = true;
            comboController.GetComponent<comboController>().autoCombo = true;

            FindObjectOfType<AudioManager>().Play("genericButtonOn", 1);

            autocomboText.GetComponent<TextMeshProUGUI>().text = "AUTO-BOOST!";

        }

    }

    public void ButtonToggleVibra()
    {
        if (vibrateBool)
        {
            FindObjectOfType<AudioManager>().Play("genericButtonOff", 1);

            vibrateBool = false;
            gamepadController.GetComponent<gamepadController>().canVibrate = false;

            vibrateText.GetComponent<TextMeshProUGUI>().text = "VIBRACION'T";

        }
        else if (vibrateBool == false)
        {

            vibrateBool = true;
            gamepadController.GetComponent<gamepadController>().canVibrate = true;


            FindObjectOfType<AudioManager>().Play("genericButtonOn", 1);



            vibrateText.GetComponent<TextMeshProUGUI>().text = "VIBRACION";

        }

    }

    public void ButtonToggleSound()
    {
        if (soundOnOff)
        {
            //FindObjectOfType<AudioManager>().Play("genericButtonOff", 1);

            masterMixer.SetFloat("uiVol", -90);
            masterMixer.SetFloat("bgVol", -90);

            soundOnOff = false;

            sonidoText.GetComponent<TextMeshProUGUI>().text = "SONIDON'T";

        }
        else if (soundOnOff == false)
        {
            masterMixer.SetFloat("uiVol", 0);
            masterMixer.SetFloat("bgVol", 0);

            soundOnOff = true;
            FindObjectOfType<AudioManager>().Play("genericButtonOn", 1);

            sonidoText.GetComponent<TextMeshProUGUI>().text = "SONIDO";


        }

    }

    public void ButtonQuit()
    {
        Application.Quit();
        FindObjectOfType<AudioManager>().Play("genericButtonOff", 1);

    }

}
