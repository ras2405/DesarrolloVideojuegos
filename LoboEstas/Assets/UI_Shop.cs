using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Shop : MonoBehaviour
{
    private Transform container;
    private Transform buyCarrotTemplate;
    private Transform buyPotatoTemplate;
    private Transform buyOnionTemplate;
    private Transform sellCarrotTemplate;
    private Transform sellPotatoTemplate;
    private Transform sellOnionTemplate;

    public GameObject repairWindowButton;

    private void Awake()
    {
        container = transform.Find("Container");
        buyCarrotTemplate = container.transform.Find("BuyCarrotTemplate");
        buyPotatoTemplate = container.transform.Find("BuyPotatoTemplate");
        buyOnionTemplate = container.transform.Find("BuyOnionTemplate");
        buyCarrotTemplate.gameObject.SetActive(false);
        buyPotatoTemplate.gameObject.SetActive(false);
        buyOnionTemplate.gameObject.SetActive(false);

        container = transform.Find("Container2");
        sellCarrotTemplate = container.transform.Find("SellCarrotTemplate");
        sellCarrotTemplate.gameObject.SetActive(false);
        sellPotatoTemplate = container.transform.Find("SellPotatoTemplate");
        sellPotatoTemplate.gameObject.SetActive(false);
        sellOnionTemplate = container.transform.Find("SellOnionTemplate");
        sellOnionTemplate.gameObject.SetActive(false);

        repairWindowButton.gameObject.SetActive(false);
    }
}
