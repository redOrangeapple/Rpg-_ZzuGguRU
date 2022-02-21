using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private FadeManager theFade;

    private AudioManager theAudio;

    public string Click_Sound;
    private Player_Manager thePlayer;

    private GameManager theGM;

    public GameObject hpbar;
    public GameObject mpbar;


    // Start is called before the first frame update
    void Start()
    {
        theFade = FindObjectOfType<FadeManager>();
        theAudio = FindObjectOfType<AudioManager>();
        thePlayer = FindObjectOfType<Player_Manager>();
        theGM = FindObjectOfType<GameManager>();
    }

    public void StartGmae()
    {
        StartCoroutine(GameStartCoroutine());

    }


    IEnumerator GameStartCoroutine()
    {
       
        theFade.FadeOut();

        theAudio.Play(Click_Sound);
        yield return new WaitForSeconds(2f);


        Color color = thePlayer.GetComponent<SpriteRenderer>().color;
        color.a=1f;

        thePlayer.GetComponent<SpriteRenderer>().color=color;

        thePlayer.currentMapName ="Start";
        thePlayer.currentSceneName="Start";

        theGM.LoadStart();

//         hpbar.SetActive(true);
  //       mpbar.SetActive(true);
        SceneManager.LoadScene("Start");

    }


    public void ExitGame()
    {
        theAudio.Play(Click_Sound);
        Application.Quit();
    }
}
