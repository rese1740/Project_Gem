using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public EnemyData EnemyData;
    public float maxHp;
    public float currentHp;
    public float moveSpeed;
    private bool isDotActive = false;

    private int currentWaypointIndex = 0;

    void Start()
    {
        maxHp =EnemyData.maxHp;
        currentHp =EnemyData.currentHp;
        moveSpeed =EnemyData.moveSpeed;

        currentHp = maxHp;

        if (Waypoint.Points == null || Waypoint.Points.Length == 0)
        {
            Debug.LogError("��������Ʈ�� �������� �ʾҽ��ϴ�! Enemy ����");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Waypoint.Points.Length == 0) return;

        Transform target = Waypoint.Points[currentWaypointIndex];
        Vector3 direction = target.position - transform.position;

        // 2D ȸ�� (���⸸ �����ֱ�)
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        }

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


    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        Debug.Log($"���� {damage}, ���� ü�� {currentHp}");

        if (currentHp <= 0f)
        {
            Die();
        }
    }


    IEnumerator DealDotDamage(float damagePerTick, int tickCount, float interval)
    {
        isDotActive = true;

        for (int i = 0; i < tickCount; i++)
        {
            yield return new WaitForSeconds(interval);
            currentHp -= damagePerTick;

            if (currentHp <= 0f)
            {
                Die();
                isDotActive = false;
                yield break;
            }
        }

        isDotActive = false;
    }

    void Die()
    {
        Debug.Log(gameObject.name + "��(��) ����߽��ϴ�.");
        GameManager.Instance.currentEnemyCount--;
        GameManager.Instance.gold += GameManager.Instance.enemyGold;
        Destroy(gameObject);
    }
}
