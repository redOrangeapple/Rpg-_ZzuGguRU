using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTest : MonoBehaviour
{
    public GameObject go;

    private bool flag;

    void OnTriggerEnter2D(Collider2D other) {

        if(!flag)
        {
            flag =true;
            go.SetActive(true);
        }


    }

}
