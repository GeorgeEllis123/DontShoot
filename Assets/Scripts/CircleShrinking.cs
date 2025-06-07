using UnityEngine;

public class CircleShrinking : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float startingScale = 5f;

    private int level = 0;

    private float currentScale;

    private void OnEnable()
    {
        level++;
        ResetCircle();
    }

    void Update()
    {
        int levelScale = level / 2;
        currentScale -= speed * Time.deltaTime * ((float) levelScale * .25f + 1);
        gameObject.transform.localScale = new Vector3(currentScale, currentScale, 0);
    }

    public void ResetCircle()
    {
        currentScale = startingScale;
        gameObject.transform.localScale = new Vector3(startingScale, startingScale, 0);
    }
}
