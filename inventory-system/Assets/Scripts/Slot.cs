using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Image icon;
    private Item item;
    private GameObject itemObj;

    public bool IsEmpty {
        get
        {
            return item == null && itemObj == null;
        }
    }

    private void Awake()
    {        
        icon = transform.Find(Constants.SLOT_ICON_HOLDER_OBJ_NAME).GetComponent<Image>();
    }

    public bool AddItem(Item hItem)
    {
        if (item == null)
        {
            item = hItem;
            itemObj = hItem.gameObject;
            icon.sprite = item.Img;
            icon.color = Color.white;
            return true;
        } else
        {
            return false;
        }
    }

    public void RemoveItem()
    {
        item = null;
        itemObj = null;
        icon.sprite = null;
        icon.color = new Vector4(0, 0, 0, 0);
    }
}
