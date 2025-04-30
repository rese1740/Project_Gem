using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<Transform> spawnPoints;           // 오브젝트를 소환할 수 있는 위치들 (월드 상의 Transform)
    public GameObject[] prefabList;               // 생성할 게임 오브젝트 프리팹들
    public float _cost;

    public void SpawnRandomPrefabAtRandomPoint()
    {
        if (GameManager.Instance.gold < _cost)
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
            Debug.Log("빈 스폰 포인트가 없습니다.");
            return;
        }

        // 랜덤 프리팹과 포인트 선택
        GameObject randomPrefab = prefabList[Random.Range(0, prefabList.Length)];
        Transform randomPoint = emptyPoints[Random.Range(0, emptyPoints.Count)];

        // 프리팹 인스턴스 생성 및 위치 설정
        GameObject instance = Instantiate(randomPrefab, randomPoint.position, Quaternion.identity);
        instance.transform.SetParent(randomPoint); // 포인트에 자식으로 넣으면 중복 방지 가능

        GameManager.Instance.gold -= _cost;
    }
}
