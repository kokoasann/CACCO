using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public GameObject EnemyFamiry = null;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyFamiry.transform.childCount == 0)
        {
            SceneManager.LoadSceneAsync("Scene/ResultScene");
            SceneManager.UnloadSceneAsync("Scene/SampleScene");
        }
    }
}
