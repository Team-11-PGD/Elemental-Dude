using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chris Huider
public class WaterWallMoveToCenter : WaterWaveScript
{

    public Transform roomCenter;

    protected Vector3 moveToCenter;

    protected bool stage1Moving, stage2Moving;

    protected Vector3 startPosition;

    [SerializeField]
    private float stage1Distance, stage2Distance;

    protected override void Start()
    {
        startPosition = gameObject.transform.position;
        rigidBody = GetComponent<Rigidbody>();
        canDamage = true;
    }
 
    protected virtual void Update()
    {
        float centerAndWallDistance = (roomCenter.position - transform.position).magnitude;

        if (stage1Moving && centerAndWallDistance <= stage1Distance) StopClosingIn(1);          //Stops transformation once complete.
        if (stage2Moving && centerAndWallDistance <= stage2Distance) StopClosingIn(2);          //<-
    }

    /// <summary>
    /// Start first stage of water boss arena transformation.
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator Appear()
    {
        rigidBody.AddForce(Vector3.up * waveSpeed);
        yield return new WaitUntil(() => transform.position.y -6 >= roomCenter.position.y);     //The -6 is so it lines up with the bottom and not the middle
        moveToCenter = (roomCenter.position - transform.position).normalized * waveSpeed;
        rigidBody.velocity = Vector3.zero;
        StartStage(1);
    }

    /// <summary>
    /// Make water boss arena transformation slowly disappear into the ground.
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator Disappear()
    {
        rigidBody.AddForce(Vector3.down * waveSpeed);
        yield return new WaitUntil(() => transform.position.y - 6 >= startPosition.y);
        rigidBody.velocity = Vector3.zero;
    }

    /// <summary>
    /// Start given stage.
    /// </summary>
    /// <param name="stageID"></param>
    public void StartStage(int stageID)
    {
        if (stageID == 0) {StartCoroutine(Appear()); /*rigidBody.AddForce(moveToCenter);*/ }
        if (stageID == 1) {stage1Moving = true; /*rigidBody.AddForce(moveToCenter); */}
        if (stageID == 2) {stage2Moving = true; /*rigidBody.AddForce(moveToCenter); */}
        if (stageID == 3) {StartCoroutine(Disappear()); /*rigidBody.AddForce(-moveToCenter);*/}
    }

    /// <summary>
    /// Stops arena assets' movements when stage is complete.
    /// </summary>
    /// <param name="stageID"></param>
    public void StopClosingIn(int stageID)
    {
        if (stageID == 1) stage1Moving = false;
        if (stageID == 2) stage2Moving = false;
        rigidBody.velocity = Vector3.zero;
    }
}
