using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2d;


    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxLifeTime = 10f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        this.transform.eulerAngles = new Vector3(0f, 0f, Random.value * 360f);
    }

    public void SetTrajectory(Vector2 direction)
    {
        rigidbody2d.AddForce(direction * this.speed);
        Destroy(this.gameObject, maxLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            GameplayEvents.OnExplosion?.Invoke(this.transform);
            Destroy(this.gameObject);
        }
    }
}
