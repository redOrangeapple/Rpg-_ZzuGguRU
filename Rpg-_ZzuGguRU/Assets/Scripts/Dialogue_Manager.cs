using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_Manager : MonoBehaviour
{
    public static Dialogue_Manager instance;



    public Text text;
    public SpriteRenderer rendererSprite;
    public SpriteRenderer rendererDialogueWindow;

    private List<string> listSentences;
    private List<Sprite> listSprite;
    private List<Sprite> listDialogueWindows;

    private int count ; // 대화 진행 상황 카운트
    public Animator animSprite;
    public Animator animDialogueWindow;
    public bool talking=false;
    #region Singleton
         void Awake() {
         
         if(instance == null)
         {
             DontDestroyOnLoad(this.gameObject);
             instance = this;

         }
         else Destroy(this.gameObject);
        
          }
          #endregion
    // Start is called before the first frame update
    void Start()
    {
        text.text="";
        count = 0;
        listSentences = new List<string>();
        listSprite = new List<Sprite>();
        listDialogueWindows = new List<Sprite>();
    }

    // Update is called once per frame
    void Update()
    {
        if(talking)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                count++;
                text.text="";
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


    public void ShowDialogue(Dialogue dialogue)
    {
        talking = true;

        for(int i = 0 ; i< dialogue.sentences.Length ; i++)
        {
            listSentences.Add(dialogue.sentences[i]);
            listSprite.Add(dialogue.sprites[i]);
            listDialogueWindows.Add(dialogue.dialogueWindows[i]);

        }
        animSprite.SetBool("Appear",true);
        animDialogueWindow.SetBool("Appear",true);
        StartCoroutine(StartDialogueCoroutine());
    }

    public void ExitDialogue()
    {
        text.text="";
        count = 0 ;
        listSentences.Clear();
        listSprite.Clear();
        listDialogueWindows.Clear();
         animSprite.SetBool("Appear",false);
        animDialogueWindow.SetBool("Appear",false);
        talking = false;
    }
    

    IEnumerator StartDialogueCoroutine()
    {

        if(count>0)
        {
            if(listDialogueWindows[count] != listDialogueWindows[count-1])
            {
                animSprite.SetBool("Change",true);
                animDialogueWindow.SetBool("Appear",false);
                yield return new WaitForSeconds(0.2f);

                rendererDialogueWindow.GetComponent<SpriteRenderer>().sprite = listDialogueWindows[count];
                rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprite[count];
                animDialogueWindow.SetBool("Appear",true);
                animSprite.SetBool("Change",false);

            }
            else 
            {
                 if(listSprite[count] != listSprite[count-1])
                {   
                    animSprite.SetBool("Change",true);
                    yield return new WaitForSeconds(0.1f);
                    rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprite[count];
                    animSprite.SetBool("Change",false);
                }
                else
                {   
                    yield return new WaitForSeconds(0.1f);

                }

            }


        }
        else
        {
            rendererDialogueWindow.GetComponent<SpriteRenderer>().sprite = listDialogueWindows[count];
            rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprite[count];

        }
    

        for(int i = 0 ; i < listSentences[count].Length ;i++)
        {
            text.text += listSentences[count][i]; // 화면에 한글자 씩 출력
            yield return new WaitForSeconds(0.01f);
        }



    }

}
