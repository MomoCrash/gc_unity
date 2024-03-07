using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "scriptableObjects/Item")]

public class itemTemplate : ScriptableObject
{
    [Header("Main infos")]

    [SerializeField] private string itemName;

    [SerializeField] private int id;

    [SerializeField] private Sprite icon;

    [Header("Item infos")]

    [SerializeField] private int stack;

    public ItemType type;

    [Range(0f, 1f)]
    public float BaseResistance;
    [Range(0f, 1f)]
    public float EarthResistance;
    [Range(0f, 1f)]
    public float FireResistance;

    public float Health;
    public float Damage;

    [Range(0, 10)]
    public int DashBoost;
    [Range(0, 10)]
    public int JumpBoost;
    [Range(0, 10)]
    public float SpeedBoost;

    public string Name => itemName;

    public int ItemId => id;

    public Sprite Icon => icon;

    public int Stack => stack;
}

public enum ItemType
{
    SWORD,
    SHIELD,
    AMULET,
    HELMET,
    CHESTPLATE,
    RESSOURCE
}