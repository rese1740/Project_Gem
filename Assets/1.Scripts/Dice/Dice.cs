using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public float attackDamage = 10f;  // ���ݷ�
    public float attackSpeed = 1f;    // ���� �ӵ�

    private float nextAttackTime = 0f;  // ���� �ֱ�

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackSpeed;  // ���� �ӵ��� �°� �ֱ� ����
        }
    }

    void Attack()
    {
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); 

        foreach (var enemy in enemies)
        {
            // ���� ������ ����
            if (enemy != null)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }
    }
}
