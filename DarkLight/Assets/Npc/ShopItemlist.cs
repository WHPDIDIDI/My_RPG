using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using TinyTeam.UI;

public class ShopItemlist : MonoBehaviour {
    public GameObject shop;
    public GameObject Prefab;
    DataMgr dataMgr;
    int MinID,MaxID;
    Transform itemParent;
    //public static event Action<bool> OnNpcTigger;//触发商店按钮
    public List<int> itemID = new List<int>();
	
	void Start()
	{
        dataMgr = DataMgr.GetIntence();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (shop==null)
        {
            shop = GameObject.Find("UIRoot").transform.GetChild(1).GetChild(0).GetChild(7).gameObject;
            itemParent = shop.transform.GetChild(2).GetChild(0).GetChild(0);
        }
        
        if (gameObject.name== "Weapon_Npc")
        {
            MinID = 2001;
            MaxID = 3002;
            ShoWshop(MinID, MaxID);
            shop.SetActive(true);
        }
        else if(gameObject.name == "Potion_Npc")
        {
            MinID = 1001;
            MaxID = 1003;
            ShoWshop(MinID, MaxID);
            shop.SetActive(true);
        }
        else if (gameObject.name == "Quest_NPC")
        {
            TTUIPage.ShowPage<ForgePanel>();
        }
              
    }
    private void OnTriggerExit(Collider other)
    {
         if (gameObject.name == "Quest_NPC")
        {
            if (!GameObject.FindWithTag("ForgePanel"))
            {
                return;
            }
            GameObject.FindWithTag("ForgePanel").SetActive(false);
            return;
        }
        shop.SetActive(false);
        MsgPanel.msgPrefebs.gameObject.SetActive(false);
        shop.transform.GetChild(2).GetChild(0).GetChild(0).DestroyChildren();
    }
    public void ShowTiShi()
    {
        //显示提示按钮
    }
    public void ShoWshop(int min,int max)
    {
        for (int i = 0; i < DataMgr.GetIntence().itemList.Count; i++)
        {
            if (DataMgr.GetIntence().itemList[i].item_ID >= min && DataMgr.GetIntence().itemList[i].item_ID <= max)//物品数量不等于零时
            {
                GameObject go = Instantiate(Prefab);
                
                go.transform.SetParent(itemParent);
                go.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(DataMgr.GetIntence().itemList[i].item_ID.ToString());
                go.transform.GetChild(2).GetComponent<Text>().text = DataMgr.GetIntence().itemList[i].item_Name;
                go.transform.GetChild(3).GetComponent<Text>().text = DataMgr.GetIntence().itemList[i].item_Type;
                go.transform.GetChild(4).GetComponent<Text>().text = DataMgr.GetIntence().itemList[i].price.ToString();
                go.transform.localScale = Vector3.one;
            }   
        }
    }
}


