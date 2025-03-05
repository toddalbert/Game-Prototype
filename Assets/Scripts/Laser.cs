using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;

    [SerializeField] private float _topBound = 8f;

    void Update()
    {
        transform.Translate(Vector3.up * (Time.deltaTime * _speed));

        if (transform.position.y > _topBound)
        {
            Destroy(this.gameObject);
        }
    }
}
