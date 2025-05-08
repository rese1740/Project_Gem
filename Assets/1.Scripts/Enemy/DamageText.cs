using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float moveSpeed = 1f;    
    public float alphaSpeed = 1f;
    public float destroyTime = 1f;
    public float damage;
    TextMeshProUGUI tmp;
    Color alpha;

    private void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        alpha = tmp.color;
        tmp.text = damage.ToString();
        Destroy(gameObject,destroyTime);
    }

    private void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        tmp.color = alpha;
    }
}
