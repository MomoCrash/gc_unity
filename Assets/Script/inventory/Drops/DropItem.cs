using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DropItem
{
    public static void DropItemInWorld(GameObject parent, Vector2 position, GameObject itemObject, ItemStack itemStack)
    {

        var newItem = GameObject.Instantiate(itemObject);
        newItem.SetActive(true);
        newItem.GetComponent<ItemDrop>().ItemStack = itemStack;
        newItem.GetComponent<ItemDrop>().Amount = itemStack.amount;
        newItem.GetComponent<SpriteRenderer>().sprite = itemStack.item.Icon;
        newItem.GetComponent<Rigidbody2D>().simulated = true;
        newItem.transform.parent = parent.transform;
        newItem.transform.position = position;

    }
}

[System.Serializable]
public struct ItemStack
{
    public itemTemplate item;
    public int amount;

    public ItemStack(itemTemplate item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}