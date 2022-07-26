using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakerScript : MonoBehaviour
{
    public float magnitudeX;
    public float magnitudeY;

    private Vector3 originalPos;

    private void Start()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        float xX = Random.Range(-1f, 1f) * magnitudeX;
        float yX = Random.Range(-1f, 1f) * magnitudeY;

        transform.localPosition = new Vector3(originalPos.x + xX, originalPos.y + yX, originalPos.z);
        Invoke("ComeBack",1);
    }

    void ComeBack()
    {
        transform.localPosition = originalPos;

    }
}
