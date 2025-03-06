using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private int lives = 3;

    [SerializeField] private float speed = 5f; // in meters per second
    [SerializeField] private float speedMultiplier = 2f;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject tripleShotPrefab;

    [SerializeField] private float fireRate = 0.25f;
    [SerializeField] private float nextFireTime = 0f;
    [SerializeField] private float tripleShotPowerDown = 5f;
    [SerializeField] private float speedPowerDown = 5f;
    [SerializeField] private float shieldPowerDown = 5f;

    [SerializeField] private Vector3 _projectileOffset = new Vector3(0, 0.85f, 0);

    private SpawnManager spawnManager;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 direction;
    private Vector3 _newPosition;

    private bool _isTripleShotActive = false;
    private bool _isShieldActive = false;

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

        transform.Translate(direction * (Time.deltaTime * speed));
        
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
        if (_isTripleShotActive){
            Instantiate(tripleShotPrefab, transform.position, projectilePrefab.transform.rotation);
        } else {
            Instantiate(projectilePrefab, transform.position + _projectileOffset, projectilePrefab.transform.rotation);
        }
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

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDown());
    }

    public void SpeedActive()
    {
        speed *= speedMultiplier;
        StartCoroutine(SpeedPowerDown());
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        StartCoroutine(ShieldPowerDown());
    }

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(speedPowerDown);
        speed /= speedMultiplier;
    }

    IEnumerator ShieldPowerDown()
    {
        yield return new WaitForSeconds(shieldPowerDown);
        _isShieldActive = false;
    }

    IEnumerator TripleShotPowerDown()
    {
        yield return new WaitForSeconds(tripleShotPowerDown);
        _isTripleShotActive = false;
    }
}
