using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    //public GameObject[] droppedItemPrefab;
    public List<ItemObject> lootList = new List<ItemObject>();

    public ItemObject GetDroppedItem()
    {
        int randomNumber = Random.Range(1, 101);
        List<ItemObject> possibleItems = new List<ItemObject>();
        foreach (ItemObject item in lootList)
        {
            if(randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }
        if(possibleItems.Count > 0)
        {
            ItemObject droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        return null;
    }
}
