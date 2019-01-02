using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TinyTeam.UI;

public class PlayerMoveChar : MonoBehaviour {
    public GameObject effect1;
    public GameObject effect2;
    public int speed = 5;
    AnimatorStateInfo stateInfo;
    CharacterController m_characterController;
    Animator m_animator;
    int mHitCount=0;
    // Use this for initialization
    void Start () {
        m_characterController = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();
        stateInfo = m_animator.GetCurrentAnimatorStateInfo(0);
    }

    // Update is called once per frame
    void Update() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 v = new Vector3(-x, 0, -y);
        if (x != 0||y!=0)
        {
            m_animator.SetBool("isWalk", true); 
            m_characterController.SimpleMove(v*speed);
            if (!(Mathf.Abs(x) < 1 && Mathf.Abs(y) < 1))
            {
               
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(v), 0.5f);
            }
        }
        else
        {
            m_animator.SetBool("isWalk", false);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            m_animator.SetTrigger("YuKaSkill1");
           
         }
        if (Input.GetKeyDown(KeyCode.K))
        {
            m_animator.SetTrigger("YuKaSkill2");
            //mHitCount = 1;
            // if ( mHitCount == 1 && stateInfo.normalizedTime >0.65f&& Input.GetKeyDown(KeyCode.K))
            //{
            //    m_animator.SetBool("YuKaLianji", true);
            //}

        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            transform.position += transform.forward * 3;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_animator.SetTrigger("Jump");
        }

    }
    void MySkill1()
    {
        Camera.main.DOShakePosition(0.02f);
            GameObject o = Instantiate(effect1, transform.position + transform.forward * 3, Quaternion.identity);
           Collider[] temp = Physics.OverlapSphere(transform.position + transform.forward * 3,2,LayerMask.GetMask("Enemy"));
        if (temp.Length < 0) return;
        else
        {
            for (int i = 0; i < temp.Length; i++)
            {
                Destroy(temp[i].gameObject);
            }
        }
       

        
    }
    void MySkill2()
    {
        Camera.main.DOShakePosition(0.02f);
         GameObject o = Instantiate(effect2, transform.position , Quaternion.identity);
        Collider[] temp = Physics.OverlapSphere(transform.position, 2, LayerMask.GetMask("Enemy"));
        if (temp.Length < 0) return;
        else
        {
            for (int i = 0; i < temp.Length; i++)
            {
                Destroy(temp[i].gameObject);
            }
        }
       
    }
}
