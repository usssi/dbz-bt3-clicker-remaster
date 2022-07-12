using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;


public class numberController : MonoBehaviour
{

    public TextMeshProUGUI texto;

    private int numberPushups;

    private int numeroPlato=1;

    void Start()
    {
        texto.text= "···";
    }

    public void VegetaPushUps(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (numeroPlato >= 0 && numeroPlato <= 4)
            {
                numeroPlato++;

            }
            else if (numeroPlato >= 5)
            {
                numeroPlato = 1;
                numberPushups++;
                texto.text = numberPushups.ToString();
            }
        }
    }
}
