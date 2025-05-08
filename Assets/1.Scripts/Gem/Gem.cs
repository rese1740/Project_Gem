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
    private Enemy currentTarget = null;  // 현재 타겟을 저장할 변수

    public int currentRank;

    void Start()
    {
        currentRank = itemData.rank;
        SetTargetToClosestEnemy();

        if (itemData != null)
        {
            ApplyRankStats();  // 현재 랭크에 맞는 능력치 적용
            UpdateIcon();      // 아이콘도 갱신
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
        dotDamage = stats.dotDamage;
        slowValue = stats.slowValue;
        critValue = stats.critValue;
    }

    #region 공격
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


    // 도트 데미지 코루틴
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
