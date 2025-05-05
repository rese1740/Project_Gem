using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public EnemyData EnemyData;
    public float maxHp;
    public float currentHp;
    public float moveSpeed;
    private bool isDotActive = false;
    public bool isDead = false;
    public Text hpTxt;

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


    public void TakeDamage(float damage)
    {
        if(isDead) return;

        currentHp -= damage;
       

        if (currentHp <= 0f)
        {
            isDead = true;
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
