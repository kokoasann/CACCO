using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneRun : Enemy
{
    public override void TargetAction()
    {
        if (!isfirst)
        {
            Vector3 tarpo = new Vector3();
            Vector3 tarto = new Vector3();

            foreach (var ta in Target)
            {
                var to = ta - LR[LRnum].transform.position;
                if (to.magnitude > tarto.magnitude)
                {
                    tarto = to;
                    tarpo = LR[LRnum].transform.position;
                }
            }
            nma.SetDestination(tarpo);
            isfirst = false;
        }
        var tor = LR[LRnum].transform.position - transform.position;
        if (tor.magnitude >50)
        {
            isLook = false;
        }
    }
}
