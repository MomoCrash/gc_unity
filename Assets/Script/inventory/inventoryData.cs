using UnityEngine;
public class inventoryData : MonoBehaviour
{
    public int addedSlots;
    [SerializeField] private int slotNumber;
     
    [SerializeField] private SlotsInfos[] slotsInfos;

    [SerializeField] private itemTemplate[] templates;

    private inventoryControll controll;
    public void Init(inventoryControll _controll)
    {
        controll = _controll;
        slotNumber += addedSlots;

        if (PlayerPrefs.HasKey("Slot_Number"))
        {
            var SlotCount = PlayerPrefs.GetInt("Slot_Number");
            slotsInfos = new SlotsInfos[SlotCount];
            slotNumber = SlotCount;
            for (int i = 0; i < SlotCount; i++)
            {
                slotsInfos[i] = new SlotsInfos(templates[PlayerPrefs.GetInt("Slot_" + i + "_Item")], PlayerPrefs.GetInt("Slot_" + i + "_Amount"));
            }
        } else
        {
            slotsInfos = new SlotsInfos[slotNumber];
            for (int i = 0; i < slotNumber; i++)
            {
                slotsInfos[i] = new SlotsInfos(templates[0], 0);
            }
        }
    }

    public void SwitchSlots(int _slot1, int _slot2)
    {
        bool isRessourceSlotEnd = controll.display.slots[_slot2].ItemType == ItemType.RESSOURCE;

        bool isNotRessourceSlotStart = controll.display.slots[_slot1].ItemType != ItemType.RESSOURCE;
        bool isArmorInSlotStart = controll.display.slots[_slot1].ItemType == slotsInfos[_slot2].template.type;
        bool isEmptySlot = slotsInfos[_slot2].template.ItemId == 0;

        bool isSameRessourceType = controll.display.slots[_slot2].ItemType == slotsInfos[_slot1].template.type;

        if (_slot1 == _slot2 || _slot1 >= slotsInfos.Length || _slot2 >= slotsInfos.Length || slotsInfos[_slot1].ItemId == 0 || (!isRessourceSlotEnd && !isSameRessourceType) || (isNotRessourceSlotStart && !isArmorInSlotStart && !isEmptySlot)) return;

        SlotsInfos _save1 = slotsInfos[_slot1];
        SlotsInfos _save2 = slotsInfos[_slot2];

        if (_save1.ItemId == _save2.ItemId)
        {
            slotsInfos[_slot2] = new SlotsInfos(templates[slotsInfos[_slot1].ItemId], slotsInfos[_slot2].Number + slotsInfos[_slot1].Number);
            slotsInfos[_slot1] = new SlotsInfos(templates[0], 0);
            return;
        }

        slotsInfos[_slot1] = _save2;
        slotsInfos[_slot2] = _save1;
    }

    public bool HasItem(int _index)
    {
        return slotsInfos[_index].ItemId != 0;
    }

    public SlotsInfos GetItem(int index)
    {
        return slotsInfos[index];
    }

    public void SetItem(int _index, itemTemplate _item, int amount)
    {
        slotsInfos[_index] = new SlotsInfos(_item, amount);
    }

    public int SlotNumber => slotNumber;
    public SlotsInfos[] Slots => slotsInfos;

}
