using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;

public class UserModel {
    /*  {"UserList":[{"Hp":80,"MaxHp":120,"Attack":35,"Speed":25}]}  */
    public int Hp;
    public int Mp;
    public int Attack;
    public int Speed;
    public int Exp;
}
//public  class UserModelList
//{
//    public List<UserModel> UserList = new List<UserModel>();
//}

public class GoodsModel //商品信息
{
    public int Id;
    //public string Name;
    //public string Nature;//图片种类(图片名)
    //public string Function;
    //public int Value;//值
    public int Num;//数量
}
public class GoodsModelList
{
    public List<GoodsModel> GoodsList;
}
public class TaskModel //商品信息
{
    public string TaskName;
    public int enemy1;
    public int enemy2;
}

public  class Save
{
  //  public static UserModelList SaveUser;
    public static GoodsModelList SaveGoods;
    private static List<GoodsModel> GoodsList ;
    public static List<UserModel> UserList;
    private static List<GoodsModel> wearList;
    public static List<TaskModel> TakeList;
    public static List<GoodsModel> GoodsList1
    {
        get
        {
            return GoodsList;
        }

        set
        {
            GoodsList = value;
        }
    }

    public static List<GoodsModel> WearList
    {
        get
        {
            return wearList;
        }

        set
        {
            wearList = value;
        }
    }
    //购买物品
    public static void BuyItem(Item _item)
    {
        if (GoodsList1==null)
        {
            GoodsList1 = new List<GoodsModel>();
        }
        GoodsModel gm = Save.GoodsList1.Find(x => x.Id == _item.item_ID);
        if (gm != null)
        {
            gm.Num++;
        }
        else
        {
            Save.GoodsList1.Add(new GoodsModel() { Id = _item.item_ID, Num = 1 });
        }
        MainPanel.ShowBag();
         SaveBag();
    }
    //保存背包
    public static void SaveBag()
    {
        string path = Application.dataPath + @"/Resources/Setting/GoodsList 1.json";
        using (StreamWriter sw = new StreamWriter(path))
        {
           string json = JsonConvert.SerializeObject(Save.GoodsList1);
            sw.Write(json);
        }
    }
    //穿戴物品
    public static int WearItem(Item _item)
    {
        if (WearList == null)
        {
            WearList = new List<GoodsModel>();
        }
        GoodsModel gm = WearList.Find(x => x.Id == _item.item_ID);
        if (gm != null)
        {
            return 0;
        }
        else
        {
           
        }
        //遍历已穿戴的装备类型  如果有就替换，没有就Add
        for (int i = 0; i < WearList.Count; i++)
        {
            Item item = DataMgr.GetItemByID(WearList[i].Id);
            if (item.equipment_Type == _item.equipment_Type)
            {
                BuyItem(item); 
                WearList.Remove(WearList[i]);
            }
        }
        WearList.Add(new GoodsModel() { Id = _item.item_ID, Num = 1 });
        SaveWear();
        RefreshUserList();
        return 1;
    }
    //保存穿戴物品
    private static void SaveWear()
    {
        string path = Application.dataPath + @"/Resources/Setting/WearList.json";
        using (StreamWriter sw = new StreamWriter(path))
        {
            string json = JsonConvert.SerializeObject(Save.WearList);
            sw.Write(json);
        }
    }
    public static void RefreshUserList()
    {  
            UserList = new List<UserModel>();
            UserList.Add(new UserModel());
        foreach (var item in WearList)
        {
            Item M_item= DataMgr.GetItemByID(item.Id);
            UserList[0].Hp += M_item.hp;
            UserList[0].Mp += M_item.mp;
            UserList[0].Attack += M_item.atk;
            UserList[0].Speed += M_item.spd;
        }
        SaveNature();
    }
    public static void SaveNature()
    {
        string path = Application.dataPath + @"/Resources/Setting/UserJson.json";
        using (StreamWriter sw =new StreamWriter(path))
        {
           string json = JsonConvert.SerializeObject(UserList);
            sw.Write(json);
        }
    }
    public static void TakeOff(Item item)
    {
        GoodsModel gm = WearList.Find(x => x.Id == item.item_ID);
        WearList.Remove(gm);
        BuyItem(item);
        SaveWear();
        EquipPanel.Init();

    }
    public static void SaveTask()
    {
        string path = Application.dataPath + @"/Resources/Setting/TaskList.json";
        using (StreamWriter sw = new StreamWriter(path))
        {
            string json = JsonConvert.SerializeObject(Save.TakeList);
            sw.Write(json);
        }
    }
}    

