using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PatternManager pm;
    [SerializeField] private CircleShrinking cs;
    [SerializeField] private GameObject tc;
    //[SerializeField] private AudioClip spinSFX;
    //[SerializeField] private AudioClip clickSFX;
    [SerializeField] private BulletSpawner bs;
    [SerializeField] private ParticleSystem smoke;
    private bool targetReady = false;

    AudioSource[] audiosources;
    AudioSource spinSFX;
    AudioSource clickSFX;

    private void Start()
    {
        audiosources = GetComponents<AudioSource>();
        spinSFX = audiosources[0];
        clickSFX = audiosources[1];
    }

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
                spinSFX.Play();
                cs.ResetCircle();
            }
            else
            {
                pm.GetShot(true);
            }
        }
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.S))
        {
            if(cs.transform.localScale.x < tc.transform.localScale.x)
            {
                bool correct = pm.VerifyClick(false);
                if (correct)
                    smoke.Play();
                clickSFX.Play();
                cs.ResetCircle();
            }
            else
            {
                pm.GetShot(true);
            }
        }
    }

    public bool TargetReady()
    {
        return targetReady;
    }
}
