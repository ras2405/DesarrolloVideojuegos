using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Shop : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip selectionSound;

    public GameObject newSellShop;

    public GameObject newBuyShop;

    public GameObject newShopTemplate;

    private void Awake()
    {
        newShopTemplate.gameObject.SetActive(false);
    }

    public void SwitchToSell()
    {
        PlaySelectionSound();
        newBuyShop.gameObject.SetActive(false);
        newSellShop.gameObject.SetActive(true);
    }

    public void SwitchToBuy()
    {
        PlaySelectionSound();
        newSellShop.gameObject.SetActive(false);
        newBuyShop.gameObject.SetActive(true);
    }

    private void PlaySelectionSound()
    {
        if (audioSource != null && selectionSound != null)
        {
            audioSource.PlayOneShot(selectionSound);
        }
    }
}
