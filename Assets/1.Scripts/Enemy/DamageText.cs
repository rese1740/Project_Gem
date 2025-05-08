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
    TextMeshPro tmp;
    Color alpha;
    public string itemID;

    private void Start()
    {
        tmp = GetComponent<TextMeshPro>();
        tmp.text = damage.ToString();

        switch (itemID)
        {
            case "Emerald":
                tmp.color = Color.green;
                break;
            case "Ruby":
                tmp.color = Color.red;
                break;
            case "Sapphire":
                tmp.color = Color.blue;
                break;
            case "Diamond":
                tmp.color = Color.cyan;
                break;
            default:
                tmp.color = Color.white;
                break;
        }

        Destroy(gameObject,destroyTime);
        alpha = tmp.color;
    }

    private void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        tmp.color = alpha;
    }
}
