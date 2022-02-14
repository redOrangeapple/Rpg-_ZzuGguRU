using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Confirm_Cancel : MonoBehaviour
{
    private AudioManager theAudio;

    public string Key_sound;
    public string Enter_Sound;
    public string Cancle_Sound;

    public GameObject Confirm_Panel;
    public GameObject Cancel_Panel;
    // Start is called before the first frame update
    public Text Confirm_TEXT;
    public Text Cancel_TEXT;

    public bool activated;
    private bool keyinput;
    private bool result=true;
    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(keyinput)
        {
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                Selected();
            }
        
            else if(Input.GetKeyDown(KeyCode.UpArrow))
            {                
                Selected();
            }

            else if(Input.GetKeyDown(KeyCode.Z))
            {
                theAudio.Play(Enter_Sound);
                keyinput = false;
                activated = false;

            }
            else if(Input.GetKeyDown(KeyCode.Escape))
            {
                theAudio.Play(Cancle_Sound);
                keyinput = false;
                activated = false;
                result =false;
            }

        }
        
    }


    public void Selected()
    {

        Debug.Log("1차성공");
        result = !result;
        theAudio.Play(Key_sound);
        if(result)
        {
             Debug.Log("2차성공");
            Confirm_Panel.gameObject.SetActive(false);
            Cancel_Panel.gameObject.SetActive(true);

        }
        else
        {
            Confirm_Panel.gameObject.SetActive(true);
            Cancel_Panel.gameObject.SetActive(false);
        }

    }

    public void showChoice(string _ConfirmText , string _CancelText)
    {  //Debug.Log("여기 까지는 정상 출력 되나요");
        activated = true;
        result = true;
        Confirm_TEXT.text = _ConfirmText;
        Cancel_TEXT.text  = _CancelText;

        Confirm_Panel.gameObject.SetActive(false);
        Cancel_Panel.gameObject.SetActive(true);

        StartCoroutine(ShowChoiceCoroutine());
    }



    IEnumerator ShowChoiceCoroutine()
    {
        yield return new WaitForSeconds(0.01f);
        keyinput = true;
    }


    public bool GetResult()
    {
        return result;

    }
}
