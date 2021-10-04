using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupMain : MonoBehaviour
{
    public PickupType type = PickupType.Health;
    private ShowPickupText text;
    private MeshRenderer model;
    private HealthPickupFunctionality healthFunc;
    private ShootingSpeedPickupFunctionality shootSpeedFunc;

    [SerializeField]
    public string HealthPowerupText = "You picked up health.";
    [SerializeField]
    public string ShootingSpeedPowerupText = "You picked up extra shooting speed.";

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
        healthFunc = GetComponent<HealthPickupFunctionality>();
        shootSpeedFunc = GetComponent<ShootingSpeedPickupFunctionality>();
    }

    void Update()
    {
        ///Testing code start
        if (Input.GetKeyDown(KeyCode.E) && allowPickup)
        {
            pickedUpPowerup();
        }
        ///Testing code end
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
                text.powerupText = HealthPowerupText;
                StartCoroutine("removePickupAndActivateText");
                healthFunc.AddHealth();
                return;
            case PickupType.ShootingSpeed:
                //Debug.Log("picked up shooting speed");
                text.powerupText = ShootingSpeedPowerupText;
                StartCoroutine("removePickupAndActivateText");
                shootSpeedFunc.IncreaseShootingSpeed();
                return;
        }
    }

    private IEnumerator removePickupAndActivateText()
    {
        allowPickup = false;
        text.uiObject.SetActive(true);
        text.active = true;
        model.enabled = false;
        yield return new WaitForSeconds(text.showTextDuration + 0.1f);
        Destroy(model);
        Destroy(gameObject);
    }
}
