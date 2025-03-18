using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private float _bottomBound = -4.8f;
    [SerializeField] private float _topBound = 8.0f;
    [SerializeField] private int _points = 10;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private AudioClip _laserSoundClip;

    private Player _player;
    private Animator _animator;
    private Collider2D _collider;
    private AudioSource _audioSource;
    private bool _isDestroyed = false;
    private float _fireRate = 3.0f;
    private float _canFire = -1f;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null) Debug.LogError("The Player is null");

        _animator = GetComponent<Animator>();
        if (_animator == null) Debug.LogError("The Animator is null");

        _collider = GetComponent<Collider2D>();
        if (_collider == null) Debug.LogError("The Collider is null");

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null) Debug.LogError("The AudioSource is null");

        _canFire = -1f;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();
            foreach (Laser laser in lasers)
            {
                laser.SetIsEnemyLaser();
            }
            // _audioSource.clip = _laserSoundClip;
            // _audioSource.Play();
        }
    }

    private void CalculateMovement()
    {
        transform.Translate(Vector3.down * (Time.deltaTime * _speed));

        if (transform.position.y < _bottomBound && !_isDestroyed)
        {
            float randomX = Random.Range(-10.5f, 10.5f);
            transform.position = new Vector3(randomX, _topBound, 0);
        }
    }

    private void OnDestroy()
    {
        _animator.SetTrigger("OnEnemyDeath");
        _isDestroyed = true;
        _audioSource.Play();
        // _speed = 0;
        _collider.enabled = false;
        Destroy(this.gameObject, 3.1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null) 
            {
                player.TakeDamage();
                OnDestroy();
            }
        } else if (other.CompareTag("Projectile"))
        {
            if (_player != null) 
            {
                _player.UpdateScore(_points);
            }
            Destroy(GetComponent<Collider2D>());
            OnDestroy();
            Destroy(other.gameObject);
        }
    }

}
