using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMaster : MonoBehaviour
{
    public GameObject[] gameObjects;
    public Material[] material;
    // Start is called before the first frame update
    public void GiveColors()
    {
        foreach (var obj in gameObjects)
        {
            obj.GetComponent<Renderer>().sharedMaterial = material[0];
            obj.GetComponent<Renderer>().sharedMaterial = material[1];
            obj.GetComponent<Renderer>().sharedMaterial = material[2];
            obj.GetComponent<Renderer>().sharedMaterial = material[3];
        }
    }
}

