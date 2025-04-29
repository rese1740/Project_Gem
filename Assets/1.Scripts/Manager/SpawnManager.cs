using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> slotList = new List<GameObject>();    // ���� ������Ʈ�� (DroppableUI �پ� ����)
    public GameObject[] prefabList;                                // ������ UI �����յ�

    /// <summary>
    /// �� ���� �� �ϳ��� ������ �������� ����
    /// </summary>
    public void SpawnRandomPrefabInRandomSlot()
    {
        // �� ���Ը� ����
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
    }
}
