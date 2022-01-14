using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_manager : MonoBehaviour
{
    /// <summary>
    /// 카메라 영역을 설정
    /// 
    /// minBound 와 maxBound 는 boxcolider영역의 최소 최대 좌표값들을 소유
    /// 
    /// </summary>
    public BoxCollider2D bound;
    private Vector3 minBound;
    private Vector3 maxBound;
    /// <summary>
    /// 카메라의 반너비 반 높이 값을 가져서 범위를 벗어나 미지의 영역이 드러나지 않도록 함
    /// </summary>
    private float halfWidth;
    private float halfHeight;
    
    /// <summary>
    /// 카메라의 반높이 값을 구할 속성값
    /// </summary>
    private Camera theCamera;


    static public Camera_manager instance;
    //카메라가 따라갈 대상
    public GameObject target;

    public float MoveSpeed; // 카메라가 얼마나 빠른 속도로 따라갈것인가

    public Vector3 targetPosition; //대상의 현재 위치값
    // Start is called before the first frame update


    private void Awake() {

        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
            Destroy(this.gameObject);
        
    }


    void Start()
    {
        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight= theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;


    }

    // Update is called once per frame
    void Update()
    {
        if (target.gameObject != null)
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

        //1초에 movespeed 만큼 이동
        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, MoveSpeed * Time.deltaTime);

        /// <summary>
        /// (10,0,100) --> 10
        /// (-100,0,100) ---> 0
        /// (1000,0,100) ---> 100
        /// </summary>
        float clampedX = Mathf.Clamp(this.transform.position.x,minBound.x+halfWidth,maxBound.x-halfWidth);
        float clampedY = Mathf.Clamp(this.transform.position.y,minBound.y+halfHeight,maxBound.y-halfHeight);

        this.transform.position = new Vector3(clampedX,clampedY,this.transform.position.z);


    }

    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;


    }

}
