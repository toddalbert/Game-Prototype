using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private float _bottomBound = -4.8f;
    [SerializeField] private float _topBound = 8.0f;

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
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
