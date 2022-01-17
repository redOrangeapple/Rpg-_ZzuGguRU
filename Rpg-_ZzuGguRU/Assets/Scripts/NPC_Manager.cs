using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class NPCMove
{   
    [Tooltip("NPCMove 를 체크하면 NPC 가 움직임")]
    public bool NPCmove;
    public string[] direction; // npc 가 움직일 방향설정
    
    [Range(1,5)] [Tooltip("1 = 천천히  , 2 = 조금 천천히 , 3 = 보통 , 4 = 빠르게 , 5 = 연속적으로")]
    public int frequency; // npc 가 움직일 방향으로 얼마나 빠른 속도로 움직일것인가

}
public class NPC_Manager : MovingObject
{
    [SerializeField]
    public NPCMove npc;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveCoroutine());
    }
    public void SetMove()
    {


    }
       public void SetNotMove()
    {


    }
    
    IEnumerator MoveCoroutine()
    {
        if(npc.direction.Length!=0)
        {
            for(int i=0;i<npc.direction.Length;i++)
            {
                switch(npc.frequency)
                {
                    case 1: yield return new WaitForSeconds(4f);
                    break;

                    case 2: yield return new WaitForSeconds(3f);
                    break;

                    case 3: yield return new WaitForSeconds(2f);
                    break;

                    case 4: yield return new WaitForSeconds(1f);
                    break;

                    case 5: 
                    break;
                }
            
            yield return new WaitUntil(()=>NpcCanMove);

              base.Move(npc.direction[i],npc.frequency);

             // 실질적인 이동구간
             if(i == npc.direction.Length -1)
             {
                 i=-1;
             }

                
            }
        }

    }


}
