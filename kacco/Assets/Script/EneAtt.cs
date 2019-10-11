using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneAtt : Enemy
{
    float time = 0.0f;
    float va = 0f;
    bool AS = false;
    public override void TargetAction() 
    {
        nma.SetDestination(transform.position);
        var tpo = LR[LRnum].transform.position;
        var pos = transform.position;
        var top = pos - tpo;
        var to = top;

        var rot = transform.rotation;
        var front = rot * Vector3.forward;
        top.Normalize();
        front.Normalize();
        var rad = Vector3.Dot(front, top);
        var add = Quaternion.AngleAxis(rad * Mathf.Rad2Deg, Vector3.up);
        //var add = Quaternion.FromToRotation(front, top);
        rot *= add;
        transform.rotation = rot;

        if (AS)
        {
            va -= Time.deltaTime*3f;
            if (va <= 0.5f)
            {
                AS = false;
            }
        }
        else
        {
            va += Time.deltaTime*3f;
            if (va >= 1.5f)
            {
                AS = true;
            }
        }
        transform.localScale = Vector3.one * 0.3f * va;

        time += Time.deltaTime;
        if(time > 3f)
        {
            if(LRnum == 0)
            {
                //Debug.Log(LC);
                LC.LHP += -1;
            }
            else
            {
                //Debug.Log("aaaRR");
                RC.RHP += -1;
            }
            SE.PlayOneShot(SE.clip);
            var eff = Instantiate(effect, LR[LRnum].transform.position, Quaternion.identity);
            eff.transform.localScale = Vector3.one * 0.3f;
            time = 0f;
        }
        if (to.magnitude > 15f)
        {
            transform.localScale = Vector3.one * 0.3f;
            isLook = false;
        }
    }
}
