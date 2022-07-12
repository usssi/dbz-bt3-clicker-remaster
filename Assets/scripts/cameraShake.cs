using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    public Vector3 lerpedPos;
    public bool shaketrue=true;
    public  IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0f;

        
        if (shaketrue)
        {
            while (elapsed < duration)
            {
                float xX = Random.Range(-1f, 1f) * magnitude;
                float yX = Random.Range(-1f, 1f) * magnitude;

                transform.localPosition = new Vector3(originalPos.x + xX, originalPos.y + yX, originalPos.z);

                elapsed += Time.deltaTime;

                yield return null;
            }
        }
        

        //este transform se usa con joystick turbo
        /*transform.localPosition = lerpedPos;*/


        //no deberia haber problemas con este transfor en condiciones de joystick normal
        transform.localPosition = originalPos;
    }
}
