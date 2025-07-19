using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _shieldObject;
    [SerializeField] private GameObject[] _explosionPrefabs;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Vector3 _projectileOffset = new Vector3(0, 0.85f, 0);
    [SerializeField] private AudioClip _laserSoundClip;
    [SerializeField] private AudioClip _powerUpSoundClip;
    [SerializeField] private AudioClip _explosionSoundClip;

    [SerializeField] private int _lives = 3;
    [SerializeField] private int _maxLives = 3;
    [SerializeField] private int _laserShots = 15;
    [SerializeField] private int _maxLaserShots = 15;
    [SerializeField] private float _speed = 5f; // in meters per second
    [SerializeField] private float _speedMultiplier = 2f;
    [SerializeField] private float _fireRate = 0.25f;
    [SerializeField] private float _nextFireTime = 0f;
    [SerializeField] private float _tripleShotPowerDown = 5f;
    [SerializeField] private float _speedPowerDown = 5f;
    [SerializeField] private float _shieldPowerDown = 5f;

    private SpawnManager _spawnManager;

    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _direction;
    private Vector3 _newPosition;

    private bool _isTripleShotActive = false;
    private bool _isShieldActive = false;
    private int _score = 0;

    private AudioSource _audioSource;
    private UIManager _uiManager;

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager == null) Debug.LogError("The UIManager is null");
        
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null) Debug.LogError("The AudioSource is null");

        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null) Debug.LogError("The SpawnManager is null");
        
        _score = 0;
        _lives = _maxLives;
        _laserShots = _maxLaserShots;
        UpdateScore(_score);
        _uiManager.UpdateLives(_lives);
        _uiManager.UpdateLaserShots(_laserShots);
        
        transform.position = Vector3.zero;
        
    }

    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFireTime && _laserShots > 0)
        {
            FireProjectile();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _speed *= _speedMultiplier;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _speed /= _speedMultiplier;
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
        _laserShots--;
        _uiManager.UpdateLaserShots(_laserShots);
        
        if (_isTripleShotActive){
            Instantiate(_tripleShotPrefab, transform.position, _projectilePrefab.transform.rotation);
        } else {
            Instantiate(_projectilePrefab, transform.position + _projectileOffset, _projectilePrefab.transform.rotation);
        }
        _audioSource.clip = _laserSoundClip;
        _audioSource.Play();
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
        switch (_lives)
        {
            case 2:
                _explosionPrefabs[0].SetActive(true);
                break;
            case 1:
                _explosionPrefabs[1].SetActive(true);
                break;
            case 0:
                _spawnManager.OnPlayerDeath();
                _audioSource.clip = _explosionSoundClip;
                _audioSource.Play();
                Destroy(this.gameObject, 3.0f);
                break;
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        _audioSource.clip = _powerUpSoundClip;
        _audioSource.Play();
        StartCoroutine(TripleShotPowerDown());
    }

    public void SpeedActive()
    {
        _speed *= _speedMultiplier;
        _audioSource.clip = _powerUpSoundClip;
        _audioSource.Play();
        StartCoroutine(SpeedPowerDown());
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldObject.SetActive(true);
        _audioSource.clip = _powerUpSoundClip;
        _audioSource.Play();
        StartCoroutine(ShieldPowerDown());
    }

    public void AmmoRecharge()
    {
        _laserShots = _maxLaserShots;
        _uiManager.UpdateLaserShots(_laserShots);
        _audioSource.clip = _powerUpSoundClip;
        _audioSource.Play();
        // StartCoroutine(AmmoRechargePowerDown());
    }

    public void HealthCollected()
    {
        if (_lives < _maxLives)
        {
            _lives++;
            _uiManager.UpdateLives(_lives);
            
            // Reverse visual damage effects based on current lives
            switch (_lives)
            {
                case 3:
                    _explosionPrefabs[1].SetActive(false);
                    break;
                case 2:
                    _explosionPrefabs[0].SetActive(false);
                    break;
            }
            
            _audioSource.clip = _powerUpSoundClip;
            _audioSource.Play();
        }
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
