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

    // Update is called once per frame
    void Update()
    {
        if (health.damageTaken > 0)
        {
            DamageUiEffect();
            StartCoroutine(Timer());
        }
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.6f);

        healthDamageText.enabled = false;
        health.damageTaken = 0;
    }
    public void DamageUiEffect()
    {
        damageTaken = health.damageTaken;
        healthDamageText.enabled = true;
        healthDamageText.text = ("-" + damageTaken.ToString());
    }
}

