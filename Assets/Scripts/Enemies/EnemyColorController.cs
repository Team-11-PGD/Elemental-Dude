using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColorController : MonoBehaviour
{
    [SerializeField]
    ElementMain elementMain;
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        //Fire element
        if (elementMain.currentType == ElementMain.ElementType.Fire)
        {
            rend.material = ColorMaster.instance.ElementFire;
        }
        //Water element
        if (elementMain.currentType == ElementMain.ElementType.Water)
        {
            rend.material = ColorMaster.instance.ElementWater;
        }
        //Earth element
        if (elementMain.currentType == ElementMain.ElementType.Earth)
        {
            rend.material = ColorMaster.instance.ElementEarth;
        }
        //Air element
        if (elementMain.currentType == ElementMain.ElementType.Air)
        {
            rend.material = ColorMaster.instance.ElementAir;
        }
    }
}
