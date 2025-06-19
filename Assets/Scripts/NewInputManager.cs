using UnityEngine;

public class NewInputManager : MonoBehaviour
{
    [SerializeField] private NewPatternManager pm;
    [SerializeField] private CircleShrinking cs;
    [SerializeField] private GameObject tc;
    [SerializeField] private BulletSpawner bs;
    [SerializeField] private ParticleSystem smoke;
    private bool targetReady = false;

    private AudioSource[] audiosources;
    private AudioSource spinSFX;
    private AudioSource clickSFX;

    private void Awake()
    {
        audiosources = GetComponents<AudioSource>();
        spinSFX = audiosources[0];
        clickSFX = audiosources[1];
    }

    void Update()
    {
        if (cs.transform.localScale.x < 7f)//tc.transform.localScale.x)
        {
            targetReady = true;
        }
        else
        {
            targetReady = false;
        }
        if (cs.transform.localScale.x < tc.transform.localScale.x)
        {
            pm.GetShot(true);
        }

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.D))
        {
            //if (cs.transform.localScale.x < tc.transform.localScale.x)
            //{
            bool correct = pm.VerifyClick(true);
            if (correct)
                bs.SpawnBullet();
            spinSFX.Play();
            cs.ResetCircle();
            //}
            //else
            //{
            //    pm.GetShot(true);
            //}
        }
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.A))
        {
            //if(cs.transform.localScale.x < tc.transform.localScale.x)
            //{
            bool correct = pm.VerifyClick(false);
            if (correct)
                smoke.Play();
            clickSFX.Play();
            cs.ResetCircle();
            //}
            //else
            //{
            //    pm.GetShot(true);
            //}
        }
    }

    public bool TargetReady()
    {
        return targetReady;
    }
}
