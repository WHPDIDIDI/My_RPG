using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NEW : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{ 
    GameObject obj;
    public void OnBeginDrag(PointerEventData eventData)
    {
        obj = new GameObject("Temp");
        obj.AddComponent<Image>().sprite = GetComponent<Image>().sprite;
        obj.transform.SetParent(GameObject.Find("Canvas").transform);
        var group = obj.AddComponent<CanvasGroup>();
        group.blocksRaycasts = false;
    }
   
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out globalMousePos))
        {
            obj.GetComponent<RectTransform>().position = globalMousePos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (!eventData.pointerEnter)
        {
            Destroy(obj);
            return;
        }
        GameObject data = eventData.pointerEnter;
        if (data.name== "Viewport"|| data.name == "Scroll View")
        {
            Destroy(obj);
            return;
        }
        if (!data.GetComponent<Image>())
        {
            return;
        }

        if (!data.GetComponent<Image>().sprite)
        {
            data.GetComponent<Image>().sprite = obj.GetComponent<Image>().sprite;
            transform.GetComponent<Image>().sprite = null;
        }
        else
        {
            Sprite tempsprite = data.GetComponent<Image>().sprite;
            data.GetComponent<Image>().sprite = obj.GetComponent<Image>().sprite;
            transform.GetComponent<Image>().sprite = tempsprite;
        }
        Destroy(obj);
    }
   
}
       
    


