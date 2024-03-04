using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(inventoryData), typeof(inventoryDisplay))]
public class inventoryControll : MonoBehaviour
{
    private inventoryData data;

    private inventoryDisplay display;


    private void Start()
    {
        data = GetComponent<inventoryData>();
        display = GetComponent<inventoryDisplay>();

        data.Init(this);
        display.Init(this);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GetComponent<Image>().enabled)
            {
                GetComponent<Image>().enabled = false;
                transform.GetChild(0).gameObject.SetActive(false);

                display.UpdateDisplay(data.Slots);
            } else
            {
                GetComponent<Image>().enabled = true;
                transform.GetChild(0).gameObject.SetActive(true);


                display.UpdateDisplay(data.Slots);
            }
        }
    }

    public void AddItem(itemTemplate template, int amount)
    {
        for (int i = 0; i < data.Slots.Length; i++)
        {
            if (!data.HasItem(i))
            {
                data.SetItem(i, template, amount);
            }
        }
    }

    public void SwitchSlots(int _slot1, int _slot2)
    {
        data.SwitchSlots(_slot1, _slot2);
        display.UpdateDisplay(data.Slots);
    }

    public int SlotNumber => data.SlotNumber;

}