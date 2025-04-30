using UnityEngine;

[CreateAssetMenu(menuName = "Gem/GemData")]
public class GemData : ScriptableObject
{
    public string itemID;           // ������ ���п� ID
    public Sprite[] rankIcons;             // ������ ������
    public float attackValue;       // ���ݷ�
    public float attackSpeed;       // ���� �ӵ�
    public int rank;
    public int maxRank = 4; 
}

