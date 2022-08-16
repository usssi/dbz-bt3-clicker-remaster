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


    private bool isSelling;
    public GameObject camController;
    public GameObject gamepadController;

    private Color dorado = new Color(1,.86f, .46f);

    public Button sellButton;

    private void Start()
    {
        //stackList = GameObject.FindGameObjectsWithTag("stack");
        //print(stackList.Length);

        foreach (var item in stackList)
        {
            item.GetComponent<SpriteRenderer>().enabled = false;
        }

    }

    void Update()
    {
        //Debug.Log("pito stacknumber value " + iStackNumber);
        //Debug.Log("poto isselling " + isSelling);


        //autosell cuando se llega a 95 stacks activos
        if (iStackNumber>=95)
        {
            SellStacks();
            comboController.GetComponent<comboController>().isPaused = true;
            gamepadController.GetComponent<gamepadController>().canVibrate = false;
            camController.GetComponent<zoomController>().isInStore = true;
            camController.GetComponent<changeBG>().isInStore = true;
        }

        //si la lista tiene o llega a 0 le devuelve el control al player
        if (iStackNumber == 0)
        {
            iStackNumber = 0;
            playerObj.SetActive(true);
            comboController.GetComponent<comboController>().isPaused = false;
            gamepadController.GetComponent<gamepadController>().canVibrate = true;
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


        #region oldcode
        //print(stackList.Length);

        //time it takes to sell stack depending on amount of stacks
        //if (stackList.Length>=0 && stackList.Length<=19)
        //{
        //    waitForThis = 0.0005f;
        //}
        //if (stackList.Length > 19 && stackList.Length <= 38)
        //{
        //    waitForThis = 0.0004f;
        //}
        //if (stackList.Length > 38 && stackList.Length <= 57)
        //{
        //    waitForThis = 0.0003f;
        //}
        //if (stackList.Length > 57 && stackList.Length <= 76)
        //{
        //    waitForThis = 0.0002f;
        //}

        //cuando la lista llega a 0 pone el zoom de la camara en normal
        //if (isSelling)
        //{
        //    if (stackList.Length>0)
        //    {

        //    }
        //    if (stackList.Length == 0)
        //    {
        //        cameraObject.GetComponent<zoomController>().CamDefault();

        //        isSelling = false;
        //    }
        //}
        #endregion

        #region old code
        ///////////este codigo vende automaticamente cuando la lista llega a 95 y le quita el control al player //deprecated
        //if (stackList.Length >= 95)
        //{
        //    SellStacks();
        //    comboController.GetComponent<comboController>().isPaused = true;
        //    gamepadController.GetComponent<gamepadController>().canVibrate = false;
        //    camController.GetComponent<zoomController>().isInStore = true;
        //    camController.GetComponent<changeBG>().isInStore = true;
        //}

        ////////////si la lista tiene o llega a 0 le devuelve el control al player // deprecated
        //if (stackList.Length == 0 )
        //{
        //    playerObj.SetActive(true);
        //    playerObj.GetComponent<stackControllerPrueba>().positionOffsetX = 0;
        //    playerObj.GetComponent<stackControllerPrueba>().positionOffsetY = 0;
        //    comboController.GetComponent<comboController>().isPaused = false;
        //    gamepadController.GetComponent<gamepadController>().canVibrate = true;
        //    camController.GetComponent<zoomController>().isInStore = false;
        //    camController.GetComponent<changeBG>().isInStore = false;
        //    i = 0;
        //}

        //this activates zoomercontroller ervery time an stack is created under 19 //deprecated
        //if (stackList.Length > i && i<19)
        //{
        //    i = stackList.Length;
        //    //Debug.Log(i);
        //    cameraObject.GetComponent<zoomController>().ZoomerController();
        //}
        #endregion



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

        //Debug.Log("stackea " + iStackNumber);
        //Debug.Log("is it dorado? " + isItDorado);
    }

    public void SellStacks()
    {
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

                storeController.GetComponent<StoreController>().money += 5;
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("sellGold", 1);

                storeController.GetComponent<StoreController>().initialMoney = (int)storeController.GetComponent<StoreController>().currentMoney;

                storeController.GetComponent<StoreController>().money += 10;
            }

            //Debug.Log("pito stacknumber value " + iStackNumber);
            yield return new WaitForSeconds(.1f);
        }

    }

    #region old code

    //old sell stacks function, destruye stacks de la lista empezando por el final. 
    //public void SellStacks()
    //{
    //    if (stackList.Length >= 95)
    //    {
    //        FindObjectOfType<AudioManager>().Play("sellButton", 1);
    //        Array.Reverse(stackList, 0, stackList.Length);
    //        StartCoroutine(DestroyStacks());
    //        playerObj.SetActive(false);
    //        isSelling = true;
    //        playerObj.GetComponent<stackControllerPrueba>().DefaultSpawnerPosition();
    //    }
    //    else if (stackList.Length>0 && stackList.Length < 94)
    //    {
    //        FindObjectOfType<AudioManager>().Play("sellButton", 1);
    //        Array.Reverse(stackList, 0, stackList.Length);
    //        StartCoroutine(DestroyStacks());
    //        playerObj.SetActive(false);
    //        storeController.GetComponent<StoreController>().StoreToggle();
    //        isSelling = true;
    //        playerObj.GetComponent<stackControllerPrueba>().DefaultSpawnerPosition();
    //    }
    //    else
    //    {
    //        FindObjectOfType<AudioManager>().Play("buttonDenied", 1);
    //    }
    //}


    //destruir stacks consume memoria //deprecated
    //IEnumerator DestroyStacks()
    //{
    //    foreach (var item in stackList)
    //    {
    //        for (int i = 0; i < stackList.Length; i++)
    //        {
    //            Destroy(item);
    //            yield return new WaitForSeconds(waitForThis);
    //        }
    //    }
    //}
    #endregion

}
