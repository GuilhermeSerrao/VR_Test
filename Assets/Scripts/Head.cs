using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{

    public Transform rootObject, followObject;
    public Vector3 positionOffset, rotationOffset, headBodyOffset;
    // Start is called before the first frame update
  

    // Update is called once per frame
    void LateUpdate()
    {
        rootObject.position = transform.position + headBodyOffset;
        rootObject.forward = Vector3.ProjectOnPlane(followObject.up, Vector3.up).normalized;

        transform.position = followObject.TransformPoint(positionOffset);
        transform.rotation = followObject.rotation * Quaternion.Euler(rotationOffset);
    }
}
