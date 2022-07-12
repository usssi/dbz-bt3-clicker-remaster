using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.UI;
using TMPro;

public class comboController : MonoBehaviour
{
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    private bool r1 = false;
    private bool l1 = false;
    private bool rLComboPress;

    public GameObject stackController;
    public GameObject platosContoller;
    public GameObject changeBGContoller;
    public GameObject ereele;
    public GameObject comboData;

    public GameObject gamepadController;

    private bool comboCanBeActivated;

    public int duration;
    public float durationTimerText;
    public int intensidad;

    public GameObject comboBarFondo;
    public GameObject ComboBarFill;
    public GameObject timeBarFill;
    public GameObject rayios;




    public float fillAmount;
    public float fillerCounter;
    private float maxFill = 1;

    public bool isPaused;

    bool shitTrue = false;

    float timerFillAmount;

    public GameObject doradoBarFill;

    public TMP_ColorGradient yellow;
    public TMP_ColorGradient cyan;

    public float lerpedshit;

    public float minimunFillTime;




    void Start()
    {
        comboCanBeActivated = true;
        ComboBarFill.GetComponent<Image>().fillAmount = fillAmount;

        //comboData.SetActive(false);
        durationTimerText = duration+1;

        rayios.SetActive(false);


    }

    void Update()
    {
        if (comboCanBeActivated)
        {
            if (fillAmount > 0)
            {
                fillAmount -= .1f * Time.deltaTime;
            }
        }
       
        if (fillAmount>1.1)
        {
            fillAmount = 1;
        }
        if (fillAmount<0)
        {
            fillAmount = 0;
        }


        //input controller R1+L1
        prevState = state;
        state = GamePad.GetState(playerIndex);

        if (prevState.Buttons.RightShoulder == ButtonState.Released && state.Buttons.RightShoulder == ButtonState.Pressed)
        {
            r1 = true;
        }

        if (prevState.Buttons.RightShoulder == ButtonState.Pressed && state.Buttons.RightShoulder == ButtonState.Released)
        {
            r1 = false;
        }

        if (prevState.Buttons.LeftShoulder == ButtonState.Released && state.Buttons.LeftShoulder == ButtonState.Pressed)
        {
            l1 = true;
        }

        if (prevState.Buttons.LeftShoulder == ButtonState.Pressed && state.Buttons.LeftShoulder == ButtonState.Released)
        {
            l1 = false;
        }

        if (comboCanBeActivated)
        {
            if (fillAmount > .99)
            {
                rayios.SetActive(true);

                ereele.SetActive(true);
                if (l1 && r1)
                {
                    durationTimerText = duration+1;

                    rLComboPress = true;
                    DoShit();
                }
            }
            else if (fillAmount < .99)
            {
                rayios.SetActive(false);

                ereele.SetActive(false);

            }

        }
      

        ///////////////////////////////


        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            if (!isPaused)
            {
                fillAmount += fillerCounter;
            }
        }

        ColorChanger();


        float minChanceGold = (((float)stackController.GetComponent<stackControllerPrueba>().minChanceGold * -1) + 100) / 100;
        if (lerpedshit<minChanceGold)
        {
            lerpedshit += minChanceGold * Time.deltaTime*3;
            if (lerpedshit>=minChanceGold)
            {
                lerpedshit = minChanceGold;
            }
        }
        doradoBarFill.GetComponent<Image>().fillAmount = lerpedshit;


    }


    private void FixedUpdate()
    {

        ComboBarFill.GetComponent<Image>().fillAmount = fillAmount;


        if (rLComboPress == true)
        {
            durationTimerText -= 1 * Time.deltaTime;
            shitTrue = true;
        }
       

        comboData.GetComponent<TextMeshProUGUI>().text = "x" + intensidad;


        if (shitTrue)
        {
            timerFillAmount = Mathf.Lerp(minimunFillTime, 1, durationTimerText / (duration + 1));

            timeBarFill.GetComponent<Image>().fillAmount = timerFillAmount;

            comboData.GetComponent<TextMeshProUGUI>().colorGradientPreset = cyan;
        }
        else
        {

            timeBarFill.GetComponent<Image>().fillAmount = 1;
            comboData.GetComponent<TextMeshProUGUI>().colorGradientPreset = yellow;


        }




    }

    public void DoShit()
    {
        ereele.SetActive(false);
        comboData.SetActive(true);


        FindObjectOfType<AudioManager>().Play("powerUP", 1f);
        comboCanBeActivated = false;

        stackController.GetComponent<stackControllerPrueba>().OnButtonActivatePowerUpStack(duration, intensidad);
        platosContoller.GetComponent<platosController>().OnButtonActivatePowerUpPlatos(duration, intensidad);
        changeBGContoller.GetComponent<changeBG>().ButtonPressMultiComboBGChange(duration, intensidad/2);
        changeBGContoller.GetComponent<zoomController>().ButtonPressMultiComboZoom(duration, intensidad);
        gamepadController.GetComponent<gamepadController>().OnButtonActivatePowerUpShaker(duration, intensidad);



        Invoke("StopDoingShit", duration);

    }

    private void StopDoingShit()
    {
        comboCanBeActivated = true;
        fillAmount = 0;
        rLComboPress = false;
        shitTrue = false;
    }

    void ColorChanger() 
    {
        Color fillColor = Color.Lerp(Color.green, Color.cyan, (fillAmount/maxFill));
        ComboBarFill.GetComponent<Image>().color = fillColor;
    }

   
}
