using UnityEngine;
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
    }

    public void SwitchSlots(int _slot1, int _slot2)
    {
        if (_slot1 == _slot2 || _slot1 >= slotsInfos.Length || _slot2 >= slotsInfos.Length || slotsInfos[_slot1].ItemId == 0) return;

        SlotsInfos _save1 = slotsInfos[_slot1];
        SlotsInfos _save2 = slotsInfos[_slot2];

        slotsInfos[_slot1] = _save2;
        slotsInfos[_slot2] = _save1;
    }

    public bool HasItem(int _index)
    {
        return slotsInfos[_index].ItemId != 0;
    }

    public void SetItem(int _index, itemTemplate _item, int amount)
    {
        slotsInfos[_index] = new SlotsInfos(_item, amount);
    }

    public int SlotNumber => slotNumber;
    public SlotsInfos[] Slots => slotsInfos;

}
