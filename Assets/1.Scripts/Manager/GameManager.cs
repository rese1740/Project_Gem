using System.Collections;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform spawnPosition;
    public GameObject enemyPrefab;

    public int[] enemyCountsPerWave = { 5, 10, 15 };
    public float spawnDelay = 1f;
    public float restTimeBetweenWaves = 3f;

    [Header("재화")]
    public float gold;
    public float enemyGold;
    public Text goldText;


    [Header("UI")]
    public Text spawnCountTxt;
    public Text timerText;
    public float startTime = 10f;
    private float currentTime;


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
        StartCoroutine(SpawnEnemies());
        currentTime = startTime;
    }

    void Update()
    {

        spawnCountTxt.text = $"{currentEnemyCount}/{maxSpawnCount}";
        goldText.text = gold.ToString();

        if (currentSpawnCount >= maxSpawnCount)
        {
            timerText.gameObject.SetActive(true);
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
                    timerText.color = Color.red;
                }
            }
        }
        else
        {
            timerText.gameObject.SetActive(false);
            currentTime = 10;
        }

    }


    IEnumerator SpawnEnemies()
    {
        int spawnCount = enemyCountsPerWave[currentWaveIndex];
        currentSpawnCount = spawnCount;  // 해당 웨이브의 몬스터 수를 설정
        currentEnemyCount += currentSpawnCount;

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPos = spawnPosition.position;
            spawnPos.z = 0f; // 2D이니까 z = 0 고정
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            currentSpawnCount--; // 몬스터가 하나 스폰되면 남은 몬스터 수를 감소
            yield return new WaitForSeconds(spawnDelay);  // 각 몬스터 간의 대기 시간
        }

        // 웨이브가 끝나면 잠시 대기 후 다음 웨이브 시작
        yield return new WaitForSeconds(restTimeBetweenWaves);  // 웨이브 사이의 휴식 시간
        NextWave();
    }

    void NextWave()
    {
        currentWaveIndex++;

        if (currentWaveIndex < enemyCountsPerWave.Length)
        {
            Debug.Log("휴식 후 다음 웨이브 시작!");
            StartCoroutine(SpawnEnemies());
        }
        else
        {
            Debug.Log("모든 웨이브 클리어!");
            // 게임 클리어 처리 가능
        }
    }

    void UpdateTimerUI()
    {
        timerText.text = Mathf.Ceil(currentTime).ToString(); // 소수점 없이 표시
    }
}
