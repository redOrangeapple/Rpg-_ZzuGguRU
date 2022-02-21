using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class Save_Load : MonoBehaviour
{
    [System.Serializable]
    public class Data {
        public float playerX;
        public float playerY;
        public float playerZ;

        public int playerLV;
        public int playerHp;
        public int playerMp;
        public int playerCurrentHp;
        public int playerCurrentMp;
        public int playerCurrentExp;

        public int playerhpr;
        public int playermpr;
        public int playerAtk;
        public int playerDef;
        public int playerAdded_atk;
        public int playerAdded_def;
        public int playerAdded_hpr;
        public int playerAdded_Mpr;

        //아이템의 ID값 정보를 이용하여 기억
        public List<int> playeriteminventory;
        public List<int> playeriteminventoryCount;
        public List<int> playerEquipitem;

        public string mapName;
        public string sceneName;
        public List<bool> swList;
        public List<string> swNameList;
        public List<string> varNameList;
        public List<float> varNumberList;
    }

    private Player_Manager thePlayer;
    private PlayerStat thePlayerStat;
    private DataBase_Manager theDatabase;
    private Inventory theinven;
    private Equipment theEquip;

    public Data theData;

    private Vector3 vector;

    private FadeManager theFade;


    public void CallSave()
    {
        theDatabase = FindObjectOfType<DataBase_Manager>();
        thePlayer = FindObjectOfType<Player_Manager>();
        thePlayerStat  =FindObjectOfType<PlayerStat>();
        theEquip = FindObjectOfType<Equipment>();
        theinven = FindObjectOfType<Inventory>();

        theData.playerX = thePlayer.transform.position.x;
        theData.playerY = thePlayer.transform.position.y;
        theData.playerZ = thePlayer.transform.position.z;

        theData.playerLV = thePlayerStat.Character_LV;
        theData.playerHp = thePlayerStat.Hp;
        theData.playerMp = thePlayerStat.Mp;
        theData.playerCurrentHp = thePlayerStat.CurrentHp;
        theData.playerCurrentMp = thePlayerStat.CurrentMp;
        theData.playerCurrentExp = thePlayerStat.CurrentExp;
        theData.playerAtk = thePlayerStat.atk;
        theData.playerDef = thePlayerStat.def;
        theData.playerhpr = thePlayerStat.recoverHP;
        theData.playermpr = thePlayerStat.recoverMp;
        theData.playerAdded_atk = theEquip.added_atk;
        theData.playerAdded_def = theEquip.added_def;
        theData.playerAdded_hpr = theEquip.added_hpr;
        theData.playerAdded_Mpr = theEquip.added_mpr;

        theData.mapName = thePlayer.currentMapName;
        theData.sceneName = thePlayer.currentSceneName;

        Debug.Log("기초 데이터 입력 성공");

        theData.playeriteminventory.Clear();
        theData.playeriteminventoryCount.Clear();
        theData.playerEquipitem.Clear();

        for(int i = 0 ; i < theDatabase.var_name.Length;i++)
        {
            theData.varNameList.Add(theDatabase.var_name[i]);
            theData.varNumberList.Add(theDatabase.var[i]);
        }

             for(int i = 0 ; i < theDatabase.switch_name.Length;i++)
        {
            theData.swNameList.Add(theDatabase.switch_name[i]);
            theData.swList.Add(theDatabase.switchs[i]);
        }


        List<Item> itemList = theinven.SaveItem();


        for(int i = 0 ; i < itemList.Count;i++)
        {
            Debug.Log("아이템 저장 완료  :  "+ itemList[i].itemID);
            theData.playeriteminventory.Add(itemList[i].itemID);
            theData.playeriteminventoryCount.Add(itemList[i].itemCount);

        }

        for(int i = 0 ; i < theEquip.equipitemList.Length;i++)
        {
            theData.playerEquipitem.Add(theEquip.equipitemList[i].itemID);
            Debug.Log("장착 된 아이템 저장 완료 : "+theEquip.equipitemList[i].itemID );
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/SaveFile.dat");
        bf.Serialize(file, theData);
        file.Close();

        Debug.Log(Application.dataPath+ " 의 위치에 저장했습니다");



    }

    public void CallLoad()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/SaveFile.dat",FileMode.Open);

        //파일이 존재할때만 접근 허용
        if(file !=null && file.Length >0)
        {
            theData = (Data)bf.Deserialize(file);

        theDatabase = FindObjectOfType<DataBase_Manager>();
        thePlayer = FindObjectOfType<Player_Manager>();
        thePlayerStat  =FindObjectOfType<PlayerStat>();
        theEquip = FindObjectOfType<Equipment>();
        theinven = FindObjectOfType<Inventory>();
        theFade = FindObjectOfType<FadeManager>();


        theFade.FadeOut();

        thePlayer.currentMapName = theData.mapName;
        thePlayer.currentSceneName = theData.sceneName;

        vector.Set(theData.playerX,theData.playerY,theData.playerZ);
        thePlayer.transform.position = vector;

        thePlayerStat.Character_LV = theData.playerLV;
        thePlayerStat.Hp= theData.playerHp;
        thePlayerStat.Mp = theData.playerMp;
        thePlayerStat.CurrentHp = theData.playerCurrentHp;
        thePlayerStat.CurrentMp = theData.playerCurrentMp;
        thePlayerStat.CurrentExp = theData.playerCurrentExp;
        thePlayerStat.atk =theData.playerAtk;
        thePlayerStat.def = theData.playerDef;
        thePlayerStat.recoverHP = theData.playerhpr;
        thePlayerStat.recoverMp = theData.playermpr;


        theEquip.added_atk = theData.playerAdded_atk;
        theEquip.added_def = theData.playerAdded_def;
        theEquip.added_hpr = theData.playerAdded_hpr;
        theEquip.added_mpr = theData.playerAdded_Mpr;


        theDatabase.var = theData.varNumberList.ToArray();
        theDatabase.var_name = theData.varNameList.ToArray();
        theDatabase.switchs = theData.swList.ToArray();
        theDatabase.switch_name = theData.swNameList.ToArray();


            for(int i = 0 ; i < theEquip.equipitemList.Length;i++)
            {
                for(int x= 0 ; x <theDatabase.itemList.Count;x++)
                {
                    if(theData.playerEquipitem[i] == theDatabase.itemList[x].itemID)
                    {
                        theEquip.equipitemList[i] = theDatabase.itemList[x];
                        Debug.Log("장착된 아이템을 로드했습니다 : "+ theEquip.equipitemList[i].itemID);
                        break;
                    }

                }

            }


            List<Item> itemList = new List<Item>();

            for(int i = 0 ; i < theData.playeriteminventory.Count;i++)
            {
                for(int x= 0 ; x <theDatabase.itemList.Count;x++)
                {
                    if(theData.playeriteminventory[i] == theDatabase.itemList[x].itemID)
                    {
                        itemList.Add(theDatabase.itemList[x]);
                        Debug.Log("인벤토리 아이템을 로드했습니다 : "+ theDatabase.itemList[x].itemID);
                        break;
                    }

                }

            }

                for(int i = 0 ; i < theData.playeriteminventoryCount.Count ; i++)
                {
                    itemList[i].itemCount = theData.playeriteminventoryCount[i];
                }

                theinven.LoadItem(itemList);
                theEquip.ShowText();

                StartCoroutine(WaitCoroutine());


        }

        else
        {
            Debug.Log("저장된 세이브 파일이 없습니다");
        }

         file.Close();

        //Debug.Log(Application.dataPath+ " 의 위치에 저장했습니다");
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(2f);
        GameManager theGm = FindObjectOfType<GameManager>();
            theGm.LoadStart();

            SceneManager.LoadScene(theData.sceneName);

    }
  
}
