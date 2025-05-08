using DG.Tweening;
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
    public float targetScale = 2f;  // 원하는 크기

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
        alpha = tmp.color;

        transform.localScale = Vector3.one * 0.1f;  
        transform.DOScale(Vector3.one * targetScale, 0.3f).SetEase(Ease.OutBack);  // 원하는 크기(targetScale)로 커짐
        transform.DOMoveY(transform.position.y + 1f * moveSpeed, 1f)
         .OnComplete(() => Destroy(gameObject));

        // 알파 값을 점차적으로 변경 (투명하게)
        tmp.DOFade(0, destroyTime).SetEase(Ease.Linear);

       
    }

   
}
