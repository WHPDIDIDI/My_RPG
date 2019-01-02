using UnityEngine;
using System.Collections;
using LitJson;
using Newtonsoft.Json;
using System.Collections.Generic;
/// <summary>
/// 解析数据
/// </summary>
public class Analysis : MonoBehaviour {
    //public GameObject cavas;
	void Awake () {        
        // 用户数据解析
        //UserAnalysis();
        // 物品数据解析
        GoodsAnalysis();
        //穿戴装备解析
        WearAnalysis();
        //任务数据解析
        TaskAnalysis();
    }
    /// <summary>
    /// 用户数据解析
    /// </summary>
	void UserAnalysis()
    {
        TextAsset u = Resources.Load("Setting/UserJson") as TextAsset;
        if (!u)
        {
            return;
        }
        Save.UserList = JsonConvert.DeserializeObject<List<UserModel>>(u.text);
        if (Save.UserList==null)
        {
            Save.UserList = new List<UserModel>();
            Save.UserList.Add(new UserModel());
        }
    }

    /// <summary>
    /// 物品数据解析
    /// </summary>
    //public static List<GoodsModel> BagList;
    void GoodsAnalysis()
    {
        TextAsset g = Resources.Load("Setting/GoodsList 1") as TextAsset;
        if (!g)
        {
            Debug.Log("GoodsList 1 不存在");
            return;
        }
        //Save.SaveGoods = JsonMapper.ToObject<GoodsModelList>(g.text);
         Save.GoodsList1 = JsonConvert.DeserializeObject<List<GoodsModel>>(g.text);
        //print(g.text);
    }
    /// <summary>
    /// 穿戴数据解析
    /// </summary>
    void WearAnalysis()
    {
        TextAsset w = Resources.Load("Setting/WearList") as TextAsset;
        if (!w)
        {
            Debug.Log("WearList 不存在");
            return;
        }   
        Save.WearList = JsonConvert.DeserializeObject<List<GoodsModel>>(w.text);
    }
    /// <summary>
    /// 任务数据解析
    /// </summary>
    void TaskAnalysis()
    {
        TextAsset w = Resources.Load("Setting/TaskList") as TextAsset;
        if (!w)
        {
            Debug.Log("TaskList 不存在");
            return;
        }
        Save.TakeList = JsonConvert.DeserializeObject<List<TaskModel>>(w.text);
    }
}
