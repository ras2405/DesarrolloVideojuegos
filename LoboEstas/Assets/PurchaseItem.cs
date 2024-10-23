using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
    

public class PurchaseItem : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] Item carrotSeed;
    [SerializeField] Item potatoSeed;
    [SerializeField] Item onionSeed;

    [SerializeField] Item carrot;
    [SerializeField] Item potato;
    [SerializeField] Item onion;

    [SerializeField] public  GameObject price;
    private MoneyController moneyController;

    Text priceText;

    void Start()
    {
        moneyController = GameObject.FindWithTag("Money").GetComponent<MoneyController>();
    }

    // Este método se llamará cuando se detecte un clic sobre el objeto
    public void OnPointerClick(PointerEventData eventData)
    {
        // Verificar si el clic fue el derecho
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Llamar a la función que quieres ejecutar
            Debug.LogWarning("Click derecho presionado");
            OnRightClick();
        }
    }

    public void OnRightClick()
    {
        Debug.Log("Buy Carrot seed function");
        int count =1;
        if(moneyController.currentMoney >= 20)
        {
         moneyController.Sub(20);
         GameManager.instance.inventoryContainer.Add(carrotSeed,count);
        }
       
    }

    public void BuyPotato()
    {
        int count = 1;
        if(moneyController.currentMoney >= 40)
        {
         moneyController.Sub(40);
         GameManager.instance.inventoryContainer.Add(potatoSeed,count);
        }
    }

    public void BuyOnion()
    {
        int count =1;
        if(moneyController.currentMoney >= 60)
        {
         moneyController.Sub(60);
         GameManager.instance.inventoryContainer.Add(onionSeed,count);
        } 
    }

    public void SellCarrot()
    {
        int count =1;
        this.priceText = price.GetComponent<Text>();
        Debug.Log(priceText);
        string priceString = priceText.text.ToString();
        int priceOfItem = Int32.Parse(priceString);
        if(GameManager.instance.inventoryContainer.Remove(carrot))
        {
         moneyController.Add(priceOfItem);
        }
        
    }

    public void SellPotato()
    {
       int count =1;
        this.priceText = price.GetComponent<Text>();
        Debug.Log(priceText);
        string priceString = priceText.text.ToString();
        int priceOfItem = Int32.Parse(priceString);
        if(GameManager.instance.inventoryContainer.Remove(potato))
        {
         moneyController.Add(priceOfItem);
        } 
    }

    public void SellOnion()
    {
        int count =1;
        this.priceText = price.GetComponent<Text>();
        Debug.Log(priceText);
        string priceString = priceText.text.ToString();
        int priceOfItem = Int32.Parse(priceString);
        if(GameManager.instance.inventoryContainer.Remove(onion))
        {
         moneyController.Add(priceOfItem);
        } 
    }
}
