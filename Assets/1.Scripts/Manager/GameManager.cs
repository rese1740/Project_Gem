using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform spawnPosition;
    public GameObject enemyPrefab;

    public int[] enemyCountsPerWave = { 5, 10, 15 }; // ���̺꺰 ��ȯ�� �� ��
    public float spawnDelay = 1f; // �� ��ȯ ����
    public float restTimeBetweenWaves = 3f; // ���̺� �� �޽� �ð�

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
            // ���Ͱ� ��� �Ҹ��ϸ� �޽� �ð��� �ְ�, ���� ���̺� ����
            if (!IsInvoking("NextWave")) // �̹� ���� ���̺갡 ���� ���� �ƴ��� üũ
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
            spawnPos.z = 0f; // 2D�̴ϱ� z = 0 ����
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void NextWave()
    {
        currentWaveIndex++;

        if (currentWaveIndex < enemyCountsPerWave.Length)
        {
            Debug.Log("�޽� �� ���� ���̺� ����!");
            StartCoroutine(SpawnEnemies());
        }
        else
        {
            Debug.Log("��� ���̺� Ŭ����!");
            // ���� Ŭ���� ó�� ����
        }
    }
}
