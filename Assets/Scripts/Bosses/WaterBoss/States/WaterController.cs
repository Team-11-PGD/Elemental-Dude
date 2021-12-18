using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chris Huider
public class WaterController : MonoBehaviour
{
    [SerializeField]
    private GameObject waterWall;

    private WaterWallMoveToCenter[] arenaAssets = new WaterWallMoveToCenter[13];

    [SerializeField]
    private WaterFloorMove floor;

    [SerializeField]
    public bool startAppear, start1, start2;

    void Start()
    {
        WaterWallMoveToCenter[] walls = waterWall.GetComponentsInChildren<WaterWallMoveToCenter>();
        for (int i = 0; i < walls.Length; i++)
        {
            arenaAssets[i] = walls[i];
        }
        arenaAssets[12] = floor;
    }

    void Update()
    {
        if (startAppear)
        {
            StartStage(0);
            startAppear = false;
        }
        if (start1)
        {
            StartStage(1);
            start1 = false;
        }
        if (start2)
        {
            StartStage(2);
            start2 = false;
        }
    }

    void StartStage(int stage)
    {
        foreach (WaterWallMoveToCenter arenaAsset in arenaAssets)
        {
            arenaAsset.StartStage(stage);
        }
    }
}
