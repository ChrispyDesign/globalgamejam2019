using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    public Image img;
    public Image img2;

    public void Start()
    {
        StartCoroutine("Splash_Screen");
    }

    IEnumerator Splash_Screen()
    {
        yield return new WaitForSecondsRealtime(2f);

        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Color c = img.color;
            c.a = f;
            img.color = c;
            yield return null;
        }
        img.gameObject.SetActive(false);

        yield return new WaitForSecondsRealtime(2f);

        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Color c = img2.color;
            c.a = f;
            img2.color = c;
            yield return null;
        }
        img2.gameObject.SetActive(false);
    }
}
