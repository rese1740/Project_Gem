using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Image 사용을 위해 추가

public class Jam : MonoBehaviour
{
    public JamData itemData;  // ItemData를 public으로 추가 (Inspector에서 할당 가능)
    public Image image;  // 아이콘을 보여줄 Image 컴포넌트 (SpriteRenderer -> Image)

    private float attackDamage;  // 공격력 변수 (변경 가능)
    private float attackSpeed;   // 공격 속도 변수 (변경 가능)
    private float nextAttackTime = 0f;  // 공격 주기

    void Start()
    {
        // 아이템 데이터를 통해 공격력, 공격 속도 설정
        if (itemData != null)
        {
            attackDamage = itemData.attackValue;  // itemData에서 공격력 받아오기
            attackSpeed = itemData.attackSpeed;   // itemData에서 공격 속도 받아오기
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
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);  // 공격력 적용
            }
        }
    }

    // 아이템의 랭크에 맞는 아이콘을 업데이트
    void UpdateIcon()
    {
        if (itemData != null && image != null && itemData.rank >= 1 && itemData.rank <= itemData.rankIcons.Length)
        {
            image.sprite = itemData.rankIcons[itemData.rank - 1];  // 랭크에 맞는 아이콘 설정
        }
    }

    // 랭크를 올리고 아이콘을 업데이트
    public void LevelUp()
    {
        if (itemData.rank < 7)  // 최대 7까지 랭크 상승
        {
            itemData.rank++;
            attackDamage = itemData.attackValue;  // 공격력 갱신
            attackSpeed = itemData.attackSpeed;   // 공격 속도 갱신
            UpdateIcon();  // 랭크에 맞는 아이콘으로 업데이트
        }
    }
}
