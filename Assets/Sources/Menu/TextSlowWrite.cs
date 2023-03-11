using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSlowWrite : MonoBehaviour
{
    public float delay = 0.05f;
    private string currentText = "";

    public void WriteText(TextMeshProUGUI uiText, string fullText)
    {
        StartCoroutine(ShowText(uiText, fullText));
    }

    IEnumerator ShowText(TextMeshProUGUI uiText, string fullText)
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i + 1);
            uiText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
    }
}
