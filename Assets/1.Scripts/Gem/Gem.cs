using System.Collections;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public GemData itemData;
    public SpriteRenderer spriteRenderer;
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;
    public float dotDamage;

    private float nextAttackTime = 0f;
    private Enemy currentTarget = null;  // ���� Ÿ���� ������ ����

    public int currentRank;

    void Start()
    {
        currentRank = itemData.rank;
        SetTargetToClosestEnemy();
        if (itemData != null)
        {
            dotDamage = itemData.dotDamage;
            attackDamage = itemData.attackValue;
            attackSpeed = itemData.attackSpeed;
            attackRange = itemData.attackRange;
            UpdateIcon();
        }
    }

    void Update()
    {
        // ������ ������ �ð��̰�, Ÿ���� ���� ���� ����
        if (Time.time >= nextAttackTime && currentTarget != null)
        {
            // ���� ����
            Attack();
            nextAttackTime = Time.time + attackSpeed;
        }
        else if (currentTarget == null)
        {
            // Ÿ���� ������ �� Ÿ���� ����
            SetTargetToClosestEnemy();
        }
    }

    void Attack()
    {
        if (currentTarget != null)
        {
            // Ÿ���� �׾��ų� ������ ��� ���, �� Ÿ���� ����
            if (currentTarget.isDead || Vector3.Distance(transform.position, currentTarget.transform.position) > attackRange)
            {
                SetTargetToClosestEnemy();  // �� Ÿ�� ����
            }

            // Ÿ���� ��ȿ�ϸ� ����
            if (currentTarget != null)
            {
                currentTarget.TakeDamage(attackDamage);
                StartCoroutine(DealDotDamage(currentTarget, dotDamage, 3, 1f));
            }
        }
        else
        {
            Debug.Log("No target to attack!");  // Ÿ���� ���� �� �α� ���
        }
    }


    public void SetTargetToClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            currentTarget = closestEnemy.GetComponent<Enemy>();
        }
        else
        {
            Debug.Log("No enemies found!");
        }
    }


    // ��Ʈ ������ �ڷ�ƾ
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
