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
    private ElementMain OtherElementMain;
    private Health OtherHealth;

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
            OtherElementMain = other.gameObject.GetComponent<ElementMain>();
            OtherHealth = other.gameObject.GetComponent<Health>();
            DamageHandler();
        }
    }

    public void DamageHandler()
	{
        dmgPercentage = OtherElementMain.ElementDmgPercentage(OtherElementMain.currentType, elementType);
        OtherHealth.Hit(damageAmount * dmgPercentage);
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
