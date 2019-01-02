using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
using System.IO;
using LitJson;
using UnityEditor;
using UnityEngine.EventSystems;
public class MainPanel :  TTUIPage{
    public GameObject user;
    public GameObject bag;
    public GameObject equ;
    public GameObject skill;
    public static GameObject GoodsPrefab;
    public Button bagbutton, usebutton, backbutton, stautsbutton, equbutton, skillbutton,taskButton;
    public static Transform  Content;//背包空格
    public GameObject[] GridArray;

    public MainPanel() : base(UIType.Normal, UIMode.DoNothing, UICollider.Normal)
    {
        uiPath = "MainPanel";
    }
    public override void Awake(GameObject go)
    {
        GoodsPrefab = Resources.Load("MyGoods") as GameObject;
        bag = transform.Find("BagPanel").gameObject;
        
        skill = transform.Find("SkillPanel").gameObject;
        stautsbutton = transform.Find("StatusButton").GetComponent<Button>();
        bagbutton = transform.Find("BagButton").GetComponent<Button>();
        equbutton = transform.Find("EquipButton").GetComponent<Button>();
        skillbutton = transform.Find("SkillButton").GetComponent<Button>();
        taskButton = transform.Find("TaskButton").GetComponent<Button>();
        usebutton = transform.Find("ShowInfo").GetChild(3).GetComponent<Button>();
        backbutton = transform.Find("ShowInfo").GetChild(4).GetComponent<Button>();
        
        Content = bag.transform.GetChild(1);

        bagbutton.onClick.AddListener(BagBtnClick);
        usebutton.onClick.AddListener(() => { ShowInfo_UseGoods(); });
        backbutton.onClick.AddListener(ShowInfo_BackBtnClick);
        bool isuseropen = true;
        bool isequopen = true;
        bool isskillopen = true;
        bool istaskopen = true;
        stautsbutton.onClick.AddListener(() => {
            if (isuseropen)
            {
                TTUIPage.ShowPage<StatusPanel>();
            }
            else GameObject.FindWithTag("StatusPanel").SetActive(false);
            isuseropen = !isuseropen;

        });
        equbutton.onClick.AddListener(() => {
            if (isequopen)
            {
                TTUIPage.ShowPage<EquipPanel>();
            }
            else
             {
               GameObject.FindWithTag("EquipPanel").SetActive(false);
            } 
            isequopen = !isequopen;

        });
        taskButton.onClick.AddListener(() => {
            if (istaskopen)
            {
                TTUIPage.ShowPage<TaskPanel>();
            }
            else
            {
                GameObject.FindWithTag("TaskPanel").SetActive(false);
            }
            istaskopen = !istaskopen;

        });
        skillbutton.onClick.AddListener(() => {
            if (isskillopen)
            {
                skill.SetActive(true);
            }
            else skill.SetActive(false);
            isskillopen = !isskillopen;

        });
    }
   
    /// <summary>
    /// 点击返回按钮
    /// </summary>
    public void OnbackClick()
    {
        backbutton.gameObject.transform.parent.gameObject.SetActive(false);
    }

    bool isBag = false;
    public void BagBtnClick()
    {
        if (!isBag)
        {
            bag.gameObject.SetActive(true);
            ShowBag();
        }
        else
        {
            bag.gameObject.SetActive(false);
            if (ItemButton.ShowInfo)
            {
                ItemButton.ShowInfo.GetComponent<CanvasGroup>().alpha = 0;
                ItemButton.ShowInfo.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
        isBag = !isBag;

    }
    /// <summary>
    /// 显示背包数据
    /// </summary>
    public static void ShowBag()
    {

        //清除背包
        ClearBag();

        //遍历物品信息
        int j = 0;
        if (Save.GoodsList1==null)
        {
            return;
        }
        foreach (GoodsModel item in Save.GoodsList1)
        {
            // if (Save.SaveGoods.GoodsList[j].Num !=0)
            if (item.Num != 0)//物品数量不等于零时
            {
                //创建物品 
                GameObject go = Object.Instantiate(GoodsPrefab);
                go.transform.SetParent(Content.transform.GetChild(j));
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;
                //显示物体的图片及数量
                Sprite tempsprite = Resources.Load<Sprite>(item.Id.ToString());
                go.GetComponent<Image>().sprite = tempsprite;
                go.transform.GetChild(0).GetComponent<Text>().text = item.Num + "";
                j++;
            }
        }
    }
    /// <summary>
    /// 清除背包数据
    /// </summary>
    public static void ClearBag()
    {
        //删除之前创建物品的预设物
        for (int i = 0; i < Content.childCount; i++)
        {
            if (Content.GetChild(i).childCount != 0)
            {
                Transform temp = Content.GetChild(i).GetChild(0);
                temp.parent = null;
                Object.Destroy(temp.gameObject);
            }
        }
    }
    /// <summary>
    ///提示框的返回按钮
    /// </summary>
    public void ShowInfo_BackBtnClick()
    {
        ItemButton.ShowInfo.GetComponent<CanvasGroup>().alpha = 0;
        ItemButton.ShowInfo.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }
    /// <summary>
    /// 提示框中的使用物品按钮方法
    /// </summary>
    //当前使用的物品
    public GoodsModel CurrentGoods;
    public void ShowInfo_UseGoods()
    {
        GoodsModel goods = Save.GoodsList1.Find((i) => { return i.Id == ItemButton.CurrentGoodsId; });
        Item m_item = DataMgr.GetItemByID(ItemButton.CurrentGoodsId);
        int temp=  Save.WearItem(m_item);
        if (temp==0)
        {
            Debug.Log("此物品已穿戴");
            return;
        }
        if (goods.Num>0&&temp==1)
        {
            goods.Num--;
            Save.SaveBag();
        }
        if (goods.Num==0)
        {
             ItemButton.ShowInfo.GetComponent<CanvasGroup>().alpha = 0;
             ItemButton.ShowInfo.GetComponent<CanvasGroup>().blocksRaycasts = false;

        }
          ShowBag();
    }
    /// <summary>
    /// 点击保存按钮
    /// </summary>
    public void SaveBtnClick()
    {
        string path = Application.dataPath + @"/Resources/Setting/UserJson.txt";
        FileInfo info = new FileInfo(path);
        StreamWriter sw = info.CreateText();
        string json = JsonMapper.ToJson(Save.UserList);
        sw.Write(json);
        sw.Close();
        sw.Dispose();
        AssetDatabase.Refresh();

        string path1 = Application.dataPath + @"/Resources/Setting/GoodsList.json";
        FileInfo info1 = new FileInfo(path1);
        StreamWriter sw1 = info1.CreateText();
        string json1 = JsonMapper.ToJson(Save.SaveGoods);
        sw1.Write(json1);
        sw1.Close();
        sw1.Dispose();
        AssetDatabase.Refresh();
    }
}
