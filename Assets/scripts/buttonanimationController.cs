using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonanimationController : MonoBehaviour
{
    public bool isSTart;
    public GameObject borde;
    private void Start()
    {
        borde.SetActive(false);


        if (isSTart)
        {
            gameObject.GetComponent<Animator>().speed = 1;
            borde.SetActive(true);

        }
        else
        {
            gameObject.GetComponent<Animator>().speed = 0;
            borde.SetActive(false);

        }

    }

    public void IsSelected()
    {
        gameObject.GetComponent<Animator>().speed = 1;
        borde.SetActive(true);

    }

    public void DesSelected()
    {
        gameObject.GetComponent<Animator>().speed = 0;
        borde.SetActive(false);

    }

    private void OnDisable()
    {
        gameObject.GetComponent<Animator>().speed = 0;
        borde.SetActive(false);

    }

}
