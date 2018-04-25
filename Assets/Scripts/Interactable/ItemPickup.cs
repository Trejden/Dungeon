public class ItemPickup : Interactable
{
    public Item item;

    public override void Interract()
    {
        PickUp();
    }
    private void PickUp()
    {
        if (Inventory.Instance.Add(item))
            Destroy(gameObject);
    }
}
