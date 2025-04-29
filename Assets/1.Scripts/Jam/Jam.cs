using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Image ����� ���� �߰�

public class Jam : MonoBehaviour
{
    public JamData itemData;  // ItemData�� public���� �߰� (Inspector���� �Ҵ� ����)
    public Image image;  // �������� ������ Image ������Ʈ (SpriteRenderer -> Image)

    private float attackDamage;  // ���ݷ� ���� (���� ����)
    private float attackSpeed;   // ���� �ӵ� ���� (���� ����)
    private float nextAttackTime = 0f;  // ���� �ֱ�

    void Start()
    {
        // ������ �����͸� ���� ���ݷ�, ���� �ӵ� ����
        if (itemData != null)
        {
            attackDamage = itemData.attackValue;  // itemData���� ���ݷ� �޾ƿ���
            attackSpeed = itemData.attackSpeed;   // itemData���� ���� �ӵ� �޾ƿ���
            UpdateIcon();  // ���� �� ������ ������Ʈ
        }
    }

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
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);  // ���ݷ� ����
            }
        }
    }

    // �������� ��ũ�� �´� �������� ������Ʈ
    void UpdateIcon()
    {
        if (itemData != null && image != null && itemData.rank >= 1 && itemData.rank <= itemData.rankIcons.Length)
        {
            image.sprite = itemData.rankIcons[itemData.rank - 1];  // ��ũ�� �´� ������ ����
        }
    }

    // ��ũ�� �ø��� �������� ������Ʈ
    public void LevelUp()
    {
        if (itemData.rank < 7)  // �ִ� 7���� ��ũ ���
        {
            itemData.rank++;
            attackDamage = itemData.attackValue;  // ���ݷ� ����
            attackSpeed = itemData.attackSpeed;   // ���� �ӵ� ����
            UpdateIcon();  // ��ũ�� �´� ���������� ������Ʈ
        }
    }
}
