using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MovingObject
{

 static public Player_Manager instance;
    public string currentMapName ; //transferMap 스크립트에 있는 transferMapName 의 변수값을 저장



    public string WalkSound_001;
    public string WalkSound_002;
    public string WalkSound_003;
    public string WalkSound_004;

    private AudioManager theAudio;



    public float Run_Faster;

    private float ApplyRun_Faster;


    private bool canMove =true;

    private bool ApplyRunFlag = false;

    public bool notMove = false;
    // Start is called before the first frame update


    /// <summary>
    /// audooclip 을 이용하여 사운드를 이용해봅시다
    /// </summary>

    private void Awake()
    {
           if(instance == null)
        {
            
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else Destroy(this.gameObject);

    }
    void Start()
    {
            queue = new Queue<string>();
            boxCollider2D = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();
            theAudio = FindObjectOfType<AudioManager>();

    }


    //코르틴을이용하여 다중 처리하기
    //코르틴을 이용하여 움직임 제어하기
    IEnumerator MoveCoroutine()
    {

            while(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal")!=0 && !notMove)
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

            bool checkCollsionFlag = base.CheckCollsion();
            
            if(checkCollsionFlag) break;
                
            

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

            int tmp = Random.Range(1,4);
            switch(tmp)
            {
                case 1: 
                theAudio.Play(WalkSound_001);
                break;
                
                case 2: 
                theAudio.Play(WalkSound_002);
                break;
                
                case 3:
                theAudio.Play(WalkSound_003);
                break;

                case 4:
                theAudio.Play(WalkSound_004);
                break;

            }
                
 boxCollider2D.offset = new Vector2(vector.x*0.7f*speed*WalkCount,vector.y*0.7f*speed*WalkCount);

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

                    if(currentWalkCount==12)
                boxCollider2D.offset= Vector2.zero;
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
        if(canMove && !notMove)
        {
       if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            canMove = false;
           StartCoroutine(MoveCoroutine());    

        }

        }
  
    }
}
