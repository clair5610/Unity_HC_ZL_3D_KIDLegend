using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [Header("速度"), Range(0, 1500)]
    public float speed = 1.5f;
    [Header("玩家資料")]
    public playerData data;

    

    private Rigidbody rig;
    private FixedJoystick Joystick;
    private Animator ani; // 動畫控制器元件
    private Transform target; //目標物件
    private LevelManager levelManager;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>(); // 動畫控制器 = 取得元件<動畫控制器>()
        Joystick = GameObject.Find("虛擬搖桿").GetComponent<FixedJoystick>();
        //target = GameObject.Find("虛擬搖桿").GetComponent<Transform>();
        target = GameObject.Find("目標").transform;
        levelManager = FindObjectOfType<LevelManager>(); // 透過類型尋找物件(場景上只有一個的時候)
    }

    // 固定更新 : 一秒執行 50 次 - 處理物理行為
    private void FixedUpdate()
    {
        Move();
    }

    // 只要碰到物件身上有勾 IsTrigger 碰撞器就會執行一次
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "傳送區域")
        {
            StartCoroutine(levelManager.NextLevel());  // 使用協程方法，必須要用 啟動協程
        }
    }

    /// <summary>
    ///  移動
    /// </summary>
    private void Move()
    {
        //print("水平:" + Joystick.Horizontal);
        float v = Joystick.Vertical;
        //print("垂直:" + Joystick.Horizontal);
        float h = Joystick.Horizontal;

        rig.AddForce(-h * speed, 0, -v * speed);

        ani.SetBool("跑步開關", v != 0 || h != 0); // 動畫控制器.設定布林值("參數名稱"，布林值)

        Vector3 pos = transform.position; // 玩家目標 = 變形.座標
        target.position = new Vector3(pos.x - h, 1.09F, pos.z - v); // 目標.座標 = 新 三維向量(玩家.x + 水平,0.3f, 玩家.z + 垂直)

        //transform.LookAt(target); ///直接這樣寫會吃土(角色靜止時無法站立，但行走正常)

        Vector3 posTarget = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.LookAt(posTarget);
    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">接收的傷害值</param>
    public void Hit(float damage)
    {
        data.hp -= damage;
    }
}













