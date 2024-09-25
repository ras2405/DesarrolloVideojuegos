using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MoneyController : MonoBehaviour
{
   [SerializeField] public Text text;   
   public int currentMoney; 

   public void Add(int value)
   {
    currentMoney+=value;
    UpdateText();
   }

   public void Sub(int value)
   {
    currentMoney-=value;
    UpdateText();
   }

   private void UpdateText()
   {
    text.text = currentMoney.ToString();
   }
}
