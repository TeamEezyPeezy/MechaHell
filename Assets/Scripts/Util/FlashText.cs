using System.Collections;
using TMPro;
using UnityEngine;

public class FlashText : MonoBehaviour
{
    private float fadeTime;

    public void Flash(float fadeTime)
    {
        this.fadeTime = fadeTime;

        FadeOut(this.fadeTime);
    }

    private void FadeIn(float fadeTime)
    {
        StartCoroutine(FadeTextToFullAlpha(fadeTime, GetComponent<TextMeshProUGUI>()));
    }

    private void FadeOut(float fadeTime)
    {
        StartCoroutine(FadeTextToZeroAlpha(fadeTime, GetComponent<TextMeshProUGUI>()));
    }

    public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }

        FadeIn(this.fadeTime);
    }
}
