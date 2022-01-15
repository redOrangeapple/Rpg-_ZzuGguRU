using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Manger : MonoBehaviour
{
    static public BGM_Manger instance;
    public AudioClip[] clips; //배경음악들

    private AudioSource source;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);


    private void Awake() {

        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
            Destroy(this.gameObject);
        
    }


    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    public void Play(int _playMusicTrack)
    {
        source.volume=1f;
        source.clip= clips[_playMusicTrack];
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void Pause()
    {
        source.Pause();
    }

    public void UnPause()
    {
        source.UnPause();
    }

    public void Setvolumn(float _volumn)
    {   
        source.volume=_volumn;
    }
    public void FadeoutMusic()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutMusiccoroutine());

    }

        IEnumerator FadeOutMusiccoroutine()
        {
            for(float i =1.0f;i>=0;i-=0.01f)
            {
                source.volume=i;
                yield return waitTime;

            }

        }



    public void FadeInMusice()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInMusiccoroutine());
    }


        IEnumerator FadeInMusiccoroutine()
        {
            for(float i =0;i<=1.0f;i+=0.01f)
            {
                source.volume=i;
                yield return waitTime;

            }

        }
}
