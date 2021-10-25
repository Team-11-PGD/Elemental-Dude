using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupMain : MonoBehaviour
{
    protected ShowPickupText text;
    protected MeshRenderer model;
    protected Collider user;

    protected bool allowPickup = true;

    private void Start()
    {
        text = GetComponent<ShowPickupText>();
        model = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        ///Testing code start
        /*if (Input.GetKeyDown(KeyCode.E) && allowPickup)
        {
            PickedUpPowerup();
        }*/
        ///Testing code end
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if ((other.name == "Player") && allowPickup)
        {
            user = other;
            PickedUpPickup();
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
