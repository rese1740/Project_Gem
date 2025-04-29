using UnityEngine;
using UnityEngine.UI;

public class Jam : MonoBehaviour
{
    public JamData itemData;  // 아이템 데이터
    public Image spriteRenderer;  // 아이콘을 보여줄 SpriteRenderer
    public float attackDamage;  // 공격력
    public float attackSpeed;   // 공격 속도

    private float nextAttackTime = 0f;  // 공격 주기

    // Jam의 랭크를 관리하는 변수 (원본 데이터를 건드리지 않음)
     public int currentRank;

    void Start()
    {
        // 랭크를 초기화하고 시작
        currentRank = itemData.rank; // 초기 랭크는 ItemData의 랭크로 설정

        // 아이템 데이터를 통해 초기화
        if (itemData != null)
        {
            // 공격력과 공격 속도 설정
            attackDamage = itemData.attackValue;
            attackSpeed = itemData.attackSpeed;
            UpdateIcon();  // 시작 시 아이콘 업데이트
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
            // 적이 있으면 공격
            if (enemy != null)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }
    }

    // 아이템의 랭크에 맞는 아이콘을 업데이트
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
}
