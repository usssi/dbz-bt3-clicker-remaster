    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class buttonController : MonoBehaviour
{
    public GameObject cameraObject; 
    public GameObject gamepadController;
    private bool juiceToogleBool;

    public AudioMixer masterMixer;

    private bool soundOnOff;



    // Start is called before the first frame update
    void Start()
    {
        soundOnOff = true;
        juiceToogleBool = true;

    }


    public void ButtonToggleJuice()
    {
        if (juiceToogleBool)
        {
            cameraObject.layer = 0;
            cameraObject.GetComponent<cameraShake>().shaketrue = false;
            gamepadController.GetComponent<gamepadController>().canVibrate = false;

            juiceToogleBool = false;
            FindObjectOfType<AudioManager>().Play("genericButtonOff", 1);


        }
        else if (!juiceToogleBool)
        {
            cameraObject.layer = 6;

            cameraObject.GetComponent<cameraShake>().shaketrue = true;
            gamepadController.GetComponent<gamepadController>().canVibrate = true;

            juiceToogleBool = true;
            FindObjectOfType<AudioManager>().Play("genericButtonOn", 1);

        }
    }

    public void ButtonToggleSound()
    {
        if (soundOnOff)
        {
            FindObjectOfType<AudioManager>().Play("genericButtonOff", 1);

            masterMixer.SetFloat("uiVol", -90);
            masterMixer.SetFloat("bgVol", -90);

            soundOnOff = false;
        }
        else if (soundOnOff==false)
        {
            masterMixer.SetFloat("uiVol", 0);
            masterMixer.SetFloat("bgVol", 0);

            soundOnOff = true;
            FindObjectOfType<AudioManager>().Play("genericButtonOn", 1);

        }

    }

    public void ButtonQuit()
    {
        Application.Quit();
        FindObjectOfType<AudioManager>().Play("genericButtonOff", 1);

    }
}
