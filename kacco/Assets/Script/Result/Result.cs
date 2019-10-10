using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{

    public GameObject time;
    public GameObject HP;
    public GameObject score;
    TextEditor t_time;
    TextEditor t_HP;
    TextEditor t_score;

    GameMaster GM;
    
    // Start is called before the first frame update
    void Start()
    {
        t_time = time.GetComponent<TextEditor>();
        t_HP = HP.GetComponent<TextEditor>();
        t_score = score.GetComponent<TextEditor>();
        GM = GameObject.Find("GameMaster").GetComponent<GameMaster>();
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
