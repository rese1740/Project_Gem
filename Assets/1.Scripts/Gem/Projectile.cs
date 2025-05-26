using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;

    private Vector3 origin;
    private Enemy target;
    private float damage;
    private float dotDamage;
    private float critChance;
    private float slowValue;
    private string itemID;

    public void Init(Vector3 originPos, Enemy targetEnemy, float dmg, float dot, float crit, float slow, string id)
    {
        origin = originPos;
        transform.position = origin; // 이 줄이 중요!
        target = targetEnemy;
        damage = dmg;
        dotDamage = dot;
        critChance = crit;
        slowValue = slow;
        itemID = id;
    }

    void Update()
    {
        if (target == null || target.isDead)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (target.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            Impact();
        }
    }

    void Impact()
    {
        float finalDamage = damage;
        if (Random.value <= critChance)
        {
            finalDamage *= 2f;
        }

        target.TakeDamage(finalDamage, itemID);

        if (dotDamage > 0)
        {
            target.StartCoroutine(DealDotDamage(target, dotDamage, 10, 0.2f));
        }

        if (slowValue > 0)
        {
            target.ApplySlow(slowValue, 10f);
        }

        Destroy(gameObject);
    }

    IEnumerator DealDotDamage(Enemy enemy, float damagePerTick, int tickCount, float interval)
    {
        for (int i = 0; i < tickCount; i++)
        {
            yield return new WaitForSeconds(interval);
            if (enemy != null && !enemy.isDead)
            {
                enemy.TakeDamage(damagePerTick, itemID);
            }
        }
    }
}
