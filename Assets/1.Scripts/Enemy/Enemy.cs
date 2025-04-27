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
        if (currentWaypointIndex < Waypoint.Points.Length)
        {
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
            }
        }
        else
        {
            // 마지막 웨이포인트 도착
            ReachDestination();
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

    void ReachDestination()
    {
        Debug.Log(gameObject.name + "이(가) 목적지에 도착했습니다.");
        GameManager.Instance.currentSpawnCount--;
        Destroy(gameObject);
    }

    void Die()
    {
        Debug.Log(gameObject.name + "이(가) 사망했습니다.");
        GameManager.Instance.currentSpawnCount--;
        Destroy(gameObject);
    }
}
