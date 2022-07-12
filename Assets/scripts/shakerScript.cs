using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakerScript : MonoBehaviour
{
    public float magnitude;
    private Vector3 originalPos;

    private void Start()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        float xX = Random.Range(-1f, 1f) * magnitude;
        float yX = Random.Range(-1f, 1f) * magnitude;

        transform.localPosition = new Vector3(originalPos.x + xX, originalPos.y + yX, originalPos.z);
        Invoke("ComeBack",1);
    }

    void ComeBack()
    {
        transform.localPosition = originalPos;

    }
}
