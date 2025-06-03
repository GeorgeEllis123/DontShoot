using UnityEngine;

public class BarrelBehavior : MonoBehaviour
{
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TimingCircle"))
        {
            levelManager.GameOver();
        }
    }
}
