using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(inventoryData), typeof(inventoryDisplay))]
public class inventoryControll : MonoBehaviour
{

    public inventoryData data;

    public inventoryDisplay display;

    public itemTemplate noneItem;


    private void Start()
    {
        data = GetComponent<inventoryData>();
        display = GetComponent<inventoryDisplay>();

        data.Init(this);
        display.Init(this);

        display.UpdateDisplay(data.Slots);
        display.PlayerStats.Health = display.PlayerStats.MaxHealth;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GetComponent<Image>().enabled)
            {
                GetComponent<Image>().enabled = false;
                transform.GetChild(0).gameObject.SetActive(false);

                foreach (var slot in display.slots)
                {
                    if (slot.dragObject != null)
                    {
                        Destroy(slot.dragObject);
                    }
                    if (slot.infoObject != null)
                    {
                        Destroy(slot.infoObject);
                    }
                }

                display.UpdateDisplay(data.Slots);
            } else
            {
                GetComponent<Image>().enabled = true;
                transform.GetChild(0).gameObject.SetActive(true);


                display.UpdateDisplay(data.Slots);
            }
        }
    }

    void OnDisable()
    {
        for (var i = 0; i < data.Slots.Length; i++)
        {
            PlayerPrefs.SetInt("Slot_" + i + "_Item", data.Slots[i].ItemId);
            PlayerPrefs.SetInt("Slot_" + i + "_Amount", data.Slots[i].Number);
        }
        PlayerPrefs.SetInt("Slot_Number", data.Slots.Length);
    }

    public bool HasEnoughtItem(ItemStack item)
    {
        for (int i = 0; i < data.Slots.Length; i++)
        {
            if (data.GetItem(i).ItemId == item.item.ItemId && data.HasItem(i))
            {
                if (data.GetItem(i).Number >= item.amount)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void RemoveItem(ItemStack item)
    {
        for (int i = 0; i < data.Slots.Length; i++)
        {
            if (data.HasItem(i))
            {
                if (data.GetItem(i).ItemId == item.item.ItemId && data.GetItem(i).Number > item.amount)
                {
                    data.SetItem(i, item.item, data.GetItem(i).Number - item.amount);
                } else if (data.GetItem(i).ItemId == item.item.ItemId && data.GetItem(i).Number == item.amount)
                {
                    data.SetItem(i, noneItem, 0);
                }
            }
        }
    }

    public void AddItem(ItemStack itemStack)
    {
        for (int i = 0; i < data.Slots.Length; i++)
        {
            if (!data.HasItem(i))
            {
                data.SetItem(i, itemStack.item, itemStack.amount);
                break;
            } else if (data.GetItem(i).ItemId == itemStack.item.ItemId)
            {
                data.SetItem(i, itemStack.item, itemStack.amount + data.GetItem(i).Number);
                break;
            }
        }
        display.UpdateDisplay(data.Slots);
    }

    public void SwitchSlots(int _slot1, int _slot2)
    {
        data.SwitchSlots(_slot1, _slot2);
        display.UpdateDisplay(data.Slots);
    }

    public int SlotNumber => data.SlotNumber;

    public int AddedSlots => data.addedSlots;

}