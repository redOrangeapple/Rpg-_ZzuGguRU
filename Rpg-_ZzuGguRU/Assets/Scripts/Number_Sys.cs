using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Number_Sys : MonoBehaviour
{
    private AudioManager theAudio;
    public string key_sound; // 방향키 사운드

    public string Enter_Sound; // 결정키 사운드

    public string Cancel_Sound; // 취소 사운드

    public string Correct_Sound; // 정답 사운드

    private int count; // 배열의 크기 자릿수 지정

    private int Selected_Text_Box;

    private int result; // 플레이어가 도출해낸값

    private int Correct_NUm; // 실제 정답수

    public GameObject superObj; // 화면 가운데 정렬 역할

    public GameObject[] panel;
    public Text[] Number_Text;

    public Animator anim;

    public bool activated; // return new waituntil 이용역할

    private bool keyInput ;//키처리 비/활성화 역할

    private bool correctFlag; // 정답인지 아닌지 여부 결정

    private string tmpNum;


    // Start is called before the first frame update
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
    }
    public void ShowNumner(int _correctNumber)
    {
       Correct_NUm = _correctNumber;
       activated = true;
       correctFlag = false;

       string temp = Correct_NUm.ToString();

       for(int i = 0 ; i < temp.Length ; i++)
       {
           count = i;
           panel[i].SetActive(true);
   
           Number_Text[i].text = "0";

       }

        superObj.transform.position = new Vector3(superObj.transform.position.x + 50*count,
                                                  superObj.transform.position.y,
                                                  superObj.transform.position.z);

        Selected_Text_Box= 0;
        result = 0;
        SetColor();
        anim.SetBool("Appear",true);
        keyInput = true;



    }

    public bool GetResult()
    {
        return correctFlag;
    }


    public void SetNumber(string _arrow)
    {
        int temp = int.Parse(Number_Text[Selected_Text_Box].text); // 선택된 자리수의 텍스트를 interger 숫자형식으로 강제변환
        
        if(_arrow =="Down")
        {
            if(temp == 0)   
            temp=9;
            else temp--;
 
        }

        else if(_arrow == "Up")
        {
            if(temp==9)
            temp=0;
            else temp++;

        }
        Number_Text[Selected_Text_Box].text = temp.ToString();


    }



    public void SetColor()
    {

        Color color = Number_Text[0].color;
        color.a = 0.3f;
        
        for(int i = 0 ; i <= count ; i++)
        {
            Number_Text[i].color = color;

        }
        color.a =1f;

        Number_Text[Selected_Text_Box].color = color;

    }



    // Update is called once per frame
    void Update()
    {
        if(keyInput)
        {
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                theAudio.Play(key_sound);
                SetNumber("Down");

            }
            else if(Input.GetKeyDown(KeyCode.UpArrow)) 
            {
                theAudio.Play(key_sound);
                SetNumber("Up");
            }

            else if(Input.GetKeyDown(KeyCode.LeftArrow)) 
            {
                theAudio.Play(key_sound);
                if(Selected_Text_Box<count)
                {
                    Selected_Text_Box++;
                }    
                else Selected_Text_Box = 0;

                SetColor();


            }
            else if(Input.GetKeyDown(KeyCode.RightArrow)) 
            {
                theAudio.Play(key_sound);

                if(Selected_Text_Box>0)
                Selected_Text_Box--;
                else Selected_Text_Box = count;

                SetColor();
            }

            else if(Input.GetKeyDown(KeyCode.Z)) // 항목을 결정
            {
                theAudio.Play(Enter_Sound);
                keyInput = false;
                StartCoroutine(OXcoroutine());

            }

            else if(Input.GetKeyDown(KeyCode.X)) // 취소 결정
            {
                theAudio.Play(Cancel_Sound);
                keyInput =false;
                StartCoroutine(ExitCoroutine());

            }



        }
        
    }



    IEnumerator OXcoroutine()
    {
        Color color =Number_Text[0].color;
        color.a =1f;



        for(int i = count; i>=0 ;i--)
        {
            Number_Text[i].color = color;
            tmpNum += Number_Text[i].text;
        }

        yield return new WaitForSeconds(1f);

        result = int.Parse(tmpNum);

        if(result == Correct_NUm)
        {
            theAudio.Play(Correct_Sound);
            correctFlag = true;
        }
        else 
        {
            theAudio.Play(Cancel_Sound);
            correctFlag =false;

        }

        StartCoroutine(ExitCoroutine());

    }
    IEnumerator ExitCoroutine()
    {
        Debug.Log("result is "+ result + "then corrrct number is" + Correct_NUm); 
        result = 0 ;
        tmpNum = "";
        anim.SetBool("Appear",false);

        yield return new WaitForSeconds(0.1f);

        for(int i = 0 ; i <= count ; i++)
        {
            panel[i].SetActive(false);

        }

          superObj.transform.position = new Vector3(superObj.transform.position.x -(50*count),
                                                  superObj.transform.position.y,
                                                  superObj.transform.position.z);
                                                
          activated = false;
    }
}
