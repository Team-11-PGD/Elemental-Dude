using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectice : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 40;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

	private void Start()
	{
        rb.velocity = transform.forward * speed;
	}

	private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
