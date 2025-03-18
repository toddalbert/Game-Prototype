using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;

    [SerializeField] private float _topBound = 8f;
    [SerializeField] private float _bottomBound = -8f;

    private bool _isEnemyLaser = false;

    void Update()
    {
        if (_isEnemyLaser)
        {
            MoveDown();
        }
        else
        {
            MoveUp();
        }
    }

    private void MoveUp()
    {
        transform.Translate(Vector3.up * (Time.deltaTime * _speed));

        if (transform.position.y > _topBound)
        {
            // check if this object has a parent
            if (transform.parent != null)
            {
                Destroy(this.transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }

    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * (Time.deltaTime * _speed));

        if (transform.position.y < _bottomBound)
        {
            // check if this object has a parent
            if (transform.parent != null)
            {
                Destroy(this.transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }

    }

    public void SetIsEnemyLaser() {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _isEnemyLaser)
        {
            Player player = other.GetComponent<Player>();
            if (player != null) 
            {
                player.TakeDamage();
                Destroy(this.gameObject);
            }
        }
    }
}
