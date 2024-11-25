using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Shop : MonoBehaviour
{
    

    public GameObject newSellShop;

    public GameObject newBuyShop;

    public GameObject newShopTemplate;

    private void Awake()
    {
        newShopTemplate.gameObject.SetActive(false);
    }

    public void SwitchToSell()
    {
        newBuyShop.gameObject.SetActive(false);
        newSellShop.gameObject.SetActive(true);
    }

    public void SwitchToBuy()
    {
        newSellShop.gameObject.SetActive(false);
        newBuyShop.gameObject.SetActive(true);
    }
}
