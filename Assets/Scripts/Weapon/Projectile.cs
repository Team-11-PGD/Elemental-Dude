using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    protected float damageAmount = 10;
    [SerializeField]
    [TagSelector]
    private string[] hitableTags;

    private ElementMain.ElementType elementType;

    private void OnCollisionEnter(Collision collision)
    {
        Collided(collision.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        Collided(other);
    }

    private void Collided(Collider other)
    {
        foreach (string tag in hitableTags)
        {
            if (other.gameObject.tag == tag)
            {
                Hit(other);
                return;
            }
        }
    }

    protected virtual void Hit(Collider other)
    {
        DamageHandler(other.gameObject.GetComponent<Health>(), other.gameObject.GetComponent<ElementMain>());
        Destroy(gameObject);
    }

    protected void DamageHandler(Health otherHealth, ElementMain otherElementMain)
	{
        float dmgPercentage = otherElementMain.ElementDmgPercentage(elementType, otherElementMain.currentType);
        otherHealth.Hit(damageAmount * dmgPercentage);
	}

    internal void SetElementType(ElementMain.ElementType type)
    {
        elementType = type;
    }
}
