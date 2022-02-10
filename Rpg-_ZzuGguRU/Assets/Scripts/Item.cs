using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [System.Serializable]
public class Item
{
    public int itemID; // 아이템 고유값 중복 불가
    public string itemName; // 아이템 이름 , 중복가능
    public string itemDescription ; // 아이템 설명

    public int itemCount; // 아이템 소지 갯수

    public Sprite itemIcon; // 아이템 아이콘
    public ItemType itemType;


    // 아이템의 분류를 열거형으로 정리

    public enum ItemType
    {
        Use,
        Equip,
        Quest,
        ETC
    }

    public Item(int _itemID, string  _itemName,string _itemDes , ItemType _itemtype,int _itemCount=1)
    {
        itemID=_itemID ;
        itemName=_itemName ;
        itemDescription=_itemDes ;
        itemType= _itemtype;
        itemCount=_itemCount;

        itemIcon = Resources.Load("Item/"+ _itemID.ToString(),typeof(Sprite)) as Sprite;


    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
