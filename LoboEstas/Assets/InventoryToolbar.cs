using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryToolbar : MonoBehaviour
{
   [SerializeField] ItemContainer inventory;
   [SerializeField] List<ToolbarSlot> slots;

   [SerializeField] private AudioSource audioSource; 
   [SerializeField] private AudioClip selectionSound;

    private void Start()
   {
    SetIndex();
    Show();
   }
    private void OnEnable()
    {
        Show();
    }

    private void Update()
    {
        Show();
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
          SelectItem1();
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectItem2();
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectItem3();
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectItem4();
        }
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectItem5();
        }
        if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectItem6();
        }
    }

    private void Clean()
    {
        foreach(var slot in slots)
        {
            slot.CleanBorder();
        }
    }
   private void SetIndex()
   {
    for(int i = 0; i < inventory.slots.Count; i++)
    {
        slots[i].SetIndex(i);
    }
   }
   private void Show()
   {
     for(int i =0; i < inventory.slots.Count;i++)
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

    public void ResetInventory()
    {
        // Limpiar los �tems del contenedor del inventario
        foreach (var slot in inventory.slots)
        {
            slot.item = null; // Eliminar el �tem de cada slot
        }

        // Limpiar la interfaz de la barra de herramientas
        foreach (var toolbarSlot in slots)
        {
            toolbarSlot.Clean(); // Limpiar visualmente los slots de la toolbar
        }
    }

    public void SelectItem1()
    {
        inventory.SelectItem(0);
        Clean();
        slots[0].SetBorder();
        PlaySelectionSound();
    }

    public void SelectItem2()
    {
        inventory.SelectItem(1);
        Clean();
        slots[1].SetBorder();
        PlaySelectionSound();
    }

    public void SelectItem3()
    {
        inventory.SelectItem(2);
        Clean();
        slots[2].SetBorder();
        PlaySelectionSound();
    }

    public void SelectItem4()
    {
        inventory.SelectItem(3);
        Clean();
        slots[3].SetBorder();
        PlaySelectionSound();
    }

    public void SelectItem5()
    {
        inventory.SelectItem(4);
        Clean();
        slots[4].SetBorder();
        PlaySelectionSound();
    }

    public void SelectItem6()
    {
        inventory.SelectItem(5);
        Clean();
        slots[5].SetBorder();
        PlaySelectionSound();
    }

    private void PlaySelectionSound()
    {
        if (audioSource != null && selectionSound != null)
        {
            audioSource.PlayOneShot(selectionSound);
        }
    }
}
