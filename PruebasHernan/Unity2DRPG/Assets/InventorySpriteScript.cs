using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class InventorySpriteScript : MonoBehaviour
{
    public GameObject player;
    public UnityEngine.UI.Image itemImage;
    Inventory playerInventory;
    void Start()
    {
         playerInventory = player.GetComponent<Inventory>();
         itemImage = GetComponent<UnityEngine.UI.Image>();
    }


    void Update()
    {
        
    }

    public void UpdateInventory(){
        for (int i = 0; i<7;i++){
            if(playerInventory.inventory[i] !=null){
                Transform child = playerInventory.inventory[i].transform;
                UnityEngine.UI.Image childImageComponent = child.GetComponent<UnityEngine.UI.Image>();
                itemImage.sprite = childImageComponent.sprite;
                child.gameObject.SetActive(true);
            }
        }
    }
}
