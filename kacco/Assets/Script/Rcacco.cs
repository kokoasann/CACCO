using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rcacco : MonoBehaviour
{
    public float RstickX = 0.0f;        //右スティックの横の入力量。
    public float RstickY = 0.0f;        //右スティックの縦の入力量。

    public float RTrriger = 0.0f;       //R2ボタンの入力量。

    bool isTrriger = false;             //押しているかどうか。

    public Vector3 direction;           //かっこが向いている方向。

    public Vector3 RayDrc;              //レイの向き。

    public GameObject LCACCO;           //左のカッコ。

    public GameObject Enemy;            //エネミー。

    public float RHP;                     //右かっこのHP。

    CharacterController CharaCon;       //キャラコン。

    Vector3 pos;

    private AudioSource sound01;

    GameObject go;

    ParticleSystem effect;

    List<ParticleSystem> eff = new List<ParticleSystem>();

    List<ParticleSystem> effdeath = new List<ParticleSystem>();

    private Transform targetTra;
    //　ターゲットとの距離
    private float distanceFromTargetObj;

    // Start is called before the first frame update
    void Start()
    {
        direction = gameObject.transform.forward;
        RayDrc = gameObject.transform.forward;
        RayDrc.z = RayDrc.y;
        RayDrc.y = 0.0f;
        Enemy = GameObject.Find("EnemyFmiry");
        RHP = 15;
        CharaCon = gameObject.GetComponent<CharacterController>();
        pos = gameObject.transform.position;
        sound01 = GetComponent<AudioSource>();
        go = Resources.Load<GameObject>("PlasmaExplosionEffect");
        effect = go.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var eee in eff)
        {
            if(!eee.isPlaying)
            {
                Destroy(eee.gameObject);
                effdeath.Add(eee);
            }
        }
        foreach(var eee in effdeath)
        {
            eff.Remove(eee);
        }
        effdeath.Clear();

        if (RHP <= 0.0f)
        {
            Dead();
        }

        //ターゲットとの距離。
        distanceFromTargetObj = Vector3.Distance(transform.position, LCACCO.GetComponent<Lcacco>().transform.position);
        //移動処理。
        Move();

        var rot = gameObject.transform.rotation;
        var addrot = Quaternion.identity;
        //R1ボタンを押したときの処理。
        if (Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            //Debug.Log("button5");
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
           // Debug.Log("button17");
            addrot = Quaternion.Euler(0f, 0f, -90f);
            direction = gameObject.transform.forward;
            //RayDrc = addrot * RayDrc;
            if (RayDrc.z >= 0.5f)
            {
               // Debug.Log("aaaaaaaaaaaaaaaa");
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

        //if (Input.GetKeyDown(KeyCode.JoystickButton1))
        //{
        //    Debug.Log("Bボタンが押された。");

        //　Cubeのレイを飛ばしターゲットと接触しているか判定
        //Debug.Log(RayDrc);
        Vector3 normal = LCACCO.GetComponent<Lcacco>().transform.position - transform.position;
        normal.Normalize();
        if (Physics.BoxCast(this.transform.position - normal, Vector3.one * 1.0f, RayDrc, out hit, Quaternion.identity, 15, LayerMask.GetMask("Target")))
        {
            //Debug.Log("右から左にレイが当たった");
           


            if (!Physics.BoxCast(this.transform.position - normal, Vector3.one * 1.0f, RayDrc, out hit, Quaternion.identity, distanceFromTargetObj, LayerMask.GetMask("Wall")))
            
            {
                //Debug.Log("壁に当たらなかったので");
                Vector3 LDrc;
                LDrc = LCACCO.GetComponent<Lcacco>().RayDrc;
                
                if (RayDrc == LDrc)
                {
                    //Debug.Log("ここに敵が消滅する処理");
                    hit = new RaycastHit();
                    if (Physics.BoxCast(this.transform.position - normal, Vector3.one * 1.0f, RayDrc, out hit, Quaternion.identity, distanceFromTargetObj, LayerMask.GetMask("Enemy")))
                    {
                        //Debug.Log("エネミーとレイが衝突した。");
                        if (Enemy != null)
                        {
                            float min = 999999999;
                            Transform tran = null;
                            int cont = Enemy.transform.GetChildCount();
                            for (int i = 0; i < cont; i++)
                            {
                                var child = Enemy.transform.GetChild(i);
                                Vector3 kyori;
                                kyori = hit.transform.position - child.position;
                                if (kyori.magnitude < min)
                                {
                                    min = kyori.magnitude;
                                    tran = child;
                                }
                            }
                            sound01.PlayOneShot(sound01.clip);
                            Destroy(tran.gameObject);
                           
                            eff.Add(Instantiate(go, tran.transform.position, Quaternion.identity).GetComponent<ParticleSystem>());
                            
                            //Debug.Log("消滅した！！！！！！");
                        }
                    }
                }
            }
        }

        //}

        CharaCon.Move(pos);
        //gameObject.transform.position = moveSpeed;
        gameObject.transform.rotation = rot * addrot;
    }
    void OnDrawGizmos()
    {
        //　Cubeのレイを疑似的に視覚化
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + transform.forward * distanceFromTargetObj, Vector3.one);
    }
    //移動処理。
    private void Move()
    {
        RstickX = Input.GetAxis("Horizontal2");
        RstickY = Input.GetAxis("Vertical2");

        RTrriger = Input.GetAxis("LTrriger");

        pos.x = RstickX;
        pos.z = RstickY;

        pos *= 0.15f;
        pos.y = 0.0f;
        pos.y -= 1.0f;
    }

    private void Dead()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
