using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class MsgPanel : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {
    public  GameObject XuanZhongXG;
    public Button BuyButton;
    public GameObject BuyTiShi;
    public static Transform msgPrefebs;
    Item item;
    private void Start()
    { 
        item = DataMgr.GetIntence().itemList.Find((a) => { return a.item_ID.ToString() == GetComponent<Image>().sprite.name; });
        msgPrefebs = GameObject.Find("UIRoot").transform.GetChild(1).GetChild(0).GetChild(9);
        BuyButton.onClick.AddListener(()=> {
             Save.BuyItem(item);
             BuyTiShi.GetComponent<Text>().color = new Color(1,1,1,1);
             BuyTiShi.GetComponent<Text>().DOFade(0,1);                 
        });
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        msgPrefebs.GetChild(1).GetComponent<Text>().text = item.item_Name;
        msgPrefebs.GetChild(2).GetComponent<Text>().text = item.description;
        msgPrefebs.GetChild(3).GetComponent<Text>().text = item.equipment_Type.ToString();
        msgPrefebs.GetChild(4).GetComponent<Text>().text = item.price.ToString();
        msgPrefebs.localPosition = Vector3.zero ;
        msgPrefebs.gameObject.SetActive(true);
        XuanZhongXG.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        XuanZhongXG.SetActive(false);
        msgPrefebs.gameObject.SetActive(false);
    }
}
