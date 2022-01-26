using UnityEngine;

public class LavaMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    void FixedUpdate()
    {
        Vector2 randomDirection = Random.insideUnitCircle * speed * Time.fixedDeltaTime;
        transform.position += new Vector3(randomDirection.x, 0, randomDirection.y);
    }
}
