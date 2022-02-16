using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private DataBase_Manager theData;
    private OrderManager theOrder;
    private AudioManager theAudio;

    private Equipment theEquip;
    
    public string Enter_Sound;
    public string Cancle_Sound;
    public string Key_sound;
    public string open_Sound;
    public string beep_Sound;
    public string yummuSound;

    private Inventory_Slot[] slots;

    private List<Item> inventoryItemList; //  플레이어가 소지한 아이템 리스트
    private List<Item> inventoryTabList; //  플레이어가 텝 한 아이템 리스트 

    public Text Description_TEXT; // 부연설명
    public string[] tabDescription; // 텝 부연설명

    public Transform tf; // slot 부모 객체

    public GameObject go ; // 인벤토리 활성화 , 비활성화 역할 플래그
    public GameObject[] SelectedTabImages; // 카테고리 항목

    private Confirm_Cancel theCC;
    //public GameObject SeletctionWindow;

    private int selecteditem; // 선택된 아이템
    private int selected_Tab ; // 선택된 텝

    private bool activated ; // 인벤토리 활성화시 True;
    private bool tabActivated; // 탭 활성화 시 True;

    private bool itemActivated ; // 아이템  활성화시 true
    private bool stopKeyInput; // 키 입력제한 아이템 을 소비할때 키입력을 순간적으로 방지시킨다

    private bool PreventExec ;// 중복실행 방지 

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    public int invenFlag = 0;

    public GameObject CCC_flag;
    public GameObject prefab_floating_TEXT;


    void Start()
    {
        theData = FindObjectOfType<DataBase_Manager>();
        theOrder = FindObjectOfType<OrderManager>();
        theAudio = FindObjectOfType<AudioManager>();
        inventoryItemList =  new List<Item>();
        inventoryTabList = new List<Item>();
        slots = tf.GetComponentsInChildren<Inventory_Slot>();
        theCC = FindObjectOfType<Confirm_Cancel>();
        theEquip = FindObjectOfType<Equipment>();

        // inventoryItemList.Add(new Item(10001,"빨간포션","체력 50 회복",Item.ItemType.Use));
        // inventoryItemList.Add(new Item(10002,"파란포션","마나 50 회복",Item.ItemType.Use));
        // inventoryItemList.Add(new Item(10003,"고급 빨간포션","체력 350 회복",Item.ItemType.Use));
        // inventoryItemList.Add(new Item(10004,"고급 파란포션","마나 80 회복",Item.ItemType.Use));
        // inventoryItemList.Add(new Item(11001,"랜덤 상자","랜덤으로 포션 등장",Item.ItemType.Use));
        // inventoryItemList.Add(new Item(20001,"소검","초보자용 검",Item.ItemType.Equip));
        // inventoryItemList.Add(new Item(21001,"사파이어 반지","1분마다 마나 1 자동회복",Item.ItemType.Equip));
        // inventoryItemList.Add(new Item(30001,"누런 고대 청사진","고대유물 파편",Item.ItemType.Quest));
        // inventoryItemList.Add(new Item(30002,"파란 고대 청사진","고대유물 파편",Item.ItemType.Quest));
        // inventoryItemList.Add(new Item(30003,"고대 유물","고대유물 도감",Item.ItemType.ETC));
       // inventoryItemList.Add(new Item(99999,"고급장비","세이버의 혼이 담긴 칼",Item.ItemType.Equip));



    }

    public List<Item> SaveItem()
    {
        return inventoryItemList;

    }
    public void LoadItem(List<Item> _itemList)
    {
        inventoryItemList = _itemList;
    }




    public void Equip2Inventory(Item _item)
    {
        inventoryItemList.Add(_item);
    }

    // Update is called once per frame
    void Update()
    {
        if(!stopKeyInput)
        {
            //Debug.Log("뭔데시발");
            if(Input.GetKeyDown(KeyCode.I))
            {       
                 if(Input.GetKeyDown(KeyCode.Escape))
                StartCoroutine(CLoseInventotyCoroutin());
              //  Debug.Log("I 눌리면 나와");
                activated = true;
                
                if(activated)
                {
                    invenFlag++;
                    theAudio.Play(open_Sound);
                    theOrder.notMove();
                    go.SetActive(true); // 인벤토리 창 활성화
                    selected_Tab = 0 ;
                    tabActivated= true;
                    itemActivated = false;
                    ShowTab();
                

                }
                // if(Input.GetKeyDown(KeyCode.H))
                // {   
                //         Debug.Log("Escape key was pressed");
                //         theAudio.Play(Cancle_Sound);
                //         StopAllCoroutines();
                //         go.SetActive(false);
                //         tabActivated = false;
                //         itemActivated = false;
                //         theOrder.Move();   
                // }

            }

            if(activated)
            {   
                 if(Input.GetKeyDown(KeyCode.Escape))
                StartCoroutine(CLoseInventotyCoroutin());
                
                if(tabActivated) //tab 활성화시 키입력 처리
                {
                    if(Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if(selected_Tab < SelectedTabImages.Length-1)
                            selected_Tab++;
                            else 
                            selected_Tab = 0;

                            theAudio.Play(Key_sound);

                            SelectedTab();

                    }


                    else if(Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if(selected_Tab >0)
                            selected_Tab--;
                            else 
                            selected_Tab = SelectedTabImages.Length-1;

                            theAudio.Play(Key_sound);

                            SelectedTab();

                    }

                    else if(Input.GetKeyDown(KeyCode.Z)) // 결정키 
                    {
                        theAudio.Play(Enter_Sound);
                        Color color = SelectedTabImages[selected_Tab].GetComponent<Image>().color;
                        color.a=0.25f;
                        SelectedTabImages[selected_Tab].GetComponent<Image>().color = color;
                        itemActivated = true;
                        tabActivated =false;
                        PreventExec= true;

                        Debug.Log("아니 시발 색이 안바뀌는데?");
                        Debug.Log(selected_Tab);
                        
                        
                        

                        ShowItem();

                    }

                }

                else if(itemActivated) // item 활성화시 키입력
                {
                   
                    if(inventoryTabList.Count > 0)
                    {
                    if(Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        if(selecteditem < inventoryTabList.Count-2)
                        selecteditem+=2;
                        else
                        selecteditem %=2;

                        theAudio.Play(Key_sound);
                        Seleted_func_item();

                    }
                    else if(Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        if(selecteditem > 1)
                        selecteditem-=2;
                        else
                        selecteditem =inventoryTabList.Count-1-selecteditem;

                        theAudio.Play(Key_sound);
                        Seleted_func_item();
                    }

                    else if(Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        if(selecteditem < inventoryTabList.Count-1)
                        selecteditem++;
                        else selecteditem = 0;
                         theAudio.Play(Key_sound);
                        Seleted_func_item();

                    }

                    else if(Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        if(selecteditem >0)
                        selecteditem--;
                        else selecteditem = inventoryTabList.Count-1;
                         theAudio.Play(Key_sound);
                        Seleted_func_item();
                    }
            
                    else if(Input.GetKeyDown(KeyCode.Z) && !PreventExec)
                    {
            
                        if(selected_Tab ==0)  //소모품
                        {


                             StartCoroutine(CCCoroutine("사용","취소")); 
                             //물약을 마실지에 대한 선택지 호출 
                        }
                        else if(selected_Tab == 1) // 장비
                        {   
                              StartCoroutine(CCCoroutine("장착","취소")); 
                            //장비부분
                        }
                        else
                        {
                             theAudio.Play(beep_Sound);
                        }
                
                 
                  }
                    }  
                    if(Input.GetKeyDown(KeyCode.X))
                    {
                        theAudio.Play(Cancle_Sound);
                        StopAllCoroutines();
                        itemActivated = false;
                        tabActivated=true;
                        ShowTab();
                    }
                
            }
                if(Input.GetKeyUp(KeyCode.Z)) // 중복 실행 방지
                PreventExec = false;
            }
        }
        
    }

    public void GetItem(int _item_ID, int _count =1)
    {
        for(int i = 0 ; i <theData.itemList.Count;i++)
        {
            if(_item_ID == theData.itemList[i].itemID)
            {
                var clone = Instantiate(prefab_floating_TEXT,
                Player_Manager.instance.transform.position,Quaternion.Euler(Vector3.zero));

                clone.GetComponent<Floating_TEXT>().text.text = theData.itemList[i].itemName +" "+_count+"개 획득++";

                clone.transform.SetParent(this.transform);

                for(int j = 0 ; j < inventoryItemList.Count;j++)
                {
                    if(inventoryItemList[j].itemID == _item_ID)
                    {   
                        if(inventoryItemList[j].itemType == Item.ItemType.Use)
                        inventoryItemList[j].itemCount += _count;
                        return;
                    }
                    else
                    {   
                        for( int k=0 ; k<_count; k++)
                        inventoryItemList.Add(theData.itemList[i]);
                        return;

                    }

                }

                inventoryItemList.Add(theData.itemList[i]);
                inventoryItemList[inventoryItemList.Count-1].itemCount = _count;
                return;

            }

        }

        Debug.LogError("There is a no ItemID about that item");

    }
    public void ShowItem()
    {
         if(Input.GetKeyDown(KeyCode.Escape))
                StartCoroutine(CLoseInventotyCoroutin());

        inventoryTabList.Clear();
        RemoveSlots();
        selecteditem = 0 ;

        switch(selected_Tab)  // tab에 따른 아이템 분류 
        {

            case 0:
                for(int i = 0 ; i <inventoryItemList.Count;i++)
                {
                    if(Item.ItemType.Use == inventoryItemList[i].itemType)
                     inventoryTabList.Add(inventoryItemList[i]);

                }
            break;


            case 1:
                for(int i = 0 ; i <inventoryItemList.Count;i++)
                {
                    if(Item.ItemType.Equip == inventoryItemList[i].itemType)
                     inventoryTabList.Add(inventoryItemList[i]);

                }
            break;

            case 2:
                for(int i = 0 ; i <inventoryItemList.Count;i++)
                {
                    if(Item.ItemType.Quest == inventoryItemList[i].itemType)
                     inventoryTabList.Add(inventoryItemList[i]);

                }
            break;

            case 3: //기타
                for(int i = 0 ; i <inventoryItemList.Count;i++)
                {
                    if(Item.ItemType.ETC == inventoryItemList[i].itemType)
                     inventoryTabList.Add(inventoryItemList[i]);

                }
            break;



        }

        for(int i = 0; i < inventoryTabList.Count; i++) // 인벤토리 tab 리스트 내용을 인벤토리 슬롯에 추가
        {
            slots[i].gameObject.SetActive(true);
            slots[i].Additem(inventoryTabList[i]);

        }

        


    }
/// <summary>
/// 아이템 활성화 inventoryList 에 맞는 아이템들 모아주고 inventory slot 에 출력
/// </summary>
    public void Seleted_func_item()
    {

        Debug.Log("닝기미시뷰래");
         if(Input.GetKeyDown(KeyCode.Escape))
                StartCoroutine(CLoseInventotyCoroutin());
        StopAllCoroutines();

        if(inventoryTabList.Count >0)
        {   
            Color color = slots[0].selected_ITEM.gameObject.GetComponent<Image>().color;
            color.a = 0f;

            for(int i = 0 ; i < inventoryTabList.Count; i++)
                slots[i].selected_ITEM.GetComponent<Image>().color = color;

        
           Description_TEXT.text = inventoryTabList[selecteditem].itemDescription;

            StartCoroutine(SelectedItemEffectCoroutin());
        }
        else Description_TEXT.text = "해당타입의 아이템을 소유하고 있지 않습니다";
    }
/// <summary>
/// 선택된 아이템을 제외하고 다른 아이템 a 값을 0 으로 초기화
/// </summary>

    public void ShowTab()
    {
        RemoveSlots();
        SelectedTab();


    }
/// <summary>
/// Tab 활성화
/// </summary>

    public void RemoveSlots()
    {
         if(Input.GetKeyDown(KeyCode.Escape))
                StartCoroutine(CLoseInventotyCoroutin());
        for(int i = 0 ; i < slots.Length ; i++)
        {
            slots[i].RemovItem();
            slots[i].gameObject.SetActive(false);

        }

    }
/// <summary>
/// 인벤토리 슬롯 초기화
/// </summary>



    public void SelectedTab()
    {
         if(Input.GetKeyDown(KeyCode.Escape))
                StartCoroutine(CLoseInventotyCoroutin());
        StopAllCoroutines();
        Color color = SelectedTabImages[selected_Tab].GetComponent<Image>().color;
        color.a = 0f;

        for(int i = 0 ; i < SelectedTabImages.Length ; i++)
        {
         
                Debug.Log(selected_Tab);
                SelectedTabImages[i].GetComponent<Image>().color = color;      
        }
       // color.a = 0.5f;
        //SelectedTabImages[selected_Tab].GetComponent<Image>().color = color; 
        Description_TEXT.text = tabDescription[selected_Tab];

        StartCoroutine(SelectedTabEffectCoroutin());

    }
    /// <summary>
    ///  선택한 tab 을 제외하고 모든 tab a 값 0으로초기화
    /// </summary>
 
    IEnumerator SelectedTabEffectCoroutin()
    {
         if(Input.GetKeyDown(KeyCode.Escape))
                StartCoroutine(CLoseInventotyCoroutin());
        while(tabActivated)
        {
              Color color = SelectedTabImages[0].GetComponent<Image>().color;

              while(color.a < 0.5f)
              {
                  color.a += 0.03f;

                  SelectedTabImages[selected_Tab].GetComponent<Image>().color = color;
                  yield return waitTime;

              }

              while(color.a >0f)
              {
                  color.a -=0.03f; 
                  SelectedTabImages[selected_Tab].GetComponent<Image>().color = color;
                  yield return waitTime;

              }

              yield return new WaitForSeconds(0.3f);

        }

    }
    /// <summary>
    /// 선택한 tab 반짝여
    /// </summary>
    IEnumerator SelectedItemEffectCoroutin()
    {
        while(itemActivated)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
                StartCoroutine(CLoseInventotyCoroutin());

              Color color = slots[0].GetComponent<Image>().color;

              while(color.a < 0.5f)
              {
                  color.a += 0.03f;

                  slots[selecteditem].selected_ITEM.GetComponent<Image>().color = color;
                  yield return waitTime;

              }

              while(color.a >0f)
              {
                  color.a -=0.03f; 
                    slots[selecteditem].selected_ITEM.GetComponent<Image>().color = color;
                  yield return waitTime;

              }

              yield return new WaitForSeconds(0.3f);

        }

    }

    IEnumerator CLoseInventotyCoroutin()
    {
        
        Debug.Log("Escape key was pressed");
                theAudio.Play(Cancle_Sound);
                StopAllCoroutines();
                go.SetActive(false);
                tabActivated = false;
                itemActivated = false;
                theOrder.Move(); 

                yield return null;
    }

    IEnumerator CCCoroutine(string _Up,string _Down)
    {

        theAudio.Play(Enter_Sound);
        stopKeyInput =true;

        CCC_flag.SetActive(true);
        theCC.showChoice(_Up,_Down);
        stopKeyInput =true;

        yield return new WaitUntil (()=> !theCC.activated);

        if(theCC.GetResult())
        {
            for(int i = 0 ;  i <inventoryItemList.Count;i++)
            {
                if(inventoryItemList[i].itemID == inventoryTabList[selecteditem].itemID)
                {
                    if(selected_Tab == 0)
                    {
                        theData.UseItem(inventoryItemList[i].itemID);
                        if(inventoryItemList[i].itemCount>1)
                        inventoryItemList[i].itemCount--;
                        else
                        inventoryItemList.RemoveAt(i);

                        theAudio.Play(yummuSound);
                

                    ShowItem();
                    break;

                    }
                    else if(selected_Tab == 1)
                    {
                        theEquip.EquipItem(inventoryItemList[i]);
                        inventoryItemList.RemoveAt(i);
                        ShowItem();
                        break;

                    }


               
                }
            

            }

        }
        stopKeyInput =false;
        CCC_flag.SetActive(false);

    }

    /// <summary>
    /// 선택한 아이템 반짝여
    /// </summary>
}
