using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using XInputDotNetPure;



public class gokuPrueba : MonoBehaviour
{
    public GameObject goku1;
    public GameObject goku2;

    private int numeroGoku=0;

    InputMaster controls;

    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    private bool esperaPaClickear = true;

    private void Awake()
    {
        controls = new InputMaster();
        Invoke("Esperaculiado", 0.1f);
    }

    private void Update()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);

        if (esperaPaClickear == false)
        {
            if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
            {

                if (numeroGoku == 0)
                {
                    numeroGoku = 1;
                    goku1.SetActive(true);
                    goku2.SetActive(false);
                }
                else if (numeroGoku == 1)
                {
                    numeroGoku = 0;
                    goku1.SetActive(false);
                    goku2.SetActive(true);
                }
                FindObjectOfType<AudioManager>().Play("click_down", Random.Range( 0.7f , 1.3f));

            }

            if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released)
            {
                FindObjectOfType<AudioManager>().Play("click_up", Random.Range(0.7f, 1.3f));
            }
        }
        
    }

    void Esperaculiado()
    {
        esperaPaClickear = false;

    }
}
