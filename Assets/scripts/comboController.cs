using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class comboController : GamepadInputHandler // Inherit from GamepadInputHandler
{
    private bool r1 = false;
    private bool l1 = false;
    private bool rLComboPress;

    public GameObject stackController;
    public GameObject platosContoller;
    public GameObject changeBGContoller;
    public GameObject ereele;
    public GameObject maxPower;
    public GameObject boostText;
    public GameObject aAnimationButton;
    public GameObject inputCounterImage;

    public GameObject comboData;

    public GameObject gamepadController;

    public bool comboCanBeActivated;

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

    public GameObject inpuTExt;
    public GameObject comboFinalNUmber;

    public GameObject stackRealController;

    public bool autoCombo;

    void Start()
    {
        comboCanBeActivated = true;
        ComboBarFill.GetComponent<Image>().fillAmount = fillAmount;

        durationTimerText = duration + 1;

        rayios.SetActive(false);
        boostText.SetActive(false);
        aAnimationButton.SetActive(false);
        inputCounterImage.SetActive(false);

        comboFinalNUmber.SetActive(false);
    }

    protected override void Update() // Override the base class Update method
    {
        base.Update(); // Call the base class Update method to ensure input handling works

        if (comboCanBeActivated)
        {
            if (fillAmount > 0)
            {
                fillAmount -= .1f * Time.deltaTime;
            }
        }

        if (fillAmount > 1.1)
        {
            fillAmount = 1;
        }
        if (fillAmount < 0)
        {
            fillAmount = 0;
        }

        // Handle R1 and L1 inputs
        if (IsRightShoulderPressed())
        {
            r1 = true;
        }
        if (IsRightShoulderReleased())
        {
            r1 = false;
        }
        if (IsLeftShoulderPressed())
        {
            l1 = true;
        }
        if (IsLeftShoulderReleased())
        {
            l1 = false;
        }

        if (comboCanBeActivated)
        {
            if (fillAmount > .99)
            {
                rayios.SetActive(true);
                maxPower.SetActive(true);
                ereele.SetActive(true);

                if ((l1 && r1) || autoCombo || Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
                {
                    durationTimerText = duration + 1;
                    rLComboPress = true;
                    DoShit();
                }
            }
            else if (fillAmount < .99)
            {
                rayios.SetActive(false);
                maxPower.SetActive(false);
                ereele.SetActive(false);
            }
        }

        ColorChanger();

        float minChanceGold = (((float)stackController.GetComponent<stackControllerPrueba>().minChanceGold * -1) + 100) / 100;
        if (lerpedshit < minChanceGold)
        {
            lerpedshit += minChanceGold * Time.deltaTime * 3;
            if (lerpedshit >= minChanceGold)
            {
                lerpedshit = minChanceGold;
            }
        }
        doradoBarFill.GetComponent<Image>().fillAmount = lerpedshit;

        inpuTExt.GetComponent<TextMeshProUGUI>().text = platosContoller.GetComponent<platosController>().inputcount.ToString();
    }

    protected override void OnButtonAPressed() // Implement the abstract method
    {
        if (!isPaused && stackRealController.GetComponent<stackController>().isSelling == false)
        {
            fillAmount += fillerCounter;
        }
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
        maxPower.SetActive(false);

        comboData.SetActive(true);
        boostText.SetActive(true);
        aAnimationButton.SetActive(true);
        inputCounterImage.SetActive(true);

        comboBarFondo.GetComponent<Image>().color = Color.cyan;

        FindObjectOfType<AudioManager>().Play("powerUP", 1f);
        comboCanBeActivated = false;

        stackController.GetComponent<stackControllerPrueba>().OnButtonActivatePowerUpStack(duration, intensidad);
        platosContoller.GetComponent<platosController>().OnButtonActivatePowerUpPlatos(duration, intensidad);
        changeBGContoller.GetComponent<changeBG>().ButtonPressMultiComboBGChange(duration, intensidad / 2);
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
        boostText.SetActive(false);
        aAnimationButton.SetActive(false);
        inputCounterImage.SetActive(false);

        comboFinalNUmber.SetActive(true);

        comboFinalNUmber.GetComponent<TextMeshProUGUI>().color = Color.white;
        comboFinalNUmber.GetComponent<TextMeshProUGUI>().text = inpuTExt.GetComponent<TextMeshProUGUI>().text;

        StartCoroutine(DoAThingOverTime(Color.white, Color.clear, 2));

        comboBarFondo.GetComponent<Image>().color = Color.white;
    }

    void ColorChanger()
    {
        Color fillColor = Color.Lerp(Color.green, Color.cyan, (fillAmount / maxFill));
        ComboBarFill.GetComponent<Image>().color = fillColor;
    }

    IEnumerator DoAThingOverTime(Color start, Color end, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            comboFinalNUmber.GetComponent<TextMeshProUGUI>().color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        comboFinalNUmber.GetComponent<TextMeshProUGUI>().color = end;
        comboFinalNUmber.SetActive(false);
    }

    private bool IsRightShoulderPressed()
    {
        return Gamepad.current != null && Gamepad.current.rightShoulder.wasPressedThisFrame;
    }

    private bool IsRightShoulderReleased()
    {
        return Gamepad.current != null && Gamepad.current.rightShoulder.wasReleasedThisFrame;
    }

    private bool IsLeftShoulderPressed()
    {
        return Gamepad.current != null && Gamepad.current.leftShoulder.wasPressedThisFrame;
    }

    private bool IsLeftShoulderReleased()
    {
        return Gamepad.current != null && Gamepad.current.leftShoulder.wasReleasedThisFrame;
    }

}