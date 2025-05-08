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
    public float slowValue;
    public float critValue;

    private float nextAttackTime = 0f;
    private Enemy currentTarget = null;  // ���� Ÿ���� ������ ����

    public int currentRank;

    void Start()
    {
        currentRank = itemData.rank;
        SetTargetToClosestEnemy();

        if (itemData != null)
        {
            ApplyRankStats();  // ���� ��ũ�� �´� �ɷ�ġ ����
            UpdateIcon();      // �����ܵ� ����
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

    void ApplyRankStats()
    {
        GemStats stats = itemData.GetStatsByRank(currentRank);
        attackDamage = stats.attackValue;
        attackSpeed = stats.attackSpeed;
        attackRange = stats.attackRange;
        dotDamage = stats.dotDamage;
        slowValue = stats.slowValue;
        critValue = stats.critValue;
    }

    #region ����
    void Attack()
    {
        if (currentTarget != null)
        {
            
            if (currentTarget.isDead || Vector3.Distance(transform.position, currentTarget.transform.position) > attackRange)
            {
                SetTargetToClosestEnemy();  
            }

            if (currentTarget != null)
            {
                float finalAttackDamage = attackDamage;
                if (Random.value <= critValue)  
                {
                    finalAttackDamage *= 2f;  
                }

                currentTarget.TakeDamage(finalAttackDamage);  
                StartCoroutine(DealDotDamage(currentTarget, dotDamage, 3, 1f));

                if (slowValue > 0)
                {
                    currentTarget.ApplySlow(slowValue, 2f); 
                }
            }
        }
        else
        {
            Debug.Log("No target to attack!");  
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
    #endregion

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

            // ���� ��ũ�� �ɷ�ġ ��������
            GemStats stats = itemData.GetStatsByRank(currentRank);

            attackDamage = stats.attackValue;
            attackSpeed = stats.attackSpeed;
            attackRange = stats.attackRange;
            dotDamage = stats.dotDamage;
            slowValue = stats.slowValue;

            UpdateIcon();
        }
    }

    #endregion
}
