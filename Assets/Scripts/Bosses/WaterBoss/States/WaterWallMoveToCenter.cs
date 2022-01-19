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
    protected override void FixedUpdate()
    {

    }
    protected virtual void Update()
    {
        float centerAndWallDistance = (roomCenter.position - transform.position).magnitude;

        if (stage1Moving && centerAndWallDistance <= stage1Distance) StopClosingIn(1);
        if (stage2Moving && centerAndWallDistance <= stage2Distance) StopClosingIn(2);
    }

    protected virtual IEnumerator Appear()
    {
        rigidBody.AddForce(Vector3.up * waveSpeed);
        yield return new WaitUntil(() => transform.position.y -6 >= roomCenter.position.y); //The -6 is so it lines up with the bottom and not the middle
        moveToCenter = (roomCenter.position - transform.position).normalized * waveSpeed;
        rigidBody.velocity = Vector3.zero;
        StartStage(1);
    }

    protected virtual IEnumerator Disappear()
    {
        rigidBody.AddForce(Vector3.down * waveSpeed);
        yield return new WaitUntil(() => transform.position.y - 6 >= startPosition.y);
        rigidBody.velocity = Vector3.zero;
    }

    public void StartStage(int stage)
    {
        if (stage == 0) {StartCoroutine(Appear()); rigidBody.AddForce(moveToCenter); }
        if (stage == 1) {stage1Moving = true; rigidBody.AddForce(moveToCenter); }
        if (stage == 2) {stage2Moving = true; rigidBody.AddForce(moveToCenter); }
        if (stage == 3) {StartCoroutine(Disappear()); rigidBody.AddForce(-moveToCenter);}
    }

    public void StopClosingIn(int stage)
    {
        if (stage == 1) stage1Moving = false;
        if (stage == 2) stage2Moving = false;
        rigidBody.velocity = Vector3.zero;
    }
}
