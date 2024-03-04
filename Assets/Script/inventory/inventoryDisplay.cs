using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryDisplay : MonoBehaviour
{
    private int startDragSlotID = 999999;

    private slotController[] slots;

    [SerializeField] private Transform slotPrefab;

    [SerializeField] private Canvas slotCanvas; 

    private inventoryControll controll;
    public void Init(inventoryControll _controll)
        {
            controll = _controll;

            slots = new slotController[controll.SlotNumber];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = Instantiate(slotPrefab, transform.position, Quaternion.identity, slotCanvas.transform).GetComponent<slotController>();
            slots[i].Init(i, this);
        }
    }

    public void UpdateDisplay(SlotsInfos[] _slotInfos)
    {
        for (int i = 0; i < _slotInfos.Length;i++) 
        {
            slots[i].UpdateDisplay(_slotInfos[i].Icon, _slotInfos[i].Number);
        }
    }

    public void StartDrag(int _startSlotID) => startDragSlotID = _startSlotID;

    public void EndDrag(int _endSlotID) => controll.SwitchSlots(startDragSlotID, _endSlotID);
}
