using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryDisplay : MonoBehaviour
{
    public Player PlayerStats;
    public GameObject[] AddedSlots;
    private int startDragSlotID = 999999;

    public slotController[] slots;

    [SerializeField] private Transform slotPrefab;

    [SerializeField] private Canvas slotCanvas; 

    public inventoryControll controll;
    public void Init(inventoryControll _controll)
    {
        controll = _controll;
        slots = new slotController[controll.SlotNumber];
        var index = 0;
        for (int i = 0; i < slots.Length - controll.AddedSlots+1; i++)
        {
            slots[i] = Instantiate(slotPrefab, transform.position, Quaternion.identity, slotCanvas.transform).GetComponent<slotController>();
            slots[i].Init(i, this);
            index++;
        }
        for (int i = index-1; i < slots.Length; i++)
        {
            slots[i] = AddedSlots[slots.Length-1 - i].GetComponent<slotController>();
            slots[i].Init(i, this);
        }
    }

    public void UpdateDisplay(SlotsInfos[] _slotInfos)
    {

        var totalStatHealth = 100f;
        var totalStatDamage = 10f;
        var totalStatFireResistance = 1f;
        var totalStatBaseResistance = 1f;
        var totalStatEarthResistance = 1f;
        var totalStatJump = 2;
        var totalStatDash = 1;
        var totalStatSpeed = 3f;

        for (int i = 0; i < _slotInfos.Length;i++) 
        {
            slots[i].UpdateDisplay(_slotInfos[i].Icon, _slotInfos[i].Number);

            if (slots[i].ItemType != ItemType.RESSOURCE)
            {
                totalStatEarthResistance -= _slotInfos[i].template.EarthResistance;
                totalStatFireResistance -= _slotInfos[i].template.FireResistance;
                totalStatBaseResistance -= _slotInfos[i].template.BaseResistance;
                totalStatHealth += _slotInfos[i].template.Health;
                totalStatDamage += _slotInfos[i].template.Damage;
                totalStatDash += _slotInfos[i].template.DashBoost;
                totalStatJump += _slotInfos[i].template.JumpBoost;
                totalStatSpeed += _slotInfos[i].template.SpeedBoost;
            }

        }

        PlayerStats.Health = totalStatHealth;
        PlayerStats.BaseDamage = totalStatDamage;
        PlayerStats.FireResistance = totalStatFireResistance;
        PlayerStats.EarthResistance = totalStatEarthResistance;
        PlayerStats.Resistance = totalStatBaseResistance;
        PlayerStats.MaxDashCount = totalStatDash;
        PlayerStats.MaxJumpCount = totalStatJump;
        PlayerStats.Speed = totalStatSpeed;

    }

    public void StartDrag(int _startSlotID) => startDragSlotID = _startSlotID;

    public void EndDrag(int _endSlotID) => controll.SwitchSlots(startDragSlotID, _endSlotID);
}
