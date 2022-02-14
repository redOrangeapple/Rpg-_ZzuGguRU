using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment : MonoBehaviour
{
    private OrderManager theOrder;
    private AudioManager theAudio;
    private PlayerStat thePlayerstat;
    private Inventory theinven;

    public string Key_sound;
    public string enter_sound;
    public string open_Sound;
    public string close_sound;
    public string takeOff_sound;

    private const int WEAPON=0 , SHILED=1,AMULT=2,LEFT_RING=3,Right_RING=4
    , HELMET=5, ARMOR=6, LEFT_GLOVE=7,RIGHT_GLOVE=8,BELT=9,LEFT_BOOTS=10,Right_BOOTS=11;                                    

    public GameObject GO;

    public Text[] texts; // 스텟
    public Image[] img_slots; // 장비 가 들어갈 아이콘
    
    public GameObject selected_slots_UI; // 선택된 장비 슬롯 UI
    public Item[] equipitemList; // 장착된 장비 리스트
    private int selectedslots; // 선택된 장비 슬롯
    private bool activated=false;
    private bool inputKey=true;

    private Confirm_Cancel cc;

    public GameObject go_cc;
    void Start()
    {
        theinven = FindObjectOfType<Inventory>();
        theAudio = FindObjectOfType<AudioManager>();
        theOrder = FindObjectOfType<OrderManager>();
        thePlayerstat = FindObjectOfType<PlayerStat>();
        cc = FindObjectOfType<Confirm_Cancel>();
    }
    public void ClearEquip()
    {
        Color color =  img_slots[0].color;

        color.a = 0f;

        for(int i = 0 ; i < img_slots.Length;  i++ )
        {
            img_slots[i].sprite = null;
            img_slots[i].color =color;
        }
    }


    public void ShowEquip()
    {
        Color color = img_slots[0].color;
        color.a=1f;

        for(int i = 0 ; i <img_slots.Length;i++)
        {
            if(equipitemList[i].itemID!=0)
            {
                img_slots[i].sprite = equipitemList[i].itemIcon;
                img_slots[i].color = color;
            }

        }


    }
    public void EquipItem(Item _item)
    {
        string temp = _item.itemID.ToString();
        temp = temp.Substring(0,3);
        switch(temp)
        {
            case "200" : 
            EquipItemCheck(WEAPON, _item);

            break; // 무기
            case "201" :
            
            EquipItemCheck(SHILED,_item);

             break; // 방패
            case "202" :
             EquipItemCheck(AMULT,_item);
             break; //아뮬렛
            case "203" :
             EquipItemCheck(LEFT_RING,_item);
             break; //반지
        }

    }

    public void EquipItemCheck(int _count, Item _item)
    {
        if(equipitemList[_count].itemID ==0)
        equipitemList[_count] = _item;
        else 
        {
            theinven.Equip2Inventory(equipitemList[_count]);
            equipitemList[_count] = _item;

        }

    }

    public void Selected_Slot()
    {

        selected_slots_UI.transform.position = img_slots[selectedslots].transform.position;

    }
    // Update is called once per frame
    void Update()
    {
        if(inputKey)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                activated = !activated;
                if(activated)
                {
                    theOrder.notMove();
                    theAudio.Play(open_Sound);
                    GO.SetActive(true);
                    selectedslots=0;
                    Selected_Slot();
                    ClearEquip();
                    ShowEquip();
                }
                else 
                {
                    theOrder.Move();
                    theAudio.Play(close_sound);
                    GO.SetActive(false);
                    ClearEquip();
                }


            }


            if(activated)
            {
                if(Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if(selectedslots < img_slots.Length-1)
                        selectedslots++;
                        else
                        selectedslots=0;


                        Selected_Slot();
                          theAudio.Play(Key_sound);
                }

                else if(Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if(selectedslots>0)
                    selectedslots--;
                    else
                    selectedslots=img_slots.Length-1;

                    Selected_Slot();
                      theAudio.Play(Key_sound);
                }


                else  if(Input.GetKeyDown(KeyCode.RightArrow))
                {
                     if(selectedslots < img_slots.Length-1)
                        selectedslots++;
                        else
                        selectedslots=0;

                        Selected_Slot();
                          theAudio.Play(Key_sound);
                }

                else if(Input.GetKeyDown(KeyCode.LeftArrow))
                {
                        if(selectedslots>0)
                        selectedslots--;
                        else
                        selectedslots=img_slots.Length-1;
                        Selected_Slot();
                        theAudio.Play(Key_sound);
                }

                else  if(Input.GetKeyDown(KeyCode.Z))
                {
                    if(equipitemList[selectedslots].itemID!=0)
                    {
                    theAudio.Play(enter_sound);
                    inputKey = false;
                
                    StartCoroutine(CCCoroutine("벗기","취소"));
                    }
                }

              

            }

        }
        
    }






    IEnumerator CCCoroutine(string _Up,string _Down)
    {
        go_cc.SetActive(true);
        Debug.Log("여기 까지는 정상 출력 되나요");
        cc.showChoice(_Up,_Down);


        yield return new WaitUntil (()=> !cc.activated);

        if(cc.GetResult())
        {
               
            theinven.Equip2Inventory(equipitemList[selectedslots]);
            equipitemList[selectedslots] = new Item(0,"","",Item.ItemType.Equip);
            theAudio.Play(takeOff_sound);
            ClearEquip();
            ShowEquip();
        }
        inputKey = true;
        go_cc.SetActive(false);

    }

}
