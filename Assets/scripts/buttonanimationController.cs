using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonanimationController : MonoBehaviour
{
    public bool isSTart;
    private void Start()
    {
        if (isSTart)
        {
            gameObject.GetComponent<Animator>().speed = 1;

        }
        else
        {
            gameObject.GetComponent<Animator>().speed = 0;

        }

    }

    public void IsSelected()
    {
        gameObject.GetComponent<Animator>().speed = 1;
    }

    public void DesSelected()
    {
        gameObject.GetComponent<Animator>().speed = 0;
    }

    private void OnDisable()
    {
        gameObject.GetComponent<Animator>().speed = 0;

    }

}
