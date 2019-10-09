using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rcacco : MonoBehaviour
{
    public float RstickX = 0.0f;
    public float RstickY = 0.0f;

    public float RTrriger = 0.0f;

    bool isTrriger = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            Debug.Log("button5");
            addrot = Quaternion.Euler(0f, 0f, 90f); 
        }
        if (RTrriger <= -0.5 && isTrriger == false)
        {
            Debug.Log("button17");
            addrot = Quaternion.Euler(0f, 0f, -90f);
            isTrriger = true;
        }
        else if (RTrriger >= -0.5)
        {
            isTrriger = false;
        }

        gameObject.transform.position = moveSpeed;
        gameObject.transform.rotation = rot * addrot;
    }
}
