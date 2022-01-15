using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript1 : MonoBehaviour
{
    private BGM_Manger bGM_Manger;

    public int MusicTrack;



    // Start is called before the first frame update
    void Start()
    {
        bGM_Manger = FindObjectOfType<BGM_Manger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        bGM_Manger.Play(MusicTrack);

        //기능을 꺼준다
     //   this.gameObject.SetActive(false);

    }
}
