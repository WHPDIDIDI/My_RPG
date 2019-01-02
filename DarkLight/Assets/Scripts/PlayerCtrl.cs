using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {
    public int speed =5;
    CharacterController m_characterController;
    RaycastHit hit;
    bool isWalk =false;
    Vector3 moveDir = Vector3.zero;
    // Use this for initialization
    void Start () {
      
        m_characterController = GetComponent<CharacterController>();
    }
   
  
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.GetPoint(1000));
            if (Physics.Raycast(ray, out hit, 200, LayerMask.GetMask("Ground")))
            {
                 Vector3 targetPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                moveDir = targetPos - transform.position;
                Instantiate(GameSetting.Instance.mousefxNormal, hit.point, Quaternion.identity);
                //transform.LookAt(targetPos);
               
               
            }
            isWalk = true;
        }
        if (isWalk)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), 0.5f);
            m_characterController.SimpleMove(moveDir.normalized*speed);
            if (Vector3.Distance(hit.point,transform.position) <=0.5)
            {
                isWalk = false;
            }
        }
      
    }

    //void MyClickToWMove()
    //{

    //    //transform.LookAt(targettran.transform.position);
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        //Debug.Log(Input.mousePosition);
    //        //Vector3 wordpos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0.3f   ));
    //        //GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
    //        //go.transform.position = wordpos;
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        Debug.DrawRay(ray.origin, ray.GetPoint(1000));
    //        if (Physics.Raycast(ray, out hit, 200, LayerMask.GetMask("Ground")))
    //        {
    //            GameObject o = Instantiate(GameSetting.Instance.mousefxNormal, hit.point, Quaternion.identity);
    //            targettranform = o.transform;
    //        }
    //    }

    //}
}
