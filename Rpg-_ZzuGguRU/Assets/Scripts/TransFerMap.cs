using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TransFerMap : MonoBehaviour
{

    public string transFerMapName;

    public Transform target;

    public BoxCollider2D targetBound;
    // Start is called before the first frame update
    private Player_Manager thePlayer;
    private Camera_manager theCamera;

    private FadeManager thFade;

    private OrderManager theOm;

    public Animator anim_01;
    public Animator anim_02;

    public int door_count;

    [Tooltip("Up , Down , Left , Right")]
    public string direction; // 캐릭터가 바라보고 있는 방향
    private Vector2 vector; //getfloat("DirX" , DirY)

    [Tooltip("문이 존재 true , 문 없음 false")]
    public bool door; // 문의 존재여부를 체크






    void Start()
    {
        thePlayer = FindObjectOfType<Player_Manager>();
        theCamera = FindObjectOfType<Camera_manager>();
        thFade = FindObjectOfType<FadeManager>();
        theOm = FindObjectOfType<OrderManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) {
        
        Debug.Log("충돌 발생");
        if(!door)
        if(other.gameObject.name=="Player")
        {
            StartCoroutine(TransferCoutine());

        }
    }



   private void OnTriggerStay2D(Collider2D other) {
       if(door)
        {


        if(other.gameObject.name=="Player")
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                vector.Set(thePlayer.animator.GetFloat("DirX"),thePlayer.animator.GetFloat("DirY"));
                
                switch(direction)
                {
                        case "Up": 
                        if(vector.y == 1f)
                        {
                            StartCoroutine(TransferCoutine());
                        }
                        break;

                         case "Down": 
                        if(vector.y == -1f)
                        {
                            StartCoroutine(TransferCoutine());
                        }
                        break;


                        case "Right": 
                        if(vector.x == 1f)
                        {
                            StartCoroutine(TransferCoutine());
                        }
                        break;

                        case "Left": 
                        if(vector.x == -1f)
                        {
                            StartCoroutine(TransferCoutine());
                        }
                        break;

                        

                        default: StartCoroutine(TransferCoutine());
                        break;

                }

                StartCoroutine(TransferCoutine());
            
            }
        }

       }
   }



    IEnumerator TransferCoutine()
    {

        theOm.PreLpadCharacter();

            theOm.notMove();
            thFade.FadeOut();

            if(door) 
            {
                if(door_count == 1)
                {
                    anim_01.SetBool("Open",true);
                }
                else if(door_count>1)
                {
                     anim_01.SetBool("Open",true);
                     anim_02.SetBool("Open",true);

                }

            }
            yield return new WaitForSeconds(0.5f);

            theOm.setTransparent("Player");


            if(door) 
            {
                if(door_count == 1)
                {
                    anim_01.SetBool("Open",false);
                }
                else if(door_count>1)
                {
                     anim_01.SetBool("Open",false);
                     anim_02.SetBool("Open",false);

                }

            }


             yield return new WaitForSeconds(0.5f);

                 theOm.setUnTransparent("Player");

            thePlayer.currentMapName = transFerMapName;

            theCamera.SetBound(targetBound);
           //SceneManager.LoadScene(transFerMapName);
           thePlayer.transform.position = target.transform.position;
           theCamera.transform.position =  new Vector3(target.transform.position.x,target.transform.position.y,theCamera.transform.position.z);
            thFade.FadeIn();
             yield return new WaitForSeconds(0.5f);
            theOm.Move();


    }

}

