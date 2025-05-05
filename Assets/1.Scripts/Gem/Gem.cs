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
    private Enemy currentTarget = null;  // 현재 타겟을 저장할 변수

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

    void Attack()
    {
        if (currentTarget != null)
        {
            // 타겟이 죽었거나 범위를 벗어난 경우, 새 타겟을 설정
            if (currentTarget.isDead || Vector3.Distance(transform.position, currentTarget.transform.position) > attackRange)
            {
                SetTargetToClosestEnemy();  // 새 타겟 설정
            }

            // 타겟이 유효하면 공격
            if (currentTarget != null)
            {
                currentTarget.TakeDamage(attackDamage);
                StartCoroutine(DealDotDamage(currentTarget, dotDamage, 3, 1f));
            }
        }
        else
        {
            Debug.Log("No target to attack!");  // 타겟이 없을 때 로그 출력
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
            attackDamage = itemData.attackValue;
            attackSpeed = itemData.attackSpeed;
            UpdateIcon();
        }
    }
    #endregion
}
