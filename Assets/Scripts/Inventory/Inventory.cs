using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<Item> Items = new List<Item>();

    public int Capacity = 20;

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangeEvent;

    private void Awake()
    {
        Instance = this;
    }

    public bool Add(Item item)
    {
        if (item.ShowInInventory && Items.Count < Capacity)
        {
            Items.Add(item);

            if (OnItemChangeEvent != null)
                OnItemChangeEvent.Invoke();

            return true;
        }
        return false;
    }
    public void Remove(Item item)
    {
        if (Items.Contains(item))
        {
            Items.Remove(item);

            if (OnItemChangeEvent != null)
                OnItemChangeEvent.Invoke();
        }
    }
}
