using Assets.Food;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    // I am well aware having a global inventory on a seperate object might be the stoopid
    // But i am not willing to put in the effort of sharing one script across both players.

    public List<BaseIngredient> Inventory = new List<BaseIngredient>();
    [SerializeField] private int _maxInventoryAmount;

    /// <summary>
    /// Handles picking up items and adding it to a list that serves as inventory.
    /// </summary>
    public void AddToInventory(BaseIngredient objectToAdd)
    {
        if (Inventory.Contains(objectToAdd)) return; // Just in the rare case one might pick up the same object twice.
        if (Inventory.Count >= _maxInventoryAmount) return; // This means the inventory is full

        Inventory.Add(objectToAdd);
        objectToAdd.gameObject.SetActive(false);
    }

    public List<BaseIngredient> GetInventory()
    {
        return Inventory;
    }
}
