using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class slotController : MonoBehaviour, IBeginDragHandler, IDragHandler ,IEndDragHandler, IDropHandler
{
    public int slotID { private set; get; }

    private Sprite iconSprite;

    [SerializeField] private Image iconImage;

    [SerializeField] private TextMeshProUGUI numberText;

    private inventoryDisplay display;

    public void Init(int _id, inventoryDisplay _display)
    { 
        display = _display;
        slotID = _id; 
    }

    public void UpdateDisplay(Sprite _icon, int _number)
    {
        iconSprite = _icon;
        bool _empty = _number == 0;

        numberText.text = _empty ? "": _number.ToString("00");
        iconImage.color = _empty ? new Color(1,1,1,0f) : Color.white;
        iconImage.sprite = _empty ? null: _icon;

    }

    #region DragAndDrop

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin : " + slotID);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag : " + slotID);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End : " + slotID);
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop : " + slotID);
    }

    #endregion
}
