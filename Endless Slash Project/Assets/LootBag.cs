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
                Debug.Log("algo");

                possibleItems.Add(item);
            }
        }
        if(possibleItems.Count > 0)
        {
            ItemObject droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        Debug.Log("nada");

        return null;
    }

 /*  public void InstantiateLoot(Vector3 spawnPosition)
    {
        ItemObject droppedItem = GetDroppedItem();
        if(droppedItem != null)
        {
            Debug.Log("nadasa");
            GameObject lootGameObject = Instantiate(droppedItemPrefab[1], spawnPosition, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.uiDisplay;
        }
    }*/
}
