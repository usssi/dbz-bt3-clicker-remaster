using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using XInputDotNetPure;


public class platosController : MonoBehaviour
{
    //controla los platos de la mesa

    private int numeroPlato = 0;

    public SpriteRenderer platoDefinitivo;
    public Sprite[] platosArray;
    private int platoArrayCount;

    private float pitchPlus = .1f;

    private int inputPlus = 1;

    private bool buttonCanBeActivated = true;

    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    public int inputcount;

    private bool bool1;
    private bool bool2;

    private void Start()
    {
        bool1 = true;
    }


    void Update()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);

        numeroPlato = FindObjectOfType<stackControllerPrueba>().numeroStack;

        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            if (numeroPlato >= 0 && numeroPlato <= 58)
            {
                //numeroPlato += inputPlus;
                inputcount += inputPlus;
            }
            else if (numeroPlato >= 59)
            {
                //numeroPlato += inputPlus;
                numeroPlato = 0;
            }

        }

        if (numeroPlato >= 0 * 1 && numeroPlato < 6 * 1)
        {
            platoArrayCount = 0;
            platoDefinitivo.sprite = platosArray[platoArrayCount];
            bool1 = false;
            bool2 = true;
        }
        else if (numeroPlato >= 6 * 1 && numeroPlato < 6 * 2 && bool2)
        {
            platoArrayCount = 1;
            platoDefinitivo.sprite = platosArray[platoArrayCount];
            PlaySoundPitched(0);
            bool1 = true;
            bool2 = false;
        }
        else if (numeroPlato >= 6 * 2 && numeroPlato < 6 * 3 && bool1)
        {
            platoArrayCount = 2;
            platoDefinitivo.sprite = platosArray[platoArrayCount];
            PlaySoundPitched(pitchPlus);
            bool1 = false;
            bool2 = true;
        }
        else if (numeroPlato >= 6 * 3 && numeroPlato < 6 * 4 && bool2)
        {
            platoArrayCount = 3;
            platoDefinitivo.sprite = platosArray[platoArrayCount];
            PlaySoundPitched(pitchPlus*2);
            bool1 = true;
            bool2 = false;
        }
        else if (numeroPlato >= 6 * 4 && numeroPlato < 6 * 5 && bool1)
        {
            platoArrayCount = 4;
            platoDefinitivo.sprite = platosArray[platoArrayCount];
            PlaySoundPitched(pitchPlus*3);
            bool1 = false;
            bool2 = true;
        }
        else if (numeroPlato >= 6 * 5 && numeroPlato < 6 * 6 && bool2)
        {
            platoArrayCount = 5;
            platoDefinitivo.sprite = platosArray[platoArrayCount];
            PlaySoundPitched(pitchPlus * 4);
            bool1 = true;
            bool2 = false;
        }
        else if (numeroPlato >= 6 * 6 && numeroPlato < 6 * 7 && bool1)
        {
            platoArrayCount = 6;
            platoDefinitivo.sprite = platosArray[platoArrayCount];
            PlaySoundPitched(pitchPlus * 5);
            bool1 = false;
            bool2 = true;
        }
        else if (numeroPlato >= 6 * 7 && numeroPlato < 6 * 8 && bool2)
        {
            platoArrayCount = 7;
            platoDefinitivo.sprite = platosArray[platoArrayCount];
            PlaySoundPitched(pitchPlus * 6);
            bool1 = true;
            bool2 = false;
        }
        else if (numeroPlato >= 6 * 8 && numeroPlato < 6 * 9 && bool1)
        {
            platoArrayCount = 8;
            platoDefinitivo.sprite = platosArray[platoArrayCount];
            PlaySoundPitched(pitchPlus * 7);
            bool1 = false;
            bool2 = true;
        }
        else if (numeroPlato >= 6 * 9 && numeroPlato < 6 * 10 && bool2)
        {
            platoArrayCount = 9;
            platoDefinitivo.sprite = platosArray[platoArrayCount];
            PlaySoundPitched(pitchPlus * 8);
            bool1 = true;
            bool2 = false;
        }
        else if (numeroPlato >= 6 * 10 && bool1)
        {
            platoArrayCount = 10;
            platoDefinitivo.sprite = platosArray[platoArrayCount];
            PlaySoundPitched(pitchPlus * 9);
            bool1 = false;
            bool2 = true;
        }
    }

    //void Plato0()
    //{
    //    platoArrayCount = 0;
    //    platoDefinitivo.sprite = platosArray[platoArrayCount];

    //}
    //void Plato1()
    //{
    //    PlaySoundPitched(0);
    //    platoArrayCount += 1;
    //    platoDefinitivo.sprite = platosArray[platoArrayCount];

    //}
    //void Plato2()
    //{
    //    PlaySoundPitched(pitchPlus);
    //    platoArrayCount += 1;
    //    platoDefinitivo.sprite = platosArray[platoArrayCount];
    //}
    //void Plato3()
    //{
    //    PlaySoundPitched(pitchPlus * 2);
    //    platoArrayCount += 1;
    //    platoDefinitivo.sprite = platosArray[platoArrayCount];
    //}
    //void Plato4()
    //{
    //    PlaySoundPitched(pitchPlus * 3);
    //    platoArrayCount += 1;
    //    platoDefinitivo.sprite = platosArray[platoArrayCount];
    //}
    //void Plato5()
    //{
    //    PlaySoundPitched(pitchPlus * 4);
    //    platoArrayCount += 1;
    //    platoDefinitivo.sprite = platosArray[platoArrayCount];
    //}
    //void Plato6()
    //{
    //    PlaySoundPitched(pitchPlus * 5);
    //    platoArrayCount += 1;
    //    platoDefinitivo.sprite = platosArray[platoArrayCount];
    //}
    //void Plato7()
    //{
    //    PlaySoundPitched(pitchPlus * 6);
    //    platoArrayCount += 1;
    //    platoDefinitivo.sprite = platosArray[platoArrayCount];
    //}
    //void Plato8()
    //{
    //    PlaySoundPitched(pitchPlus * 7);
    //    platoArrayCount += 1;
    //    platoDefinitivo.sprite = platosArray[platoArrayCount];
    //}
    //void Plato9()
    //{
    //    PlaySoundPitched(pitchPlus * 8);
    //    platoArrayCount += 1;
    //    platoDefinitivo.sprite = platosArray[platoArrayCount];
    //}
    //void Plato10()
    //{
    //    PlaySoundPitched(pitchPlus * 9);
    //    platoArrayCount += 1;
    //    platoDefinitivo.sprite = platosArray[platoArrayCount];
    //}

    void PlaySoundPitched(float pitch)
    {
        FindObjectOfType<AudioManager>().Play("plato", 1 + pitch);
    }

    public void OnButtonActivatePowerUpPlatos(int duracion, int intensidad)
    {
        if (buttonCanBeActivated)
        {
            inputPlus = intensidad;
            Invoke("PowerUpDisable", duracion+.1f);
            buttonCanBeActivated = false;
            inputcount = 0;
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
        inputcount = 0;
    }
}
