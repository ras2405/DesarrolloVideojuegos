using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterPlay : MonoBehaviour
{
    private void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        if (ps != null)
        {
            Destroy(gameObject, ps.main.duration);
        }
        else
        {
            Animator anim = GetComponent<Animator>();
            if (anim != null)
            {
                Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
            }
        }
    }
}

