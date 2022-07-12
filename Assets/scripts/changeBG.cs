using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using XInputDotNetPure;



public class changeBG : MonoBehaviour
{
    public Camera mainCam;
    
    public Gradient gradient;
    private float gradientValue;
    private float glerpedValue;
    public float t;
    public float resta;

    public int multiCombo;
    private bool buttonCanBeActivated = true;
    public bool isInStore;


    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    private bool goesDown;


    private void Awake()
    {
        gradientValue = 0f;
        goesDown = false;
        multiCombo = 1;
    }

    void Update()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);

        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            if (isInStore==false)
            {
                gradientValue += .01f * multiCombo;
                goesDown = false;
            }
           

        }
        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Released)
        {
            Invoke("GoDown", 2);

        }

    }

    private void FixedUpdate()
    {

        if (goesDown==true)
        {
            if (gradientValue > 0)
            {
                gradientValue -= resta * Time.deltaTime;
                glerpedValue = Mathf.Lerp(0, gradientValue, t);
            }
            else if (gradientValue < 0)
            {
                gradientValue = 0;
            }
           
        }

        if (gradientValue >= 1)
        {
            gradientValue = 1;
            print("lets fuckin goooo");
        }

        //esto mide la velocidad con la que baja la intensidad dependiendo de a que nivel de intensidad está
        if (gradientValue<0.25f)
        {
            resta = 0.04f;
        }
        if (gradientValue<0.5f)
        {
            if (gradientValue>0.35f)
            {
                resta = 0.06f;
            }
        }
        if (gradientValue<0.75f)
        {
            if (gradientValue>0.65f)
            {
                resta = 0.08f;
            }
        }
        if (gradientValue<0.9f)
        {
            if (gradientValue>0.85f)
            {
                resta = 0.1f;

            }
        }

        mainCam.backgroundColor = gradient.Evaluate(glerpedValue);

    }

    private void GoDown()
    {
        goesDown = true;
    }


    public void ButtonPressMultiComboBGChange(int duracion, int intensidad)
    {
        if (buttonCanBeActivated)
        {
            
            if (intensidad > 2)
            {
                multiCombo = intensidad / 3;

            }
            Invoke("PowerUpDisable", duracion);
            buttonCanBeActivated = false;
        }
    }

    private void PowerUpDisable()
    {
        multiCombo = 1;
        buttonCanBeActivated = true;
    }
}
