using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class Toolbar : MonoBehaviour
{
    [SerializeField] ItemContainer inventory;
    [SerializeField] List<ToolbarSlot> slots;

    public void Start()
    {
        SetIndex();
        Show();
    }


    public void Update()
    {
        
        Show();
    }
    private void SetIndex()
    {
        for (int i = 0;i< inventory.slots.Count;i++)
        {
            slots[i].SetIndex(i);
        }
    }

    private void Show()
    {
        for(int i = 0;i<inventory.slots.Count;i++)
        {
            if(inventory.slots[i].item == null)
            {
                slots[i].Clean();
            }
            else{
                slots[i].Set(inventory.slots[i]);
            }
        }
    }
}
