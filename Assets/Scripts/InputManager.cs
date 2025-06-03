using UnityEngine;

public class InputManager : MonoBehaviour
{
    //[SerializeField] private BulletGenerator bulletGenerator;
    [SerializeField] private CircleShrinking cs;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            cs.ResetCircle();
            // if (bulletGenerator.VerifyClick
        }
        if (Input.GetMouseButtonDown(0))
        {
            cs.ResetCircle();
        }
    }
}
