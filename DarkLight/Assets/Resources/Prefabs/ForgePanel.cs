using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
public class ForgePanel : TTUIPage {
    Button buttonok, buttonback;
    public static  Image imageA, imageB;
    public static Text textA, textB;
    public static Dictionary<string,int> forDic;
    public ForgePanel() : base(UIType.Fixed, UIMode.DoNothing, UICollider.None)
    {
        uiPath = "ForgePanel";
    }
    public override void Awake(GameObject go)
    {
        forDic = new Dictionary<string, int>();
        buttonok = transform.Find("ButtonOk").GetComponent<Button>();
        buttonback = transform.Find("ButtonBack").GetComponent<Button>();
        imageA = transform.Find("Grid00").GetComponent<Image>();
        imageB = transform.Find("Grid01").GetComponent<Image>();
        textA = transform.Find("TextA").GetComponent<Text>();
        textB = transform.Find("TextB").GetComponent<Text>();

        buttonok.onClick.AddListener(Forge);
        buttonback.onClick.AddListener(()=>go.SetActive(false));
    }
    public void Forge()
    {
        if (imageB.sprite==null|| imageA.sprite == null)
        {
             Debug.Log("不可锻造的类型");
             return;
        }
        ForgeGonfShi();
    }
    public void ForgeGonfShi()
    {
        Item item;
        if (forDic[imageA.sprite.name]==1&& forDic[imageB.sprite.name]==1)
        {
             item= DataMgr.GetItemByID(2004);
        }
        if (forDic[imageA.sprite.name] == 1 && forDic[imageB.sprite.name] == 2)
        {
            item = DataMgr.GetItemByID(2001);
        }
        if (forDic[imageA.sprite.name] == 2 && forDic[imageB.sprite.name] == 1)
        {
            item = DataMgr.GetItemByID(2002);
        }
        else
        {
             item = DataMgr.GetItemByID(2020);
        }  

        Save.GoodsList1.Remove(Save.GoodsList1.Find((i) => { return i.Id == int.Parse(imageB.sprite.name); }));
        Save.GoodsList1.Remove(Save.GoodsList1.Find((i) => { return i.Id == int.Parse(imageA.sprite.name); }));
        Save.BuyItem(item);
        MainPanel.ShowBag();
        imageB.sprite = null;
        imageA.sprite = null;
        forDic.Clear();
    }
}
