using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    public LayerMask layerMask;
    private BoxCollider2D boxCollider2D;

    public float speed;
    private Vector3 vector;

    public float Run_Faster;

    private float ApplyRun_Faster;

    public int WalkCount;

    public int currentWalkCount;

    private bool canMove =true;

    private bool ApplyRunFlag = false;
    // Start is called before the first frame update
    private Animator animator;
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }


    //코르틴을이용하여 다중 처리하기
    //코르틴을 이용하여 움직임 제어하기
    IEnumerator MoveCoroutine()
    {

            while(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal")!=0)
            {
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    ApplyRun_Faster = Run_Faster;
                    ApplyRunFlag=true;
                }
                else 
                {
                    ApplyRun_Faster =0 ;
                    ApplyRunFlag=false;
                }

            //객체의 좌표값을 지정함
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if(vector.x !=0)
                vector.y=0; 

            animator.SetFloat("DirX",vector.x);
            animator.SetFloat("DirY",vector.y);

            RaycastHit2D hit;
            Vector2 start = transform.position; //캐릭터의 현재 위치값
            Vector2 end = start + new Vector2(vector.x *speed *WalkCount,vector.y*speed*WalkCount); // 캐릭터가 이동하고자 하는 위치값


            boxCollider2D.enabled=false;

            hit = Physics2D.Linecast(start,end,layerMask);

            boxCollider2D.enabled = true;
            //null 일경우 방해물이 없다 , null 이 아니면 방해물 존재
            if(hit.transform != null)
            break;


            animator.SetBool("Walking",true);

            while(currentWalkCount < WalkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed+ApplyRun_Faster), 0, 0);
                    //또는 
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed+ApplyRun_Faster), 0);
                }
                if(ApplyRunFlag)
                {
                    currentWalkCount++;
                }
                currentWalkCount++;
                  yield return new WaitForSeconds(0.01f);
            }
            currentWalkCount = 0 ;


            }

           //상하 좌우 키가 눌렸을 경우 상하 좌우의 움직임을 구현한다
                canMove =true;
                animator.SetBool("Walking",false);

        //1초동안 대기 하겠습니다
      

    }
    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
       if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            canMove = false;
           StartCoroutine(MoveCoroutine());    

        }

        }
  
    }

}