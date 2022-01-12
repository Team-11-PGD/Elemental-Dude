using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damageAmount;
    [SerializeField]
    [TagSelector]
    private string[] hitableTags;

    private ElementMain.ElementType elementType;


    protected void Collided(Collider other)
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
        Debug.Log("projectile HIT " + damageAmount);
        DamageHandler(other.gameObject.GetComponent<Health>(), other.gameObject.GetComponent<ElementMain>());
        Destroy(gameObject);
    }

    protected void DamageHandler(Health otherHealth, ElementMain otherElementMain)
    {
        Debug.Log("projectile DAMAGAEHANDLER " + damageAmount);
        float dmgPercentage = otherElementMain.ElementDmgPercentage(elementType, otherElementMain.currentType);
        otherHealth.Hit(damageAmount * dmgPercentage);
    }

    internal void SetElementType(ElementMain.ElementType type)
    {
        elementType = type;
    }

    internal void SetDamage(float DamageAmount)
    {
        damageAmount = DamageAmount;
    }
}