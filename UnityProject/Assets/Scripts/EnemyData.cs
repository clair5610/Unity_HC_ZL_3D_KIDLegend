﻿using UnityEngine;


// ScriptableObject 腳本物件化 : 將腳本的資料存放在專案內 (ex:攻擊力、血量...等等設定可直接在Unity內改動) (不需要掛在物件上)


[CreateAssetMenu(fileName = "怪物資料", menuName ="CL/怪物資料")]
public class EnemyData : ScriptableObject
{
    [Header("移動速度"), Range(0, 10)]
    public float speed;
    [Header("血量"), Range(100, 5000)]
    public float hp;
    [Header("攻擊力"), Range(10, 1000)]
    public float attack;
    [Header("冷卻時間"), Range(1, 10)]
    public float cd;
    [Header("停止距離"), Range(0.5f, 100)]
    public float stopDistance;
}