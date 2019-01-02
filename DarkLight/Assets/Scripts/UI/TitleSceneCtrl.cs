    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TinyTeam.UI;
using UnityEngine.SceneManagement;

public class TitleSceneCtrl : MonoBehaviour {
    public Camera cam;//Main Camera
    public Transform targetPoint;//摄像机移动目标点

    // Use this for initialization
	void Start () {
        cam.transform.DOMove(targetPoint.transform.position,7);
        TTUIPage.ShowPage<TitlePanel>();

	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, 3 * Time.deltaTime);
        if (Input.anyKeyDown && Time.time > 5)
        {
            TitlePanel.imageAnyKey.gameObject.SetActive(false);
            TitlePanel.newgame.gameObject.SetActive(true);
            TitlePanel.loadgame.gameObject.SetActive(true);
        }
      
    }
}
