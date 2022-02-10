using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory_Slot : MonoBehaviour
{
    public Image icon;
    public Text itemName_TEXT;
    public Text itemCount_TEXT;
    public GameObject selected_ITEM;
    
    public void Additem(Item _item)
    {   
        itemName_TEXT.text = _item.itemName;
        icon.sprite = _item.itemIcon;
        
        if(Item.ItemType.Use == _item.itemType)
        {
            if(_item.itemCount >0)
             itemCount_TEXT.text = "x "+ _item.itemCount.ToString();
             else itemCount_TEXT.text = ""; //없으면 공란으로 출력

        }

    }

    public void RemovItem()
    {
        itemName_TEXT.text ="";
        itemCount_TEXT.text ="";
        icon.sprite = null;
    }
    
}
