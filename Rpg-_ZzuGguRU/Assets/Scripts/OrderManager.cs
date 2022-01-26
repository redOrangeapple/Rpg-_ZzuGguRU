using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{

  
    private Player_Manager thePlayer; // 이벤트 도중에 키입력 처리 방지
    private List<MovingObject> characters;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<Player_Manager>();
    }

    public void PreLpadCharacter()
    {
        characters = ToList();

    }

    public List<MovingObject> ToList()
    {
        List<MovingObject> tempList = new List<MovingObject>();
        MovingObject[] temp = FindObjectsOfType<MovingObject>();


        for(int i = 0; i < temp.Length ; i++)
        {
            tempList.Add(temp[i]);

        }
        return tempList;

    }

    public void Move(string _name , string _dir)
    {
        for(int i = 0 ; i <characters.Count;i++)
        {
            if(_name == characters[i].characterName)
            {
                characters[i].Move(_dir);
                Debug.Log("any problem?");

            }
        }

    }


    public void setTransparent(string _name)
    {
        for(int i = 0 ; i <characters.Count;i++)
        {
            if(_name == characters[i].characterName)
            {
                characters[i].gameObject.SetActive(false);
            }
        }

    }

    public void setUnTransparent(string _name)
    {
        for(int i = 0 ; i <characters.Count;i++)
        {
            if(_name == characters[i].characterName)
            {
                characters[i].gameObject.SetActive(true);
            }
        }

    }
    public void SetThorought(string _name)
    {
         for(int i = 0 ; i <characters.Count;i++)
        {
            if(_name == characters[i].characterName)
            {
                characters[i].boxCollider2D.enabled =false;
            }
        }

    }

    public void SetUnThorought(string _name)
    {
         for(int i = 0 ; i <characters.Count;i++)
        {
            if(_name == characters[i].characterName)
            {
                characters[i].boxCollider2D.enabled = true;
            }
        }

    }

    public void Turn(string _name,string _dir)
    {
        for(int i = 0 ; i <characters.Count;i++)
        {
            if(_name == characters[i].characterName)
            {
                characters[i].animator.SetFloat("DirX",0f);
                characters[i].animator.SetFloat("DirY",0f);
                switch(_dir)
                {
                    case "Up": characters[i].animator.SetFloat("DirY",1f);  break; 
                    case "Down":characters[i].animator.SetFloat("DirY",-1f); break;
                    case "Left":characters[i].animator.SetFloat("DirX",-1f); break;
                    case "Right":characters[i].animator.SetFloat("DirX",1f); break;

                }
                
            }
        } 


    }


    // Update is called once per frame  
    void Update()
    {
        
    }
}
