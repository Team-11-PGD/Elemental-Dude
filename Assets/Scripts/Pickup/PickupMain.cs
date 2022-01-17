using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Chris Huider
public class PickupMain : MonoBehaviour
{
    [SerializeField]
    protected ShowPickupText text;
    [SerializeField]
    protected MeshRenderer model;
    protected Collider user;

    protected bool allowPickup = true;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Player") && allowPickup)
        {
            user = other;
            PickedUpPickup();
            //SOUND: (pick up)
        }
    }

    protected virtual void PickedUpPickup(){}

    protected IEnumerator RemovePickupOnTimer()
    {
        allowPickup = false;
        text.StartText();
        model.enabled = false;

        yield return new WaitUntil(() => !text.uiObject.activeInHierarchy);

        Destroy(model);
        Destroy(gameObject);
    }
}
