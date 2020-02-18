using UnityEngine;
using System.Linq;  // 引用 查詢 API - Min、Max 與 ToList

public class player : MonoBehaviour
{
    [Header("速度"), Range(0, 1500)]
    public float speed = 1.5f;
    [Header("玩家資料")]
    public playerData data;
    [Header("子彈")]
    public GameObject bullet;
    

    private Rigidbody rig;
    private FixedJoystick Joystick;
    private Animator ani;                  // 動畫控制器元件
    private Transform target;              // 目標物件
    private LevelManager levelManager;     // 關卡管理器
    private HpValueManager hpValueManager; // 血條數值管理器
    private Vector3 posBullet;             // 子彈座標
    private float timer;                   // 計時器
    private Enemy[] enemys;                // 敵人陣列 : 存放所有敵人
    private float[] enemysDis;             // 距離陣列 : 存放所有敵人的距離
    

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>(); // 動畫控制器 = 取得元件<動畫控制器>()
        Joystick = GameObject.Find("虛擬搖桿").GetComponent<FixedJoystick>();

        //target = GameObject.Find("虛擬搖桿").GetComponent<Transform>();
        target = GameObject.Find("目標").transform;

        levelManager = FindObjectOfType<LevelManager>();            // 透過類型尋找物件(場景上只有一個的時候)
        hpValueManager = GetComponentInChildren<HpValueManager>();  // 取得Unity子物件元件 
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

        if (v == 0 && h == 0) Attack();
    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">接收的傷害值</param>
    public void Hit(float damage)
    {
        if (ani.GetBool("死亡開關")) return;                                  // 如果 死亡開關 是勾選 跳出
        data.hp -= damage;
        hpValueManager.SetHp(data.hp, data.hpMax);                            // 更新血量(目前(血量),最大(血量))
        StartCoroutine(hpValueManager.ShowValue(damage, "-", Color.white));   // 啟動協程
        if (data.hp <= 0) Dead();
    }

    /// <summary>
    /// 死亡
    /// </summary>
    private void Dead()
    {
        ani.SetBool("死亡開關", true);    // 死亡動畫
        enabled = false;                  // 關閉此腳本 (this.enabled = false , this可省略)
        StartCoroutine(levelManager.ShowRevival());
    }
    /// <summary>
    /// 復活
    /// </summary>
    public void Revival()
    {
        enabled = true;                    // 關閉此腳本 (this 可省略)
        ani.SetBool("死亡開關", false);    // 死亡動畫
        data.hp = data.hpMax;              // 恢復血量 
        hpValueManager.SetHp(data.hp, data.hpMax);   // 更新血量(目前(血量),最大(血量))
        levelManager.HideRevival();
    }
    /// <summary>
    /// 攻擊
    /// </summary>
    private void Attack()
    {
        if (timer < data.cd)                // 如果 計時器 < 冷卻時間
        {
            timer += Time.deltaTime;        // 計時器 累加
        }
        else
        {
            // 1. 取得所有敵人
            enemys = FindObjectsOfType<Enemy>();

            // 過關
            if (enemys.Length == 0)         // 如果沒有敵人就跳出
            {
                levelManager.Pass(); 
                return;
            }

            timer = 0;                      // 計時器 歸零
            ani.SetTrigger("攻擊觸發");     // 攻擊動畫

            // 2. 取得所有敵人的距離

            enemysDis = new float[enemys.Length];

            for (int i = 0; i < enemys.Length; i++)
            {
                enemysDis[i] = Vector3.Distance(transform.position, enemys[i].transform.position);  // 距離 = 三維向量.距離(A，B)
            }

            // 3. 判斷誰最近與面向
            float min = enemysDis.Min();                   // 距離陣列.最小值()
            //print("最近的距離 : " + min);
            int index = enemysDis.ToList().IndexOf(min);   // 距離陣列.轉為清單().取得資料的編號(資料) - 清單才能使用
            //print("最近的編號 : " + index);
            Vector3 enemyPos = enemys[index].transform.position;
            enemyPos.y = transform.position.y;
            transform.LookAt(enemyPos);
                                                                                             // 子彈座標 = 飛龍.座標 + 飛龍前方 * Z + 飛龍上方 * Y
            posBullet = transform.position + transform.forward * data.attackZ + transform.up * data.attackY;
            
            // 下兩條是發射武器面向相反時轉正使用
            // Vector3 angle = transform.eulerAngles;                                         三維向量 玩家角度 = 變形.歐拉角度(0-360度)
            // Quaternion qua = Quaternion.Euler(angle.x + 180, angle.y, angle.z);            四元角度 = 四元.歐拉() - 歐拉轉為四元角度
            GameObject temp = Instantiate(bullet, posBullet, transform.rotation);            // 區域變數 = 生成(物件,座標,角度)
            temp.GetComponent<Rigidbody>().AddForce(transform.forward * data.bulletPower);   // 取得剛體.推力(敵人前方 * 力道)
            temp.AddComponent<Bullet>();                        // 暫存.添加元件<泛型>
            temp.GetComponent<Bullet>().damage = data.attack;   // 暫存.取得元件<泛型>.傷害值 = 怪物.攻擊力
            temp.GetComponent<Bullet>().player = true;
        }
       
    }

    private void OnDrawGizmos()  //Game裡不會執行
    {
        // 圖示.顏色 = 顏色
        Gizmos.color = Color.red;
        // 子彈座標 = 飛龍.座標 + 飛龍前方 * Z + 飛龍上方 * Y
        posBullet = transform.position + transform.forward * data.attackZ + transform.up * data.attackY;
        // 圖示.繪製球體(中心點,半徑)
        Gizmos.DrawSphere(posBullet, 0.1f);
    }
}













