using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     private Bound[] bounds;
     private Player_Manager thePlayer;
     private Camera_manager theCamera;

     private FadeManager theFade;
     private Menu theMenu;
     private Dialogue_Manager theDm;
     private Camera thecam;

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
        theFade = FindObjectOfType<FadeManager>();
        theMenu = FindObjectOfType<Menu>();
        thecam = FindObjectOfType<Camera>();
        theDm = FindObjectOfType<Dialogue_Manager>();


         Color color = thePlayer.GetComponent<SpriteRenderer>().color;
         color.a=1f;

         thePlayer.GetComponent<SpriteRenderer>().color=color;


        Debug.Log("씬이동 접근좀 하자!!");
        theCamera.target = GameObject.Find("Player");
        theMenu.GetComponent<Canvas>().worldCamera = thecam;
        theDm.GetComponent<Canvas>().worldCamera = thecam;

        for(int i = 0 ; i<bounds.Length ;i++)
        {
            if(bounds[i].BoundName == thePlayer.currentMapName)
            {
                bounds[i].SetBound();
                break;

            }
        //  hpbar.SetActive(true);
        //  mpbar.SetActive(true);
            theFade.FadeIn();
        }

        
    }



}
