using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Vector3[] Target = new Vector3[8];

    public GameObject[] LR;

    protected Lcacco LC;
    protected Rcacco RC;

    protected int LRnum = 0;
    protected NavMeshAgent nma;
    protected Vector3 tar;
    protected bool isLook = false;
    protected bool isfirst = true;
    void Start()
    {
        Target[0] = new Vector3(25, 0, 25);
        Target[1] = new Vector3(-25, 0, 25);
        Target[2] = new Vector3(-25, 0, -25);
        Target[3] = new Vector3(25, 0, -25);
        Target[4] = new Vector3(0, 0, 25);
        Target[5] = new Vector3(0, 0, -25);
        Target[6] = new Vector3(25, 0, 0);
        Target[7] = new Vector3(-25, 0, 0);
        nma = GetComponent<NavMeshAgent>();
        RandomPoint();

        LC = LR[0].GetComponent<Lcacco>();
        RC = LR[1].GetComponent<Rcacco>();
    }
    
    void Update()
    {
        if (nma.hasPath || isLook)
        {
            var f = transform.forward;
            for(int i=0;i<2;i++)
            {
                var lrpo = LR[i].transform.position;
                var to = lrpo - transform.position;
                to.Normalize();
                var rad = Vector3.Dot(to, transform.position);

                if (Mathf.Abs(rad * Mathf.Rad2Deg) < 60)
                {
                    
                    LRnum = i;
                    isLook = true;
                }
            }

            if (!isLook)
            {
                var to = tar - nma.transform.position;
                
                if (to.magnitude < 10)
                {
                    RandomPoint();
                    isfirst = true;
                }
            }
            else
            {
                Debug.Log("^^;;;");
                TargetAction();
            }
        }
        else
        {
            Debug.Log("^^;");
            RandomPoint();
        }
    }


    void RandomPoint()
    {
        var x = Random.Range(-25f,25f);
        var z = Random.Range(-25f,25f);
        tar = new Vector3(x, 0, z);
        nma.SetDestination(tar);
        Debug.Log(tar);
    }
    virtual public void TargetAction() { }
}
