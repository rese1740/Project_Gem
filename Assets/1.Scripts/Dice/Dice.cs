using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public float attackDamage = 10f;  // 공격력
    public float attackSpeed = 1f;    // 공격 속도

    private float nextAttackTime = 0f;  // 공격 주기

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackSpeed;  // 공격 속도에 맞게 주기 설정
        }
    }

    void Attack()
    {
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); 

        foreach (var enemy in enemies)
        {
            // 적이 있으면 공격
            if (enemy != null)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }
    }
}
