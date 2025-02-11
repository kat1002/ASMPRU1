using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float shootDelay = 0.5f; // Delay between shots
    private Rigidbody2D rb2d;
    [SerializeField] private Bullet pBullet;
    [SerializeField] private Transform shootPosition;
    private Vector2 moveInput;
    private float lastShotTime = 0f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb2d.AddForce(moveInput * moveSpeed);
    }

    public void Move(InputAction.CallbackContext value)
    {
        moveInput = value.ReadValue<Vector2>();
    }

    public void Shoot(InputAction.CallbackContext value)
    {
        if (value.performed && Time.time >= lastShotTime + shootDelay)
        {
            lastShotTime = Time.time; // Update last shot time
            Bullet bullet = Instantiate(pBullet, shootPosition.position, Quaternion.Euler(0, 0, 45));
            bullet.Project(this.transform.up);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            Destroy(this.gameObject);
            GameplayEvents.OnGameOver?.Invoke();
        }
    }
}
