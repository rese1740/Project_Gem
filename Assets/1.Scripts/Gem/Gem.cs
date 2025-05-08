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
        // 공격이 가능한 시간이고, 타겟이 있을 때만 공격
        if (Time.time >= nextAttackTime && currentTarget != null)
        {
            // 공격 수행
            Attack();
            nextAttackTime = Time.time + attackSpeed;
        }
        else if (currentTarget == null)
        {
            // 타겟이 없으면 새 타겟을 설정
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

    #region 공격
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


    // 도트 데미지 코루틴
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

    #region 합체
    void UpdateIcon()
    {
        if (itemData != null && spriteRenderer != null && currentRank >= 1 && currentRank <= itemData.rankIcons.Length)
        {
            spriteRenderer.sprite = itemData.rankIcons[currentRank - 1];  // 랭크에 맞는 아이콘 설정
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

            // 현재 랭크의 능력치 가져오기
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
