using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_OnlyShowText : MonoBehaviour
{
    private OrderManager theOrder;

    private Dialogue_Manager theDial;
   // private Number_Sys theNum;
    public bool flag;
    public string[] texts;
    // Start is called before the first frame update
    void Start()
    {
        theOrder = FindObjectOfType<OrderManager>();
        theDial =  FindObjectOfType<Dialogue_Manager>();
       // theNum = FindObjectOfType<Number_Sys>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
     if(!flag)
     {
         //Debug.Log("선택지 코르틴 접속 성공~!");
         StartCoroutine(Acoroutin());
     }   


    }

    IEnumerator Acoroutin()
    {
        flag = true;
        theOrder.notMove();

      //  theNum.ShowNumner(correctnum);
           theDial.ShowText(texts);
        yield return new WaitUntil(()=> !theDial.talking);

        theOrder.Move();


    }
}
