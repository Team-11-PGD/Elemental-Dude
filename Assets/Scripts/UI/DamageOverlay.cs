using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageOverlay : MonoBehaviour
{
    public Image dmgOverlay;
    public Slider healthBar;
    public int HP = 10;
    int maxHP;
    Color alphaColor;
    // Start is called before the first frame update
    void Start()
    {
        maxHP = HP;
        healthBar.maxValue = maxHP;
        alphaColor = dmgOverlay.color;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = HP;
    }

    private void takeDamage()
    {
        alphaColor.a += 0.1f;
        dmgOverlay.color = alphaColor;
    }

    private void heal()
    {

    }
}
