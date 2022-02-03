using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Controller : MonoBehaviour
{
    private Player_Manager thePlayer; // 플레이어가 바라 보고 있는 방향
    private Vector2 vector;

    private Quaternion rotation; // 회전 각도를 담당하는 vevtor4 x y z w
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<Player_Manager>();
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.position = thePlayer.transform.position;
        vector.Set(thePlayer.animator.GetFloat("DirX"),thePlayer.animator.GetFloat("DirY"));

        if(vector.x ==1f)
        {
            rotation = Quaternion.Euler(0,0,90);
            this.transform.rotation = rotation;

        }
        else if(vector.x == -1f)
        {
            rotation = Quaternion.Euler(0,0,-90);
            this.transform.rotation = rotation;
        }
        else if(vector.y ==1f)
        {
            rotation = Quaternion.Euler(0,0,180);
            this.transform.rotation = rotation;

        }
        else if(vector.y == -1f)
        {
            rotation = Quaternion.Euler(0,0, 0);
            this.transform.rotation= rotation;

        }




    }
}
