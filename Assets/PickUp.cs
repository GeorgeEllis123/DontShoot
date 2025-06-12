using System;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private GameObject gunLogic;

    private void OnEnable()
    {
        // move up
        Invoke("GunLogicEnable", 1f);
    }

    private void GunLogicEnable()
    {
        gunLogic.SetActive(true);
    }

    private void OnDisable()
    {
        // return to below screen
        gunLogic.SetActive(false);
    }
}
