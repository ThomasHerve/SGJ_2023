using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFade : MonoBehaviour
{
    [SerializeField]
    public AudioSource audioSource;
    [SerializeField]
    float fadeTime = 0.2f;

    private Coroutine fadeInCoroutine;
    private Coroutine fadeOutCoroutine;

    public void FadeIn()
    {
        if (fadeOutCoroutine != null)
        {
            StopCoroutine(fadeOutCoroutine);
            fadeOutCoroutine = null;
        }
        if(fadeInCoroutine == null)
            fadeInCoroutine = StartCoroutine(PlaySoundWithFadeIn());
    }
    private IEnumerator PlaySoundWithFadeIn()
    {
        while (audioSource.volume < 1.0f)
        {
            audioSource.volume += Time.deltaTime / fadeTime;
            yield return null;
        }
        
    }

    public void FadeOut()
    {
        if (fadeInCoroutine != null)
        {
            StopCoroutine(fadeInCoroutine);
            fadeInCoroutine = null;
        }

        if(fadeOutCoroutine == null)
            fadeOutCoroutine=StartCoroutine(PlaySoundWithFadeOut());
    }
    private IEnumerator PlaySoundWithFadeOut()
    {
        while (audioSource.volume > 0.0f)
        {
            audioSource.volume -= Time.deltaTime / fadeTime;
            yield return null;
        }
    }

}
