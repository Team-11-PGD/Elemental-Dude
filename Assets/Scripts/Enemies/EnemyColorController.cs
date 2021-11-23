using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColorController : MonoBehaviour
{
    [SerializeField]
    ElementMain elementMain;
    [SerializeField]
    ColorMaster colorMaster;
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        //Fire element
        if (elementMain.currentType == ElementMain.ElementType.Fire)
        {
            rend.material = colorMaster.ElementFire;
        }
        //Water element
        if (elementMain.currentType == ElementMain.ElementType.Water)
        {
            rend.material = colorMaster.ElementWater;
        }
        //Earth element
        if (elementMain.currentType == ElementMain.ElementType.Earth)
        {
            rend.material = colorMaster.ElementEarth;
        }
        //Air element
        if (elementMain.currentType == ElementMain.ElementType.Air)
        {
            rend.material = colorMaster.ElementAir;
        }
    }
}
