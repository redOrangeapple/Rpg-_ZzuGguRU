using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeletin_STAT : MonoBehaviour
{

    public int Hp;
    public int cureentHp;
    public int atk;
    public int def;
    public int exp;


    // Start is called before the first frame update
    void Start()
    {
        cureentHp = Hp;
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

        return dmg;
    }

}
