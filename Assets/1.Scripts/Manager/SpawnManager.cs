using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManagerUI : MonoBehaviour
{
    public List<Transform> uiSlots;             // UI ���Ե� (��: �� ī�� �ڸ���)
    public GameObject[] cardPrefabList;         // ������ ī�� ������ (UI�� ������, Image/Text ����)
    public float cost;

    public void SpawnRandomCardInEmptySlot()
    {
        if (GameManager.Instance.gold < cost)
        {
            Debug.Log("��尡 �����մϴ�!");
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
            Debug.Log("�� ������ �����ϴ�.");
            return;
        }

        // ���� ī�� �����հ� ���� ����
        GameObject randomCardPrefab = cardPrefabList[Random.Range(0, cardPrefabList.Length)];
        Transform randomSlot = emptySlots[Random.Range(0, emptySlots.Count)];

        // ī�� �ν��Ͻ� ����
        GameObject cardInstance = Instantiate(randomCardPrefab, randomSlot);
        cardInstance.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        GameManager.Instance.gold -= cost;
    }
}
