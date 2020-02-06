using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// 子彈的傷害值
    /// </summary>
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "女孩")                        // 如果碰到.名稱 = "女孩"
        {
            other.GetComponent<player>().Hit(damage);    // 取得<玩家>().受傷(傷害值)
        }
    }
}
