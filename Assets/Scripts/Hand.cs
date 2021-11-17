using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Animator))]

public class Hand : MonoBehaviour
{


    //animation
    Animator animator;
    //public float animationSpeed;
    //public string animatorGripParam = "Grip", animatorTriggerParam = "Trigger";
    //private float gripTarget;
    //private float triggerTarget;
    //private float gripCurrent, triggerCurrent;

    //physics movement
    public GameObject followObject;
    public float followSpeed = 30f;
    public float rotateSpeed = 100f;
    public Vector3 positionOffset;
    public Vector3 rotationOffset;

    Transform followTarget;
    Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();

        followTarget = followObject.transform;
        body = GetComponent<Rigidbody>();

        body.position = followTarget.position;
        body.rotation = followTarget.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //AnimateHand();
        PhysicsMove();
    }

    private void PhysicsMove()
    {
        //position
        var positionWithOffset = followTarget.TransformPoint(positionOffset);
        var distance = Vector3.Distance(positionWithOffset, transform.position);
        body.velocity = (positionWithOffset - transform.position).normalized * (followSpeed * distance);

        //rotation
        var rotationWithOffset = followTarget.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);
    }

//     internal void SetGrip(float v)
//     {
//         gripTarget = v;
//     }
// 
//     internal void SetTrigger(float v)
//     {
//         triggerTarget = v;
//     }
// 
//     void AnimateHand()
//     {
//         if (gripCurrent != gripTarget)
//         {
//             gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * animationSpeed);
//             animator.SetFloat(animatorGripParam, gripCurrent);
//         }
//         if (triggerCurrent != triggerTarget)
//         {
//             triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * animationSpeed);
//             animator.SetFloat(animatorTriggerParam, triggerCurrent);
//         }
//     }
}
