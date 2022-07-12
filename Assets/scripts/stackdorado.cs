using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stackdorado : MonoBehaviour
{
    public StoreController storeController;

    private void Awake()
    {
        storeController = GameObject.FindObjectOfType<StoreController>();

    }
    private void OnDestroy()
    {
        FindObjectOfType<AudioManager>().Play("sellGold", 1);

        storeController.GetComponent<StoreController>().initialMoney = (int)storeController.GetComponent<StoreController>().currentMoney;

        storeController.GetComponent<StoreController>().money += 10;

    }
}
