using UnityEngine;

public class Pigeon_Visitor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PigeonEnter()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Enter");

    }
}
