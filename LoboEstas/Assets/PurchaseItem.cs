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

    [SerializeField] Item rock;
    [SerializeField] Item stick;

    public GameObject reinforcedWindow;

    public GameObject reinforcedDoor;

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
        if(moneyController.currentMoney >= 10)
        {
         moneyController.Sub(10);
         GameManager.instance.inventoryContainer.Add(carrotSeed,count);
        }
       
    }

    public void BuyPotato()
    {
        int count = 1;
        if(moneyController.currentMoney >= 50)
        {
         moneyController.Sub(50);
         GameManager.instance.inventoryContainer.Add(potatoSeed,count);
        }
    }

    public void BuyOnion()
    {
        Debug.Log("comprar cebolla");
        int count =1;
        if(moneyController.currentMoney >= 100)
        {
         moneyController.Sub(100);
         GameManager.instance.inventoryContainer.Add(onionSeed,count);
        } 
    }

    public void SellCarrot()
    {
        if(GameManager.instance.inventoryContainer.Remove(carrot))
        {
         moneyController.Add(20);
        }
        
    }

    public void SellPotato()
    {
        if(GameManager.instance.inventoryContainer.Remove(potato))
        {
         moneyController.Add(80);
        } 
    }

    public void SellOnion()
    {
        if(GameManager.instance.inventoryContainer.Remove(onion))
        {
         moneyController.Add(250);
        } 
    }

    public void SellStone()
    {
        if(GameManager.instance.inventoryContainer.Remove(rock))
        {
         moneyController.Add(10);
        } 
    }

    public void SellStick()
    {
        if(GameManager.instance.inventoryContainer.Remove(stick))
        {
         moneyController.Add(10);
        } 
    }

    public void BuyWindowDefense()
    {
        if(moneyController.currentMoney >= 250)
        {
         moneyController.Sub(250);
         reinforcedWindow.gameObject.SetActive(true);
        } 
    }

    public void BuyDoorDefense()
    {
        if(moneyController.currentMoney >= 300)
        {
         moneyController.Sub(300);
         reinforcedDoor.gameObject.SetActive(true);
        } 
    }

}
