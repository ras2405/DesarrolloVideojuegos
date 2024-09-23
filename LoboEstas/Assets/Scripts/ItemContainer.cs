using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemSlot
{
    public Item item;
    public int count;
}


[CreateAssetMenu(menuName="Data/Item Container")]
public class ItemContainer : ScriptableObject
{
   public List<ItemSlot>slots;

   public void Add(Item item, int count = 1)
   {
    if(item.stackable)
    {
        ItemSlot slot = slots.Find(x=>x.item == item);
        if(slot != null)
        {
            slot.count+=count;
        }
        else{
            slot = slots.Find(x=>x.item == null);
            if(slot !=null)
            {
                slot.item = item;
                slot.count = count;
            }
        }
    }
    else{
        //Add non stackable item
        ItemSlot slot = slots.Find(x=>x.item == null);
        if(slot != null)
        {
            slot.item = item;
        }
    }
   }
}
