using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarSlot : MonoBehaviour
{
   [SerializeField] Image icon;
   [SerializeField] Text text;
   [SerializeField] Image selectedBorder;

   int myIndex;


    private void Start()
    {
        selectedBorder.gameObject.SetActive(false);
    }
   public void SetIndex(int index)
   {
    myIndex = index;
   }

   public void Set(ItemSlot slot)
   {
    icon.gameObject.SetActive(true);
    icon.sprite = slot.item.icon;
    if(slot.item.stackable)
    {
        text.gameObject.SetActive(true);
        text.text = slot.count.ToString();
    }
    else{
        text.gameObject.SetActive(false);
    }
   }

   public void SetBorder()
   {
    selectedBorder.gameObject.SetActive(true);
   }
   public void CleanBorder()
   {
    selectedBorder.gameObject.SetActive(false);
   }

   public void Clean()
   {
    icon.sprite = null;
    selectedBorder.gameObject.SetActive(false);
    icon.gameObject.SetActive(false);
    text.gameObject.SetActive(false);
   }
}
