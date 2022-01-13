using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TransFerMap : MonoBehaviour
{

    public string transFerMapName;

    public Transform target;
    // Start is called before the first frame update
    private MovingObject thePlayer;
    private Camera_manager theCamera;
    void Start()
    {
        thePlayer = FindObjectOfType<MovingObject>();
        theCamera = FindObjectOfType<Camera_manager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) {
        
        Debug.Log("충돌 발생");
        if(other.gameObject.name=="Player")
        {
            thePlayer.currentMapName = transFerMapName;
           //SceneManager.LoadScene(transFerMapName);
           thePlayer.transform.position = target.transform.position;
           theCamera.transform.position =  new Vector3(target.transform.position.x,target.transform.position.y,theCamera.transform.position.z);


        }


    }
}
