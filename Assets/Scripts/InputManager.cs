using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PatternManager pm;
    [SerializeField] private CircleShrinking cs;
    [SerializeField] private GameObject tc;
    [SerializeField] private AudioClip spinSFX;
    [SerializeField] private AudioClip clickSFX;
    [SerializeField] private BulletSpawner bs;
    [SerializeField] private ParticleSystem smoke;
    private bool targetReady = false;

    void Update()
    {
        if (cs.transform.localScale.x < tc.transform.localScale.x)
        {
            targetReady = true;
        }
        else
        {
            targetReady = false;
        }

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.D))
        {
            if (cs.transform.localScale.x < tc.transform.localScale.x)
            {
                bool correct = pm.VerifyClick(true);
                if (correct)
                    bs.SpawnBullet();
                AudioSource.PlayClipAtPoint(spinSFX, Vector3.zero);
                cs.ResetCircle();
            }
            else
            {
                pm.GetShot();
            }
        }
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.S))
        {
            if(cs.transform.localScale.x < tc.transform.localScale.x)
            {
                bool correct = pm.VerifyClick(false);
                if (correct)
                    smoke.Play();
                AudioSource.PlayClipAtPoint(clickSFX, Vector3.zero);
                cs.ResetCircle();
            }
            else
            {
                pm.GetShot();
            }
        }
    }

    public bool TargetReady()
    {
        return targetReady;
    }
}
