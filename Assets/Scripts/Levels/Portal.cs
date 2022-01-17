using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Range(0, RoomGeneration.PATH_OPTIONS - 1)]
    [SerializeField] int nextSceneId;
    [SerializeField] Renderer rend;

    private void Start()
    {
        // TODO: add option to have multiple elements on a portal
        Color color1 = ElementColors.GetElement(RoomGeneration.NextElements[nextSceneId][0]).color;
        rend.material.SetColor("_Color", color1);

        Color color2 = color1;
        if (RoomGeneration.NextElements[nextSceneId].Count == 2)
        {
            color2 = ElementColors.GetElement(RoomGeneration.NextElements[nextSceneId][1]).color;
        }
        rend.material.SetColor("_Color2", color2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            RoomGeneration.LoadNextLevel(RoomGeneration.NextElements[nextSceneId], RoomGeneration.Level < RoomGeneration.MULTI_ELEMENT_LEVEL ? 1 : RoomGeneration.Level < RoomGeneration.FINAL_LEVEL ? 2 : 3);
        }
    }
}
