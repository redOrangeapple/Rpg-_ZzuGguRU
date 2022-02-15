using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skeletin_STAT : MonoBehaviour
{

    public int Hp;
    public int cureentHp;
    public int atk;
    public int def;
    public int exp;

    public GameObject healthBarBackground;
    public Image healthBarFilled;


    // Start is called before the first frame update
    void Start()
    {
        cureentHp = Hp;
        healthBarFilled.fillAmount =1f;
    }

    public int Hit(int _PlayerAtk)
    {
        int playeratk = _PlayerAtk;
        int dmg;

        if(def >= playeratk)
            dmg=1;
            else
            dmg = playeratk-def;

            cureentHp -=dmg;

            if(cureentHp <=0)
            {
                //이후 죽는 애니메이션 추가하기
                Destroy(this.gameObject);
                PlayerStat.instance.CurrentExp+= exp;
            }

        healthBarFilled.fillAmount = (float)cureentHp/ Hp;
        healthBarBackground.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(WaitCoroutine());


        return dmg;
    }


    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(3f);

        healthBarBackground.SetActive(false);
 

    }

}
