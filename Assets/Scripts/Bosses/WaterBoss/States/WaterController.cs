using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chris Huider
public class WaterController : MonoBehaviour
{
    [SerializeField]
    private GameObject waterWall;

    [SerializeField]
    private WaterFloorMove floor;

    private WaterWallMoveToCenter[] arenaAssets = new WaterWallMoveToCenter[13];

    #region Debug-Tool Variables
    [SerializeField]
    public bool startAppear, start1, start2, start3;
    #endregion

    void Start()
    {
        WaterWallMoveToCenter[] walls = waterWall.GetComponentsInChildren<WaterWallMoveToCenter>();

        for (int i = 0; i < walls.Length; i++)
        {
            arenaAssets[i] = walls[i];                      //Sets arenaAssets 0-11 as water walls.
        }
        arenaAssets[12] = floor;                            //Sets arenaAsset 12 as water floor.
    }

    void Update()
    {
        //Debug tool for starting water boss arena transformations.
        #region Debug-Tool Functionality
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
        if (start3)
        {
            StartStage(3);
            start3 = false;
        }
        #endregion
    }

    /// <summary>
    /// Start water boss arena transformation stage to given stageID.
    /// </summary>
    /// <param name="stageID"></param>
    void StartStage(int stageID)
    {
        foreach (WaterWallMoveToCenter arenaAsset in arenaAssets)
        {
            arenaAsset.StartStage(stageID);
        }
    }
}
