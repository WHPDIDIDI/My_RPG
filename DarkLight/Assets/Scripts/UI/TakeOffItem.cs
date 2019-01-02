using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class TakeOffItem : MonoBehaviour {

    Sprite sprite;
    public Button buttonOk ,buttonBack;
    public Button button1, button2, button3,button4,button5,button6,button7; 
    public GameObject takeOff;
    Button tempbutton;
	// Use this for initialization
	void Start () {


        action += MyRefrace;
        buttonBack.onClick.AddListener(()=> { takeOff.SetActive(false); });
        buttonOk.onClick.AddListener(() => {
            Test(DataMgr.GetItemByID(int.Parse(sprite.name)));
            action(tempbutton);
            takeOff.SetActive(false);
        });



        button1.onClick.AddListener(() =>
        {
            takeOff.SetActive(true);
            tempbutton = button1;
            sprite = button1.gameObject.GetComponent<Image>().sprite;
        });
        button2.onClick.AddListener(() =>
        {
            tempbutton = button2;
            takeOff.SetActive(true);
            sprite = button2.gameObject.GetComponent<Image>().sprite;
        });
        button3.onClick.AddListener(() =>
        {
            tempbutton = button3;
            takeOff.SetActive(true);
            sprite = button3.gameObject.GetComponent<Image>().sprite;
        });
        button4.onClick.AddListener(() =>
        {
            tempbutton = button4;
            takeOff.SetActive(true);
            sprite = button4.gameObject.GetComponent<Image>().sprite;
        });
        button5.onClick.AddListener(() =>
        {
            tempbutton = button5;
            takeOff.SetActive(true);
            sprite = button5.gameObject.GetComponent<Image>().sprite;
        });
        button6.onClick.AddListener(() =>
        {
            tempbutton = button6;
            takeOff.SetActive(true);
            sprite = button6.gameObject.GetComponent<Image>().sprite;
        });
       
    }
    public void MyRefrace(Button button)
    {
        button.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Slot-layout");
    }
    public void Test(Item item)
    {
        
        Save.TakeOff(item);
       
    }
    public Action<Button> action;
}
