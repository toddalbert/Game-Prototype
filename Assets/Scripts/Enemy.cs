using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 4.0f;
    private float bottomBound = -4f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);

        if (transform.position.y < bottomBound)
        {
            float randomX = Random.Range(-10.5f, 10.5f);
            transform.position = new Vector3(randomX, 6, 0);
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
            }
        } else if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
        }
        Destroy(this.gameObject);
    }
}
