using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    public override void Interract()
    {
        PickUp();
    }
    private void PickUp()
    {
        Debug.Log("Picking up" + item.name);
        if (Inventory.Instance.Add(item))
            Destroy(gameObject);
    }
}
