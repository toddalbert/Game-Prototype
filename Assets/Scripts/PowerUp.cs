using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField] private float _speed = 3f;
    // ID for powerups
    [SerializeField] private int powerupId = 0;
    // 0 = triple shot, 1 = speed, 2 = shield, 3 = ammo, 4 = health

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
                switch (powerupId)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    case 3:
                        player.AmmoRecharge();
                        break;
                    case 4:
                        player.HealthCollected();
                        break;
                    default:
                        Debug.LogError("Invalid powerup ID");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
