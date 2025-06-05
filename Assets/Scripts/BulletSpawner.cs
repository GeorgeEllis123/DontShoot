using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float strength = 15f;
    [SerializeField] private float rotationStrengthMin = 10f;
    [SerializeField] private float rotationStrengthMax = 20f;

    public void SpawnBullet()
    {
        GameObject newbullet = Instantiate(bullet, this.transform);
        Rigidbody2D bulletRB = newbullet.GetComponent<Rigidbody2D>();

        float angle = Random.Range(30, 60) * Mathf.Deg2Rad;

        float xForce = Mathf.Cos(angle) * strength;
        float yForce = Mathf.Sin(angle) * strength;

        bulletRB.AddForce(new Vector2(xForce, yForce),  ForceMode2D.Impulse);
        bulletRB.AddTorque(-Random.Range(rotationStrengthMin, rotationStrengthMax));
        Destroy(newbullet, 3);

    }
}
