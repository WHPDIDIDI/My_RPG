using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class DataMgr
{
    public  List<Item> itemList = new List<Item>();
    private static DataMgr instence = null;
    private DataMgr()
    {
        TextAsset ta = Resources.Load("ItemData/Data") as TextAsset;
        itemList = JsonConvert.DeserializeObject<List<Item>>(ta.text);
        
       // Debug.Log(itemList.Count);
    }
    public static DataMgr GetIntence()
    {
         if (instence == null)
        {
            instence = new DataMgr();
        }
        return instence;
    }
    // Use this for initialization

    public static Item GetItemByID(int _id)
    {
        return GetIntence().itemList.Find((item) => { return item.item_ID == _id; });
    }
}
[System.Serializable]
public class Item
{
    public string item_Name = "Item Name";
    public string item_Type = "Item Type";
    [Multiline]
    public string description = "Description Here";
    public int item_ID;
    public string item_Img; //图片名字
    public string item_Effect;//特效名
    public string item_Sfx;//特效名
    public int item_Num;
    public bool gold;
    public Equipment_Type equipment_Type;
    public ClassType require_Class;
    public bool potion;

    public int price;
    public int hp, mp, atk, def, spd, hit;
    public float criPercent, atkSpd, atkRange, moveSpd;
}
public enum Equipment_Type
{
    Null = 0, Head_Gear = 1, Armor = 2, Shoes = 3, Accessory = 4, Left_Hand = 5, Right_Hand = 6, Two_Hand = 7
}