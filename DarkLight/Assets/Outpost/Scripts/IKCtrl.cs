using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKCtrl : MonoBehaviour {
    Animator m_Animator;
   public  Transform rightHandObj;
   public Transform  leftHandObj;
    public Transform rightLegObj;
    public Transform leftLegObj;

    public Transform lookObj;
   public bool isActive;
    // Use this for initialization
    void Start () {
        m_Animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnAnimatorIK(int layerIndex)
    {
        if (m_Animator)
        {
            if (isActive)
            {
                if (lookObj)
                {
                    m_Animator.SetLookAtWeight(1);
                    m_Animator.SetLookAtPosition(lookObj.position);
                }

                if (rightHandObj)
                {
                    m_Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    m_Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    m_Animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    m_Animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                }
                if (leftHandObj)
                {
                    m_Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    m_Animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                    m_Animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                    m_Animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
                }
                if (rightLegObj)
                {
                    m_Animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
                    m_Animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
                    m_Animator.SetIKPosition(AvatarIKGoal.RightFoot, rightLegObj.position);
                    m_Animator.SetIKRotation(AvatarIKGoal.RightFoot, rightLegObj.rotation);
                }
                if (leftLegObj)
                {
                    m_Animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
                    m_Animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
                    m_Animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftLegObj.position);
                    m_Animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftLegObj.rotation);
                }
            }
            else
            {
                m_Animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
                m_Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                m_Animator.SetLookAtWeight(0);
            }
        }
    }
}
