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
public class inventoryData : MonoBehaviour
{
    [SerializeField] private int slotNumber;
    [SerializeField] private SlotsInfos[] slotsInfos;

    [SerializeField] private itemTemplate[] templates;

    private inventoryControll controll;
    public void Init(inventoryControll _controll)
    {
        controll = _controll;

        slotsInfos = new SlotsInfos[slotNumber];
        for (int i = 0; i < slotNumber; i++) 
        {
            slotsInfos[i] = new SlotsInfos(templates[0], 0);
        }

        slotsInfos[0] = new SlotsInfos(templates[1], 1);
    }

    public int SlotNumber => slotNumber;
    public SlotsInfos[] Slots => slotsInfos;

}
