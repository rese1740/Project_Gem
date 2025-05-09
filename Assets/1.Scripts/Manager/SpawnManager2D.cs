using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager2D : MonoBehaviour
{
    public List<Transform> spawnPoints;             // 2D �� ī�尡 ��ġ�� ��ġ��
    public GameObject[] cardPrefabList;             // ��������Ʈ ��� 2D ī�� �����յ�
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
            Debug.Log("�� ��ġ�� �����ϴ�.");
            return;
        }

        // ���� ī�� �����հ� ��ġ ����
        GameObject randomCardPrefab = cardPrefabList[Random.Range(0, cardPrefabList.Length)];
        Transform randomPoint = emptyPoints[Random.Range(0, emptyPoints.Count)];

        
        GameObject cardInstance = Instantiate(randomCardPrefab, randomPoint.position, Quaternion.identity);
        cardInstance.transform.SetParent(randomPoint);  // �θ� �������� ���� ���� ����

        GameManager.Instance.gold -= cost;
        cost += 2;
    }
}
