using UnityEngine;
using UnityEngine.AI;   // 引用 人工智慧 API

public class Enemy : MonoBehaviour
{
    [Header("怪物資料")]
    public EnemyData data;

    private Animator ani;      // 動畫控制器
    private NavMeshAgent nav;  // 導覽網格代理器

    private Transform player;  // 玩家變形
    private float timer;

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

    // protected 保護 : 允許子類別存取，禁止外部類別存取
    // virtual 虛擬 : 允許子類別複寫
    protected virtual void attack()       
    {
        ani.SetTrigger("攻擊觸發");   // 攻擊動畫
        timer = 0;                    // 時間歸零 ( 攻擊後時間歸零 )
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
        ani.SetBool("跑步開關", false);  // 等待動畫
        timer += Time.deltaTime;         // 計時器累加

        // print("計時器:" + timer);     輸出時間到U   

        if (timer >= data.cd)            // 如果 計時器 >= 資料.冷卻 ( U裡怪物設定的攻擊冷卻時間 )
        {
            attack();                    // 就攻擊
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void move()
    {
        Vector3 posplayer = player.position;  // 區域三維向量 = 目標( 玩家 ).座標  讓怪物的位置和玩家重疊
        posplayer.y = transform.position.y;   // 三維向量.Y = 本身.Y  ( 為了不跌倒所以Y值必須保持怪物本身的 )
        transform.LookAt(posplayer);          // 變形.看著 ( 三維向量 ) 讓怪物能注視著玩家
        nav.SetDestination(player.position);  // 代理器 . 設定目的地( 玩家 . 座標 )

        // print("剩餘距離" + nav.remainingDistance);  輸出到 Unity 當前位置距离目標位置的距离

        // 如果 代理器.剩餘距離(離玩家的距離) < 資料.停止距離 (Data裡寫入到U的欄位設定的停止距離)
        if (nav.remainingDistance < data.stopDistance) 
        {
            wait();  // 就變成 等待 狀態
        }
        // 
        else
        {
            ani.SetBool("跑步開關", true);        // 走路 動畫.設定布林值( 參數 , 勾選 )
        }
    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage"></param>
    private void hit(float damage)
    {

    }
}
