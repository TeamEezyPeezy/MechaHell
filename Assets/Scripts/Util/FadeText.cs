using System.Collections;
using TMPro;
using UnityEngine;

public class FadeText : MonoBehaviour
{
    private TextMeshProUGUI text;
    private bool isFading = false;
    private float fadeValue;

    public float FadeValue
    {
        get
        {
            return fadeValue;
        }
        set
        {
            fadeValue = value;
        }
    }

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        if (text != null)
        {
            text.alpha = 0f;
        }
    }

    public void FadeIn(float fadeTime)
    {
        if (text != null && !isFading && text.gameObject.activeInHierarchy)
        {
            isFading = true;
            StartCoroutine(FadeTextToFullAlpha(fadeTime, text));
        }
    }

    public void FadeOut(float fadeTime)
    {
        if (text != null && !isFading && text.gameObject.activeInHierarchy)
        {
            isFading = true;
            StartCoroutine(FadeTextToZeroAlpha(fadeTime, text));
        }
    }

    public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }

        isFading = false;
        FadeValue = 1f;
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }

        isFading = false;
        FadeValue = 0f;
    }
}
