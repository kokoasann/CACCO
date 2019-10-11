using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Result : MonoBehaviour
{

    public GameObject time;
    public GameObject HP;
    public GameObject score;
    Text t_time;
    Text t_HP;
    Text t_score;

    GameMaster GM;
    
    // Start is called before the first frame update
    void Start()
    {
        t_time = time.GetComponent<Text>();
        t_HP = HP.GetComponent<Text>();
        t_score = score.GetComponent<Text>();
        GM = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        GM.time = GM.MAXTime - GM.time;
        int m = (int)(GM.time/60f);
        float s = GM.time % 60f;
        t_time.text += m.ToString() + ":" + s.ToString();
        t_HP.text += GM.HP.ToString();
        t_score.text += (GM.HP * GM.time).ToString();

        GM.Deth();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("joystick button 0"))
        {
            SceneManager.LoadScene("Scenes/TitleScene");
            SceneManager.UnloadSceneAsync("Scenes/ResultScene");
            Destroy(this);
        }
    }
}
