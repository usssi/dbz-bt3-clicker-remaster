using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class stackController : MonoBehaviour
{
    public GameObject[] stackList;
    public GameObject playerObj;
    public GameObject gamepadControllerObj;
    public GameObject cameraObject;
    public GameObject comboController;
    private float waitForThis;

    public GameObject storeController;

    private int iStackNumber;
    private int i;
    private int multiplierCalculator;
    private int multiplierSeller;

    public bool isSelling;
    public GameObject camController;
    public GameObject gamepadController;

    private Color dorado = new Color(1,.86f, .46f);

    public Button sellButton;

    public Text textoProfit;
    private int multicalc;
    private int profitNumber;


    private void Start()
    {
        foreach (var item in stackList)
        {
            item.GetComponent<SpriteRenderer>().enabled = false;
        }

        multiplierSeller = 1;

    }

    void Update()
    {

        //autosell cuando se llega a 95 stacks activos
        if (iStackNumber>=95)
        {
            SellStacks();
            //comboController.GetComponent<comboController>().isPaused = true;
            //gamepadController.GetComponent<gamepadController>().canVibrate = false;
            camController.GetComponent<zoomController>().isInStore = true;
            camController.GetComponent<changeBG>().isInStore = true;
        }

        //si la lista tiene o llega a 0 le devuelve el control al player
        if (iStackNumber == 0)
        {
            iStackNumber = 0;
            playerObj.SetActive(true);
            //comboController.GetComponent<comboController>().isPaused = false;
            //gamepadController.GetComponent<gamepadController>().canVibrate = true;
            camController.GetComponent<zoomController>().isInStore = false;
            camController.GetComponent<changeBG>().isInStore = false;
            i = 0;
        }

        //this activates zoomercontroller every time an stack is activated under 19
        if (iStackNumber > i && i < 19)
        {
            i = iStackNumber;
            cameraObject.GetComponent<zoomController>().ZoomerController();
            //Debug.Log("valor de i " + i);
        }

        //cuando los stacks activos llegan a 0 pone el zoom de la camara en normal
        if (isSelling)
        {
            if (iStackNumber > 0)
            {

            }
            if (iStackNumber == -1)
            {
                cameraObject.GetComponent<zoomController>().CamDefault();
                //Debug.Log("concha cam default");
                iStackNumber = 0;

                isSelling = false;
            }
        }

    }

    public void Stackeador(bool isItDorado)
    {
        if (!isItDorado)
        {
            stackList[iStackNumber].GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            stackList[iStackNumber].GetComponent<SpriteRenderer>().color = dorado;

        }

        stackList[iStackNumber].GetComponent<SpriteRenderer>().enabled = true;

        iStackNumber++;
    }

    public void CalculaordeProfit()
    {
        int multiplierCalculatorx = iStackNumber;

        profitNumber = 0;

        if (multiplierCalculatorx >= 1 && multiplierCalculatorx <= 19)
        {
            multicalc = 1;

        }
        else if (multiplierCalculatorx > 19 && multiplierCalculatorx <= 38)
        {
            multicalc = 2;

        }
        else if (multiplierCalculatorx > 38 && multiplierCalculatorx <= 57)
        {
            multicalc = 3;

        }
        else if (multiplierCalculatorx > 57 && multiplierCalculatorx <= 76)
        {
            multicalc = 4;

        }
        else if (multiplierCalculatorx > 76 && multiplierCalculatorx < 95)
        {
            multicalc = 5;

        }
        else if (multiplierCalculatorx == 95)
        {
            multicalc = 10;

        }

        foreach (var item in stackList)
        {
            if (item.GetComponent<SpriteRenderer>().enabled && item.GetComponent<SpriteRenderer>().color == Color.white)
            {
                profitNumber += 5 * multicalc;
            }
            else if (item.GetComponent<SpriteRenderer>().enabled && item.GetComponent<SpriteRenderer>().color != Color.white)
            {
                profitNumber += 10 * multicalc;
            }
        }

        textoProfit.text = "Profit: +$" + profitNumber.ToString();
    }

    public void SellStacks()
    {
        multiplierCalculator = iStackNumber;
        Debug.Log("istacknumber: " + iStackNumber);
        Debug.Log("multiplierCalculator: " + multiplierCalculator);

        if (multiplierCalculator >= 1 && multiplierCalculator <= 19)
        {
            multiplierSeller = 1;

        }
        else if (multiplierCalculator > 19 && multiplierCalculator <= 38)
        {
            multiplierSeller = 2;

        }
        else if (multiplierCalculator > 38 && multiplierCalculator <= 57)
        {
            multiplierSeller = 3;

        }
        else if (multiplierCalculator > 57 && multiplierCalculator <= 76)
        {
            multiplierSeller = 4;

        }
        else if (multiplierCalculator > 76 && multiplierCalculator < 95)
        {
            multiplierSeller = 5;

        }
        else if (multiplierCalculator == 95)
        {
            multiplierSeller = 10;

        }

        Debug.Log("multiplierSeller: " + multiplierSeller);

        if (iStackNumber >= 95)
        {
            FindObjectOfType<AudioManager>().Play("sellButton", 1);
            playerObj.SetActive(false);
            isSelling = true;
            StartCoroutine(DeactivateSprites());

            var colors = sellButton.colors;

            colors.pressedColor = Color.green;
            sellButton.colors = colors;

        }
        else if (iStackNumber > 0 && iStackNumber < 94)
        {
            FindObjectOfType<AudioManager>().Play("sellButton", 1);
            storeController.GetComponent<StoreController>().StoreToggle();
            playerObj.SetActive(false);
            isSelling = true;
            StartCoroutine(DeactivateSprites());

            var colors = sellButton.colors;

            colors.pressedColor = Color.green;
            sellButton.colors = colors;

        }
        else
        {
            FindObjectOfType<AudioManager>().Play("buttonDenied", 1);

            var colors = sellButton.colors;

            colors.pressedColor = Color.red;
            sellButton.colors = colors;
        }
    }

    IEnumerator DeactivateSprites()
    {
        Debug.Log(" entro en la coroutine");

        while(iStackNumber > -1 && isSelling)
        {
            Debug.Log(" entro al while");
            
            iStackNumber--;

            stackList[iStackNumber].GetComponent<SpriteRenderer>().enabled = false;

            if (stackList[iStackNumber].GetComponent<SpriteRenderer>().color == Color.white)
            {
                FindObjectOfType<AudioManager>().Play("sellStack", 1);

                storeController.GetComponent<StoreController>().initialMoney = (int)storeController.GetComponent<StoreController>().currentMoney;

                storeController.GetComponent<StoreController>().money += 5* multiplierSeller;
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("sellGold", 1);

                storeController.GetComponent<StoreController>().initialMoney = (int)storeController.GetComponent<StoreController>().currentMoney;

                storeController.GetComponent<StoreController>().money += 10* multiplierSeller;
            }

            yield return new WaitForSeconds(.1f);
        }

    }
}
