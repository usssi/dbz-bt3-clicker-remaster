using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autodestruccion : MonoBehaviour
{
    public float tiempoDeAutodestruccion = 2;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("autodstruccion", tiempoDeAutodestruccion);
    }

    void autodstruccion()
    {
        Destroy(this.gameObject);
    }
}
