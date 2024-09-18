using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    public float Health{
        set{
            health = value;
            if(health <= 0){
                Defeated();
            }
        }
        get{
            return health;
        }
    }
    
    private void Start(){
        animator = GetComponent<Animator>();
    }

    public float health = 1;

   public void Defeated(){
    animator.SetTrigger("Defeated");
   }

   public void RemoveEnemy(){
    Destroy(gameObject);
   }
}
