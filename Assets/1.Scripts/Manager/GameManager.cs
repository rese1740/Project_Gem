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

    [Header("UI")]
    public Text spawnCountTxt;
    public Text countDownTxt;
    public Text timerTxt;

    [Header("시간")]
    public float startTime = 10f;
    private float currentTime;
    private int minutes = 0;
    private int seconds = 0;
    private bool isRunning = false;

    [Header("몬스터 수")]
    private int currentWaveIndex = 0;
    public int currentEnemyCount;
    public int currentSpawnCount;
    public int maxSpawnCount = 50;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentTime = startTime;
        StartCoroutine(StopWatch());
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
        
    }

    IEnumerator StopWatch()
    {
        isRunning = true;

        while (isRunning)
        {
            timerTxt.text = $"{minutes:D2}:{seconds:D2}";
            yield return new WaitForSeconds(1f);

            seconds++;
            if (seconds >= 60)
            {
                seconds = 0;
                minutes++;
            }
        }
       
    }

    #region Wave 진행
    void StartWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            currentWave = waves[currentWaveIndex];
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
        currentEnemyCount = spawnList.Count;
        foreach (var enemyPrefab in spawnList)
        {
            SpawnEnemy(enemyPrefab);
            yield return new WaitForSeconds(currentWave.spawnDelay);
        }

        yield return new WaitForSeconds(currentWave.restTimeBetweenWaves);
        NextWave();
    }

    void SpawnEnemy(GameObject prefab)
    {
        Vector3 spawnPos = spawnPosition.position;
        spawnPos.z = 0f;
        Instantiate(prefab, spawnPos, Quaternion.identity);
        currentSpawnCount--;
    }

    void NextWave()
    {
        currentWaveIndex++;
        StartWave();
    }
    #endregion
    void UpdateTimerUI()
    {
        countDownTxt.text = Mathf.Ceil(currentTime).ToString();
    }
}
