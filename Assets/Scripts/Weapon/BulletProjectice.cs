using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectice : MonoBehaviour
{
    [SerializeField]
    private float damageAmount = 5;

    private Rigidbody rb;

    private ElementMain.ElementType elementType;

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

    internal void SetElementType(ElementMain.ElementType type)
    {
        elementType = type;
    }
}
