using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stackNormal : MonoBehaviour
{
    public StoreController storeController;
    private void Awake()
    {
        storeController = GameObject.FindObjectOfType<StoreController>();

    }
    private void OnDestroy()
    {
        FindObjectOfType<AudioManager>().Play("sellStack", 1);

        storeController.GetComponent<StoreController>().initialMoney = (int)storeController.GetComponent<StoreController>().currentMoney;

        storeController.GetComponent<StoreController>().money += 5;

    }
}
