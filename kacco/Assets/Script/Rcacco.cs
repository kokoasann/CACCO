﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rcacco : MonoBehaviour
{
    public float RstickX = 0.0f;        //右スティックの横の入力量。
    public float RstickY = 0.0f;        //右スティックの縦の入力量。

    public float RTrriger = 0.0f;       //R2ボタンの入力量。

    bool isTrriger = false;             //押しているかどうか。

    public Vector3 direction;           //かっこが向いている方向。

    public Vector3 RayDrc;              //レイの向き。

    public GameObject LCACCO;

    private Transform targetTra;
    //　ターゲットとの距離
    private float distanceFromTargetObj;

    //Lcacco LCacco;

    // Start is called before the first frame update
    void Start()
    {
        direction = gameObject.transform.forward;
        RayDrc = gameObject.transform.forward;
        RayDrc.z = RayDrc.y;
        RayDrc.y = 0.0f;
        //LCACCO = GameObject.Find("LCacco");
    }

    // Update is called once per frame
    void Update()
    {
        //ターゲットとの距離。
        distanceFromTargetObj = Vector3.Distance(transform.position, LCACCO.GetComponent<Lcacco>().transform.position);

        RstickX = Input.GetAxis("Horizontal2");
        RstickY = Input.GetAxis("Vertical2");

        RTrriger = Input.GetAxis("LTrriger");

        var pos = gameObject.transform.position;
        pos.x = RstickX;
        pos.z = RstickY;

        var moveSpeed = gameObject.transform.position;
        moveSpeed.x += pos.x * 0.3f;
        moveSpeed.z += pos.z * 0.3f;

        var rot = gameObject.transform.rotation;
        var addrot = Quaternion.identity;
        //R1ボタンを押したときの処理。
        if (Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            Debug.Log("button5");
            addrot = Quaternion.Euler(0f, 0f, 90f);
            direction = gameObject.transform.forward;
            //RayDrc = addrot * RayDrc;
            if(RayDrc.z >= 0.5f)
            {
                //Debug.Log("aaaaaaaaaaaaaaaa");
                RayDrc.x = 1.0f;
                RayDrc.y = 0.0f;
                RayDrc.z = 0.0f;
            }
            else if(RayDrc.x >= 0.5f)
            {
                RayDrc.z = -1.0f;
                RayDrc.y = 0.0f;
                RayDrc.x = 0.0f;
            }
            else if(RayDrc.z <= -0.5f)
            {
                RayDrc.x = -1.0f;
                RayDrc.y = 0.0f;
                RayDrc.z = 0.0f;
            }
            else if(RayDrc.x <= -0.5f)
            {
                RayDrc.z = 1.0f;
                RayDrc.y = 0.0f;
                RayDrc.x = 0.0f;
            }
        }
        //R2ボタンを押したときの処理。
        if (RTrriger <= -0.5 && isTrriger == false)
        {
            Debug.Log("button17");
            addrot = Quaternion.Euler(0f, 0f, -90f);
            direction = gameObject.transform.forward;
            //RayDrc = addrot * RayDrc;
            if (RayDrc.z >= 0.5f)
            {
                Debug.Log("aaaaaaaaaaaaaaaa");
                RayDrc.x = -1.0f;
                RayDrc.z = 0.0f;
            }
            else if (RayDrc.x <= -0.5f)
            {
                RayDrc.z = -1.0f;
                RayDrc.x = 0.0f;
            }
            else if (RayDrc.z <= -0.5f)
            {
                RayDrc.x = 1.0f;
                RayDrc.z = 0.0f;
            }
            else if (RayDrc.x >= 0.5f)
            {
                RayDrc.z = 1.0f;
                RayDrc.x = 0.0f;
            }
            isTrriger = true;
        }
        //R2ボタンを離したときの処理。
        else if (RTrriger >= -0.5)
        {
            isTrriger = false;
        }
        //var lCacco = FindObjectOfType<Lcacco>();
        RaycastHit hit = new RaycastHit();
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            Debug.Log("Bボタンが押された。");
           
            //　Cubeのレイを飛ばしターゲットと接触しているか判定
            Debug.Log(RayDrc);
            if (Physics.BoxCast(this.transform.position, Vector3.one * 1.0f, RayDrc, out hit, Quaternion.identity, 1000f, LayerMask.GetMask("Target")))
            {
                Vector3 LDrc;
                LDrc = LCACCO.GetComponent<Lcacco>().RayDrc;
                Debug.Log("右から左にレイが当たった");
                if(RayDrc == LDrc)
                {
                    Debug.Log("ここに敵が消滅する処理");
                }
            }

        }
       
   
        gameObject.transform.position = moveSpeed;
        gameObject.transform.rotation = rot * addrot;
    }
    void OnDrawGizmos()
    {
        //　Cubeのレイを疑似的に視覚化
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + transform.forward * distanceFromTargetObj, Vector3.one);
    }
}
