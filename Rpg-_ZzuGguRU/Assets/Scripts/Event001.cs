using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event001 : MonoBehaviour
{
    public Dialogue dialogue_001;
    public Dialogue dialogue_002;
    
    private Dialogue_Manager theDm;
    private OrderManager theOM;
    private Player_Manager thePlayer; // DirY == 1f 일때 

    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        theDm = FindObjectOfType<Dialogue_Manager>();
        theOM = FindObjectOfType<OrderManager>();
        thePlayer = FindObjectOfType<Player_Manager>();
        

    }

    // Update is called once per frame
     private void OnTriggerStay2D(Collider2D other) {
        
        if(!flag && Input.GetKey(KeyCode.Z) && thePlayer.animator.GetFloat("DirY") == 1f)
        {
            flag = true;
            StartCoroutine(EventCoroutine());

        }

    }

    IEnumerator EventCoroutine()
    {
        theOM.PreLpadCharacter();
        theOM.notMove();

        theDm.ShowDialogue(dialogue_001);
        
        yield return new WaitUntil(()=> !theDm.talking);

        theOM.Move("Player","Right");
        theOM.Move("Player","Right");
        theOM.Move("Player","Up");

        yield return new WaitUntil(()=> thePlayer.queue.Count == 0);

        theDm.ShowDialogue(dialogue_002);

        yield return new WaitUntil(()=> !theDm.talking);
        
        theOM.Move();

    }




}
