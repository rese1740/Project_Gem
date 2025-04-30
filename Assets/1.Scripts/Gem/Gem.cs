using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Gem : MonoBehaviour
{
    public GemData itemData;  // 아이템 데이터
    public Image spriteRenderer;  // 아이콘을 보여줄 SpriteRenderer
    public float attackDamage;  // 공격력
    public float attackSpeed;   // 공격 속도
    public float dotDamage;

    private float nextAttackTime = 0f;  // 공격 주기

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
            nextAttackTime = Time.time + attackSpeed;  // 공격 속도에 맞게 주기 설정
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

                // 즉시 피해
                enemyScript.TakeDamage(attackDamage);

                // 도트뎀 적용: 이건 Gem이 직접 관리해야 함
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
