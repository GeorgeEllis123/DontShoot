using UnityEngine;

public class BarrelAnimation : MonoBehaviour
{

    public float rotationSpeed = 180f;

    private Quaternion targetRotation;
    private bool isRotating = false;

    void Start()
    {
        targetRotation = transform.rotation;
    }

    // update is better for on the fly rotations (since user could be clicking really fast)
    void Update()
    {
        if (isRotating)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation;
                isRotating = false;
            }
        }
    }


    public void RotateMinus60()
    {
        targetRotation = targetRotation * Quaternion.Euler(0, 0, -60f);
        isRotating = true;
    }

    public void ResetRotation()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
