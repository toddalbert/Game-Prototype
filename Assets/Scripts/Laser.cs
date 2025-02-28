using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float speed = 8f;

    [SerializeField] private float topBound = 8f;

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);

        if (transform.position.y > topBound)
        {
            Destroy(this.gameObject);
        }
    }
}
