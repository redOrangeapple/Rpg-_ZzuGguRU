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

    public List<Item> itemList = new List<Item>();
    
    public void UseItem(int _item_ID)
    {
        switch(_item_ID)
        {
            case 10001:
            Debug.Log("체력 50 회복");
            break;

            case 10002:
            Debug.Log("마나 15 회복");
            break;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        itemList.Add(new Item(10001,"빨간포션","체력 50 회복",Item.ItemType.Use));
        itemList.Add(new Item(10002,"파란포션","마나 50 회복",Item.ItemType.Use));
        itemList.Add(new Item(10003,"고급 빨간포션","체력 350 회복",Item.ItemType.Use));
        itemList.Add(new Item(10004,"고급 파란포션","마나 80 회복",Item.ItemType.Use));
        itemList.Add(new Item(11001,"랜덤 상자","랜덤으로 포션 등장",Item.ItemType.Use));
        itemList.Add(new Item(20001,"소검","초보자용 검",Item.ItemType.Equip));
        itemList.Add(new Item(21001,"사파이어 반지","1분마다 마나 1 자동회복",Item.ItemType.Equip));
        itemList.Add(new Item(30001,"누런 고대 청사진","고대유물 파편",Item.ItemType.Quest));
        itemList.Add(new Item(30002,"파란 고대 청사진","고대유물 파편",Item.ItemType.Quest));
        itemList.Add(new Item(30003,"고대 유물","고대유물 도감",Item.ItemType.Quest));
        itemList.Add(new Item(99999,"고급장비","세이버의 혼이 담긴 칼",Item.ItemType.Equip));
    }
}
