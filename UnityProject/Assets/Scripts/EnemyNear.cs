using UnityEngine;
using System.Collections;

public class EnemyNear : Enemy
{
    // override 複寫 : 複寫父類別有 virtual 的成員
    protected override void attack()
    {
        base.attack();  // 父類別原本的敘述或演算法

        StartCoroutine(AttackDelay()); //啟動協程 AttackDelay
    }

    private IEnumerator AttackDelay() //協程 攻擊傷害延遲
    {
        yield return new WaitForSeconds(data.attackDelay);  // 傳回 等待秒數(Data中的傷害延遲欄位);

        RaycastHit hit;  // 區域變數 碰撞資訊 : 用來存放射線打到的物件

        // out 參數修飾詞 : 保存方法的資訊在區域變數內
        // 物理.射線(起點，方向，碰撞資訊，長度)
        if (Physics.Raycast(transform.position + Vector3.up * data.attackY, transform.forward, out hit, data.attackLength))
        {
            hit.collider.GetComponent<player>().Hit(data.attack);
        }
    } 

    // 繪製圖示 : 只會在場景內顯示，供開發者觀看，實際遊戲上不會顯示
    private void OnDrawGizmos()
    {
        // 圖示.顏色 = 顏色.你要的顏色名字
        Gizmos.color = Color.red;

        // 前方 Z transform.forward 後方 -transform.forward
        // 右方 Z transform.right
        // 上方 Z transform.up

        // 圖示.繪製射線 ( 起點.方向 )

        // Vector3.up = new Vector3(0, 1, 0)
        // Vector3.right = new Vector3(1, 0, 0)
        // Vector3.forward = new Vector3(0, 0, 1)
        Gizmos.DrawRay(transform.position + Vector3.up * data.attackY, transform.forward * data.attackLength);
    }
}
