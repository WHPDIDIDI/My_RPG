using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class TitlePanel : TTUIPage {
    public Image imageTitle;
    public static Image imageAnyKey;
    public Image imageWhite;
  public static  Button newgame;
   public static Button loadgame;

    public TitlePanel() : base(UIType.Normal, UIMode.DoNothing, UICollider.None)
    {
        uiPath = "TitlePanel";
    }

    public override void Awake(GameObject go)
    {
        imageAnyKey = transform.Find("ImageAnyKey").GetComponent<Image>();
        imageTitle = transform.Find("ImageTitle").GetComponent<Image>();
        imageWhite = transform.Find("ImageBG").GetComponent<Image>();
        newgame = transform.Find("ButtonCreat").GetComponent<Button>();
        loadgame = transform.Find("ButtonLoad").GetComponent<Button>();

        newgame.gameObject.SetActive(false);
        loadgame.gameObject.SetActive(false);


        imageTitle.color = new Color(1, 1, 1, 0);
        imageAnyKey.gameObject.SetActive(false);
        imageWhite.DOFade(0, 5f).SetDelay(0.5f);

        imageTitle.DOFade(1, 1f).SetDelay(5f);
        imageAnyKey.DOFade(0, 1f).SetLoops(-1).SetDelay(5f).OnStart(() => imageAnyKey.gameObject.SetActive(true));

        //判断是否有存档
        if (!PlayerPrefs.HasKey("pName"))
        {
            loadgame.interactable = false;
        }
        newgame.onClick.AddListener(() => { SceneManager.LoadScene("Loading"); Globe.nextSceneName = "My Character Creation"; });
        loadgame.onClick.AddListener(() => { SceneManager.LoadScene("Loading");Globe.nextSceneName = "Dreamdev Village"; });
    }
}
public static class Test
{
    public static void LoadSceneByLoading(this SceneManager scene,string sceneName)
    {
        SceneManager.LoadScene("Loading");
        GameCtrl.Instance.nextSceneName = sceneName;
    }
}
