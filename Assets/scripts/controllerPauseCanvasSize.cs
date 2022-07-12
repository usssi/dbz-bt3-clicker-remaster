using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controllerPauseCanvasSize : MonoBehaviour
{

    public Image panel;
    public GameObject rawImage;
    public GameObject titulo;
    public GameObject butoln;

    private void Start()
    {

        gameObject.SetActive(false);

    }

    private void OnEnable()
    {
        //StopAllCoroutines();

        panel.GetComponent<Animator>().SetBool("closeUI", false);
        rawImage.GetComponent<Animator>().SetBool("negrobool", false);
        butoln.GetComponent<Animator>().SetBool("buttonOFf", false);

        titulo.GetComponent<Animator>().SetBool("bol", false);

    }

    public void CanvasAnimationExit()
    {
        panel.GetComponent<Animator>().SetBool("closeUI", true);
        rawImage.GetComponent<Animator>().SetBool("negrobool", true);
        butoln.GetComponent<Animator>().SetBool("buttonOFf", true);

        titulo.GetComponent<Animator>().SetBool("bol", true);

        Invoke("CloseUI", .3f);

    }

    void CloseUI()
    {
        gameObject.SetActive(false);

    }


}
