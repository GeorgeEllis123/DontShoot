using UnityEngine;

public class CameraBob : MonoBehaviour
{
    [SerializeField] private float bobSpeed = 2;
    [SerializeField] private float bobAmount = 2;

    private int direction = 1;

    private float startY;

    private void Start()
    {
        startY = transform.localPosition.y;
    }

    private void Update()
    {
        if (transform.localPosition.y - startY > bobAmount && direction == 1) 
        {
            direction = -1;
        }
        else if (transform.localPosition.y - startY < 0 && direction == -1)
        {
            direction = 1;
        }
        float newY = transform.localPosition.y + direction * Time.deltaTime * bobSpeed;

        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
    }
}
