using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleFall : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
     
    private void OnTriggerEnter(Collider other)
    {
        anim.Play("IcicleFallPrac");
    }
}
