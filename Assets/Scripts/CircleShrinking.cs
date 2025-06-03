using System.Xml.Serialization;
using UnityEngine;

public class CircleShrinking : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float startingScale = 5f;

    private float currentScale;

    private void OnEnable()
    {
        ResetCircle();
    }

    void Update()
    {
        currentScale -= speed * Time.deltaTime;
        gameObject.transform.localScale = new Vector3(currentScale, currentScale, 0);
    }

    public void ResetCircle()
    {
        currentScale = startingScale;
        gameObject.transform.localScale = new Vector3(startingScale, startingScale, 0);
    }
}
