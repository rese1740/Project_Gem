using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public EnemyData enemyData;
    public float maxHp;
    public float currentHp;
    public float moveSpeed;
    public float enemyGold;
    private bool isDotActive = false;
    public bool isDead = false;

    [Header("Enemy UI")]
    public Text hpTxt;
    public GameObject hudDamageText;
    public Transform hudPos;

    private int currentWaypointIndex = 0;

    void Start()
    {
        enemyGold = enemyData.enemyGold;

        float bonusHp = GameManager.Instance.currentHpBonus;

        maxHp = enemyData.maxHp + bonusHp;
        currentHp = maxHp;

        moveSpeed = enemyData.moveSpeed;
        enemyData.Init();

        if (Waypoint.Points == null || Waypoint.Points.Length == 0)
        {
            Debug.LogError("��������Ʈ�� �������� �ʾҽ��ϴ�! Enemy ����");
            Destroy(gameObject);
        }
    }


    void Update()
    {
        Move();
        hpTxt.text = currentHp.ToString();
    }

    void Move()
    {
        if (Waypoint.Points.Length == 0) return;

        Transform target = Waypoint.Points[currentWaypointIndex];
        Vector3 direction = target.position - transform.position;


        // �̵�
        transform.position += direction.normalized * moveSpeed * Time.deltaTime;

        // ��ǥ ������ ���� ���������� ���� ��������Ʈ��
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypointIndex++;

            // ������ ����Ʈ�� �����ϸ� �ٽ� ó������ ����
            if (currentWaypointIndex >= Waypoint.Points.Length)
            {
                currentWaypointIndex = 0; // �ٽ� ó������
            }
        }
    }


    public void TakeDamage(float damage, string attackerItemID)
    {
        if (isDead) return;

        currentHp -= damage;

        if (damage <= 0) return;

        // ������ �ؽ�Ʈ ����
        GameObject hudText = Instantiate(hudDamageText);
        hudText.transform.position = hudPos.position;

        DamageText dt = hudText.GetComponent<DamageText>();
        dt.damage = damage;
        dt.itemID = attackerItemID;

        if (currentHp < 0)
        {
            Die();
        }
    }

    public void ApplySlow(float slowPercent, float duration)
    {
        if (isDead) return;

        StartCoroutine(SlowCoroutine(slowPercent, duration));
    }

    IEnumerator SlowCoroutine(float slowPercent, float duration)
    {
        float originalSpeed = moveSpeed;

        moveSpeed *= 1f - (slowPercent / 100f);

        yield return new WaitForSeconds(duration);

        moveSpeed = originalSpeed;
    }


    void Die()
    {
        if (!isDead)
        {
            GameManager.Instance.currentEnemyCount--;
            GameManager.Instance.gold += enemyGold;
            Destroy(gameObject);
            isDead = true;
        }

    }
}
