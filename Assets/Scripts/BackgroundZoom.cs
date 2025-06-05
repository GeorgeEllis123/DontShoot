using UnityEngine;

public class BackgroundZoom : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 0.2f;

    private Vector3 startingScale;

    private void Start()
    {
        startingScale = transform.localScale;
    }

    private void Update()
    {
        float newScaler = startingScale.x + zoomSpeed * Time.deltaTime;
        transform.localScale = new Vector3(newScaler, newScaler, 0);
    }
}
