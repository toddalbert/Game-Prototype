using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int lives = 3;

    [SerializeField] private float speed = 5f; // in meters per second

    [SerializeField] private GameObject projectilePrefab;

    [SerializeField] private float fireRate = 0.25f;
    [SerializeField] private float nextFireTime = 0f;

    [SerializeField] private Vector3 _projectileOffset = new Vector3(0, 0.85f, 0);

    private SpawnManager spawnManager;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 direction;
    private Vector3 _newPosition;

    void Start()
    {
        transform.position = Vector3.zero;
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

        direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * Time.deltaTime * speed);
        _newPosition = transform.position;
        _newPosition.y = Mathf.Clamp(transform.position.y, -4f, 0f);

        if (transform.position.x < -11.25f)
        {
            _newPosition.x = 11.25f;
        }

        else if (transform.position.x > 11.25f)
        {
            _newPosition.x = -11.25f;
        }

        transform.position = _newPosition;
    }

    void FireProjectile()
    {
        nextFireTime = Time.time + fireRate;
        Instantiate(projectilePrefab, transform.position + _projectileOffset, projectilePrefab.transform.rotation);
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
