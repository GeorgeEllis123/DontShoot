using UnityEngine;

public class BarrelBehavior : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TimingCircle"))
        {
            Debug.Log("Fail");
        }
    }
}
