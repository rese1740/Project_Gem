using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float maxHp = 100f;
    public float currentHp;
    public float moveSpeed = 2f;

    private int currentWaypointIndex = 0;

    void Start()
    {
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
        Debug.Log(currentHp);
        currentHp -= damage;
        if (currentHp <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + "��(��) ����߽��ϴ�.");
        GameManager.Instance.currentEnemyCount--;
        GameManager.Instance.gold += GameManager.Instance.enemyGold;
        Destroy(gameObject);
    }
}
