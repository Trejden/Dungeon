using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform ItemsParent;

    InventorySlot[] Slots;

    Inventory inventory;

	// Use this for initialization
	void Start () {
        inventory = Inventory.Instance;
        inventory.OnItemChangeEvent += UpdateUI;

        Slots = ItemsParent.GetComponentsInChildren<InventorySlot>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void UpdateUI()
    {
        for(int i = 0; i < Slots.Length; i++)
        {
            if (i < inventory.Items.Count)
            {
                Slots[i].AddItem(inventory.Items[i]);
            }
            else
            {
                Slots[i].ClearSlot();
            }
        }
    }
}
