using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject slotHolder;
    [SerializeField]
    private int slotAmount;
    [SerializeField]
    private GameObject slotPrefab;

    private Slot[] slots;

    private void Awake()
    {
        slots = new Slot[slotAmount];

        for (int i = 0; i < slotAmount; i++) {
            GameObject slotObj = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity);
            Slot slot = slotObj.AddComponent<Slot>();
            slotObj.transform.SetParent(slotHolder.transform);
            slots[i] = slot;
        }
    }

    public bool AddItem(Item item)
    {
        Slot slot = FindEmptySlot();

        if (slot != null)
        {
            return slot.AddItem(item);
        }
        else
        {
            return false;
        }
    }

    private Slot FindEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty)
            {
                return slots[i];
            }
        }

        return null;
    }
}
