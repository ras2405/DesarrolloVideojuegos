using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MoneyController : MonoBehaviour
{
   [SerializeField] public GameObject text;  

   Text textComponent; 
   public int currentMoney; 

   void Start()
   {
      UpdateText();
   }

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
      textComponent = text.GetComponent<Text>();
      textComponent.text = currentMoney.ToString();
   }
}
