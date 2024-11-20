using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Shop : MonoBehaviour
{
    private Transform container;

    public Transform newShop;

    public GameObject newSellShop;

    public GameObject newBuyShop;

    private void Awake()
    {
        container = transform.Find("Container");
        newShop = container.transform.Find("NewShopTemplate");
        newShop.gameObject.SetActive(false);
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
