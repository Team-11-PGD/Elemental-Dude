using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWallMoveToCenter : WaterWaveScript
{
    [SerializeField]
    private Transform roomCenter;

    private Vector3 moveToCenter;

    #region Dev-tool
    [SerializeField]
    private bool devToolActive, start1, start2;
    #endregion

    private bool stage1Moving, stage2Moving;

    [SerializeField]
    private float stage1Distance = 30, stage2Distance = 20;

    protected override void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        moveToCenter = (roomCenter.position - transform.position).normalized * waveSpeed;
        canDamage = true;

        #region Dev-tool
        if(devToolActive) StartCoroutine(Appear());
        #endregion
    }

    void Update()
    {
        #region Dev-tool
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
        #endregion

        float centerAndWallDistance = (roomCenter.position - transform.position).magnitude;

        if (stage1Moving && centerAndWallDistance <= stage1Distance) StopClosingIn(1);
        if (stage2Moving && centerAndWallDistance <= stage2Distance) StopClosingIn(2);
    }

    public IEnumerator Appear()
    {
        rigidBody.AddForce(Vector3.up * waveSpeed);
        yield return new WaitUntil(() => transform.position.y >= roomCenter.position.y);
        rigidBody.velocity = Vector3.zero;
        StartStage(1);
    }

    public void StartStage(int stage)
    {
        if(stage == 1) stage1Moving = true;
        if(stage == 2) stage2Moving = true;
        rigidBody.AddForce(moveToCenter);
    }

    public void StopClosingIn(int stage)
    {
        if (stage == 1) stage1Moving = false;
        if (stage == 2) stage2Moving = false;
        rigidBody.velocity = Vector3.zero;
    }
}
