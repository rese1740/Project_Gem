using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public GemData itemData;
    SpriteRenderer spriteRenderer;
    [Header("Get Stat")]
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;
    public float dotDamage;
    public float slowValue;
    public float critValue;

    private float nextAttackTime = 0f;
    private Enemy currentTarget = null;  

    public int currentRank;

    public float scaleFactor = 5f;  
    public float duration = 0.5f;  

    void Start()
    {
        transform.localScale = Vector3.one * 0.2f;

        transform.DOScale(Vector3.one * scaleFactor, duration).OnKill(() =>
        {
            transform.DOScale(Vector3.one * 0.2f, duration);
        });

        currentRank = itemData.rank;
        SetTargetToClosestEnemy();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (itemData != null)
        {
            ApplyRankStats(); 
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

    void ApplyRankStats()
    {
        GemStats stats = itemData.GetStatsByRank(currentRank);
        attackDamage = stats.attackValue;
        attackSpeed = stats.attackSpeed;
        attackRange = stats.attackRange;
        slowValue = stats.slowValue;
        critValue = stats.critValue;

        if (itemData.itemID == "Emerald")
        {
            dotDamage = attackDamage * 0.3f;
        }
        else
        {
            dotDamage = 0f;
        }
    }

    #region ����
    void Attack()
    {
        if (currentTarget != null)
        {
            float finalAttackDamage = attackDamage;

            if (Random.value <= critValue)
            {
                finalAttackDamage *= 2f;
            }
            currentTarget.TakeDamage(finalAttackDamage, itemData.itemID);

            StartCoroutine(DealDotDamage(currentTarget, dotDamage, 3, 1f));

            if (slowValue > 0)
            {
                currentTarget.ApplySlow(slowValue, 10f);
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
                enemy.TakeDamage(damagePerTick, itemData.itemID);
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

        transform.localScale = Vector3.one * 0.2f;

        transform.DOScale(Vector3.one * scaleFactor, duration).OnKill(() =>
        {
            transform.DOScale(Vector3.one * 0.2f, duration);
        });

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
