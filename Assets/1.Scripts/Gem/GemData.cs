using UnityEngine;

[CreateAssetMenu(menuName = "Gem/GemData")]
public class GemData : ScriptableObject
{
    public string itemID;
    public Sprite[] rankIcons;
    public int maxRank = 4;
    public int rank;

    public GemStats[] statsPerRank; // ��ũ�� �ɷ�ġ �迭

    public GemStats GetStatsByRank(int rank)
    {
        // �迭�� 0���� �����ϹǷ�, rank 1�̸� index 0
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


