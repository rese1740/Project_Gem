using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<Transform> spawnPoints;           // ������Ʈ�� ��ȯ�� �� �ִ� ��ġ�� (���� ���� Transform)
    public GameObject[] prefabList;               // ������ ���� ������Ʈ �����յ�
    public float _cost;

    public void SpawnRandomPrefabAtRandomPoint()
    {
        if (GameManager.Instance.gold < _cost)
        {
            Debug.Log("��尡 �����մϴ�!");
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
            Debug.Log("�� ���� ����Ʈ�� �����ϴ�.");
            return;
        }

        // ���� �����հ� ����Ʈ ����
        GameObject randomPrefab = prefabList[Random.Range(0, prefabList.Length)];
        Transform randomPoint = emptyPoints[Random.Range(0, emptyPoints.Count)];

        // ������ �ν��Ͻ� ���� �� ��ġ ����
        GameObject instance = Instantiate(randomPrefab, randomPoint.position, Quaternion.identity);
        instance.transform.SetParent(randomPoint); // ����Ʈ�� �ڽ����� ������ �ߺ� ���� ����

        GameManager.Instance.gold -= _cost;
    }
}
