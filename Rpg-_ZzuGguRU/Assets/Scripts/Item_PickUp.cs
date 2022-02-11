using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_PickUp : MonoBehaviour
{
    private Inventory theInven;
    private AudioManager theAudio;
    public int itemID;
    public int _count;
    public string PickUpSound;

    private void Start() {
       theInven = FindObjectOfType<Inventory>();
       theAudio = FindObjectOfType<AudioManager>(); 
    }


    private void OnTriggerStay2D(Collider2D other) {
        
        if(Input.GetKeyDown(KeyCode.Z))
        {   
            theAudio.Play(PickUpSound);
            theInven.GetItem(itemID,2);
            
            //아이템 을 획득후 필드에 있는 아이템 삭제

            Destroy(this.gameObject);


        }
    }

}
