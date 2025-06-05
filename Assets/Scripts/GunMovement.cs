using UnityEngine;

public class GunMovement : MonoBehaviour
{
    [SerializeField] private float throwForce = 5f;
    [SerializeField] private Transform theirPos;
    [SerializeField] private Transform yourPos;

    private bool move = false;
    private int direction;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // -1 = thrown to player +1 = thrown from player
    public void Toss(int direction) 
    {
        // towards player
        if (direction < 0)
        {
            gameObject.transform.position = theirPos.position;
        }
        else 
        {
            gameObject.transform.position = yourPos.position;
        }
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(throwForce * direction, 0), ForceMode2D.Impulse);

    }
}
