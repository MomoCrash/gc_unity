using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public string tutoText;
    public string tutoSubText;

    public float timeDisplayed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(ShowTutoText());
    }

    IEnumerator ShowTutoText()
    {

        var textMeshField = GameObject.Find("Tutorial Text").GetComponent<TextMeshPro>();
        var subTextMeshField = GameObject.Find("Tutorial SubText").GetComponent<TextMeshPro>();

        textMeshField.text = tutoText;
        subTextMeshField.text = tutoSubText;

        yield return new WaitForSeconds(timeDisplayed);

        textMeshField.text = "";
        subTextMeshField .text = "";

        Destroy(gameObject);

    }

}
