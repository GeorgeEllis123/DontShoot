using System.Collections;
using UnityEngine;

public class CircleShrinking : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float startingScale = 5f;
    [SerializeField] private GameObject sweetSpot;
    [SerializeField] private Color highlightColor;

    private int level = 0;

    private float currentScale;
    private float sweetSpotScale;
    private SpriteRenderer sRenderer;
    private Color originalColor;
    private void OnEnable()
    {
        level++;
        sRenderer = gameObject.GetComponent<SpriteRenderer>();
        originalColor = sRenderer.color;
        ResetCircle();
        sweetSpotScale = sweetSpot.transform.localScale.x;
    }

    void Update()
    {
        int levelScale = level / 2;
        currentScale -= speed * Time.deltaTime * ((float)levelScale * .25f + 1);
        gameObject.transform.localScale = new Vector3(currentScale, currentScale, 0);
        //changeCircleColor(); 
    }

    public void ResetCircle()
    {
        currentScale = startingScale;
        gameObject.transform.localScale = new Vector3(startingScale, startingScale, 0);
        sRenderer.color = originalColor;
    }

    void changeCircleColor()
    {
        if (currentScale <= sweetSpotScale)
        {
            sRenderer.color = highlightColor;
        }
        else
        {
            sRenderer.color = originalColor;
        }
    }
}
