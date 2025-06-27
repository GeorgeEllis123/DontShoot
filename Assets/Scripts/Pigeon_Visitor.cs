using UnityEngine;

public class Pigeon_Visitor : MonoBehaviour
{
    public bool left = true;
    public int enterLevel;
    public int exitLevel;
    private Animator animator;
    private NewLevelManager lvlMngr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        lvlMngr = FindAnyObjectByType<NewLevelManager>();
        enterLevel = Random.Range(0, lvlMngr.levelsInDay + 2);
        // range goes higher than max levels;
        // therefore unlikely for a pigeon to leave after entering
        exitLevel = Random.Range(enterLevel + 1, 15);
    }

    public void PigeonEnter()
    {
        if (left)
        {
            animator.SetTrigger("Enter");
        }
        else
        {
            animator.SetTrigger("EnterRight");
        }
    }

    public void PigeonExit()
    {
        if (left)
        {
            animator.SetTrigger("Exit");
        }
        else
        {
            animator.SetTrigger("ExitRight");
        }
    }
}

/**
 * Level difficulty adjusting:
 * 
 * Circle Shrink Speed:
 *  PlayerGun
 *   -Logic Stuff
 *    -Timing Circle
 *     -Speed
 *     
 * Sweet Spot Size:
 *  -PlayerGun
 *   -Logic Stuff
 *    -Input Manager
 *     -Timing Circle Scale
 *    
 * Load Speed:
 *  -Game Logic
 *   -Patterns
 *    -Time Between Loads
 *    
 * 
 * 
 * 
 */
