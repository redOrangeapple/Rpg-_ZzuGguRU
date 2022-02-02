using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; //사운드 의 이름
    public AudioClip clip; // 사운드 파일
    private AudioSource Source; // 사운드 플레이어

    public float volumn;
    public bool loop;

    public void Setsource(AudioSource _source)
    {
        Source =  _source;
        Source.clip =clip;
        Source.loop=loop;
        Source.volume = volumn;
    }

    public void SetVolumn()
    {
        Source.volume = volumn;
    }

    public void Play()
    {
        Source.Play();
    }
    public void Stop()
    {
        Source.Stop();
    }
    public void SetLoop()
    {
        Source.loop=true;
    }
    public void SetLoopCancle()
    {
        Source.loop=false;
    }


}



public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;
    [SerializeField]
    public Sound[] sounds;

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
        for(int i = 0 ; i < sounds.Length ;i++)
        {
            GameObject SoundObject = new GameObject("사운드 파일 이름 : " + i + " = " +sounds[i].name);
            sounds[i].Setsource(SoundObject.AddComponent<AudioSource>());
            SoundObject.transform.SetParent(this.transform);

        }
    }

    public void Play(string _name)
    {
        for(int i = 0 ; i < sounds.Length ;i++)
        {
            if(_name == sounds[i].name)
                {
                    sounds[i].Play();
                        return;
                }
        
        }

    }

    public void Stop(string _name)
    {
        for(int i = 0 ; i < sounds.Length ;i++)
        {
            if(_name == sounds[i].name)
                {
                    sounds[i].Stop();
                        return;
                }
        
        }

    }
    public void SetLoop(string _name)
    {
        for(int i = 0 ; i < sounds.Length ;i++)
        {
            if(_name == sounds[i].name)
                {
                    sounds[i].SetLoop();
                        return;
                }
        
        }

    }

    public void SetLoopCancle(string _name)
    {
        for(int i = 0 ; i < sounds.Length ;i++)
        {
            if(_name == sounds[i].name)
                {
                    sounds[i].SetLoopCancle();
                        return;
                }
        
        }

    }

    public void SetVolumn(string _name,float _volumn)
    {
        for(int i = 0 ; i < sounds.Length ;i++)
        {
            if(_name == sounds[i].name)
                {
                    sounds[i].volumn = _volumn;
                    sounds[i].SetVolumn();
                    return;
                }
        
        }

    }
}
