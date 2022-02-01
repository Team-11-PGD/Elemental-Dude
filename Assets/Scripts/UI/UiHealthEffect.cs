using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHealthEffect : MonoBehaviour
{

    public Text healthDamageText;
    [SerializeField]
    Health health;

    private float damageTaken;

    [SerializeField] private Transform damageEffect, playerhealth;

    // Update is called once per frame
    void Update()
    {
        damageTaken = health.damageTaken;
        if (health.damageTaken > 0) DamageUiEffect();//checks if the player has taken damage
    }
    public void DamageUiEffect()
    {
        Transform damagePopUpTransform = Instantiate(damageEffect, playerhealth.position, Quaternion.identity);
        damagePopUpTransform.SetParent(playerhealth);//important if parent is not set correctly the effect will not show
        DamagePopUp damagePopUp = damagePopUpTransform.GetComponent<DamagePopUp>();
        damagePopUp.Setup(damageTaken);//sets up the effect, imput is the damage taken
        health.damageTaken = 0;
    }
}

