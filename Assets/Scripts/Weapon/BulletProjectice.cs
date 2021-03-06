using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectice : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    int damage = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            Debug.Log("Hit");
            collision.transform.GetComponent<Health>().Hit(damage);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Bullet")
        {
            Destroy(gameObject);
        }
    }

	internal void SetVelocity(Vector3 forward)
	{
        rb.velocity = forward;
    }
}
