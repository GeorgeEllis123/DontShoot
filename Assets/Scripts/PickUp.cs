using System;
using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private GameObject gunLogic;
    [SerializeField] private float putDownAnimDuration = 0.5f;

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

    public void HandlePutDown()
    {
        StartCoroutine(PutDownDisable()); 
    }

    private IEnumerator PutDownDisable()
    {
        anim.SetTrigger("PutDown");
        gunLogic.SetActive(false);

        yield return new WaitForSeconds(putDownAnimDuration);
        gameObject.SetActive(false); 
    }
}
