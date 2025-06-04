using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PatternManager pm;
    [SerializeField] private CircleShrinking cs;
    [SerializeField] private AudioClip spinSFX;
    [SerializeField] private AudioClip clickSFX;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            pm.VerifyClick(true);
            AudioSource.PlayClipAtPoint(spinSFX, Vector3.zero);
            cs.ResetCircle();
        }
        if (Input.GetMouseButtonDown(0))
        {
            pm.VerifyClick(false);
            AudioSource.PlayClipAtPoint(clickSFX, Vector3.zero);
            cs.ResetCircle();
        }
    }
}
