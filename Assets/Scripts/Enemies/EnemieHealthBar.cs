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
    // Start is called before the first frame update
    void Start()
    {
        if (SliderBar != null)
        {
            SliderBar.maxValue = health.maxHp;
            SliderBar.value = health.currentHp;
        }
        BarSee.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (health.damageTaken > 0)
        {
            BarSee.SetActive(true);
            health.damageTaken = 0;
            StartCoroutine(Timer());
        }
        else
        {
            //BarSee.active = false;
        }
        if (SliderBar != null)
        {
            SliderBar.value = health.currentHp;
        }
        IEnumerator Timer()
        {
            yield return new WaitForSeconds(3f);
            BarSee.SetActive(false);
        }
    }
}
