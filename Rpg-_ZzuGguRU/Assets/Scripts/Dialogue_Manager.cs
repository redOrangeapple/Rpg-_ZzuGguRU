using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_Manager : MonoBehaviour
{
    //싱글톤 작업
    public static Dialogue_Manager instance;

    
    #region Singleton
    private void Awake()
    {

        //싱글톤 처리
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance =this;
        }
        else
        Destroy(this.gameObject);

    }
    #endregion Singleton
    
    public Text text;
    public SpriteRenderer rendererSprite;
    public SpriteRenderer rendererDialogueWindow;

    private List<string> listSentences;
    private List<Sprite> listSprites;
    private List<Sprite> listDialogueWindows;

    //대환 진행 상황 카운트
    private int count;

    public Animator animSprite;
    public Animator animDialogueWindow;


    public string typeSound;
    public string enterSound;

    private AudioManager theAudio;
    private Order_Manager theOrder;

    public bool talking = false;
    private bool KeyActivated = false;

    // Start is called before the first frame update
    void Start()
    {
            count = 0 ;
            text.text ="";
            listSentences = new List<string>();
            listSprites = new List<Sprite>();
            listDialogueWindows = new List<Sprite>();
            theAudio = FindObjectOfType<AudioManager>();
            theOrder = FindObjectOfType<Order_Manager>();
    }
    public void ShowDialogue(Dialogue dialogue)
    {
        talking = true;

        theOrder.NotMove();
        for(int i = 0 ; i < dialogue.senetences.Length;i++)
        {
            listSentences.Add(dialogue.senetences[i]);
            listSprites.Add(dialogue.sprites[i]);
            listDialogueWindows.Add(dialogue.dialogueWindos[i]);

        }

        //코르틴이 시작되면 대화 이벤트가 시작되기 때문에 이미지를 불러옵니다.
        animSprite.SetBool("Appear",true);
        animDialogueWindow.SetBool("Appear",true);
        StartCoroutine(StartDialogueCoroutine());
    }
    public void ExitDialogue()
    {       
        text.text = "";
        count = 0 ;
        listSentences.Clear();
        listSprites.Clear();
        listDialogueWindows.Clear();
                animSprite.SetBool("Appear",false);
        animDialogueWindow.SetBool("Appear",false);

        talking = false;
        theOrder.Move();
    }


    IEnumerator StartDialogueCoroutine()
    {
        if(count>0)
        {
            if(listDialogueWindows[count]!= listDialogueWindows[count-1])
            {
                animSprite.SetBool("Change",true);
                animDialogueWindow.SetBool("Appear",false);
                yield return new WaitForSeconds(0.2f);
                rendererDialogueWindow.GetComponent<SpriteRenderer>().sprite = listDialogueWindows[count];
                rendererDialogueWindow.GetComponent<SpriteRenderer>().sprite = listSprites[count];
                animDialogueWindow.SetBool("Appear",true);
                animSprite.SetBool("Change",false);

            }
            else 
            {
                if(listSprites[count] != listSprites[count-1])
                {
                    animSprite.SetBool("Change",true);
                        yield return new WaitForSeconds(0.1f);

                        rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];


                    animSprite.SetBool("Change",false);

                }
                else{
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
        else 
        {
            rendererDialogueWindow.GetComponent<SpriteRenderer>().sprite = listDialogueWindows[count];
            rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprites[count];

        }

    KeyActivated =true;
        for(int i = 0 ; i < listSentences[count].Length; i++)
        {
            text.text+= listSentences[count][i]; // 한글자씩 출력
            if(i%7==1)
            {
                theAudio.Play(typeSound);
            }
            yield return new WaitForSeconds(0.05f);

        }


    }
    // Update is called once per frame
    void Update()
    {
        if(talking && KeyActivated)
        {
            if(Input.GetKeyDown(KeyCode.Z))
                    {
                        KeyActivated = false;
                        count++;
                        text.text="";
                        theAudio.Play(enterSound);
                        if(count == listSentences.Count)
                        {
                            StopAllCoroutines();
                            ExitDialogue();
                        }
                        else
                        {
                            StopAllCoroutines();
                            StartCoroutine(StartDialogueCoroutine());
                        }
                    }

        }
       
    }
}
