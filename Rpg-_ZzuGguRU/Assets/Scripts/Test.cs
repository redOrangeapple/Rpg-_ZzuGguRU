using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TestMove
{
    public string name;
    public string direction;


}
public class Test : MonoBehaviour
{
    public string direction;
    //[SerializeField]
   // public TestMove[] testMove;
    private Order_Manager theOther;
    // Start is called before the first frame update
    void Start()
    {
        theOther = FindObjectOfType<Order_Manager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
            if(other.gameObject.name == "Player")
            {

                theOther.PlayLoadCharacter();

                theOther.Turn("npc1",direction);
            /*
                for(int i = 0 ; i < testMove.Length;i++)
                {
                    theOther.Move(testMove[i].name,testMove[i].direction);
                }
            */
            }
    }
}
