using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager2D : MonoBehaviour
{
    public List<Transform> spawnPoints;           
    public GameObject[] cardPrefabList;           
    public int gemCost, emeraldCost, rubyCost, diamondCost,sapphireCost;
    [SerializeField] private TextMeshProUGUI[] requiredGold;
    [SerializeField] private int upgradeGold = 5;


    private void Update()
    {
        requiredGold[0].text = gemCost.ToString();
        requiredGold[1].text = rubyCost.ToString();
        requiredGold[2].text = emeraldCost.ToString();
        requiredGold[3].text = diamondCost.ToString();
        requiredGold[4].text = sapphireCost.ToString();
    }
    public void SpawnCard(int index, ref int cost)
    {
        if (GameManager.Instance.gold < cost)
        {
            Debug.Log("��尡 �����մϴ�!");
            return;
        }

        // �� ���� Ž��
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

        // ���� �� ���� ���� �� ��� �ڽ����� ����
        Transform randomPoint = emptyPoints[Random.Range(0, emptyPoints.Count)];

        GameObject cardInstance = Instantiate(cardPrefabList[index], randomPoint.position, Quaternion.identity, randomPoint); // <- �θ� �ٷ� ����
        GameManager.Instance.gold -= cost;
        cost += upgradeGold;
    }


    public void SpawnGem() => SpawnCard(0, ref gemCost);
    public void SpawnRuby() => SpawnCard(1, ref rubyCost);
    public void SpawnEmerald() => SpawnCard(2, ref emeraldCost);
    public void SpawnDiaMond() => SpawnCard(3, ref diamondCost);
    public void SpawnSapphire() => SpawnCard(4, ref sapphireCost);

}
