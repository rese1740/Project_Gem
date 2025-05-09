using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager2D : MonoBehaviour
{
    public List<Transform> spawnPoints;             // 2D 상에 카드가 배치될 위치들
    public GameObject[] cardPrefabList;             // 스프라이트 기반 2D 카드 프리팹들
    public float cost;
    [SerializeField] private TextMeshProUGUI requiredGold;


    private void Update()
    {
        requiredGold.text = cost.ToString();
    }
    public void SpawnRandomCardAtEmptyPoint()
    {
        if (GameManager.Instance.gold < cost)
        {
            Debug.Log("골드가 부족합니다!");
            return;
        }

        List<Transform> emptyPoints = new List<Transform>();

        foreach (var point in spawnPoints)
        {
            if (point.childCount == 0)
            {
                emptyPoints.Add(point);
            }
        }

        if (emptyPoints.Count == 0)
        {
            Debug.Log("빈 위치가 없습니다.");
            return;
        }

        // 랜덤 카드 프리팹과 위치 선택
        GameObject randomCardPrefab = cardPrefabList[Random.Range(0, cardPrefabList.Length)];
        Transform randomPoint = emptyPoints[Random.Range(0, emptyPoints.Count)];

        
        GameObject cardInstance = Instantiate(randomCardPrefab, randomPoint.position, Quaternion.identity);
        cardInstance.transform.SetParent(randomPoint);  // 부모 설정으로 슬롯 정보 유지

        GameManager.Instance.gold -= cost;
        cost += 2;
    }
}
