using UnityEngine;
using System.Collections;
/// <summary>
/// 属性
/// </summary>
public class Nature : MonoBehaviour {
    public static Nature Instance;
    public int Hp, Mp, Attack, Speed,Exp;
    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
        AssigNature();// 给属性赋值
    }
    /// <summary>
    /// 给属性赋值  assig分配，任务，作业，功课
    /// </summary>
    void AssigNature()
    {    
      //  for (int i = 0; i < Save.SaveUser.UserList.Count; i++)
        {
            Hp = Save.UserList[0].Hp;
            Mp = Save.UserList[0].Mp;
            Attack = Save.UserList[0].Attack;
            Speed = Save.UserList[0].Speed;
            Exp = Save.UserList[0].Exp;
        }
    }
    /// <summary>
    /// 吃药的方法
    /// 使用物品后 属性改变
    /// </summary>
	public  void Eat()
    {
       // for (int i = 0; i < Save.SaveUser.UserList.Count; i++)
        {
            Save.UserList[0].Hp = Hp ;
            Save.UserList[0].Mp= Mp;
            Save.UserList[0].Attack = Attack;
            Save.UserList[0].Speed= Speed;
        }
    }
}
