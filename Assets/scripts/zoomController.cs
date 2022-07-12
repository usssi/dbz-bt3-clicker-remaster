using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class zoomController : MonoBehaviour
{

    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    private Vector3 camPositionDefault ; //al principio es -2.5, -1, 0
    private Vector3 camPositionFinal; //al final es 0, 0, 0

    private float camSizeDefault;
    private float camSizeFinal;

    private float time;
    public float resta;

    private bool buttonCanBeActivated;
    private int multiCombo;

    public bool isInStore;


    void Start()
    {
        camPositionDefault = new Vector3(-2.5f, -1, 0);
        camPositionFinal = new Vector3(0, 0, 0);

        camSizeDefault = 3.5f;
        camSizeFinal = 5;
            //Camera.main.orthographicSize;

        time = 0;

        resta = .05f;

        buttonCanBeActivated = true;
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
                time += .01f * multiCombo;
                //ZoomerController(stackAmount);
            }
           
        }

    }

    private void FixedUpdate()
    {

        //restar a la variable t del lerp, lo que hace que la camara vuelva al tamaño inicial
        if (time > 0)
        {
            time -= resta*multiCombo*.7f * Time.deltaTime;

        }
        else if (time < 0)
        {
            time = 0;
        }
        else if (time>1)
        {
            time = 1;
        }

        //aumentar el valor de la resta al valor lerp, para que mientras mas lejos este de la posicion inicial, mas rapido vuelva
        if (time < 0.25f)
        {
            resta = 0.05f;
        }
        if (time < 0.5f)
        {
            if (time > 0.35f)
            {
                resta = 0.07f;
            }
        }
        if (time < 0.75f)
        {
            if (time > 0.65f)
            {
                resta = 0.09f;
            }
        }
        if (time < 0.9f)
        {
            if (time > 0.85f)
            {
                resta = 0.11f;

            }
        }

        //lerp la posicion de la camara entre la original y la final
        transform.localPosition = Vector3.Lerp(camPositionDefault, camPositionFinal, time);
        Camera.main.orthographicSize = Mathf.Lerp(camSizeDefault, camSizeFinal, time);


    }

    public void ZoomerController()
    {
        camPositionDefault = new Vector3(camPositionDefault.x + .13f, camPositionDefault.y + .05f, camPositionDefault.z);
        camSizeDefault += .08f;
        //Debug.Log("get zoomed");
    }

    public void ButtonPressMultiComboZoom(int duracion, int intensidad)
    {
        if (buttonCanBeActivated)
        {
            if (intensidad<=6)
            {
                multiCombo = intensidad / 2;

            }
            if (intensidad > 6)
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

    public void CamDefault()
    {
        camPositionDefault = new Vector3(-2.5f, -1, 0);
        camSizeDefault = 3.5f;

        Debug.Log("camdefalut");
    }
}
