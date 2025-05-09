using UnityEngine;

[CreateAssetMenu(menuName = "Gem/GemData")]
public class GemData : ScriptableObject
{
    public string itemID;
    public Sprite[] rankIcons;
    public int maxRank = 4;
    public int rank;

    public GemStats[] statsPerRank; 

    public GemStats GetStatsByRank(int rank)
    {
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


