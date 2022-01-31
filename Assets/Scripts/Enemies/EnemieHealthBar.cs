using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemieHealthBar : MonoBehaviour
{
    [SerializeField]
    Health health;
    [SerializeField]
    Slider SliderBar;
    [SerializeField]
    GameObject BarSee;

    public int TimerAmount = 3;//amount of time the healthbar is active
    private float TimeDelta;
    private bool TimerActive;

    // Start is called before the first frame update
    void Start()
    {
        if (SliderBar != null)//sets the parimeters for the healthbar slider
        {
            SliderBar.maxValue = health.maxHp;
            SliderBar.value = health.currentHp;
        }
        BarSee.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (SliderBar != null) SliderBar.value = health.currentHp;//updates the current health

        if (TimerActive)//runs the timer
        {
            TimeDelta += Time.deltaTime;
            if (TimeDelta >= TimerAmount)//if the timer is done, resets timer for next use
            {
                BarSee.SetActive(false);
                TimeDelta = 0;
                TimerActive = false;
            }
        }

        if (health.damageTaken > 0)//checks if damage was applied, if damaged activates the timer and healthbar
        {
            TimerActive = true;
            BarSee.SetActive(true);
            health.damageTaken = 0;
            TimeDelta = 0;
        }
    }
}
