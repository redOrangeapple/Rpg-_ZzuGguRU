using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestDialogue : MonoBehaviour
{

    public Dialogue dialogue;
    public Dialogue_Manager theDm;
    // Start is called before the first frame update
    void Start()
    {
        theDm = FindObjectOfType<Dialogue_Manager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == "Player")
        {
            theDm.ShowDialogue(dialogue);

        }
    }
}
