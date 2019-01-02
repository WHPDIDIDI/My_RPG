using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MyTask : MonoBehaviour {
   static  Button GiveUpButton, FinishButton;
   public static Text textName, textMsg;
    // Use this for initialization
    bool isok;
    Transform task2;
    private void Start()
    {
        GiveUpButton = transform.Find("GiveUpButton").GetComponent<Button>();
        FinishButton = transform.Find("FinishButton").GetComponent<Button>();

        GiveUpButton.onClick.AddListener(()=> { m_Destroy(0); });
        FinishButton.onClick.AddListener(() => { m_Destroy(1);  Debug.Log("获得奖励"); });

    }
    void Update () {
        if (!isok)
        {
             task2 = TaskPanel.Content.transform.Find("Task2");
            if (task2)
            {
                isok = !isok;
                Invoke("Temp",3);
            }
        }
    }
    private void m_Destroy(int a)
    {
        if (this.gameObject.name=="Task1")
        {
            TaskPanel.buttonTask1.interactable = true;
            TaskModel task = Save.TakeList.Find((b)=> {return b.TaskName == "RenWu1"; });
            if (task!=null)
            {
                Save.TakeList.Remove(task);
            }
            if (a==1)
            {
                TaskPanel.EnemyDic["Enemy1"] = TaskPanel.EnemyDic["Enemy1"] - 2;
                TaskPanel.EnemyDic["Enemy2"] = TaskPanel.EnemyDic["Enemy2"] - 2;
                if (TaskPanel.EnemyDic["Enemy1"] < 0)
                {
                    TaskPanel.EnemyDic["Enemy1"] = 0;
                }
                if (TaskPanel.EnemyDic["Enemy2"] < 0)
                {
                    TaskPanel.EnemyDic["Enemy2"] = 0;
                }
                task.enemy1 -= 2;
                task.enemy2 -= 2;
            }
           
        }                     
        if (this.gameObject.name == "Task2")
        {
            TaskPanel.buttonTask2.interactable = true;
            TaskModel task = Save.TakeList.Find((b) => { return b.TaskName == "RenWu2"; });
            if (task != null)
            {
                Save.TakeList.Remove(task);
            }
        }
        Save.SaveTask();
        Destroy(this.gameObject);
    }
    void Temp()
    {
        if (!task2)
        {
            return;
        } 
        FinishButton = task2.Find("FinishButton").GetComponent<Button>();
        FinishButton.transform.GetChild(0).GetComponent<Text>().text = "已完成";
        FinishButton.interactable = true;
    }
   static Transform tf =null;
    /// <summary>
    /// 刷新任务数据
    /// </summary>
    /// <param name="a"></param>
    public static void RefText(int a)
    {
        tf=  TaskPanel.Content.transform.Find("Task1");
        if (tf == null)
        {
            return;
        } 
        GiveUpButton = tf.Find("GiveUpButton").GetComponent<Button>();
        FinishButton = tf.Find("FinishButton").GetComponent<Button>();
        textName = tf.Find("NameText").GetComponent<Text>();
        textMsg = tf.Find("MsgText").GetComponent<Text>();

        if (a == 1)
        {
           TaskPanel.EnemyDic["Enemy1"]++;
            textMsg.text = "请杀死怪物1和怪物2各2个\n怪物1:" +TaskPanel.EnemyDic["Enemy1"] + "/2\n怪物2:" + TaskPanel.EnemyDic["Enemy2"] + "/2";

        }
        else
        {
           TaskPanel.EnemyDic["Enemy2"]++;
            textMsg.text = "请杀死怪物1和怪物2各2个\n怪物1:" +TaskPanel.EnemyDic["Enemy1"] + "/2\n怪物2:" + TaskPanel.EnemyDic["Enemy2"] + "/2";
        }
        if (TaskPanel.EnemyDic["Enemy1"] >= 2 && TaskPanel.EnemyDic["Enemy2"] >= 2)
        {
            FinishButton.transform.GetChild(0).GetComponent<Text>().text = "已完成";
            FinishButton.interactable = true;
        }
    }
      
}
