using UnityEngine;
using UnityEngine.UI;

public class Jam : MonoBehaviour
{
    public JamData itemData;  // ������ ������
    public Image spriteRenderer;  // �������� ������ SpriteRenderer
    public float attackDamage;  // ���ݷ�
    public float attackSpeed;   // ���� �ӵ�

    private float nextAttackTime = 0f;  // ���� �ֱ�

    // Jam�� ��ũ�� �����ϴ� ���� (���� �����͸� �ǵ帮�� ����)
     public int currentRank;

    void Start()
    {
        // ��ũ�� �ʱ�ȭ�ϰ� ����
        currentRank = itemData.rank; // �ʱ� ��ũ�� ItemData�� ��ũ�� ����

        // ������ �����͸� ���� �ʱ�ȭ
        if (itemData != null)
        {
            // ���ݷ°� ���� �ӵ� ����
            attackDamage = itemData.attackValue;
            attackSpeed = itemData.attackSpeed;
            UpdateIcon();  // ���� �� ������ ������Ʈ
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
            // ���� ������ ����
            if (enemy != null)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }
    }

    // �������� ��ũ�� �´� �������� ������Ʈ
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
}
