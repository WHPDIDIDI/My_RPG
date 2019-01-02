using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TinyTeam.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class CrearPlaterPanel : TTUIPage {
    Button buttonNext;//下一个人物
    Button buttonPrev;//上一个人物
    Button buttonOk;//确定
    Button buttonRandom;//随机筛子
    InputField enterNameArea;//昵称输入框

    //GameObject magic;//人物法师
    //GameObject jianshi;//人物剑士
    public string[] xings = { "尼古拉斯", "德玛西亚·", "约德尔·" ,"贠"};
    public string[] mings = { "爱", "风", "格和", "气氛", "就申", "达股", "份后", "来还", "十", "多个", "好好", "看看", "预算", "大V" };
    

    public GameObject[] hero;  //your hero
    public int indexHero = 0;  //index select hero
    private GameObject[] heroInstance; //use to keep hero gameobject when Instantiate
    public CrearPlaterPanel() : base(UIType.Normal, UIMode.DoNothing, UICollider.None)
    {
          uiPath = "CrearPlaterPanel";
    }
    public override void Awake(GameObject go)
    {
        //初始化可交互UI
        buttonNext = transform.Find("ButtonNext").GetComponent<Button>();
        buttonPrev = transform.Find("ButtonPrev").GetComponent<Button>();
        buttonOk = transform.Find("ButtonOk").GetComponent<Button>();
        buttonRandom = transform.Find("ButtonRandom").GetComponent<Button>();
        enterNameArea = transform.Find("EnterNameArea").GetComponent<InputField>();

        //magic = GameObject.Find("Magician-Idle").gameObject;
        // jianshi = GameObject.Find("Swordman-Idle").gameObject;
        //jianshi.SetActive(false);

        //点击ok事件
        buttonOk.onClick.AddListener(() =>
        {

            PlayerPrefs.SetString("pName",enterNameArea.text);
            PlayerPrefs.SetInt("pSelet",indexHero);
            SceneManager.LoadScene("Loading");
            Globe.nextSceneName = "My Dreamdev Village";
        });


        //筛子的旋转
        bool isRotate =false;
        buttonRandom.onClick.AddListener(()=> {
            isRotate = !isRotate;
             int rot;
            if (isRotate)
            {
                rot = 180;
            }
            else
            {
                rot = 360;
            }
            buttonRandom.transform.DORotate(Vector3.forward*rot,0.5f);
            RandomName();
        });
        //下一个

        buttonNext.onClick.AddListener(() => {
            indexHero++;
            if (indexHero >= heroInstance.Length)
            {
                indexHero = 0;
            }
            UpdateHero(indexHero);
         });
        //上一个
         buttonPrev.onClick.AddListener(() =>{
            indexHero--;
            if (indexHero <= heroInstance.Length)
            {
                indexHero = heroInstance.Length-1;
            }
            UpdateHero(indexHero);
        });


        hero = Resources.LoadAll<GameObject>("Heros");//加载指定路径下所有物体
        heroInstance = new GameObject[hero.Length]; //add array size equal hero size
        indexHero = 0; //set default selected hero
        SpawnHero(); //spawn hero to display current selected


        //check if hero is less than 1 , button next and prev will disappear
        if (hero.Length <= 1)
        {
            buttonNext.gameObject.SetActive(false);
            buttonPrev.gameObject.SetActive(false);
        }
    }
    //public void ChangePeople()
    //{
    //    if (magic.active)
    //    {
    //        magic.active =false;
    //        jianshi.active = true;
    //        return;
    //    }
    //    magic.active = true;
    //    jianshi.active = false;

    //}

    public void UpdateHero(int _indexHero)
    {
        for (int i = 0; i < hero.Length; i++)
        {
            //Show only select character
            if (i == _indexHero)
            {
                heroInstance[i].SetActive(true);
            }
            else
            {
                //Hide Other Character
                heroInstance[i].SetActive(false);
            }
        }
    }
    public void SpawnHero()
    {
        for (int i = 0; i < hero.Length; i++)
        {
             heroInstance[i] = Object.Instantiate(hero[i], transform.position, transform.rotation);
            heroInstance[i].transform.position = Vector3.zero;
            heroInstance[i].transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }

        UpdateHero(indexHero);
    }

    public void RandomName()
    {
       string xing =xings[Random.Range(0,4)];
       string ming = mings[Random.Range(0, 14)];
        enterNameArea.text = xing + ming;
    }
}
