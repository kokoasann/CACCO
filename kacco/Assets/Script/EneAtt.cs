using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneAtt : Enemy
{
    public override void TargetAction() 
    {
        var tpo = LR[LRnum].transform.position;
        var pos = transform.position;
        var top = pos - tpo;

        var rot = transform.rotation;
        var front = rot * Vector3.forward;
        top.Normalize();
        front.Normalize();
        var add = Quaternion.FromToRotation(front, top);
        rot *= add;

    }
}
