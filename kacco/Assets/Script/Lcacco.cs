using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lcacco : MonoBehaviour
{
    public float LstickX = 0.0f;
    public float LstickY = 0.0f;

    public float LTrriger = 0.0f;

    bool isTrriger = false;

    public Vector3 direction;           //かっこが向いている方向。

    public Vector3 RayDrc;              //レイの向き。

    public float LHP;                     //左かっこのHP。

    CharacterController CharaCon;       //キャラコン。

    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = gameObject.transform.position;
        direction = gameObject.transform.forward;
        RayDrc = gameObject.transform.forward;
        RayDrc.z = RayDrc.y;
        RayDrc.y = 0.0f;
        LHP = 15;
        CharaCon = gameObject.GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LHP <= 0.0f)
        {
            Dead();
        }
        Move();
       
        var rot = gameObject.transform.rotation;
        var addrot = Quaternion.identity;
        if (Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            Debug.Log("button4");
            addrot = Quaternion.Euler(0f, 0f, 90f);
            if (RayDrc.z >= 0.5f)
            {
                Debug.Log("aaaaaaaaaaaaaaaa");
                RayDrc.x = 1.0f;
                RayDrc.z = 0.0f;
            }
            else if (RayDrc.x >= 0.5f)
            {
                RayDrc.z = -1.0f;
                RayDrc.x = 0.0f;
            }
            else if (RayDrc.z <= -0.5f)
            {
                RayDrc.x = -1.0f;
                RayDrc.z = 0.0f;
            }
            else if (RayDrc.x <= -0.5f)
            {
                RayDrc.z = 1.0f;
                RayDrc.x = 0.0f;
            }
        }
        if (LTrriger >= 0.5 && isTrriger == false)
        {
            Debug.Log("button17");
            addrot = Quaternion.Euler(0f, 0f, -90f);
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
        else if(LTrriger <= 0.5)
        {
            isTrriger = false;
        }

        CharaCon.Move(pos);

        //gameObject.transform.position = moveSpeed;
        gameObject.transform.rotation = rot * addrot;
    }

    private void Move()
    {
        LstickX = Input.GetAxis("Horizontal");
        LstickY = Input.GetAxis("Vertical");

        LTrriger = Input.GetAxis("LTrriger");

        pos.x = LstickX;
        pos.z = LstickY;

        pos *= 0.15f;
        pos.y = 0.0f;
        pos.y -= 1.0f;
    }
    private void Dead()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
