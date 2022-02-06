using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager instance;

    #region  Singletom
    void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
    // Start is called before the first frame update
    
    private AudioManager theAudio; // 사운드 재생

    //커스텀 클래스에 있는 question 을 여기에 대입
    private string Question;

    private List<string> AnswerList;

    public GameObject go; // 평소에는 비활성화 시키고 대화가 필요할떄 면 끌어옴 by SetActice

    public Text question_Text;
    // Update is called once per frame
    public Text[] Answser_Text;

    public GameObject[] answerPanel;
    public Animator anim;
    public string KeySound;
    public string EnterSound;

    public bool choicing ; //  선택지 대기

    private bool keyInput; // 키처리 활성화  or 비활성화

    private int count; // 배열의 크기 

    private int result; // 선택한 선택창

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    void Start() 
    {
        theAudio = FindObjectOfType<AudioManager>();
        AnswerList = new List<string>();

        for(int i = 0 ; i <=3 ; i++)
        {
            Answser_Text[i].text = "";

            answerPanel[i].SetActive(false);
        }
        question_Text.text = "";
        
    }


    public void ShowChoice(Choice _choice)
    {   
        go.SetActive(true);
        choicing = true;
        result = 0 ;
        Question = _choice.question;
        for(int i = 0 ; i <_choice.answers.Length ; i++)
        {
            AnswerList.Add(_choice.answers[i]);
            answerPanel[i].SetActive(true);
            count = i;
        }

        anim.SetBool("Appear",true);
        Selection();
        StartCoroutine(ChoiceCoroutine());
    }

    public int GetResult()
    {
        go.SetActive(false);
        return result;

    }

    public void ExitChoice()
    {
        question_Text.text ="";
        for(int i = 0 ; i<= count ; i++)
        {
            Answser_Text[i].text = "";

            answerPanel[i].SetActive(false);

        }
     
 
        anim.SetBool("Appear",false);
           choicing = false;
        //여유 시간?
      //  go.SetActive(false);
        AnswerList.Clear();
    }

    IEnumerator ChoiceCoroutine()
    {
        yield return new WaitForSeconds(0.2f);

        //질문은 꼭 하나는 실행이 돼야함
        StartCoroutine(TypingQusetion());
        
        StartCoroutine(TypingAnswer_000());

        if(count>=1)
        StartCoroutine(TypingAnswer_001());
        if(count>=2)
        StartCoroutine(TypingAnswer_002());
        if(count>=3)
        StartCoroutine(TypingAnswer_003());

         yield return new WaitForSeconds(0.5f);

         keyInput = true;

    }
    IEnumerator TypingQusetion()
    {
        for(int i = 0 ; i <Question.Length ; i++)
        {   
            question_Text.text += Question[i]; 
            yield return waitTime;
        }

    }

     IEnumerator TypingAnswer_000()
    {
        yield return new WaitForSeconds(0.4f);
        for(int i = 0 ; i < AnswerList[0].Length ; i++)
        {   
           Answser_Text[0].text += AnswerList[0][i];
            yield return waitTime;
        }

    }

    IEnumerator TypingAnswer_001()
    {
        yield return new WaitForSeconds(0.5f);
        for(int i = 0 ; i < AnswerList[1].Length ; i++)
        {   
           Answser_Text[1].text += AnswerList[1][i];
            yield return waitTime;
        }

    }

    IEnumerator TypingAnswer_002()
    {
        yield return new WaitForSeconds(0.6f);
        for(int i = 0 ; i < AnswerList[2].Length ; i++)
        {   
           Answser_Text[2].text += AnswerList[2][i];
            yield return waitTime;
        }

    }

    IEnumerator TypingAnswer_003()
    {
        yield return new WaitForSeconds(0.7f);
        for(int i = 0 ; i < AnswerList[3].Length ; i++)
        {   
           Answser_Text[3].text += AnswerList[3][i];
            yield return waitTime;
        }

    }

    void Update() {

        if(keyInput)
        {

            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                theAudio.Play(KeySound);
                if(result >0)
                {
                    result--;
                }
                else result = count;

                Selection();
                
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                theAudio.Play(KeySound);
                if(result < count)
                {
                    result++;
                }
                else result = 0;

                Selection();

            }

            else if(Input.GetKeyDown(KeyCode.Z))
            {
                theAudio.Play(EnterSound);
                keyInput =false;
                ExitChoice();

            }

        }

        
    }
    //선택지 연출을 위한 함수
    public void Selection()
    {
        Color color = answerPanel[0].GetComponent<Image>().color;
        color.a = 0.75f;
        for(int i = 0 ; i <=count;i++)
        {
            answerPanel[i].GetComponent<Image>().color = color;
        }   
        color.a = 1f;
        answerPanel[result].GetComponent<Image>().color = color;

    }

}
