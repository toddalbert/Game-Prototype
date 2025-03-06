using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private float _bottomBound = -4.8f;
    [SerializeField] private float _topBound = 8.0f;
    [SerializeField] private int _points = 10;

    private Player _player;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The Player is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * (Time.deltaTime * _speed));

        if (transform.position.y < _bottomBound)
        {
            float randomX = Random.Range(-10.5f, 10.5f);
            transform.position = new Vector3(randomX, _topBound, 0);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null) 
            {
                player.TakeDamage();
                Destroy(this.gameObject);
            }
        } else if (other.CompareTag("Projectile"))
        {
            if (_player != null) 
            {
                _player.UpdateScore(_points);
            }
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
