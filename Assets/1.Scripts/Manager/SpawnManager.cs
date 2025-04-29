using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> slotList = new List<GameObject>();    // 슬롯 오브젝트들 (DroppableUI 붙어 있음)
    public GameObject[] prefabList;                                // 생성할 UI 프리팹들

    public float _cost;

    public void SpawnRandomPrefabInRandomSlot()
    {
        if (GameManager.Instance.gold < _cost)
        {
            Debug.Log("골드가 부족합니다!");
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

        // 빈 슬롯이 없으면 리턴
        if (emptySlots.Count == 0)
        {
            Debug.Log("빈 슬롯이 없습니다.");
            return;
        }

        // 프리팹과 슬롯을 랜덤 선택
        GameObject randomPrefab = prefabList[Random.Range(0, prefabList.Length)];
        GameObject randomSlot = emptySlots[Random.Range(0, emptySlots.Count)];

        // 프리팹 인스턴스 생성 및 슬롯에 배치
        GameObject instance = Instantiate(randomPrefab, randomSlot.transform);
        RectTransform rect = instance.GetComponent<RectTransform>();
        rect.position = randomSlot.GetComponent<RectTransform>().position;
        GameManager.Instance.gold -= _cost;
    }
}
