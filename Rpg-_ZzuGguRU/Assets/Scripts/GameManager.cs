using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     private Bound[] bounds;
     private Player_Manager thePlayer;
     private Camera_manager theCamera;

    public void LoadStart()
    {
        StartCoroutine(LoadWaitCoroutine());

    }

    IEnumerator LoadWaitCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        thePlayer = FindObjectOfType<Player_Manager>();
        bounds = FindObjectsOfType<Bound>();
        theCamera = FindObjectOfType<Camera_manager>();
        Debug.Log("씬이동 접근좀 하자!!");
        theCamera.target = GameObject.Find("Player");

        for(int i = 0 ; i<bounds.Length ;i++)
        {
            if(bounds[i].BoundName == thePlayer.currentMapName)
            {
                bounds[i].SetBound();
                break;

            }

        }

        
    }



}
