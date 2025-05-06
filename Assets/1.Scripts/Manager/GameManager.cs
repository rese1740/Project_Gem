using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("스폰 설정")]
    public Transform spawnPosition;
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;

    public WaveData[] waves; // 여러 웨이브 데이터를 받아올 수 있도록 배열로
    private WaveData currentWave;

    [Header("재화")]
    public float gold;
    public float enemyGold;
    public Text goldText;

    [Header("Wave")]
    private int currentWaveIndex = 0;
    private int displayWaveIndex = 0;
    public Text waveTxt;
    public Text restTxt;

    [Header("시간")]
    public float startTime = 10f;
    private float currentTime;
    public float sec = 0f;

    [Header("몬스터 수")]
    public int currentEnemyCount;
    public int currentSpawnCount;
    public int maxSpawnCount = 50;
    public Text spawnCountTxt;
    public Text countDownTxt;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentTime = startTime;
        StartWave();
    }

    void Update()
    {
        spawnCountTxt.text = $"{currentEnemyCount}/{maxSpawnCount}";
        goldText.text = gold.ToString();

        if (currentEnemyCount >= maxSpawnCount)
        {
            countDownTxt.gameObject.SetActive(true);
            UpdateTimerUI();
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                if (currentTime < 0)
                {
                    SceneManager.LoadScene("Test");
                }
                else if (currentTime <= 3)
                {
                    countDownTxt.color = Color.red;
                }
            }
        }
        else
        {
            countDownTxt.gameObject.SetActive(false);
            currentTime = startTime;
        }

        waveTxt.text = $"{displayWaveIndex} Wave";
        restTxt.text = sec.ToString();
        
    }

 

    #region Wave 진행
    void StartWave()
    {
        
        if (currentWaveIndex < waves.Length)
        {
            currentWave = waves[currentWaveIndex];
            displayWaveIndex++;
            StartCoroutine(SpawnEnemies());
        }
        else
        {
            Debug.Log("모든 웨이브 클리어!");
        }
    }

    IEnumerator SpawnEnemies()
    {
        List<GameObject> spawnList = new List<GameObject>();
        Debug.Log(currentWave);
   
        for (int i = 0; i < currentWave.enemy1Counts; i++) spawnList.Add(enemy1Prefab);
        for (int i = 0; i < currentWave.enemy2Counts; i++) spawnList.Add(enemy2Prefab);
        for (int i = 0; i < currentWave.enemy3Counts; i++) spawnList.Add(enemy3Prefab);

        for (int i = 0; i < spawnList.Count; i++)
        {
            int rand = Random.Range(i, spawnList.Count);
            var temp = spawnList[i];
            spawnList[i] = spawnList[rand];
            spawnList[rand] = temp;
        }
        currentSpawnCount = spawnList.Count;
        foreach (var enemyPrefab in spawnList)
        {
            SpawnEnemy(enemyPrefab);
            yield return new WaitForSeconds(currentWave.spawnDelay);
        }

        StartCoroutine(TimerDown());
        restTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(currentWave.restTimeBetweenWaves);
        NextWave();
    }

    void SpawnEnemy(GameObject prefab)
    {
        Vector3 spawnPos = spawnPosition.position;
        spawnPos.z = 0f;
        Instantiate(prefab, spawnPos, Quaternion.identity);
        currentSpawnCount--;
        currentEnemyCount++;
    }

    void NextWave()
    {
        currentWaveIndex++;
        StartWave();
    }
    #endregion

    IEnumerator TimerDown()
    {
        sec = 20;
        for(int i = 0; i < currentWave.restTimeBetweenWaves; i++)
        {
         sec -= 1;
         yield return new WaitForSeconds(1f);
        }
        restTxt.gameObject.SetActive(false);
    }

    void UpdateTimerUI()
    {
        countDownTxt.text = Mathf.Ceil(currentTime).ToString();
    }
}
