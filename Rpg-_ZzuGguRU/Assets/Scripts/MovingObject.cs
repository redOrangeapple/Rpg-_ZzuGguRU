using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public string CharacterName;
    public int WalkCount;
   protected int currentWalkCount;

    public LayerMask layerMask;
    public float speed;
    protected Vector3 vector;
    public BoxCollider2D boxCollider2D;
     public Animator animator;

     protected bool NpcCanMove = true;

     private bool notCoroutine = false;

    public Queue<string> queue;

     public void Move(string _dir,int _frequency = 5)
    {

        queue.Enqueue(_dir);

        if(!notCoroutine)
        {
           notCoroutine=true;
            StartCoroutine(Movecoroutine(_dir,_frequency));
        }
     

    }
    IEnumerator Movecoroutine(string _dir,int _frequency)
    {

        while(queue.Count !=0)
        {
            string direction = queue.Dequeue();
            NpcCanMove = false;
        vector.Set(0,0,vector.z);
        switch(direction)
        {
            case "Up":
            vector.y=1f;
                break;
            case "Down":
             vector.y=-1f;
                break;
            case "Right":
             vector.x=1f;
                break;
            case "Left":
             vector.x=-1f;
                break;             
        }

            animator.SetFloat("DirX",vector.x);
            animator.SetFloat("DirY",vector.y);

            animator.SetBool("Walking",true);

         while(currentWalkCount < WalkCount)
            {
                transform.Translate(vector.x*speed,vector.y*speed,0);
           
                currentWalkCount++;
                  yield return new WaitForSeconds(0.01f);
                
            }
            if(_frequency!=5)
            animator.SetBool("Walking",false);

            currentWalkCount=0;
            NpcCanMove = true;

        }   
           animator.SetBool("Walking",false);
           notCoroutine = false;
    }

    protected bool CheckCollision()
    {
     RaycastHit2D hit;
            Vector2 start = transform.position; //캐릭터의 현재 위치값
            Vector2 end = start + new Vector2(vector.x *speed *WalkCount,vector.y*speed*WalkCount); // 캐릭터가 이동하고자 하는 위치값


            boxCollider2D.enabled=false;

            hit = Physics2D.Linecast(start,end,layerMask);

            boxCollider2D.enabled = true;
            //null 일경우 방해물이 없다 , null 이 아니면 방해물 존재
            if(hit.transform != null)
            return true;

            return false;
    }
}