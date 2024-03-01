using UnityEngine;

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

        display.UpdateDisplay(data.Slots);
    }

    public int SlotNumber => data.SlotNumber;

}