using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopDisplay : MonoBehaviour
{
    public Player player;

    public inventoryControll inventory;
    public Transform shopContainer;

    public GameObject slotTemplate;

    public int MenuStartX;
    public int MenuStartY;

    public int MenuMaxY;

    public int SlotWidth;
    public int SlotHeight;

    public void OpenShop(ShopItem[] items)
    {
        player.isActionInProgress = true;
        gameObject.GetComponent<Image>().enabled = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        UpdateShopDisplay(items);
    }

    public void CloseShop()
    {
        player.isActionInProgress = false;
        gameObject.GetComponent<Image>().enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void UpdateShopDisplay(ShopItem[] items)
    {
        for (int i = 0; i < shopContainer.childCount; i++)
        {
            Destroy(shopContainer.GetChild(i).gameObject);
        }

        var x = MenuStartX; 
        var y = MenuStartY;

        foreach (var itemShop in items)
        {
            var itemObject = GameObject.Instantiate(slotTemplate, shopContainer);

            itemObject.transform.GetComponentInChildren<TextMeshProUGUI>().text = itemShop.reward.item.name + "x" + itemShop.reward.amount;
            itemObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = itemShop.cost.item.name + "x" + itemShop.cost.amount;
            itemObject.transform.GetChild(0).GetComponent<Image>().sprite = itemShop.reward.item.Icon;
            itemObject.transform.GetComponent<Button>().onClick.AddListener(() =>
            {
                print(inventory.HasEnoughtItem(itemShop.cost));
                if (inventory.HasEnoughtItem(itemShop.cost))
                {
                    inventory.RemoveItem(itemShop.cost);
                    inventory.AddItem(itemShop.reward);
                }
            });
            itemObject.transform.localPosition = new Vector2(x, y);

            y -= SlotHeight;

            if (y <= MenuMaxY)
            {
                x += SlotWidth;
                y = MenuStartY;
            }
        }
    }
}


[System.Serializable]
public struct ShopItem
{

    public ItemStack cost;
    public ItemStack reward;

}