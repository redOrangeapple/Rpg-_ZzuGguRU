using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Controller : MovingObject
{
    //public int atk ; // 스켈레톤의 공격
    public float attackDelay ; // 공격 대기 시간
    public float inter_MoveWaitTime; // 움직임 대기 시간
    private float current_interMWT;
    public string atkSound;

    private Vector2 PlayerPos ; // 플레이어의 좌표값

    private int random_int;
    private string direction;

    public GameObject healthBarBackGround;
    public Skeletin_STAT sst;
    // Start is called before the first frame update
    void Start()
    {
        sst = FindObjectOfType<Skeletin_STAT>();
        queue = new Queue<string>();
        current_interMWT = inter_MoveWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        current_interMWT -= Time.deltaTime;

        if(current_interMWT <=0)
        {
            current_interMWT = inter_MoveWaitTime;

            if(NearPlayer())
            {   
                Flip();
                return;
            }



            RandomDirection();

            if(base.CheckCollsion())
            {
                return;

            }

            base.Move(direction);
        }
    }

    private void Flip()
    {
        Vector3 flip = transform.localScale;
        Vector3 flipBar = sst.healthBarBackground.transform.localScale;

        if(PlayerPos.x < this.transform.position.x)
        {
            flip.x= -3.5f;
            flipBar.x=-1f;
        }
        
            else
            {
                flip.x = 3.5f;
                flipBar.x = 1f;
            }

            this.transform.localScale = flip;
    
            sst.healthBarBackground.transform.localScale = flipBar;
        animator.SetTrigger("Attack");



        StartCoroutine(WaitCoroutine());
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(attackDelay);
        AudioManager.instance.Play(atkSound);
        if(NearPlayer()) 
        {
            PlayerStat.instance.Hit(GetComponent<Skeletin_STAT>().atk);

        }
    }


    private bool NearPlayer()
    {
        PlayerPos = Player_Manager.instance.transform.position;

        if( Mathf.Abs(Mathf.Abs(PlayerPos.x) - Mathf.Abs(this.transform.position.x)) <= speed *WalkCount *1.5f )
        {
           if(Mathf.Abs(Mathf.Abs(PlayerPos.y) - Mathf.Abs(this.transform.position.y)) <= speed *WalkCount*1.5f)
           return true;

        }


         if( Mathf.Abs(Mathf.Abs(PlayerPos.y) - Mathf.Abs(this.transform.position.y)) <= speed *WalkCount *1.5f )
        {
           if(Mathf.Abs(Mathf.Abs(PlayerPos.x) - Mathf.Abs(this.transform.position.x)) <= speed *WalkCount*1.5f)
           return true;

        }

        return false;

    }



    public void RandomDirection()
    {
         vector.Set(0,0,vector.z);

         
         random_int = Random.Range(0,4);

         switch(random_int){

             case 0 :
                vector.y = 1f;
                direction = "Up";
             break;

            case 1 :
                vector.y = -1f;
                direction = "Down";
             break;

            case 2 :
                vector.x = 1f;
                direction = "Right";
             break;

            case 3 :
                vector.y = 1f;
                direction = "Left";
             break;






         }

    }


}
