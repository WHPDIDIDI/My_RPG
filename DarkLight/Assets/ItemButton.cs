using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System;
/// <summary>
/// 挂载在预设物上  
/// </summary>
public class ItemButton : MonoBehaviour,IPointerEnterHandler,IPointerClickHandler,IBeginDragHandler,IDragHandler {
    //当前物品的图片
    public static Sprite m_Sprite;
    //当前物品
    public GoodsModel CurrentGoods;
    //选中物品的Id
    public static int CurrentGoodsId;
    //物品信息显示框
    public static  GameObject ShowInfo;
    public static Text numText;
    GameObject canvas;
    static Item  item;
    static int Num;
    // Use this for initialization
    void Start () {
        m_Sprite = GetComponent<Image>().sprite;
        item = DataMgr.GetItemByID(int.Parse(m_Sprite.name));
        ShowInfo = GameObject.FindWithTag("ShowInfo");
        canvas = GameObject.Find("UIRoot");
        Num = Save.GoodsList1.Find((i) => { return i.Id == item.item_ID; }).Num;
    }
    /// <summary>
    /// 点击背包物品，显示物品信息
    /// </summary>
    public void  Show()
    {
        m_Sprite = GetComponent<Image>().sprite;
        ShowInfo.transform.GetChild(0).GetComponent<Image>().sprite = m_Sprite;
        ShowInfo.transform.GetChild(1).GetComponent<Text>().text = item.item_Name;
        ShowInfo.transform.GetChild(2).GetComponent<Text>().text = item.item_Type;
        CurrentGoodsId = item.item_ID;
        Vector3 WorldPos;
         if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.GetComponent<RectTransform>(),Input.mousePosition,canvas.transform.Find("UICamera").GetComponent<Camera>(),out WorldPos))
        {
            ShowInfo.transform.position = WorldPos;
        }
        ShowInfo.GetComponent<CanvasGroup>().alpha = 1;
        ItemButton.ShowInfo.GetComponent<CanvasGroup>().blocksRaycasts = true;
        ShowInfo.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Show();
    }
    /// <summary>
    /// 检测鼠标进入时，刷新物品信息
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item.item_ID.ToString()==m_Sprite.name)
        {
            return;
        }
        m_Sprite = GetComponent<Image>().sprite;
        item = DataMgr.GetItemByID(int.Parse(m_Sprite.name));
        Num = Save.GoodsList1.Find((i) => { return i.Id == item.item_ID; }).Num;//得到背包中当前物品数量
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {

        m_Sprite = GetComponent<Image>().sprite;
        Debug.Log(m_Sprite.name);
        FirstForge();
    }
    //锻造方法
    public static void FirstForge()
    {
       //判断锻造页面是否打开
        if (!GameObject.FindWithTag("ForgePanel"))
        {
            Debug.Log("你必须打开锻造炉");
            return;
        }
        //判断锻造炉中是否有物品
        if (ForgePanel.forDic.Count == 0)
        {
            ForgePanel.forDic.Add(m_Sprite.name, 1);
            ForgePanel.imageA.sprite = m_Sprite;
            ForgePanel.textA.text = ForgePanel.forDic[m_Sprite.name].ToString();
        }
        //叠加物品
        if ( ForgePanel.forDic.ContainsKey(m_Sprite.name)&& ForgePanel.forDic[m_Sprite.name]<Num)
        {
            ForgePanel.forDic[m_Sprite.name]++;
            if (ForgePanel.imageA.sprite == m_Sprite)
            {
                ForgePanel.textA.text = ForgePanel.forDic[m_Sprite.name].ToString();
            }
            else ForgePanel.textB.text = ForgePanel.forDic[m_Sprite.name].ToString();
        }
        //增加物品
        if (!ForgePanel.forDic.ContainsKey(m_Sprite.name) && ForgePanel.forDic.Count==1)
        {
            ForgePanel.forDic.Add(m_Sprite.name, 1);
            ForgePanel.imageB.sprite = m_Sprite;
            ForgePanel.textB.text = ForgePanel.forDic[m_Sprite.name].ToString();
        }
        //锻造炉已满
        if (!ForgePanel.forDic.ContainsKey(m_Sprite.name) && ForgePanel.forDic.Count == 2)
        {
            Debug.Log("不可再加入其他装备");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
       
    }

    
}
