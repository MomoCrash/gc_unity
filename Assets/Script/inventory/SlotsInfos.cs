using UnityEngine;

[System.Serializable]
public class SlotsInfos
{
    public SlotsInfos(itemTemplate _template, int _number)
    {
        template = _template;

        name = template.name;
        number = _number;
    }

    [SerializeField, HideInInspector] public string name;

    [SerializeField] private int number = 0;

    private itemTemplate template { get; }

    public Sprite Icon => template.Icon;

    public int ItemId => template.ItemId;
    public int Number => number;   
    public int Stack => template.Stack;
}
