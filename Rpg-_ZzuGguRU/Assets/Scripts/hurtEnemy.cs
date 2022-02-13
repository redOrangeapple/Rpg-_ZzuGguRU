using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurtEnemy : MonoBehaviour
{
    public GameObject Prefabs_Floatinf_TEXT;
    public GameObject parent;

    public string atkSound;

    private PlayerStat thePlayerStat;
    // Start is called before the first frame update
    void Start()
    {
        thePlayerStat = FindObjectOfType<PlayerStat>();
    }

    private void OnTriggerEnter2D(Collider2D other) {

           if(other.gameObject.tag =="Enemy")
           {
               int dmg = other.gameObject.GetComponent<Skeletin_STAT>().Hit(thePlayerStat.atk);
             //  AudioManager.instance.Play(atkSound);

            Vector3 vector = other.transform.position;
                        vector.y  +=60;

            GameObject Clone = Instantiate(Prefabs_Floatinf_TEXT,vector,Quaternion.Euler(Vector3.zero));

            Clone.GetComponent<Floating_TEXT>().text.text = dmg.ToString();
            Clone.GetComponent<Floating_TEXT>().text.color = Color.white;
            Clone.GetComponent<Floating_TEXT>().text.fontSize = 25;
            Clone.transform.SetParent(parent.transform);
           } 
    }

}
