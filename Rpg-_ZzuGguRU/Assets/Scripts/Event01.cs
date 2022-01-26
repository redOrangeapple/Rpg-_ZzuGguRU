using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event01 : MonoBehaviour
{
    public Dialogue dialogue_01;
    public Dialogue dialogue_02;
    // Start is called before the first frame update
    private Dialogue_Manager theDM;
    private Order_Manager theOM;
    private Player_Manager thePM; // Diry ==1f 일때 

    private bool flag = false;
    void Start()
    {
        theDM = FindObjectOfType<Dialogue_Manager>();
        theOM = FindObjectOfType<Order_Manager>();
        thePM = FindObjectOfType<Player_Manager>();
    }

    // Update is called once per frame

    private void OnTriggerStay2D(Collider2D other) {
        if(!flag && Input.GetKey(KeyCode.Z)&& thePM.animator.GetFloat("DirY")== 1f)
        {
            flag = true;
            StartCoroutine(EventCoroutine());

        }
        
    }

    IEnumerator EventCoroutine()
    {   
        theOM.PlayLoadCharacter();
        theOM.NotMove();
        theDM.ShowDialogue(dialogue_01);

        yield return new WaitUntil(()=>!theDM.talking);

        theOM.Move("Player","Right");
         theOM.Move("Player","Right");
          theOM.Move("Player","Up");


       yield return new WaitUntil(()=>thePM.queue.Count == 0);

        theDM.ShowDialogue(dialogue_02);
             yield return new WaitUntil(()=>!theDM.talking);


        theOM.Move();

    }


}
