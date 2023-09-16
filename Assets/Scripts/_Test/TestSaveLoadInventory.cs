using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaveLoadInventory : MonoBehaviour, IDataPersistence
{
    [Header("Inventory")]
    public InventorySlot[] inventorySlots;
    public ItemSO[] item;
    public string[] itemName;

    [Header("Recipe")]
    public RecipeSO[] recipes;

    public static TestSaveLoadInventory instance;

    public void LoadData(GameData data)
    {
        /*INVENTORY*/
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventoryItem itemInSlot = inventorySlots[i].GetComponentInChildren<InventoryItem>();
            for (int j = 0; j < item.Length; j++)
            {
                if (itemName[j] == data.inventory[i].Name && itemInSlot == null)
                {
                    InventoryManager.instance.LoadSpawnItem(item[j], inventorySlots[i], data.inventory[i].Count);
                }
            }
        }

        /*RECIPE*/
        if (data.RecipeListCount >= 0)
        {
            for (int i = 0; i < data.RecipeListCount; i++)
            {
                RecipeManager.instance.AddRecipe(recipes[i]);
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        /*INVENTORY*/
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                Debug.Log("i: " + i + ",name: " + itemInSlot.item.id + ",count: " + itemInSlot.count);
                data.inventory[i].Name = itemInSlot.item.id;
                data.inventory[i].Count = itemInSlot.count;
                data.inventory[i].Slot = i;
            }
            else
            {
                data.inventory[i].Name = null;
                data.inventory[i].Count = 0;
                data.inventory[i].Slot = 0;
            }
        }

        /*RECIPE*/
        data.RecipeListCount = RecipeManager.instance.listOfPaperUI.Count;
    }
}
