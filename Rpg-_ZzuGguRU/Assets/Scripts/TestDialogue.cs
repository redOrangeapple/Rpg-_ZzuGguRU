using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogue : MonoBehaviour
{
    [SerializeField]
    public Dialogue dialogue;

    private Dialogue_Manager theDM;
    // Start is called before the first frame update
    void Start()
    {
        theDM = FindObjectOfType<Dialogue_Manager>();
    }

    // Update is called once per frame
     private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == "Player")
        {
            theDM.ShowDialogue(dialogue);

        }
    }
}
