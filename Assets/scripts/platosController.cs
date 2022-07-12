using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using XInputDotNetPure;


public class platosController : MonoBehaviour
{
    //controla los platos de la mesa

    private int numeroPlato = 0;

    public GameObject plato1;
    public GameObject plato2;
    public GameObject plato3;
    public GameObject plato4;
    public GameObject plato5;
    public GameObject plato6;
    public GameObject plato7;
    public GameObject plato8;
    public GameObject plato9;
    public GameObject plato10;

    public GameObject platoDefinitivo;

    private float pitchPlus = .1f;

    private int inputPlus = 1;

    private bool buttonCanBeActivated = true;

    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    Animator platoAnimator;
    int currentKeyFrame = 0;

    private void Start()
    {
        platoAnimator = platoDefinitivo.GetComponent<Animator>();
    }

    void Update()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);

        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            if (numeroPlato >= 0 && numeroPlato <= 58)
            {
                numeroPlato += inputPlus;

            }
            else if (numeroPlato >= 59)
            {
                numeroPlato = 0;
            }


            switch (numeroPlato)
            {
                case 0:
                    Plato0();
                    break;

                case 6:
                    Plato1();
                    break;

                case 6 * 2:
                    Plato2();
                    break;

                case 6 * 3:
                    Plato3();
                    break;

                case 6 * 4:
                    Plato4();
                    break;

                case 6 * 5:
                    Plato5();
                    break;

                case 6 * 6:
                    Plato6();
                    break;

                case 6 * 7:
                    Plato7();
                    break;

                case 6 * 8:
                    Plato8();
                    break;

                case 6 * 9:
                    Plato9();
                    break;

                case 6 * 10:
                    Plato10();
                    break;

                default:
                    break;
            }
        }
    }

    void Plato0()
    {

        plato1.SetActive(false);

        plato2.SetActive(false);

        plato3.SetActive(false);

        plato4.SetActive(false);

        plato5.SetActive(false);

        plato6.SetActive(false);

        plato7.SetActive(false);

        plato8.SetActive(false);

        plato9.SetActive(false);

        plato10.SetActive(false);

    }
    void Plato1()
    {
        PlaySoundPitched(0);

        plato1.SetActive(true);

        plato2.SetActive(false);

        plato3.SetActive(false);

        plato4.SetActive(false);

        plato5.SetActive(false);

        plato6.SetActive(false);

        plato7.SetActive(false);

        plato8.SetActive(false);

        plato9.SetActive(false);

        plato10.SetActive(false);

    }
    void Plato2()
    {
        PlaySoundPitched(pitchPlus);

        plato1.SetActive(false);

        plato2.SetActive(true);

        plato3.SetActive(false);

        plato4.SetActive(false);

        plato5.SetActive(false);

        plato6.SetActive(false);

        plato7.SetActive(false);

        plato8.SetActive(false);

        plato9.SetActive(false);

        plato10.SetActive(false);

    }
    void Plato3()
    {
        PlaySoundPitched(pitchPlus*2);


        plato1.SetActive(false);

        plato2.SetActive(false);

        plato3.SetActive(true);

        plato4.SetActive(false);

        plato5.SetActive(false);

        plato6.SetActive(false);

        plato7.SetActive(false);

        plato8.SetActive(false);

        plato9.SetActive(false);

        plato10.SetActive(false);

    }
    void Plato4()
    {
        PlaySoundPitched(pitchPlus * 3);

        plato1.SetActive(false);

        plato2.SetActive(false);

        plato3.SetActive(false);

        plato4.SetActive(true);

        plato5.SetActive(false);

        plato6.SetActive(false);

        plato7.SetActive(false);

        plato8.SetActive(false);

        plato9.SetActive(false);

        plato10.SetActive(false);

    }
    void Plato5()
    {
        PlaySoundPitched(pitchPlus * 4);

        plato1.SetActive(false);

        plato2.SetActive(false);

        plato3.SetActive(false);

        plato4.SetActive(false);

        plato5.SetActive(true);

        plato6.SetActive(false);

        plato7.SetActive(false);

        plato8.SetActive(false);

        plato9.SetActive(false);

        plato10.SetActive(false);

    }
    void Plato6()
    {
        PlaySoundPitched(pitchPlus * 5);

        plato1.SetActive(false);

        plato2.SetActive(false);

        plato3.SetActive(false);

        plato4.SetActive(false);

        plato5.SetActive(false);

        plato6.SetActive(true);

        plato7.SetActive(false);

        plato8.SetActive(false);

        plato9.SetActive(false);

        plato10.SetActive(false);

    }
    void Plato7()
    {
        PlaySoundPitched(pitchPlus * 6);

        plato1.SetActive(false);

        plato2.SetActive(false);

        plato3.SetActive(false);

        plato4.SetActive(false);

        plato5.SetActive(false);

        plato6.SetActive(false);

        plato7.SetActive(true);

        plato8.SetActive(false);

        plato9.SetActive(false);

        plato10.SetActive(false);

    }
    void Plato8()
    {
        PlaySoundPitched(pitchPlus * 7);

        plato1.SetActive(false);

        plato2.SetActive(false);

        plato3.SetActive(false);

        plato4.SetActive(false);

        plato5.SetActive(false);

        plato6.SetActive(false);

        plato7.SetActive(false);

        plato8.SetActive(true);

        plato9.SetActive(false);

        plato10.SetActive(false);

    }
    void Plato9()
    {
        PlaySoundPitched(pitchPlus * 8);

        plato1.SetActive(false);

        plato2.SetActive(false);

        plato3.SetActive(false);

        plato4.SetActive(false);

        plato5.SetActive(false);

        plato6.SetActive(false);

        plato7.SetActive(false);

        plato8.SetActive(false);

        plato9.SetActive(true);

        plato10.SetActive(false);

    }
    void Plato10()
    {
        PlaySoundPitched(pitchPlus * 9);

        plato1.SetActive(false);

        plato2.SetActive(false);

        plato3.SetActive(false);

        plato4.SetActive(false);

        plato5.SetActive(false);

        plato6.SetActive(false);

        plato7.SetActive(false);

        plato8.SetActive(false);

        plato9.SetActive(false);

        plato10.SetActive(true);

    }

    void PlaySoundPitched(float pitch)
    {
        FindObjectOfType<AudioManager>().Play("plato", 1 + pitch);
    }


    public void OnButtonActivatePowerUpPlatos(int duracion, int intensidad)
    {
        if (buttonCanBeActivated)
        {
            inputPlus = intensidad;
            Invoke("PowerUpDisable", duracion);
            buttonCanBeActivated = false;
        }
        else
        {
            return;
        }

    }

    private void PowerUpDisable()
    {
        inputPlus = 1;
        buttonCanBeActivated = true;

    }

}
