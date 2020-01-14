using UnityEngine;
using UnityEngine.AI;   // 引用 人工智慧 API

public class Enemy : MonoBehaviour
{
    [Header("怪物資料")]
    public EnemyData data;

    private Animator ani;      // 動畫控制器
    private NavMeshAgent nav;  // 導覽網格代理器

    private Transform player;  // 玩家變形

    private void Start()
    {
        ani = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();

        nav.speed = data.speed;  // 調整 --> 代理器.速度 
        nav.stoppingDistance = data.stopDistance;

        player = GameObject.Find("女孩").GetComponent<Transform>();  // 取得玩家變形 
    }

    private void Update()
    {
        move();
    }

    // 內容收起 Ctrl + M O
    // 內容展開 Ctrl + M L

    /// <summary>
    /// 攻擊
    /// </summary>
    private void attack()       
    {

    }

    /// <summary>
    /// 死亡
    /// </summary>
    private void dead()
    {

    }

    /// <summary>
    /// 等待
    /// </summary>
    private void wait()
    {

    }

    /// <summary>
    /// 移動
    /// </summary>
    private void move()
    {
        Vector3 posplayer = player.position;  // 區域三維向量 = 目標(玩家).座標 
        posplayer.y = transform.position.y;   // 三維向量.Y = 本身.Y  (為了不跌倒)
        transform.LookAt(posplayer);          // 變形.看著 (三維向量)
        ani.SetBool("跑步開關", true);        
        nav.SetDestination(player.position);

        nav.SetDestination(player.position);// 代理器 . 設定目的地(玩家.座標)
    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage"></param>
    private void hit(float damage)
    {

    }
}
