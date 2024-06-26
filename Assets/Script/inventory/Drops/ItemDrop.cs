using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{

    public ItemStack ItemStack;
    public int Amount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var inventory = (inventoryControll) FindAnyObjectByType(typeof(inventoryControll));
        if (inventory != null )
        {
            inventory.AddItem(ItemStack);
        }
        Destroy(gameObject);
    }

}
