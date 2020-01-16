using UnityEngine;

[CreateAssetMenu(fileName = "玩家資料", menuName = "CL/玩家資料")]
public class playerData : ScriptableObject
{
    [Header("血量"), Range(200, 10000)]
    public float hp;

    public float hpMax;
}
