using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManagerUI : MonoBehaviour
{
    public List<Transform> uiSlots;             // UI 슬롯들 (예: 빈 카드 자리들)
    public GameObject[] cardPrefabList;         // 생성할 카드 프리팹 (UI용 프리팹, Image/Text 포함)
    public float cost;

    public void SpawnRandomCardInEmptySlot()
    {
        if (GameManager.Instance.gold < cost)
        {
            Debug.Log("골드가 부족합니다!");
            return;
        }

        List<Transform> emptySlots = new List<Transform>();

        foreach (var slot in uiSlots)
        {
            if (slot.childCount == 0)
            {
                emptySlots.Add(slot);
            }
        }

        if (emptySlots.Count == 0)
        {
            Debug.Log("빈 슬롯이 없습니다.");
            return;
        }

        // 랜덤 카드 프리팹과 슬롯 선택
        GameObject randomCardPrefab = cardPrefabList[Random.Range(0, cardPrefabList.Length)];
        Transform randomSlot = emptySlots[Random.Range(0, emptySlots.Count)];

        // 카드 인스턴스 생성
        GameObject cardInstance = Instantiate(randomCardPrefab, randomSlot);
        cardInstance.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        GameManager.Instance.gold -= cost;
    }
}
