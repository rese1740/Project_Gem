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
            Debug.LogError("웨이포인트가 설정되지 않았습니다! Enemy 삭제");
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

        // 2D 회전 (방향만 맞춰주기)
        if (direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        }

        // 이동
        transform.position += direction.normalized * moveSpeed * Time.deltaTime;

        // 목표 지점에 거의 도착했으면 다음 웨이포인트로
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypointIndex++;

            // 마지막 포인트에 도달하면 다시 처음으로 루프
            if (currentWaypointIndex >= Waypoint.Points.Length)
            {
                currentWaypointIndex = 0; // 다시 처음으로
            }
        }
    }


    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        Debug.Log($"피해 {damage}, 남은 체력 {currentHp}");

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
        Debug.Log(gameObject.name + "이(가) 사망했습니다.");
        GameManager.Instance.currentEnemyCount--;
        GameManager.Instance.gold += GameManager.Instance.enemyGold;
        Destroy(gameObject);
    }
}
