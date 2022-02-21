using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{   
    public static Menu instance;
    public GameObject go; //메뉴창 활성 비활성화 관리
    public AudioManager theAudio;

    public string Call_Sound;
    public string Cancel_Sound;

    public OrderManager theOrder;

    public GameObject[] gos;

    private bool activated;
    // Start is called before the first frame update

    public void Exit()
    {
        //게임 종료 시키기
        Application.Quit();
    }
    public void Continue()
    {
        activated = false;
        go.SetActive(false);
        theAudio.Play(Cancel_Sound);
        theOrder.Move();
    }


    public void GOTOTitle()
    {
        for(int i = 0 ; i <gos.Length;i++)
        {
            Destroy(gos[i]);
        }

        go.SetActive(false);
        activated = false;
        SceneManager.LoadScene("Title");
    }

     void Awake() {

            if(instance == null)
            {
                DontDestroyOnLoad(this.gameObject);
                instance = this;
            }
            else Destroy(this.gameObject);


    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            activated = !activated;

            if(activated)
            {
                theOrder.notMove();
                go.SetActive(true);
                theAudio.Play(Call_Sound);

            }
            else
            {   
                theOrder.Move();
                go.SetActive(false);
                theAudio.Play(Cancel_Sound);
            }




        }
    }
}
