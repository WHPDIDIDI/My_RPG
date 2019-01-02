using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
public class EquipPanel : TTUIPage {
     static Image  AccessImage, ShoeImage, LeftHandImage, RightHandImage, ArmorImage;
    public EquipPanel() : base(UIType.PopUp, UIMode.DoNothing, UICollider.None)
    {
        uiPath = "EquipPanel";
    }
    public override void Awake(GameObject go)
    {
        AccessImage = transform.Find("HeadImage").GetChild(0).GetComponent<Image>() ;
        ShoeImage = transform.Find("ShoeImage").GetChild(0).GetComponent<Image>();
        LeftHandImage = transform.Find("LeftHandImage").GetChild(0).GetComponent<Image>();
        RightHandImage = transform.Find("RightHandImage").GetChild(0).GetComponent<Image>();
        ArmorImage = transform.Find("ArmorImage").GetChild(0).GetComponent<Image>();
        Init();
    }
    public override void Refresh()
    {
        base.Refresh();
        Init();
    }
    public static void Wear(GoodsModel item)
    {
         Item _item = DataMgr.GetItemByID(item.Id);
        //ShowPage<EquipPanel>();
        switch (_item.equipment_Type)
        {
            case Equipment_Type.Accessory:
                  AccessImage.sprite = Resources.Load<Sprite>(_item.item_ID.ToString());
                break;

            case Equipment_Type.Armor:
                ArmorImage.sprite = Resources.Load<Sprite>(_item.item_ID.ToString());
                break;

              case Equipment_Type.Left_Hand:
                LeftHandImage.sprite = Resources.Load<Sprite>(_item.item_ID.ToString());
                break;

            case Equipment_Type.Right_Hand:
                RightHandImage.sprite = Resources.Load<Sprite>(_item.item_ID.ToString());
                break;

            case Equipment_Type.Shoes:
                ShoeImage.sprite = Resources.Load<Sprite>(_item.item_ID.ToString());
                break;

            case Equipment_Type.Two_Hand:
                LeftHandImage.sprite = Resources.Load<Sprite>(_item.item_ID.ToString());
                RightHandImage.sprite = Resources.Load<Sprite>(_item.item_ID.ToString());
                break;
        }
        
    }
    //读取数据
   public static void Init()
    {
        if (Save.WearList == null || Save.WearList.Count == 0)
        {
            //Sprite sprite = Resources.Load<Sprite>("Slot-layout");
            //AccessImage.sprite = sprite;
            //ShoeImage.sprite = sprite;
            //LeftHandImage.sprite = sprite;
            //RightHandImage.sprite = sprite;
            //ArmorImage.sprite = sprite;
            return;
        }
        foreach (var item in Save.WearList)
        {
            Wear(item);
        }
    }

}
