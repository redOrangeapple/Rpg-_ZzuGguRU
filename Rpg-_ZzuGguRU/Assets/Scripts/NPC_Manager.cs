using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class NPCMove
{   
    [Tooltip("NPCMove 를 체크하면 NPC 가 움직임")]
    public bool NPCmove;
    public string[] direction; // npc 가 움직일 방향설정
    
    [Range(1,5)] [Tooltip("빠르기를 결정 숫자가 클수록 빠름")]//스크롤바를 만들어주어 frequency 설정가능
    public int frequency; // npc 가 움직일 방향으로 얼마나 빠른 속도로 움직일것인가
  
}
public class NPC_Manager : MovingObject
{
    [SerializeField]
    public NPCMove npc;
    // Start is called before the first frame update
    void Start()
    {
           queue = new Queue<string>();
           StartCoroutine(MoveCoroutine());
           Debug.Log("시팔뭔데");
        
    }

    // Update is called once per frame
    public void SetMove()
    {
     //    StartCoroutine(MoveCoroutine());

    }

     public void SetNotMove()
    {


    }

    IEnumerator MoveCoroutine()
    {
        if(npc.direction.Length !=0)
        {
            for(int i = 0 ; i <npc.direction.Length ; i++)
            {
         
                //npcCanmove 가 true 가 될때까지 무한히 대기
                yield return new WaitUntil(()=>queue.Count <2);
                base.Move(npc.direction[i],npc.frequency);
                //실질적으로 이동
                if( i == npc.direction.Length-1)
                {
                    i=-1;

                }

            }

        }


    }

}
