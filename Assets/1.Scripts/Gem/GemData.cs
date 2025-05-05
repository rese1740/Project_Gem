using UnityEngine;

[CreateAssetMenu(menuName = "Gem/GemData")]
public class GemData : ScriptableObject
{
    public string itemID;           // 아이템 구분용 ID
    public Sprite[] rankIcons;      // 아이템 아이콘
    public float attackValue;       // 공격력
    public float dotDamage;
    public float attackSpeed;       // 공격 속도
    public float attackRange;
    public int rank;
    public int maxRank = 4; 
}

