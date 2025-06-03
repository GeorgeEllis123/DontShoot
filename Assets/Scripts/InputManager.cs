using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private CircleShrinking cs;

    [SerializeField] private AudioSource clickSFX;
    [SerializeField] private AudioSource spinSFX;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            clickSFX.Play();
            cs.ResetCircle();
        }
        if (Input.GetMouseButtonDown(0))
        {
            spinSFX.Play();
            cs.ResetCircle();
        }
    }
}
