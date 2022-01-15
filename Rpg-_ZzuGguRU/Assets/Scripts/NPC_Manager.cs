using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class NPCMove
{   
    [Tooltip("NPCMove 를 체크하면 NPC 가 움직임")]
    public bool NPCmove;
    public string[] direction; // npc 가 움직일 방향설정
    
    [Range(1,5)]
    public int frequency; // npc 가 움직일 방향으로 얼마나 빠른 속도로 움직일것인가

}
public class NPC_Manager : MonoBehaviour
{
    [SerializeField]
    public NPCMove npc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
