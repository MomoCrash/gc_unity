using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DropItem
{
    public static void DropItemInWorld(GameObject parent, Vector2 position, GameObject itemObject, itemTemplate item, int amount)
    {

        var newItem = GameObject.Instantiate(itemObject);
        newItem.GetComponent<ItemDrop>().item = item;
        newItem.GetComponent<ItemDrop>().amount = amount;
        newItem.GetComponent<SpriteRenderer>().sprite = item.Icon;
        newItem.transform.parent = parent.transform;
        newItem.transform.position = position;

    }
}
