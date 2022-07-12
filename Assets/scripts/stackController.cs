using System;
using System.Collections;
using UnityEngine;


public class stackController : MonoBehaviour
{
    public GameObject[] stackList;
    public GameObject playerObj;
    public GameObject gamepadControllerObj;
    public GameObject cameraObject;
    public GameObject comboController;
    private float waitForThis;

    public GameObject storeController;

    private int i;

    private bool isSelling;
    public GameObject camController;
    public GameObject gamepadController;

    void Update()
    {
        stackList = GameObject.FindGameObjectsWithTag("stack");
        //print(stackList.Length);

        //time it takes to sell stack depending on amount of stacks
        if (stackList.Length>=0 && stackList.Length<=19)
        {
            waitForThis = 0.0005f;
        }
        if (stackList.Length > 19 && stackList.Length <= 38)
        {
            waitForThis = 0.0004f;
        }
        if (stackList.Length > 38 && stackList.Length <= 57)
        {
            waitForThis = 0.0003f;
        }
        if (stackList.Length > 57 && stackList.Length <= 76)
        {
            waitForThis = 0.0002f;
        }

        if (isSelling)
        {
            if (stackList.Length>0)
            {

            }
            if (stackList.Length == 0)
            {
                cameraObject.GetComponent<zoomController>().CamDefault();

                isSelling = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (stackList.Length >= 95)
        {
            SellStacks();
            comboController.GetComponent<comboController>().isPaused = true;
            gamepadController.GetComponent<gamepadController>().canVibrate = false;
            camController.GetComponent<zoomController>().isInStore = true;
            camController.GetComponent<changeBG>().isInStore = true;
        }

        if (stackList.Length == 0 )
        {
            playerObj.SetActive(true);
            playerObj.GetComponent<stackControllerPrueba>().positionOffsetX = 0;
            playerObj.GetComponent<stackControllerPrueba>().positionOffsetY = 0;

            comboController.GetComponent<comboController>().isPaused = false;
            gamepadController.GetComponent<gamepadController>().canVibrate = true;
            camController.GetComponent<zoomController>().isInStore = false;
            camController.GetComponent<changeBG>().isInStore = false;

            i = 0;

        }

        //this activates zoomercontroller ervery time an stack is created under 19
        if (stackList.Length > i && i<19)
        {
            i = stackList.Length;
            //Debug.Log(i);
            cameraObject.GetComponent<zoomController>().ZoomerController();
           
        }

    }

    IEnumerator DestroyStacks()
    {
        foreach (var item in stackList)
        {

            for (int i = 0; i < stackList.Length; i++)
            {
                Destroy(item);
                yield return new WaitForSeconds(waitForThis);
            }

        }

    }

    public void SellStacks()
    {
        if (stackList.Length >= 95)
        {
            FindObjectOfType<AudioManager>().Play("sellButton", 1);

            Array.Reverse(stackList, 0, stackList.Length);
            StartCoroutine(DestroyStacks());

            playerObj.SetActive(false);

            isSelling = true;

            playerObj.GetComponent<stackControllerPrueba>().DefaultSpawnerPosition();


        }
        else if (stackList.Length>0 && stackList.Length < 94)
        {
            FindObjectOfType<AudioManager>().Play("sellButton", 1);

            Array.Reverse(stackList, 0, stackList.Length);
            StartCoroutine(DestroyStacks());

            playerObj.SetActive(false);

            storeController.GetComponent<StoreController>().StoreToggle();
            isSelling = true;

            playerObj.GetComponent<stackControllerPrueba>().DefaultSpawnerPosition();


        }
        else
        {
            FindObjectOfType<AudioManager>().Play("buttonDenied", 1);

        }


    }


}
