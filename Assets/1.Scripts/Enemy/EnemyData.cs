using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    public static EnemyData Instance;

    [Header("Enemy Stats")]
    public float maxHp = 100f;
    public float currentHp;
    public float moveSpeed = 2f;
    public float enemyGold = 0f;

    public void Init()
    {
        Instance = this;
    }
}
