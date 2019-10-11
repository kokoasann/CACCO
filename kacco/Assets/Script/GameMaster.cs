using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public GameObject EnemyFamiry = null;
    public GameObject[] LR = new GameObject[2];
    Lcacco LC;
    Rcacco RC;

    public GameObject TimeText;
    Text tex_time;
    public float MAXTime;
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
        tex_time = TimeText.GetComponent<Text>();
        time = MAXTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isResult)
        {
            time -= Time.deltaTime;
            if(time <= 0f)
            {
                SceneManager.LoadSceneAsync("Scenes/GameOverScene");
                isResult = true;
            }
            var m = (int)(time / 60f);
            var s = time % 60;
            tex_time.text = m.ToString() + ":" + s.ToString();

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