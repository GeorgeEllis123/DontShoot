using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PatternManager pm;
    [SerializeField] private CircleShrinking cs;
    [SerializeField] private AudioClip spinSFX;
    [SerializeField] private AudioClip clickSFX;
    [SerializeField] private BulletSpawner bs;
    [SerializeField] private ParticleSystem smoke;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            bool correct = pm.VerifyClick(true);
            if (correct)
                bs.SpawnBullet();
            AudioSource.PlayClipAtPoint(spinSFX, Vector3.zero);
            cs.ResetCircle();
        }
        if (Input.GetMouseButtonDown(0))
        {
            bool correct = pm.VerifyClick(false);
            if (correct)
                smoke.Play();
            AudioSource.PlayClipAtPoint(clickSFX, Vector3.zero);
            cs.ResetCircle();
        }
    }
}
