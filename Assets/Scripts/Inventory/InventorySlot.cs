using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image Icon;

    Item ItemInSlot;

    public void AddItem(Item newItem)
    {
        ItemInSlot = newItem;

        Icon.sprite = newItem.Icon;
        Icon.enabled = true;
    }

    public void ClearSlot()
    {
        ItemInSlot = null;

        Icon.sprite = null;
        Icon.enabled = false;
    }
}
