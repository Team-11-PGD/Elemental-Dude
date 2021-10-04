using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMain : MonoBehaviour
{
    public PickupType type = PickupType.Health;
    public ShowPickupText text;
    public MeshRenderer model;

    private bool allowPickup = true;

    public enum PickupType
    {
        Health,
        ShootingSpeed
    }

    void Start()
    {
        text = GetComponent<ShowPickupText>();
        model = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        //Testing code start
        if (Input.GetKeyDown(KeyCode.E) && allowPickup)
        {
            pickedUpPowerup();
        }
        //Testing code end
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.name == "Player") && allowPickup)
        {
            pickedUpPowerup();
        }
    }

    private void pickedUpPowerup()
    {
        switch (type)
        {

            case PickupType.Health:
                //Debug.Log("picked up health");
                StartCoroutine("removePickupAndActivateText");
                //Call for health functionality code
                return;
            case PickupType.ShootingSpeed:
                //Debug.Log("picked up shooting speed");
                StartCoroutine("removePickupAndActivateText");
                //Call for shooting speed functionality code
                return;
        }
    }

    private IEnumerator removePickupAndActivateText()
    {
        allowPickup = false;
        text.uiObject.SetActive(true);
        text.active = true;
        model.enabled = false;
        yield return new WaitForSeconds(4.5f);
        Destroy(model);
        Destroy(gameObject);
    }
}
