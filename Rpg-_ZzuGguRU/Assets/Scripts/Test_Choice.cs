using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Choice : MonoBehaviour
{
    [SerializeField]
    public Choice choice;
    private OrderManager theOrder;

    private ChoiceManager theChoice;
    public bool flag;
    // Start is called before the first frame update
    void Start()
    {
        theOrder = FindObjectOfType<OrderManager>();
        theChoice = FindObjectOfType<ChoiceManager>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
     if(!flag)
     {
         Debug.Log("선택지 코르틴 접속 성공~!");
         StartCoroutine(Acoroutin());
     }   


    }

    IEnumerator Acoroutin()
    {
        flag = true;
        theOrder.notMove();

        theChoice.ShowChoice(choice);

        yield return new WaitUntil(()=> !theChoice.choicing);

        theOrder.Move();
        Debug.Log(theChoice.GetResult());

    }

}
