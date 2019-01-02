using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
public class TaskPanel : TTUIPage {
   public static GameObject Content;
  public static  Button buttonTask1, buttonTask2, buttonEnemy1, buttonEmemy2;
    Text textName, textMsg;
    GameObject task;
   public static Dictionary<string ,int> EnemyDic;

    public TaskPanel() : base(UIType.Fixed, UIMode.DoNothing, UICollider.None)
    {
        uiPath = "TaskPanel";
    }
    public override void Awake(GameObject go)
    {
        EnemyDic = new Dictionary<string, int>();
        EnemyDic.Add("Enemy1", 0);
        EnemyDic.Add("Enemy2", 0);
        base.Awake(go);
        task = Resources.Load("MyTask") as GameObject;
        Content = transform.Find("Scroll View/Viewport/Content").gameObject;
        buttonTask1 = transform.Find("ButtonTask1").GetComponent<Button>();
        buttonTask2 = transform.Find("ButtonTask2").GetComponent<Button>();
        buttonEnemy1 = transform.Find("ButtonEnemy1").GetComponent<Button>();
        buttonEmemy2 = transform.Find("ButtonEnemy2").GetComponent<Button>();

        buttonTask1.onClick.AddListener(()=> { GetTask(1,1); buttonTask1.interactable = false; });
        buttonTask2.onClick.AddListener(() => { GetTask(2,1); buttonTask2.interactable = false; });
        buttonEnemy1.onClick.AddListener(() => { KillEnemy(1,1);});
        buttonEmemy2.onClick.AddListener(() => { KillEnemy(2,1); });
        //读取任务数据（按照数据接受任务）
        if (Save.TakeList!=null)
        {
            foreach (var item in Save.TakeList)
            {
                if (item.TaskName=="RenWu1")
                {
                    GetTask(1,0);
                    for (int i = 0; i < item.enemy1; i++)
                    {
                        KillEnemy(1,0);
                    }
                    for (int i = 0; i < item.enemy2; i++)
                    {
                        KillEnemy(2,0);
                    }

                }
                if (item.TaskName == "RenWu2")
                {
                    GetTask(2,0);
                }
            }
        }
    }
    /// <summary>
    /// 接受任务
    /// </summary>
    /// <param name="a">任务编号</param>
    /// <param name="b">1为接受任务需要存档，0为读取数据不需要存档</param>
    public void GetTask(int a,int b)
    {

        if (Save.TakeList==null)
        {
            Save.TakeList = new List<TaskModel>();
        }
        GameObject go= null;
        if (a==1)
        {
            go = GameObject.Instantiate(task);
            go.transform.SetParent(Content.transform);
            go.name = "Task1";
            textName = go.transform.Find("NameText").GetComponent<Text>();
            textMsg = go.transform.Find("MsgText").GetComponent<Text>();
            textName.text = "任务1";
            textMsg.text = "请杀死怪物1和怪物2各2个\n怪物1:"+EnemyDic["Enemy1"]+"/2\n怪物2:"+ EnemyDic["Enemy2"]+"/2";
            if (b==1) { Save.TakeList.Add(new TaskModel() { TaskName = "RenWu1", enemy1 = EnemyDic["Enemy1"], enemy2= EnemyDic["Enemy2"] }); }
        }
        else if(a==2)
        {
                go = GameObject.Instantiate(task);
                go.transform.SetParent(Content.transform);
                go.name = "Task2";
                textName = go.transform.Find("NameText").GetComponent<Text>();
                textMsg = go.transform.Find("MsgText").GetComponent<Text>();
                textName.text = "任务2";
                textMsg.text = "累计在线时长3秒";
            if (b==1)
            {
                Save.TakeList.Add(new TaskModel() { TaskName = "RenWu2", enemy1 = 0, enemy2 = 0 });
            }
                
        }
        go.transform.localScale = Vector3.one;
        Save.SaveTask();

    }
    /// <summary>
    /// 获得任务材料
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    public void KillEnemy(int a,int b)
    {
        if (b==1)
        {
            TaskModel task = Save.TakeList.Find((x) => { return x.TaskName == "RenWu1"; });
            if (task != null)
            {
                if (a == 1)
                {
                    task.enemy1++;
                }
                if (a == 2)
                {
                    task.enemy2++;
                }
            }
        }
       
        MyTask.RefText(a);//刷新任务
        Save.SaveTask();//保存数据
    }
}
