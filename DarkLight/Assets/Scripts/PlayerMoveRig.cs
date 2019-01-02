using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerMoveRig : MonoBehaviour {
    Vector3 targetRot;
    Animator myanimator;
    Rigidbody rig;
    public int speed;
    public GameObject eff;
	// Use this for initialization
	void Start () {
        myanimator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 v = new Vector3(-x, 0, -y);
        if (x!=0||y!=0)
        {
            myanimator.SetBool("isWalk", true);
            transform.position+=v*speed*Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(v), 0.5f);
        }
        else
        {
            myanimator.SetBool("isWalk", false);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            myanimator.SetTrigger("ManSkill1");
        }
    }
    void ManSkill1()
    {
            Instantiate(eff,transform.position+ transform.forward*3, Quaternion.identity);
             Camera.main.DOShakeRotation(0.02f);
    }
}
