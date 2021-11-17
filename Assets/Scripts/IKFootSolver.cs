using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootSolver : MonoBehaviour
{

    public LayerMask terrainLayer;
    public Transform body;
    public IKFootSolver otherFoot;
    public float speed = 5, stepDistance = 0.3f, stepLenght = 0.3f, stepheight = 0.3f;
    public Vector3 footPosOffset, footRotOffset;

    private float footSpacing, lerpValue;
    private Vector3 oldPos, currentPos, newPos;
    private Vector3 oldNorm, currentNorm, newNorm;
    private bool isFirstStep = true;
    // Start is called before the first frame update
    void Start()
    {
        footSpacing = transform.localPosition.x;
        currentPos = newPos = oldPos = transform.position;
        currentNorm = newNorm = oldNorm = transform.up;
        lerpValue = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = currentPos + footPosOffset;
        transform.rotation = Quaternion.LookRotation(currentNorm) * Quaternion.Euler(footRotOffset);

        Ray ray = new Ray(body.position + (body.right * footSpacing) + (Vector3.up * 2), Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 10, terrainLayer.value))
        {
            if (isFirstStep || (Vector3.Distance(newPos, hit.point) > stepDistance && !otherFoot.IsMoving() && !IsMoving()))
            {
                lerpValue = 0;
                int direction = body.InverseTransformPoint(hit.point).z > body.InverseTransformPoint(newPos).z ? 1 : -1;

                newPos = hit.point + (body.forward * direction * stepLenght);
                newNorm = hit.normal;
                isFirstStep = false;
            }
        }

        if (lerpValue < 1)
        {
            Vector3 tempPos = Vector3.Lerp(oldPos, newPos, lerpValue);
            tempPos.y += Mathf.Sin(lerpValue * Mathf.PI) * stepheight;

            currentPos = tempPos;
            currentNorm = Vector3.Lerp(oldNorm, newNorm, lerpValue);
            lerpValue += Time.deltaTime * speed;
        }
        else
        {
            oldPos = newPos;
            oldNorm = newNorm;
        }
    }

    public bool IsMoving()
    {
        return lerpValue < 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawSphere(newPos, 0.1f);
    }
}
