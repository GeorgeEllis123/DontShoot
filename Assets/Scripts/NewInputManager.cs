using UnityEngine;

public class NewInputManager : MonoBehaviour
{
    [Header("Level Stats")]
    [SerializeField] private bool timingRequired;
    [SerializeField] private float timingCircleScale = 5f;

    [Header("References")]
    [SerializeField] private NewPatternManager pm;
    [SerializeField] private CircleShrinking circleShrinking;
    [SerializeField] private GameObject sweetSpot;
    [SerializeField] private GameObject barrel;
    [SerializeField] private BulletSpawner bs;
    [SerializeField] private ParticleSystem smoke;

    private AudioSource[] audiosources;
    private AudioSource spinSFX;
    private AudioSource clickSFX;

    private void Awake()
    {
        audiosources = GetComponents<AudioSource>();
        spinSFX = audiosources[0];
        clickSFX = audiosources[1];
        if (!timingRequired)
        {
            sweetSpot.SetActive(false);
        }
        else
        {
            sweetSpot.SetActive(true);
            sweetSpot.transform.localScale = Vector3.one * timingCircleScale;
        }
    }

    void Update()
    {
        if (circleShrinking.transform.localScale.x < barrel.transform.localScale.x)
        {
            pm.GetShot(true);
        }

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.D))
        {
            if (circleShrinking.transform.localScale.x < sweetSpot.transform.localScale.x || !timingRequired)
            {
                bool correct = pm.VerifyClick(true);
                if (correct)
                    bs.SpawnBullet();
                spinSFX.Play();
                circleShrinking.ResetCircle();
            }
            else
            {
                pm.GetShot(true);
            }
        }
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.A))
        {
            if(circleShrinking.transform.localScale.x < sweetSpot.transform.localScale.x || !timingRequired)
            {
                bool correct = pm.VerifyClick(false);
                if (correct)
                    smoke.Play();
                clickSFX.Play();
                circleShrinking.ResetCircle();
            }
            else
            {
                pm.GetShot(true);
            }
        }
    }
}
