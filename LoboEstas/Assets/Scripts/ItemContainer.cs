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

    public bool IsFull(Item item)
    {
        if(item.stackable == true)
        {
            foreach(ItemSlot slot in slots)
        {
            if(slot.item == item || EmptySlot()) return false;
        }
        }
        else{
            foreach(ItemSlot slot in slots)
            {
                if(slot.item == null) return false;
            }
        }
        return true;
    }

    public bool EmptySlot()
    {
        foreach (ItemSlot slot in slots)
        {
            if(slot.item == null)return true;
        }
        return false;
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

   public bool HasItem(string name)
   {
    foreach(ItemSlot slot in slots)
    {
        if(slot.item != null && slot.item.name == name) return true;
    }
    return false;
   }
}
