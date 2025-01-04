using System.Collections;
using UnityEngine;

public class SplashScreenManager : MonoBehaviour
{
    // Reference to splash screen GameObjects
    public GameObject Splash1;
    public GameObject Splash2;

    // Time to wait before switching (in seconds)
    public float switchDelay = 0.8f;

    // Fade duration for Splash2
    public float fadeDuration = 0.5f;

    void Start()
    {
        // Start the splash screen sequence
        StartCoroutine(SplashSequence());
    }

    IEnumerator SplashSequence()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(switchDelay);

        // Hide Splash1 and show Splash2
        if (Splash1 != null)
        {
            Splash1.SetActive(false);
        }

        if (Splash2 != null)
        {
            Splash2.SetActive(true); // Ensure Splash2 is active
            yield return StartCoroutine(FadeIn(Splash2));
        }
    }

    IEnumerator FadeIn(GameObject splash)
    {
        CanvasGroup canvasGroup = splash.GetComponent<CanvasGroup>();
        if (canvasGroup == null) yield break;

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1;
    }
}
