using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectice : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

	private void Start()
	{
       
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
