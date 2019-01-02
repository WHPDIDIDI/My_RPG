using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MyCanvas : MonoBehaviour {
    public Button stautsbutton;
    public Button bagbutton;
    public Button equbutton;
    public Button skillbutton;

    public GameObject stauts;
    public GameObject bag;
    public GameObject equ;
    public GameObject skill;
   

    // Use this for initialization
    void Start () {
        stautsbutton.onClick.AddListener(() => stauts.SetActive(true));
     //   bagbutton.onClick.AddListener(() => bag.SetActive(true));
        equbutton.onClick.AddListener(() => equ.SetActive(true));
        skillbutton.onClick.AddListener(()=> skill.SetActive(true));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
