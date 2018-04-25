using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{

    new public string name = "New Item";
    public Sprite Icon = null;
    public bool ShowInInventory = true;

    public virtual void Use()
    {

    }

    public void RemoveFromInventory()
    {

    }
}
