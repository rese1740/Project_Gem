using UnityEngine;

[CreateAssetMenu(menuName = "Item/ItemData")]
public class JamData : ScriptableObject
{
    public string itemID;           // 아이템 구분용 ID
    public Sprite[] rankIcons;             // 아이템 아이콘
    public float attackValue;       // 공격력
    public float attackSpeed;       // 공격 속도
    public int rank;
    public int maxRank = 4; 
}

