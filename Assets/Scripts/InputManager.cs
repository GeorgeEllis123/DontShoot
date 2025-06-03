using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private BarrelBehavior bb;
    [SerializeField] private CircleShrinking cs;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            cs.ResetCircle();
        }
        if (Input.GetMouseButtonDown(0))
        {
            cs.ResetCircle();
        }
    }
}
