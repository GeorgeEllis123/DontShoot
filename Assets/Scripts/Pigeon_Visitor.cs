using UnityEngine;

public class Pigeon_Visitor : MonoBehaviour
{
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PigeonEnter()
    {
        animator.SetTrigger("Enter");
    }

    public void PigeonExit()
    {
        animator.SetTrigger("Exit");
    }
}
