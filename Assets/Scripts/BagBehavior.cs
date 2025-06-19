using UnityEngine;

public class BagBehavior : MonoBehaviour
{
    private Animator animator;
    private AudioSource sfx;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sfx = GetComponent<AudioSource>();
    }

    public void TakeOff()
    {
        animator.SetTrigger("BagOff");
        sfx.Play();
    }
}
