using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int lives = 3; // in meters per second

    [SerializeField] private float speed = 5f; // in meters per second

    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private float fireRate = 0.25f;
    private float nextFireTime = 0f;

    private float projectileOffset = 0.85f;

    private SpawnManager spawnManager;

    private float horizontalInput;
    private float verticalInput;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        lives = 3;
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (spawnManager == null)
        {
            Debug.LogError("The SpawnManager is null");
        }
    }

    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFireTime)
        {
            FireProjectile();
        }
    }

    void CalculateMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * Time.deltaTime * speed);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4f, 0f), transform.position.z);

        if (transform.position.x < -11.25f)
        {
            transform.position = new Vector3(11.25f, transform.position.y, transform.position.z);
        }

        else if (transform.position.x > 11.25f)
        {
            transform.position = new Vector3(-11.25f, transform.position.y, transform.position.z);
        }
    }

    void FireProjectile()
    {
        nextFireTime = Time.time + fireRate;
        Instantiate(projectilePrefab, transform.position + new Vector3(0, projectileOffset, 0), projectilePrefab.transform.rotation);
    }

    public void TakeDamage()
    {
        lives--;
        Debug.Log("Lives: " + lives);
        if (lives <= 0)
        {
            spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}
