using UnityEngine;

public class PowerUp : MonoBehaviour
{

    private float _speed = 3f;
    
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -5.5f)
        {
            Destroy(this.gameObject);
        }
    }    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null) 
            {
                player.TripleShotActive();
            }
            Destroy(this.gameObject);
        }
    }
}
