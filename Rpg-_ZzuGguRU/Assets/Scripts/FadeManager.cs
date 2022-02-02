using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public SpriteRenderer White;
    public SpriteRenderer Black;
    private Color color;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    public void FadeOut(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutCoroutine(_speed));

    }

    IEnumerator FadeOutCoroutine(float _speed)
    {
        color = Black.color;

        while(color.a <1f)
        {
            color.a += _speed;
            Black.color = color;
            yield return waitTime;

        }


    }







    public void FadeIn(float _speed = 0.02f)
    {
        StopAllCoroutines();
       StartCoroutine(FadeInCoroutine(_speed));
    }

    IEnumerator FadeInCoroutine(float _speed)
    {
        color = Black.color;

        while(color.a >0f)
        {
            color.a -= _speed;
            Black.color = color;
            yield return waitTime;

        }
    }


    public void FlashOut(float _speed = 0.02f)
    {
        StopAllCoroutines();
       StartCoroutine(FlashOutCoroutine(_speed));
    }

    IEnumerator FlashOutCoroutine(float _speed)
    {
        color = White.color;

        while(color.a <1f)
        {
            color.a += _speed;
            White.color = color;
            yield return waitTime;

        }


    }


    public void FlasIn(float _speed = 0.02f)
    {
         StopAllCoroutines();
       StartCoroutine(FlashInCoroutine(_speed));
    }

    IEnumerator FlashInCoroutine(float _speed)
    {
        color = White.color;

        while(color.a > 0f)
        {
            color.a -= _speed;
            White.color = color;
            yield return waitTime;

        }


    }

    public void Flash(float _speed = 0.1f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashCoroutine(_speed));

    }
    IEnumerator FlashCoroutine(float _speed)
    {
        
        color = White.color;

        while(color.a <1f)
        {
            color.a += _speed;
            White.color = color;
            yield return waitTime;
        }
        color = White.color;
        
        while(color.a > 0f)
        {
            color.a -= _speed;
            White.color = color;
            yield return waitTime;

        }



    }


}
