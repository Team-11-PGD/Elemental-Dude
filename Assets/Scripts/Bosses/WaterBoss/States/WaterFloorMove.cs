using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chris Huider
public class WaterFloorMove : WaterWallMoveToCenter
{
    [SerializeField]
    private WaterWallMoveToCenter waterWall;
    private Collider waterfloor;

    protected override IEnumerator Appear()
    {
        yield return new WaitUntil(() => waterWall.transform.position.y - 6 >= waterWall.roomCenter.position.y); //The -6 is so it lines up with the bottom and not the middle
        moveToCenter = (roomCenter.position - transform.position).normalized * waveSpeed;
        rigidBody.velocity = Vector3.zero;
        StartStage(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
