using System;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private GameObject gunLogic;

    private Animator anim; 

    private void OnEnable()
    {
        anim = gameObject.GetComponent<Animator>(); 
        anim.SetTrigger("PickUp"); 
        // move up
        Invoke("GunLogicEnable", 1f);
    }

    private void GunLogicEnable()
    {
        gunLogic.SetActive(true);
        
    }

    private void OnDisable()
    {
        // anim.SetTrigger("PutDown"); 
        // return to below screen
        gunLogic.SetActive(false);
    }
}
