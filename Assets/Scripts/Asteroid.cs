using UnityEngine;

public class Asteroid : MonoBehaviour
{

    [SerializeField] private float _speed = 40.0f;

    [SerializeField] GameObject _explosionPrefab;
    // [SerializeField] private float _bottomBound = -4.8f;
    // [SerializeField] private float _topBound = 8.0f;
    // [SerializeField] private int _points = 10;

    // private Player _player;
    // private Animator _animator;
    // private Collider2D _collider;
    // private bool _isDestroyed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // _player = GameObject.Find("Player").GetComponent<Player>();
        // if (_player == null) Debug.LogError("The Player is null");

        // _animator = GetComponent<Animator>();
        // if (_animator == null) Debug.LogError("The Animator is null");

        // _collider = GetComponent<Collider2D>();
        // if (_collider == null) Debug.LogError("The Collider is null");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, _speed * Time.deltaTime, Space.Self);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 2.6f);
            Destroy(this.gameObject, 0.3f);
        }
    }
}
