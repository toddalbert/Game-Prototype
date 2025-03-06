using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private int _lives = 3;

    [SerializeField] private float _speed = 5f; // in meters per second
    [SerializeField] private float _speedMultiplier = 2f;

    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _shieldObject;

    [SerializeField] private float _fireRate = 0.25f;
    [SerializeField] private float _nextFireTime = 0f;
    [SerializeField] private float _tripleShotPowerDown = 5f;
    [SerializeField] private float _speedPowerDown = 5f;
    [SerializeField] private float _shieldPowerDown = 5f;

    [SerializeField] private Vector3 _projectileOffset = new Vector3(0, 0.85f, 0);

    private SpawnManager _spawnManager;

    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _direction;
    private Vector3 _newPosition;

    private bool _isTripleShotActive = false;
    private bool _isShieldActive = false;
    private int _score = 0;

    private UIManager _uiManager;

    void Start()
    {
        _score = 0;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.LogError("The UIManager is null");
        }
        
        UpdateScore(0);
        transform.position = Vector3.zero;
        
        _lives = 3;
        _uiManager.UpdateLives(_lives);
        
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("The SpawnManager is null");
        }
    }

    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFireTime)
        {
            FireProjectile();
        }
    }

    void CalculateMovement()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        _direction = new Vector3(_horizontalInput, _verticalInput, 0);

        transform.Translate(_direction * (Time.deltaTime * _speed));
        
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
        _nextFireTime = Time.time + _fireRate;
        if (_isTripleShotActive){
            Instantiate(_tripleShotPrefab, transform.position, _projectilePrefab.transform.rotation);
        } else {
            Instantiate(_projectilePrefab, transform.position + _projectileOffset, _projectilePrefab.transform.rotation);
        }
    }

    public void UpdateScore(int points) {
        _score += points;
        _uiManager.UpdateScoreText(_score);
    }

    public void TakeDamage()
    {
        if (_isShieldActive) return;
        _lives--;
        Debug.Log("Lives: " + _lives);
        _uiManager.UpdateLives(_lives);
        if (_lives <= 0)
        {
            _spawnManager.OnPlayerDeath();
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
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedPowerDown());
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldObject.SetActive(true);
        StartCoroutine(ShieldPowerDown());
    }

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(_speedPowerDown);
        _speed /= _speedMultiplier;
    }

    IEnumerator ShieldPowerDown()
    {
        yield return new WaitForSeconds(_shieldPowerDown);
        _isShieldActive = false;
        _shieldObject.SetActive(false);
    }

    IEnumerator TripleShotPowerDown()
    {
        yield return new WaitForSeconds(_tripleShotPowerDown);
        _isTripleShotActive = false;
    }
}
