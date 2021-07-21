using System.Collections;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class FlashText : MonoBehaviour
{
    private bool isFlashing = false;

    public void Flash(float fadeTime, int timesToFlash)
    {
        if (!isFlashing)
        {
            StartCoroutine(FadeFlash(fadeTime, timesToFlash ,GetComponent<TextMeshProUGUI>()));
            isFlashing = true;
        }
    }

    public IEnumerator FadeFlash(float t, int flashTimes,TextMeshProUGUI i)
    {
        for (int j = 0; j < flashTimes; j++)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
            while (i.color.a > 0.0f)
            {
                i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
                yield return null;
            }

            i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
            while (i.color.a < 1.0f)
            {
                i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
                yield return null;
            }
        }
        isFlashing = false;
    }
}
