using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{

    public static PlayerStat instance;

    public int Character_LV;
    public int[] needExp;
    public int CurrentExp;

    public int Hp;
    public int CurrentHp;
    public int Mp;
    public int CurrentMp;

    public int atk;
    
    public int def;

    public int recoverHP;
    public int recoverMp;

    public string DmgSound;

    public GameObject Prefabs_Floatinf_TEXT;
    public GameObject parent;

    public float time;
    public float current_time;


    void Start()
    {
        instance = this;
        CurrentHp = Hp;
        CurrentMp = Mp;
        current_time =time;
        
    }

    public void Hit(int _Enemy_atk)
    {
        int dmg=0;
        
        //주인공의 방어가 적보다 같거나 클경우
        if(def >= _Enemy_atk)
        {
            dmg =1;
        }
        else if(def < _Enemy_atk)
        {   
            dmg = _Enemy_atk-def;
        }

        CurrentHp -= dmg;

        if(CurrentHp <= 0)
        {
            Debug.Log("체력이 0 이 되었습니다");
        }

        AudioManager.instance.Play(DmgSound);

        Vector3 vector = this.transform.position;

        vector.y  +=60;

        GameObject Clone = Instantiate(Prefabs_Floatinf_TEXT,vector,Quaternion.Euler(Vector3.zero));

        Clone.GetComponent<Floating_TEXT>().text.text = dmg.ToString();
        Clone.GetComponent<Floating_TEXT>().text.color = Color.red;
        Clone.GetComponent<Floating_TEXT>().text.fontSize = 25;
        Clone.transform.SetParent(parent.transform);

        StopAllCoroutines();
        StartCoroutine(HitCoroutine());

    }


    IEnumerator HitCoroutine()
    {   
        Color color = GetComponent<SpriteRenderer>().color;
 
        for(int i = 0 ; i <3;i++)
        {
            color.a = 0;
            GetComponent<SpriteRenderer>().color=color;
            yield return new WaitForSeconds(0.1f);
            color.a =1f;
            GetComponent<SpriteRenderer>().color=color;
            yield return new WaitForSeconds(0.1f);


        }

    }

    // Update is called once per frame
    void Update()
    {
      if(CurrentExp >= needExp[Character_LV]  )
      {
          Character_LV++;

          Hp+=Character_LV*2;
          Mp+= Character_LV+5;

          CurrentHp = Hp;
          CurrentMp = Mp;

          atk++;
          def++;

      }
      current_time -= Time.deltaTime;

      if(current_time<=0)
      {
          if(recoverHP>0)
          {
              if(CurrentHp + recoverHP <= Hp)
              CurrentHp +=recoverHP;
              else
              CurrentHp= Hp;
          }
          current_time = time;
      }
    }
}
