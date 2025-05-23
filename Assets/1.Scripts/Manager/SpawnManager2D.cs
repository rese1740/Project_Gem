using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager2D : MonoBehaviour
{
    public List<Transform> spawnPoints;           
    public GameObject[] cardPrefabList;           
    public float gemCost, emeraldCost, rubyCost, diamondCost,sapphireCost;
    [SerializeField] private TextMeshProUGUI[] requiredGold;
    [SerializeField] private float upgradeGold = 5f;


    private void Update()
    {
        requiredGold[0].text = gemCost.ToString();
        requiredGold[1].text = rubyCost.ToString();
        requiredGold[2].text = emeraldCost.ToString();
        requiredGold[3].text = diamondCost.ToString();
        requiredGold[4].text = sapphireCost.ToString();
    }
    public void SpawnGem()
    {
        if (GameManager.Instance.gold < gemCost)
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

        Transform randomPoint = emptyPoints[Random.Range(0, emptyPoints.Count)];

        
        GameObject cardInstance = Instantiate(cardPrefabList[0], randomPoint.position, Quaternion.identity);
        cardInstance.transform.SetParent(randomPoint);  

        GameManager.Instance.gold -= gemCost;
        gemCost += upgradeGold;
    }

    public void SpawnRuby()
    {
        if (GameManager.Instance.gold < rubyCost)
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

        Transform randomPoint = emptyPoints[Random.Range(0, emptyPoints.Count)];


        GameObject cardInstance = Instantiate(cardPrefabList[1], randomPoint.position, Quaternion.identity);
        cardInstance.transform.SetParent(randomPoint);

        GameManager.Instance.gold -= rubyCost;
        rubyCost += upgradeGold;
    }

    public void SpawnEmerald()
    {
        if (GameManager.Instance.gold < emeraldCost)
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

        Transform randomPoint = emptyPoints[Random.Range(0, emptyPoints.Count)];


        GameObject cardInstance = Instantiate(cardPrefabList[2], randomPoint.position, Quaternion.identity);
        cardInstance.transform.SetParent(randomPoint);

        GameManager.Instance.gold -= emeraldCost;
        emeraldCost += upgradeGold;
    }

    public void SpawnDiaMond()
    {
        if (GameManager.Instance.gold < diamondCost)
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

        Transform randomPoint = emptyPoints[Random.Range(0, emptyPoints.Count)];


        GameObject cardInstance = Instantiate(cardPrefabList[3], randomPoint.position, Quaternion.identity);
        cardInstance.transform.SetParent(randomPoint);

        GameManager.Instance.gold -= diamondCost;
        diamondCost += upgradeGold;
    }

    public void SpawnSapphire()
    {
        if (GameManager.Instance.gold < sapphireCost)
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

        Transform randomPoint = emptyPoints[Random.Range(0, emptyPoints.Count)];


        GameObject cardInstance = Instantiate(cardPrefabList[4], randomPoint.position, Quaternion.identity);
        cardInstance.transform.SetParent(randomPoint);

        GameManager.Instance.gold -= sapphireCost;
        sapphireCost += upgradeGold;
    }
}
