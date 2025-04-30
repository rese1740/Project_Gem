using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Gem : MonoBehaviour
{
    public GemData itemData;  // ������ ������
    public Image spriteRenderer;  // �������� ������ SpriteRenderer
    public float attackDamage;  // ���ݷ�
    public float attackSpeed;   // ���� �ӵ�
    public float dotDamage;

    private float nextAttackTime = 0f;  // ���� �ֱ�

     public int currentRank;

    void Start()
    {
        currentRank = itemData.rank; 

        if (itemData != null)
        {
            dotDamage = itemData.dotDamage;
            attackDamage = itemData.attackValue;
            attackSpeed = itemData.attackSpeed;
            UpdateIcon();  
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
            if (enemy != null)
            {
                Enemy enemyScript = enemy.GetComponent<Enemy>();

                // ��� ����
                enemyScript.TakeDamage(attackDamage);

                // ��Ʈ�� ����: �̰� Gem�� ���� �����ؾ� ��
                StartCoroutine(DealDotDamage(enemyScript, dotDamage, 3, 1f));
            }
        }
    }

    IEnumerator DealDotDamage(Enemy enemy, float damagePerTick, int tickCount, float interval)
    {
        for (int i = 0; i < tickCount; i++)
        {
            yield return new WaitForSeconds(interval);
            if (enemy != null)
            {
                enemy.TakeDamage(damagePerTick);
            }
        }
    }

    #region ��ü
    void UpdateIcon()
    {
        if (itemData != null && spriteRenderer != null && currentRank >= 1 && currentRank <= itemData.rankIcons.Length)
        {
            spriteRenderer.sprite = itemData.rankIcons[currentRank - 1];  // ��ũ�� �´� ������ ����
        }
    }

    public void LevelUp()
    {
        if (currentRank < itemData.maxRank)
        {
            currentRank++;
            attackDamage = itemData.attackValue;
            attackSpeed = itemData.attackSpeed;
            UpdateIcon();
        }
    }
    #endregion
}
