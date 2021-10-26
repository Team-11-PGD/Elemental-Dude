using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField]
    private float damageAmount = 10;

    private Rigidbody rb;
    private float dmgPercentage;

    private ElementMain.ElementType elementType;
    private ElementMain otherElementMain;

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

        if (other.gameObject.tag == "Enemy")
        {
            otherElementMain = other.gameObject.GetComponent<ElementMain>();
            DamageHandler(other.gameObject.GetComponent<Health>());
        }
    }

    public void DamageHandler(Health otherHealth)
	{
        dmgPercentage = otherElementMain.ElementDmgPercentage(otherElementMain.currentType, elementType);
        otherHealth.Hit(damageAmount * dmgPercentage);
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
