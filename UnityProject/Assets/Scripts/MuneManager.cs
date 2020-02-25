using UnityEngine;

public class MuneManager : MonoBehaviour
{
    [Header("玩家資料")]
    public playerData dataPlayer;

    public void BuyHp_500()
    {
        dataPlayer.hpMax += 500;           // 最大值遞增
        dataPlayer.hp = dataPlayer.hpMax;
    }

    public void BuyAtk_50()
    {
        dataPlayer.attack += 50;           // 攻擊力遞增
    }
}
