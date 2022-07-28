using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.UI;
using TMPro;

public class StoreController : MonoBehaviour
{
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    public GameObject storeCanvas;
    public bool storeOn;
    public bool gamePaused;

    [Space]
    public GameObject shinyController;
    public GameObject comboController;
    public GameObject moneyText;

    public int maxMinShinyChance;
    public int maxDuration;

    [Space]
    public Button botonMulti;
    public Button botonDuration;
    public Button botonShiny;
    private float pitchMulti;
    private float pitchDuration;
    private float pitchShiny;

    public int money;
    public float currentMoney;
    public int initialMoney;

    public GameObject gamepadController;
    public GameObject playerController;
    public GameObject camController;

    public int precioMulti;
    public int precioTimer;
    public int precioShiny;

    private int precioMultiMulti;
    private int precioTimerMulti;
    private int precioShinyMulti;


    public Button firstSelectedButton;

    public TextMeshProUGUI precioXxText;
    public TextMeshProUGUI precioTimeText;
    public TextMeshProUGUI precioShinyText;

    public TextMeshProUGUI multitext, multiinfo, timetext, timeinfo, shinytext, shinyinfo;

    private bool yPressed;

    public GameObject timeBarFill;
    private bool timeAnimationBool;
    private float lerpedAnimationTimer;

    public GameObject multiplierTxt;
    private bool multiAnimationBool;
    private float lerpedAnimationMultiplier;

    public GameObject particulaes;
    public GameObject particulaCuadrada;
    public GameObject particulaShiny;
    public GameObject particulaButton;





    void Start()
    {
        storeCanvas.SetActive(false);
        storeOn = false;

        precioMultiMulti = 50;
        precioTimerMulti = 50;
        precioShinyMulti = 50;

    }

    // Update is called once per frame
    void Update()
    {

        TimeAnimation();
        MultiAnimation();

        prevState = state;
        state = GamePad.GetState(playerIndex);

        if (gamePaused == false)
        {
            if (prevState.Buttons.Back == ButtonState.Released && state.Buttons.Back == ButtonState.Pressed)
            {
                StoreToggle();
            }
        }

        //animacion money
        if (currentMoney != money)
        {
            if (initialMoney < money)
            {

                currentMoney += (1.5f * Time.deltaTime) * (money - initialMoney);
                if (currentMoney >= money)
                {
                    currentMoney = money;
                }

            }
            else
            {
                currentMoney -= (1.5f * Time.deltaTime) * (initialMoney - money);
                if (currentMoney <= money)
                {
                    currentMoney = money;
                }
            }
        }

        moneyText.GetComponent<TextMeshProUGUI>().text = "$" + currentMoney.ToString("0");

        /////////////////////////////////////////////////////////////////////////////
        if (storeOn)
        {
            storeCanvas.SetActive(true);
            storeOn = true;
            playerController.SetActive(false);
            comboController.GetComponent<comboController>().isPaused = true;
            gamepadController.GetComponent<gamepadController>().canVibrate = false;
            camController.GetComponent<zoomController>().isInStore = true;
            camController.GetComponent<changeBG>().isInStore = true;

            if (prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed)
            {
                StoreToggle();
            }

        }

        precioXxText.text = "$" + precioMulti.ToString();
        precioTimeText.text = "$" + precioTimer.ToString();
        precioShinyText.text = "$" + precioShiny.ToString();


        if (prevState.Buttons.Y == ButtonState.Released && state.Buttons.Y == ButtonState.Pressed)
        {
            yPressed = true;

        }

        if (prevState.Buttons.Y == ButtonState.Pressed && state.Buttons.Y == ButtonState.Released)
        {
            yPressed = false;
        }

        if (yPressed)
        {
            if (prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed)
            {
                MoneyUP();
            }
        }

        //money color
        if (money >= precioShiny)
        {
            precioShinyText.color = Color.green;
        }
        else
        {
            precioShinyText.color = Color.red;

        }

        if (money >= precioTimer)
        {
            precioTimeText.color = Color.green;
        }
        else
        {
            precioTimeText.color = Color.red;

        }

        if (money >= precioMulti)
        {
            precioXxText.color = Color.green;
        }
        else
        {
            precioXxText.color = Color.red;

        }


    }

    public void StoreToggle()
    {
        if (storeOn == false)
        {
            firstSelectedButton.Select();
            storeCanvas.SetActive(true);
            storeOn = true;
            FindObjectOfType<AudioManager>().Play("storeOpen", 1f);
            playerController.SetActive(false);
            comboController.GetComponent<comboController>().isPaused = true;
            gamepadController.GetComponent<gamepadController>().canVibrate = false;
            camController.GetComponent<zoomController>().isInStore = true;
            camController.GetComponent<changeBG>().isInStore = true;

            if (prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed)
            {
                StoreToggle();
            }

        }
        else if (storeOn)
        {
            StopAllCoroutines();
            storeCanvas.GetComponent<Animator>().SetBool("storeout", true);
            Invoke("CloseStore", .2f);

            storeOn = false;
            FindObjectOfType<AudioManager>().Play("storeClose", 1);
            playerController.SetActive(true);
            comboController.GetComponent<comboController>().isPaused = false;
            gamepadController.GetComponent<gamepadController>().canVibrate = true;
            camController.GetComponent<zoomController>().isInStore = false;
            camController.GetComponent<changeBG>().isInStore = false;

        }

    }


    public void StoreStackShiny()
    {

        if (money >= precioShiny)
        {
            FindObjectOfType<AudioManager>().Play("buyShiny", 1 + pitchShiny);
            pitchShiny -= .05f;

            if (shinyController.GetComponent<stackControllerPrueba>().minChanceGold > maxMinShinyChance)
            {
                shinyController.GetComponent<stackControllerPrueba>().minChanceGold -= 10;

            }
            initialMoney = (int)currentMoney;

            money = money - precioShiny;

            precioShiny += precioShinyMulti;

            precioShinyMulti += 50;

            var colors = botonShiny.colors;

            colors.pressedColor = Color.green;
            botonShiny.colors = colors;

            Instantiate(particulaShiny, new Vector3(-6.83f, 1.15f, 0), transform.rotation);

            GameObject buttonParticle = Instantiate(particulaes, new Vector3(0.24f, -2.79f, 0), transform.rotation);
            buttonParticle.transform.localScale = new Vector3(5, 5, 1);

        }
        else
        {
            FindObjectOfType<AudioManager>().Play("buttonDenied", 1);

            var colors = botonShiny.colors;

            colors.pressedColor = Color.red;
            botonShiny.colors = colors;


        }

        if (shinyController.GetComponent<stackControllerPrueba>().minChanceGold == maxMinShinyChance)
        {
            botonShiny.interactable = false;
            //botonShiny.enabled = false;
            precioShinyText.enabled = false;
            firstSelectedButton.Select();

            shinytext.color = Color.grey;
            shinyinfo.color = Color.grey;

        }
    }

    public void StoreDuration()
    {

        if (money >= precioTimer)
        {
            timeAnimationBool = true;
            lerpedAnimationTimer = 0;

            FindObjectOfType<AudioManager>().Play("buyTime", 1 + pitchDuration);
            pitchDuration -= .1f;

            if (comboController.GetComponent<comboController>().duration < maxDuration)
            {
                comboController.GetComponent<comboController>().duration += 5;
                comboController.GetComponent<comboController>().durationTimerText = comboController.GetComponent<comboController>().duration + 1;
            }
            initialMoney = (int)currentMoney;

            money = money - precioTimer;

            precioTimer += precioTimerMulti;
            precioTimerMulti += 50;

            comboController.GetComponent<comboController>().minimunFillTime += .06f;

            var colors = botonDuration.colors;

            colors.pressedColor = Color.green;
            botonDuration.colors = colors;

            Instantiate(particulaCuadrada, new Vector3(-7.295f, 1, 0), transform.rotation);

            GameObject buttonParticle = Instantiate(particulaes, new Vector3(0.2f, -1.57f, 0), transform.rotation);
            buttonParticle.transform.localScale = new Vector3(5, 5, 1);

        }
        else
        {
            FindObjectOfType<AudioManager>().Play("buttonDenied", 1);

            var colors = botonDuration.colors;

            colors.pressedColor = Color.red;
            botonDuration.colors = colors;



        }

        if (comboController.GetComponent<comboController>().duration == maxDuration)
        {
            botonDuration.interactable = false;
            //botonDuration.enabled = false;
            precioTimeText.enabled = false;
            firstSelectedButton.Select();

            timetext.color = Color.grey;
            timeinfo.color = Color.grey;

        }

    }

    public void StoreMultiplier()
    {

        if (money >= precioMulti)
        {
            multiAnimationBool = true;
            lerpedAnimationMultiplier = 0;

            FindObjectOfType<AudioManager>().Play("buyMulti", 1 + pitchMulti);
            pitchMulti -= .05f;


            int intensidad = comboController.GetComponent<comboController>().intensidad;

            if (intensidad < 6)
            {
                comboController.GetComponent<comboController>().intensidad += 1;

            }
            else if (intensidad >= 6 && intensidad < 10)
            {
                comboController.GetComponent<comboController>().intensidad = 10;

            }
            else if (intensidad >= 10 && intensidad < 12)
            {
                comboController.GetComponent<comboController>().intensidad = 12;

            }
            else if (intensidad >= 12 && intensidad < 15)
            {
                comboController.GetComponent<comboController>().intensidad = 15;

            }
            else if (intensidad >= 15 && intensidad < 20)
            {
                comboController.GetComponent<comboController>().intensidad = 20;

            }
            else if (intensidad >= 20 && intensidad < 30)
            {
                comboController.GetComponent<comboController>().intensidad = 30;

            }
            else if (intensidad >= 30 && intensidad < 60)
            {
                comboController.GetComponent<comboController>().intensidad = 60;

            }


            initialMoney = (int)currentMoney;

            money = money - precioMulti;

            precioMulti += precioMultiMulti;
            precioMultiMulti += 50;

            var colors = botonMulti.colors;

            colors.pressedColor = Color.green;
            botonMulti.colors = colors;

            Instantiate(particulaes, new Vector3(-8.324f, 1.14f, 0), transform.rotation);


            GameObject buttonParticle = Instantiate(particulaes, new Vector3(0.2f, -0.47f, 0), transform.rotation);
            buttonParticle.transform.localScale = new Vector3(5, 5, 1);

        }
        else
        {
            FindObjectOfType<AudioManager>().Play("buttonDenied", 1);

            var colors = botonMulti.colors;

            colors.pressedColor = Color.red;
            botonMulti.colors = colors;
        }


        if (comboController.GetComponent<comboController>().intensidad >= 60)
        {
            botonMulti.interactable = false;
            //botonMulti.enabled = false;
            precioXxText.enabled = false;
            firstSelectedButton.Select();
            multitext.color = Color.grey;
            multiinfo.color = Color.grey;
        }

    }


    public void MoneyUP()
    {
        initialMoney = (int)currentMoney;


        money += 9999;
    }

    public void CloseStore()
    {
        storeCanvas.SetActive(false);

    }

    public void TimeAnimation()
    {
        if (timeAnimationBool)
        {
            timeBarFill.GetComponent<Image>().color = Color.Lerp(Color.magenta, Color.white, lerpedAnimationTimer);
        }

        lerpedAnimationTimer += Time.deltaTime;

        if (lerpedAnimationTimer>1)
        {
            lerpedAnimationTimer = 1;
            timeAnimationBool = false;

        }
    }

    public void MultiAnimation()
    {
        if (multiAnimationBool)
        {
            multiplierTxt.GetComponent<TextMeshProUGUI>().color = Color.Lerp(Color.yellow, Color.white, lerpedAnimationMultiplier);
        }

        lerpedAnimationMultiplier += Time.deltaTime;

        if (lerpedAnimationMultiplier > 1)
        {
            lerpedAnimationMultiplier = 1;
            multiAnimationBool = false;

        }
    }

}
