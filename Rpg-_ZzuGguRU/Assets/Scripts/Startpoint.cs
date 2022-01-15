using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startpoint : MonoBehaviour
{
    private Camera_manager theCamera;

    public string startPoint; // 맵의 이동 플레이어가 시작될 위치
    
    private Player_Manager thePlayer;
    // Start is called before the first frame update
    void Start()
    {

        thePlayer = FindObjectOfType<Player_Manager>();
        theCamera = FindObjectOfType<Camera_manager>();

        if( startPoint == thePlayer.currentMapName)
        {
            theCamera.transform.position = new Vector3(this.transform.position.x,this.transform.position.y, theCamera.transform.position.z);
            thePlayer.transform.position = this.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
