using UnityEngine;
using System.Collections;


public class ESCTooltip : MonoBehaviour
{
    private Coroutine hideCoroutine;
    private bool isCleared = false;

    private static bool hasBeenCleared = false;

    [SerializeField] private float autoHideTime = 8f;

    void OnEnable()
    {
        if (hasBeenCleared)
        {
            gameObject.SetActive(false);
            return;
        }


        isCleared = false;
        hideCoroutine = StartCoroutine(AutoHideAfterSeconds(autoHideTime));
    }

    void OnDisable()
    {
        StopAndClearCoroutine();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Clear();
        }
    }

    public void Clear()
    {
        if (isCleared) return;

        isCleared = true;
        hasBeenCleared = true;
        StopAndClearCoroutine();
        gameObject.SetActive(false);
    }

    private void StopAndClearCoroutine()
    {
        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
            hideCoroutine = null;
        }
    }

    private IEnumerator AutoHideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Clear();
    }
}