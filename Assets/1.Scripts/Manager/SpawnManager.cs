using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> slotList = new List<GameObject>();    // ���� ������Ʈ�� (DroppableUI �پ� ����)
    public GameObject[] prefabList;                                // ������ UI �����յ�

    public float _cost;

    public void SpawnRandomPrefabInRandomSlot()
    {
        if (GameManager.Instance.gold < _cost)
        {
            Debug.Log("��尡 �����մϴ�!");
            return;
        }
        List<GameObject> emptySlots = new List<GameObject>();

        foreach (var slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                emptySlots.Add(slot);
            }
        }

        // �� ������ ������ ����
        if (emptySlots.Count == 0)
        {
            Debug.Log("�� ������ �����ϴ�.");
            return;
        }

        // �����հ� ������ ���� ����
        GameObject randomPrefab = prefabList[Random.Range(0, prefabList.Length)];
        GameObject randomSlot = emptySlots[Random.Range(0, emptySlots.Count)];

        // ������ �ν��Ͻ� ���� �� ���Կ� ��ġ
        GameObject instance = Instantiate(randomPrefab, randomSlot.transform);
        RectTransform rect = instance.GetComponent<RectTransform>();
        rect.position = randomSlot.GetComponent<RectTransform>().position;
        GameManager.Instance.gold -= _cost;
    }
}
