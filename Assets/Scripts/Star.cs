using UnityEngine;

public class Star : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    [SerializeField] private int scoreEarned = 5;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxLifeTime = 10f;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "player")
        {
            Destroy(this.gameObject);
            GameplayEvents.OnEarnScore?.Invoke(scoreEarned);
        }
    }
}
