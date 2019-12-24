using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [Header("速度"), Range(0, 1500)]
    public float speed = 1.5f;

    private Rigidbody rig;
    private FixedJoystick Joystick;
    private Animator ani; // 動畫控制器元件

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>(); // 動畫控制器 = 取得元件<動畫控制器>()
        Joystick = GameObject.Find("虛擬搖桿").GetComponent<FixedJoystick>();
    }

    // 固定更新 : 一秒執行 50 次 - 處理物理行為
    private void FixedUpdate()
    {
        Move();
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

        rig.AddForce(h * speed, 0, v * speed);

        ani.SetBool("跑步開關", v != 0 || h != 0); // 動畫控制器.設定布林值("參數名稱"，布林值)
    }

}
