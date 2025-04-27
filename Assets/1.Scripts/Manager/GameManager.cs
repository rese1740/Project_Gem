using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform spawnPosition;
    public GameObject enemyPrefab;

    public int[] enemyCountsPerWave = { 5, 10, 15 }; // 웨이브별 소환할 적 수
    public float spawnDelay = 1f; // 적 소환 간격
    public float restTimeBetweenWaves = 3f; // 웨이브 간 휴식 시간

    private int currentWaveIndex = 0;
    public int currentSpawnCount;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {
        if (currentSpawnCount <= 0)
        {
            // 몬스터가 모두 소멸하면 휴식 시간을 주고, 다음 웨이브 시작
            if (!IsInvoking("NextWave")) // 이미 다음 웨이브가 진행 중이 아닌지 체크
            {
                Invoke("NextWave", restTimeBetweenWaves);
            }
        }
    }

    IEnumerator SpawnEnemies()
    {
        int spawnCount = enemyCountsPerWave[currentWaveIndex];
        currentSpawnCount = spawnCount;

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPos = spawnPosition.position;
            spawnPos.z = 0f; // 2D이니까 z = 0 고정
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
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
}
