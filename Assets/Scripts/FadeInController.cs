using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInController : MonoBehaviour
{
    [SerializeField] private Image _fader;
    [SerializeField] private float _fadeDuration;

    public IEnumerator FadeOut()
    {
        _fader.gameObject.SetActive(true);
        
        var color = _fader.color;
        color.a = 0.0f;
        _fader.color = color;

        for (var current = 0.0f; current < _fadeDuration; current += Time.deltaTime)
        {
            color.a = current / _fadeDuration;
            _fader.color = color;

            yield return null;
        }

        color.a = 1.0f;
        _fader.color = color;

        yield return null;
    }

    public IEnumerator FadeIn()
    {
        _fader.gameObject.SetActive(true);
        
        var color = _fader.color;
        color.a = 1.0f;
        _fader.color = color;

        for (var current = 0.0f; current < _fadeDuration; current += Time.deltaTime)
        {
            color.a = 1.0f - current / _fadeDuration;
            _fader.color = color;

            yield return null;
        }
        
        _fader.gameObject.SetActive(false);
    }
}
