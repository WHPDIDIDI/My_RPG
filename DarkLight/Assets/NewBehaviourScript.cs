using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

        StartCoroutine(Test2());
        StartCoroutine(Test1());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    string s = "http://ww2.sinaimg.cn/large/bd759d6dly1fk9bw8cthuj20df0ezwf6.jpg";
    IEnumerator Test1()
    {
        WWW www = new WWW(s);
        yield return www;
        Debug.Log("1");
        GameObject o = GameObject.CreatePrimitive(PrimitiveType.Cube);
        o.GetComponent<MeshRenderer>().material.mainTexture = www.texture;
    }

    IEnumerator Test2()
    {
        yield return new WaitForSeconds(2);
        Debug.Log(2);
    }
}
