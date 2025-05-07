using UnityEngine;

[CreateAssetMenu(menuName = "Gem/GemData")]
public class GemData : ScriptableObject
{
    public string itemID;
    public Sprite[] rankIcons;
    public int maxRank = 4;
    public int rank;

    public GemStats[] statsPerRank; // 랭크별 능력치 배열

    public GemStats GetStatsByRank(int rank)
    {
        // 배열은 0부터 시작하므로, rank 1이면 index 0
        int index = Mathf.Clamp(rank - 1, 0, statsPerRank.Length - 1);
        return statsPerRank[index];
    }
}

[System.Serializable]
public class GemStats
{
    public float attackValue;
    public float attackSpeed;
    public float dotDamage;
    public float slowValue;
    public float attackRange;
    public float critValue;
}


