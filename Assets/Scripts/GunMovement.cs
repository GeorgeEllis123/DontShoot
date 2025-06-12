using UnityEngine;

public class GunMovement : MonoBehaviour
{
    //[SerializeField] private float throwForce = 5f;
    [SerializeField] private Transform theirPos;
    [SerializeField] private Transform yourPos;

    //[SerializeField] private float smallScaleFactor = 0.75f;
    //[SerializeField] private float scaleDuration = 0.2f;

    //private bool move = false;
    //private int direction;
    //private Rigidbody2D rb;

    private Vector3 originalScale;
    private Vector3 smallScale;

    //private Coroutine scaleCoroutine = null;

    private Animator anim;
    private AudioSource audioSource; 

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); 

        //rb = GetComponent<Rigidbody2D>();

        originalScale = transform.localScale;
        //smallScale = originalScale * smallScaleFactor;
    }

    // -1 = thrown to player +1 = thrown from player
    public void Toss(int direction)
    {
        //Transform target = null;

        // towards player
        if (direction < 0)
        {
            gameObject.transform.position = theirPos.position;
            smallScale = transform.localScale;
            anim.SetTrigger("Towards");
            audioSource.Play();

            //transform.localScale = smallScale;
            //StartScaleCoroutine(smallScale, originalScale);
            //target = yourPos;
            // Invoke("CallPickUp", 1f);
        }
        else
        {
            gameObject.transform.position = yourPos.position;
            transform.localScale = originalScale;
            anim.SetTrigger("Away");
            audioSource.Play(); 

            //transform.localScale = originalScale;
            //StartScaleCoroutine(originalScale, smallScale);
            //target = theirPos;
        }

        //Vector2 forceDir = (target.position - transform.position).normalized;

        //rb.linearVelocity = Vector2.zero;
        //rb.AddForce(forceDir * throwForce, ForceMode2D.Impulse);

        //rb.linearVelocity = Vector2.zero;
        //rb.AddForce(new Vector2(throwForce * direction, 0), ForceMode2D.Impulse);

    }


    // hey at least i got some coding practice in!

    //private void StartScaleCoroutine(Vector3 fromScale, Vector3 toScale)
    //{
    //    if (scaleCoroutine != null)
    //        StopCoroutine(scaleCoroutine);

    //    scaleCoroutine = StartCoroutine(ScaleRoutine(fromScale, toScale));
    //}

    //private IEnumerator ScaleRoutine(Vector3 start, Vector3 end)
    //{
    //    float elapsed = 0f;

    //    while (elapsed < scaleDuration)
    //    {
    //        transform.localScale = Vector3.Lerp(start, end, elapsed / scaleDuration);
    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }

    //    transform.localScale = end;
    //    scaleCoroutine = null;
    //}

    void CallPickUp()
    {
        anim.SetTrigger("Pickup");
    }
}
