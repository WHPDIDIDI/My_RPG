using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
public class StatusPanel : TTUIPage {
    public static Text Hp, Mp, Attack, Speed, Exp;
    public StatusPanel() : base(UIType.Fixed, UIMode.DoNothing, UICollider.None)
    {
        uiPath = "StatusPanel";
    }
    public override void Awake(GameObject go)
    {
        base.Awake(go);
        Hp = transform.GetChild(1).GetChild(0).GetComponent<Text>();
        Mp = transform.GetChild(2).GetChild(0).GetComponent<Text>();
        Attack = transform.GetChild(3).GetChild(0).GetComponent<Text>();
        Speed = transform.GetChild(4).GetChild(0).GetComponent<Text>();
        Exp = transform.GetChild(5).GetChild(0).GetComponent<Text>();
        RefreshNature();
    }
    public override void Refresh()
    {
        
        RefreshNature();
    }

    public static void  RefreshNature()
    {
        Save.RefreshUserList();
        Hp.text = Save.UserList[0].Hp.ToString();
        Mp.text = Save.UserList[0].Mp.ToString();
        Attack.text = Save.UserList[0].Attack.ToString();
        Speed.text = Save.UserList[0].Speed.ToString();
        Exp.text = Save.UserList[0].Exp.ToString();
        // 吃药的方法  使用物品后 属性改变
        Save.SaveNature();
    }
    //public void 

}
