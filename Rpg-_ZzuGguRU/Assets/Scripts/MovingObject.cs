using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    public string characterName;
    public int WalkCount;
   protected int currentWalkCount;

    public LayerMask layerMask;
    public float speed;
    protected Vector3 vector;
    public BoxCollider2D boxCollider2D;
     public Animator animator;

   

     private bool notCoroutine = false;

    public Queue<string> queue;

    public void Move(string _dir,int _frequency=5)
    {
        queue.Enqueue(_dir);
          if(!notCoroutine)
          {
            notCoroutine =true;
            StartCoroutine(MoveCoroutine(_dir,_frequency));
            
          }
        

    }

      IEnumerator MoveCoroutine(string _dir,int _frequency)
      {

        Debug.Log("잘된거냐");
        while(queue.Count !=0)
        {

                switch(_frequency)
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

          string direction = queue.Dequeue();
      
          vector.Set(0,0,vector.z);

          

          switch(direction)
          {
              case "Up": vector.y=1f;
              break;
              case "Right":vector.x=1f;
              break;
              case "Left":vector.x = -1f;
              break;
              case "Down": vector.y=-1f;
              break;
          }

            animator.SetFloat("DirX",vector.x);
            animator.SetFloat("DirY",vector.y);


            while(true)
            {
              bool checkCollsionFlag = CheckCollsion();
  
              if(checkCollsionFlag) 
              {
                animator.SetBool("Walking",false);
                 yield return new WaitForSeconds(1f);
              }
               else break;
            }

    
            animator.SetBool("Walking",true);


          boxCollider2D.offset = new Vector2(vector.x*0.7f*speed*WalkCount,vector.y*0.7f*speed*WalkCount);

           while(currentWalkCount < WalkCount)
            {
           
              transform.Translate(vector.x * (speed), vector.y * (speed),0);
         
                currentWalkCount++;

                if(currentWalkCount==12)
                boxCollider2D.offset= Vector2.zero;
                  yield return new WaitForSeconds(0.01f);
                
            }

            currentWalkCount=0;

            if(_frequency!=5)
            animator.SetBool("Walking",false);

        }
            animator.SetBool("Walking",false);
            notCoroutine=false;
      }

      protected bool CheckCollsion()
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