using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript002 : MonoBehaviour
{
    private BGM_Manger bgm;
    // Start is called before the first frame update
    void Start()
    {
        bgm = FindObjectOfType<BGM_Manger>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        StartCoroutine(abc());
    }

    IEnumerator abc()
    {
        bgm.FadeoutMusic();
        yield return null;


    }
}
