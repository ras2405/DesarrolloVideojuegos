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
    [SerializeField] Item match;

    [SerializeField] Item seta;
    [SerializeField] Item baya;

    [SerializeField] Item rock;
    [SerializeField] Item stick;

    public GameObject reinforcedWindow;

    public GameObject brokenWindow;

    public GameObject reinforcedDoor;

    public GameObject brokenDoor;

    private MoneyController moneyController;

    public AudioSource audioSource;
    public AudioClip buySellSound;
    public AudioClip selectObjectSound;

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
        int count = 1;
        if (moneyController.currentMoney >= 10)
        {
            ExecuteBuySound();
            moneyController.Sub(10);
            GameManager.instance.inventoryContainer.Add(carrotSeed, count);
        }
        else {
            ExecuteSelectObjectSound();
        }

    }

    public void BuyPotato()
    {
        int count = 1;
        if (moneyController.currentMoney >= 50)
        {
            ExecuteBuySound();
            moneyController.Sub(50);
            GameManager.instance.inventoryContainer.Add(potatoSeed, count);
        }
        else
        {
            ExecuteSelectObjectSound();
        }
    }

    public void BuyOnion()
    {
        int count = 1;
        if (moneyController.currentMoney >= 100)
        {
            ExecuteBuySound();
            moneyController.Sub(100);
            GameManager.instance.inventoryContainer.Add(onionSeed, count);
        }
        else
        {
            ExecuteSelectObjectSound();
        }
    }

    public void BuyMatch()
    {
        int count = 1;
        if (moneyController.currentMoney >= 500)
        {
            ExecuteBuySound();
            moneyController.Sub(500);
            GameManager.instance.inventoryContainer.Add(match, count);
        }
        else
        {
            ExecuteSelectObjectSound();
        }
    }

    public void SellCarrot()
    {
        if (GameManager.instance.inventoryContainer.Remove(carrot))
        {
            ExecuteBuySound();
            moneyController.Add(20);
        }
        else
        {
            ExecuteSelectObjectSound();
        }
    }

    public void SellBaya()
    {
        if (GameManager.instance.inventoryContainer.Remove(baya))
        {
            ExecuteBuySound();
            moneyController.Add(10);
        }
        else
        {
            ExecuteSelectObjectSound();
        }
    }

    public void SellSeta()
    {
        if (GameManager.instance.inventoryContainer.Remove(seta))
        {
            ExecuteBuySound();
            moneyController.Add(10);
        }
        else
        {
            ExecuteSelectObjectSound();
        }

    }

    public void SellPotato()
    {
        if (GameManager.instance.inventoryContainer.Remove(potato))
        {
            ExecuteBuySound();
            moneyController.Add(80);
        }
        else
        {
            ExecuteSelectObjectSound();
        }
    }

    public void SellOnion()
    {
        if (GameManager.instance.inventoryContainer.Remove(onion))
        {
            ExecuteBuySound();
            moneyController.Add(190);
        }
        else
        {
            ExecuteSelectObjectSound();
        }
    }

    public void SellStone()
    {
        if (GameManager.instance.inventoryContainer.Remove(rock))
        {
            ExecuteBuySound();
            moneyController.Add(10);
        }
        else
        {
            ExecuteSelectObjectSound();
        }
    }

    public void SellStick()
    {
        if (GameManager.instance.inventoryContainer.Remove(stick))
        {
            ExecuteBuySound();
            moneyController.Add(10);
        }
        else
        {
            ExecuteSelectObjectSound();
        }
    }

    public void BuyWindowDefense()
    {
        if (moneyController.currentMoney >= 250)
        {
            ExecuteBuySound();
            moneyController.Sub(250);
            reinforcedWindow.gameObject.SetActive(true);
            brokenWindow.gameObject.SetActive(false);
        }
        else
        {
            ExecuteSelectObjectSound();
        }
    }

    public void BuyDoorDefense()
    {
        if (moneyController.currentMoney >= 300)
        {
            ExecuteBuySound();
            moneyController.Sub(300);
            reinforcedDoor.gameObject.SetActive(true);
            brokenDoor.gameObject.SetActive(false);
        }
        else
        {
            ExecuteSelectObjectSound();
        }
    }

    public void ExecuteBuySound() {

        if (audioSource != null && buySellSound != null)
        {
            audioSource.PlayOneShot(buySellSound);
        }
    }

    public void ExecuteSelectObjectSound()
    {

        if (audioSource != null && selectObjectSound != null)
        {
            audioSource.PlayOneShot(selectObjectSound);
        }
    }


}