using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase_Manager : MonoBehaviour
{


    //싱글톤
    static public DataBase_Manager instance;

#region Singleton
    private void Awake() {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance=this;
        }
        else Destroy(this.gameObject);
    }
#endregion
    // 씬 이동 에서 일어 날때 정보를 저장하기위한 작업 , 이미 true 로 작업이 되어있다면 그냥 기억을해준다
    //세이브와 로드시에도 필요
    //미리 만들어두면 편하다

    public string[] var_name;
    public float[] var;

    public string[] switch_name;
    public bool[] switchs;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
