using System.Collections;
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

    [Header("UI")]
    public Text spawnCountTxt;
    public Text timerText; 
    public float startTime = 10f;
    private float currentTime;


    [Header("���� ��")]
    private int currentWaveIndex = 0;
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
        if (currentSpawnCount <= 0)
        {
            if (!IsInvoking("NextWave")) 
            {
                Invoke("NextWave", restTimeBetweenWaves);
            }
        }
        spawnCountTxt.text = $"{currentSpawnCount}/{maxSpawnCount}";

        if(currentSpawnCount >= maxSpawnCount)
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
            }
        }
        else
        {
            timerText.gameObject.SetActive(false);
            currentTime = 0;
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

    void UpdateTimerUI()
    {
        timerText.text = Mathf.Ceil(currentTime).ToString(); // �Ҽ��� ���� ǥ��
    }
}
