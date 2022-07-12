using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class vegetaControler : MonoBehaviour
{
    public GameObject vegeta1;
    public GameObject vegeta2;

    private int numeroVegeta = 1;

    private void Update()
    {
      

    }

    //to triger on joystick press
    public void ActionVegeta(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (numeroVegeta >= 0 && numeroVegeta<2)
            {
                numeroVegeta ++;
                vegeta1.SetActive(true);
                vegeta2.SetActive(false);
            }
            else if (numeroVegeta >=2 && numeroVegeta<4)
            {
                numeroVegeta++;
                vegeta1.SetActive(false);
                vegeta2.SetActive(true);
            }
            else if (numeroVegeta==4)
            {
                numeroVegeta = 0;
            }
        }


    }
}
