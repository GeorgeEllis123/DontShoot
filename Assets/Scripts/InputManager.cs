using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PatternManager pm;
    [SerializeField] private CircleShrinking cs;
    [SerializeField] private AudioSource spinSFX;
    [SerializeField] private AudioSource clickSFX;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            pm.VerifyClick(true);
            spinSFX.Play();
            cs.ResetCircle();
        }
        if (Input.GetMouseButtonDown(0))
        {
            pm.VerifyClick(false);
            clickSFX.Play();
            cs.ResetCircle();
        }
    }
}
