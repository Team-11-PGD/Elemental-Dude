using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupMain : MonoBehaviour
{
    protected ShowPickupText text;
    private MeshRenderer model;
    protected Collider user;

    private bool allowPickup = true;

    private void Start()
    {
        text = GetComponent<ShowPickupText>();
        model = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        ///Testing code start
        if (Input.GetKeyDown(KeyCode.E) && allowPickup)
        {
            PickedUpPowerup();
        }
        ///Testing code end
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.name == "Player") && allowPickup)
        {
            user = other;
            PickedUpPowerup();
        }
    }

    protected virtual void PickedUpPowerup()
    {
        StartCoroutine(RemovePickup());
    }

    private IEnumerator RemovePickup()
    {
        allowPickup = false;
        text.uiObject.SetActive(true);
        model.enabled = false;

        yield return new WaitUntil(() => !text.isActiveAndEnabled);

        Destroy(model);
        Destroy(gameObject);
    }
}
