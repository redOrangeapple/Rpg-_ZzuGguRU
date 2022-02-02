using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather_Manager : MonoBehaviour
{
    static public Weather_Manager instance;

#region Singleton
    void Awake() {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else {
            Destroy(this.gameObject);

        }

    }
    #endregion
    // Start is called before the first frame update
    private AudioManager theAudio;
    public ParticleSystem  rain;
    public string rain_Sound;
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
        
    }

    public void Rain()
    {
        theAudio.Play(rain_Sound);
        rain.Play();

    }
    public void RainStop()
    {
        theAudio.Stop(rain_Sound);
        rain.Stop();
    }
    public void RainDrop()
    {
        rain.Emit(10);
    }

}
