using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Enemy Stats")]
    public float maxHp = 100f;
    public float currentHp;
    public float moveSpeed = 2f;
}
