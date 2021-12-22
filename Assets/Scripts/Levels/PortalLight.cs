using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalLight : MonoBehaviour
{
    [SerializeField]
    Renderer portal;
    [SerializeField]
    Light light;

    void FixedUpdate()
    {
        Color color = Color.Lerp(portal.material.GetColor("_Color"), portal.material.GetColor("_Color2"), Mathf.Sin(Time.time));
        light.color = color;
    }
}
