using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fencesDefense : MonoBehaviour
{
   public GameObject cornerFence1;
   public GameObject cornerFence2;
   public GameObject fence3;
   public GameObject fence4;
   public GameObject verticalFence5;
   public GameObject verticalFence6;
   public GameObject verticalFence7;
   public GameObject verticalFence8;
   public GameObject verticalFence9;
   public GameObject verticalFence10;

   public GameObject RcornerFence1;
   public GameObject RcornerFence2;
   public GameObject Rfence3;
   public GameObject Rfence4;
   public GameObject RverticalFence5;
   public GameObject RverticalFence6;
   public GameObject RverticalFence7;
   public GameObject RverticalFence8;
   public GameObject RverticalFence9;
   public GameObject RverticalFence10;

   public Sprite verticalReinforcedSprite;
   public Sprite normalReinforcedSprite;
   public Sprite cornerReinforcedSprite;

   public Sprite verticalSprite;
   public Sprite normalSprite;
   public Sprite cornerSprite;

    private MoneyController moneyController;

    void Start()
    {
        moneyController = GameObject.FindWithTag("Money").GetComponent<MoneyController>();
    }


    public void SetDefenses()
    {
        if(moneyController.currentMoney >= 200)
        {
            moneyController.Sub(200);
            AssignSprite(cornerFence1, cornerReinforcedSprite);
            AssignSprite(cornerFence2, cornerReinforcedSprite);

            AssignSprite(RcornerFence1, cornerReinforcedSprite);
            AssignSprite(RcornerFence2, cornerReinforcedSprite);

            // Asignar sprites a los fence normales
            AssignSprite(fence3, normalReinforcedSprite);
            AssignSprite(fence4, normalReinforcedSprite);

            AssignSprite(Rfence3, normalReinforcedSprite);
            AssignSprite(Rfence4, normalReinforcedSprite);

        // Asignar sprites a los verticalFence
            AssignSprite(verticalFence5, verticalReinforcedSprite);
            AssignSprite(verticalFence6, verticalReinforcedSprite);
            AssignSprite(verticalFence7, verticalReinforcedSprite);
            AssignSprite(verticalFence8, verticalReinforcedSprite);
            AssignSprite(verticalFence9, verticalReinforcedSprite);
            AssignSprite(verticalFence10, verticalReinforcedSprite);

            AssignSprite(RverticalFence5, verticalReinforcedSprite);
            AssignSprite(RverticalFence6, verticalReinforcedSprite);
            AssignSprite(RverticalFence7, verticalReinforcedSprite);
            AssignSprite(RverticalFence8, verticalReinforcedSprite);
            AssignSprite(RverticalFence9, verticalReinforcedSprite);
            AssignSprite(RverticalFence10, verticalReinforcedSprite);
        }
       
    }
     private void AssignSprite(GameObject obj, Sprite sprite)
    {
        if (obj != null)
        {
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sprite = sprite; // Asigna el sprite
            }
            else
            {
                Debug.LogWarning($"El GameObject {obj.name} no tiene un SpriteRenderer.");
            }
        }
        else
        {
            Debug.LogWarning("Se intentó asignar un sprite a un GameObject nulo.");
        }
    }
}
