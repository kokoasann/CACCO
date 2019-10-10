using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public GameObject EnemyFamiry = null;
    public GameObject[] LR = new GameObject[2];
    Lcacco LC;
    Rcacco RC;

    public float time = 0.0f;
    public float HP = 0.0f;
    bool isResult = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        LC = LR[0].GetComponent<Lcacco>();
        RC = LR[1].GetComponent<Rcacco>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isResult)
        {
            time += Time.deltaTime;
            if (EnemyFamiry.transform.childCount == 0)
            {
                HP = LC.LHP + RC.RHP;
                isResult = true;
                SceneManager.LoadSceneAsync("Scenes/ResultScene");
                SceneManager.UnloadSceneAsync("Scenes/SampleScene");
            }
            
        }
    }

    public void Deth()
    {
        Destroy(gameObject);
    }
}