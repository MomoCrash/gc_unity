using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class slotController : MonoBehaviour, IBeginDragHandler, IDragHandler ,IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int slotID;

    private Sprite iconSprite;

    [SerializeField] private Image iconImage;

    public ItemType ItemType = ItemType.RESSOURCE;

    [SerializeField] private TextMeshProUGUI numberText;

    [Header("Prefabs")]
    [SerializeField] private Transform dragPrefab;
    [SerializeField] private Transform infoPrefab;
    private GameObject dragObject;
    private GameObject infoObject;

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

        if (numberText != null) { 
            numberText.text = _empty ? "" : _number.ToString("00");
        }
        iconImage.color = _empty ? new Color(1,1,1,0f) : Color.white;
        iconImage.sprite = _empty ? null: _icon;

    }

    #region DragAndDrop

    private void OnMouseOver()
    {
        print("Salut");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragObject = Instantiate(dragPrefab, transform.position, Quaternion.identity, transform).gameObject;
        Image _img = dragObject.GetComponent<Image>();
        _img.enabled = true;
        _img.sprite = iconSprite;
        _img.color = iconSprite == null ? new Color(0, 0, 0, 0) : Color.white;

        RectTransform _rect = (RectTransform)(dragObject.transform);
        RectTransform _slotRect = (RectTransform)transform;

        _rect.sizeDelta = _slotRect.sizeDelta;

        dragObject.GetComponent<Canvas>().overrideSorting = true;

        display.StartDrag(slotID);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
        dragObject.transform.position = new Vector2(worldPos.x, worldPos.y);
    }

    public void OnEndDrag(PointerEventData eventData) => Destroy(dragObject);

    public void OnDrop(PointerEventData eventData)
    {
        display.EndDrag(slotID);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var number = 0;
        if (numberText == null) return;
        if (int.TryParse(numberText.text, out number)) {
            if (number > 0)
            {
                var item = display.controll.data.Slots[slotID].template;
                infoObject = Instantiate(infoPrefab, transform.position, Quaternion.identity, transform).gameObject;
                infoObject.GetComponentInChildren<TextMeshProUGUI>().text = item.name + "\n Resistance : " 
                    + item.BaseResistance + " \n Earth Resistance : "
                    + item.EarthResistance + " \n Fire Resistance : "
                    + item.FireResistance + " \n Damage : "
                    + item.Damage + " \n Speed : "
                    + item.SpeedBoost + " \n Jump Added : "
                    + item.JumpBoost + " \n Dash Added : "
                    + item.DashBoost
                    ;
                var rectTransform = (RectTransform) infoObject.transform;
                var spriteY = rectTransform.rect.height / 2;
                var spriteX = rectTransform.rect.width / 2;

                var worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
                infoObject.transform.localPosition = new Vector2(worldPos.x, worldPos.y) + new Vector2(spriteX, spriteY);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(infoObject);
    }

    #endregion
}
