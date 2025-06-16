using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("스폰 설정")]
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private GameObject enemy1Prefab;
    [SerializeField] private GameObject enemy2Prefab;
    [SerializeField] private GameObject enemy3Prefab;
    [SerializeField] private GameObject bossPrefab;
    

   [SerializeField] private WaveData[] waves; 
    private WaveData currentWave;

    [Header("재화")]
    public float gold;
    public float maxGold;
    public float goldUpGradeRequired;
    [SerializeField] private TextMeshProUGUI goldTxt;


    [Header("Wave")]
    private int currentWaveIndex = 0;
    private int displayWaveIndex = 0;
    [SerializeField] private TextMeshProUGUI waveTxt;
    [SerializeField] private TextMeshProUGUI restTxt;

    [Header("시간")]
    [SerializeField] private float startTime = 10f;
    private float currentTime;
    [SerializeField] private float sec = 0f;

    [Header("몬스터 수")]
    [SerializeField] private GameObject End_Panel;
    public Image WarringImg;
    public Sprite[] WarringSprite;
    private bool isWarring = false;
    public int currentEnemyCount;
    [SerializeField] private int currentSpawnCount;
    [SerializeField] private int maxSpawnCount = 50;
    [SerializeField] private TextMeshProUGUI spawnCountTxt;
    [SerializeField] private TextMeshProUGUI countDownTxt;

    [Header("점수")]
    public ScoreData ScoreData;
    public int currentscore;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI currentScoreTxt;
    [SerializeField] private TextMeshProUGUI maxScoreTxt;


    [Header("Enemy Scaling")]
    [SerializeField] private float hpIncreasePerWave = 30f;
    public float currentHpBonus = 0f;

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
        waveTxt.text = $"Wave : {displayWaveIndex}";
        restTxt.text = sec.ToString();
        goldTxt.text = $"{gold} / {maxGold}";
        scoreTxt.text = currentscore.ToString();

        //점수판
        currentScoreTxt.text = currentscore.ToString();
        maxScoreTxt.text = ScoreData.maxScore.ToString();  

        if(currentscore >= ScoreData.maxScore)
        {
            ScoreData.maxScore = currentscore;
        }
        

        if (gold >= maxGold)
            gold = maxGold;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (currentEnemyCount >= maxSpawnCount)
        {
            if(isWarring == false)
            {
                isWarring = true;
            WarringImg.gameObject.SetActive(true);
            StartCoroutine(LogWarring());
            }

            countDownTxt.gameObject.SetActive(true);
            UpdateTimerUI();
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                if (currentTime < 0)
                {
                    Time.timeScale = 0;
                    End_Panel.SetActive(true);
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
            WarringImg.gameObject .SetActive(false);
            isWarring = false;
            currentTime = startTime;
        }
    }

    IEnumerator LogWarring()
    {
        while (true)
        {
            WarringImg.sprite = WarringSprite[0];
            yield return new WaitForSeconds(0.5f);
            WarringImg.sprite = WarringSprite[1];
            yield return new WaitForSeconds(0.5f);
        }
    }

    #region Wave 진행
    void StartWave()
    {
        if (currentWaveIndex < waves.Length)
        {
            currentHpBonus = currentWaveIndex * hpIncreasePerWave;

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
        if (currentWaveIndex % 4 == 0 && currentWaveIndex != 0)
        {
            spawnList.Insert(0, bossPrefab); // 맨 앞에 보스 추가
            Debug.Log("보스 등장!");
        }
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


    #region 타이머
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

    public void GoldUpGradeBtn()
    {
        if(gold >= goldUpGradeRequired)
        {
            maxGold += 15;
            gold -= goldUpGradeRequired;
            goldUpGradeRequired += 15;
        }
        
    }
    #endregion
}
