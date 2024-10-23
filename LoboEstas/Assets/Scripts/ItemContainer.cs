using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemSlot
{
    public Item item;
    public int count;
}


[CreateAssetMenu(menuName="Data/Item Container")]
public class ItemContainer : ScriptableObject
{
   public List<ItemSlot> slots;
   public Item selectedItem;

   public void Add(Item item, int count = 1)
   {
    if(item.stackable)
    {
        ItemSlot slot = slots.Find(x => x.item == item);
        if(slot != null)
        {
            slot.count+=count; 
        }
        else{
            slot = slots.Find(x => x.item == null);
            if(slot != null)
            {
                slot.item = item;
                slot.count = count;
            }
        }
    }
    else{
        ItemSlot slot = slots.Find(x => x.item == null);
        if(slot == null)
        {
            slot.item = item;
        }
    }
   }
   public bool Remove(Item item)
   {
    ItemSlot slot = slots.Find(x => x.item == item);
    if(slot == null)
    {
        return false;
    }
    if(slot.count == 1)
    {
        slot.item = null;
        selectedItem = null;
    }
    else{
        slot.count -=1;
    }
    return true;
   }

   public void SelectItem(int slot)
   {
    if(slots[slot].item != null)
    {
        
        selectedItem = slots[slot].item;
        Debug.Log(selectedItem);
    }
   }

   public void RemoveSeed()
   {
    Remove(selectedItem);
   }
}
