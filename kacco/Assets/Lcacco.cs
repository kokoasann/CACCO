using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lcacco : MonoBehaviour
{
    public float LstickX = 0.0f;
    public float LstickY = 0.0f;

    public float LTrriger = 0.0f;

    bool isTrriger = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LstickX = Input.GetAxis("Horizontal");
        LstickY = Input.GetAxis("Vertical");

        LTrriger = Input.GetAxis("LTrriger");

        var pos = gameObject.transform.position;
        pos.x = LstickX;
        pos.z = LstickY;

        var moveSpeed = gameObject.transform.position;
        moveSpeed.x += pos.x * 0.3f;
        moveSpeed.z += pos.z * 0.3f;

        var rot = gameObject.transform.rotation;
        if(Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            Debug.Log("button4");
            
        }
        if (LTrriger >= 0.5 && isTrriger == false)
        {
            Debug.Log("button17");
            isTrriger = true;
        }
        else if(LTrriger <= 0.5)
        {
            isTrriger = false;
        }

        gameObject.transform.position = moveSpeed;
        gameObject.transform.rotation = rot;
    }
}
