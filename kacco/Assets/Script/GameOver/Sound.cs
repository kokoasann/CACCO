using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{

    private AudioSource sound01;

    private bool on = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        sound01 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            sound01.PlayOneShot(sound01.clip);
            on = true;
        }
        if (on == true)
        {
            if (!sound01.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }
}
