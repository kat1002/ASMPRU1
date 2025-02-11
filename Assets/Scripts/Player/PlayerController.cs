using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float shootDelay = 0.5f; // Delay between shots
    [SerializeField] private Camera cam;
    private Rigidbody2D rb2d;
    [SerializeField] private Bullet pBullet;
    [SerializeField] private Transform shootPosition;
    private Vector2 moveInput;
    private float lastShotTime = 0f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        LookAtMouse();
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
            Bullet bullet = Instantiate(pBullet, shootPosition.position, transform.rotation * Quaternion.Euler(0, 0, 45));
            bullet.Project(this.transform.up);
        }
    }

    public void LookAtMouse()
    {
        Vector3 mousePosition = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
        float angleRad = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad - 90;
        transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            GameplayEvents.OnExplosion?.Invoke(this.transform);
            Destroy(this.gameObject);
            GameplayEvents.OnGameOver?.Invoke();
        }
    }
}
