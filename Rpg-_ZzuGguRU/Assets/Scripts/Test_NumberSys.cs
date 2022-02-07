using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_NumberSys : MonoBehaviour
{
[SerializeField]
    private OrderManager theOrder;

    private Number_Sys theNum;
    public bool flag;
    public int correctnum;
    // Start is called before the first frame update
    void Start()
    {
        theOrder = FindObjectOfType<OrderManager>();
        theNum = FindObjectOfType<Number_Sys>();
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

        theNum.ShowNumner(correctnum);

        yield return new WaitUntil(()=> !theNum.activated);

        theOrder.Move();


    }

}
