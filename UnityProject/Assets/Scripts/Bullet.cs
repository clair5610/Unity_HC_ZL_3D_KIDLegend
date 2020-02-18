using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// 子彈的傷害值
    /// </summary>
    public float damage;

    /// <summary>
    /// 是否為玩家的武器，true 玩家的，false 敵人的
    /// </summary>
    public bool player;

    /// <summary>
    /// 有勾選 IsTrigger 的物件，偵測碰到其他物件時執行一次
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (!player && other.name == "女孩")                // 如果碰到.名稱 = "女孩"
        {
            other.GetComponent<player>().Hit(damage);       // 取得<玩家>().受傷(傷害值)
        }
        else if (player && other.tag == "敵人" && other.GetComponent<Enemy>())      // 如果碰到.名稱 = "女孩"
        {
            other.GetComponent<Enemy>().Hit(damage);                                // 取得<玩家>().受傷(傷害值)
            Destroy(gameObject);
        }
    }
}
