using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingSplittingBubble : Projectile
{
    GameObject bubble;

    Transform player;
    Transform boss;

    public int availableSplits = 3;

    [SerializeField]
    [Range(0, 360)]
    private float splitAngle = 90;

    private void Start()
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce((player.position - boss.position).normalized);
        rigidBody.AddForce(Vector3.up);
    }

    protected override void Hit(Collider other)
    {
        if (other.tag == "Player") DamageHandler(other.gameObject.GetComponent<Health>(), other.gameObject.GetComponent<ElementMain>());
        if (other.tag == "Ground" && availableSplits != 0) SplitBubble();
        Destroy(gameObject);
    }

    private void SplitBubble()
    {
        GameObject bubbleInstance1 = Instantiate(bubble);
        GameObject bubbleInstance2 = Instantiate(bubble);

        Vector3 movementDirection = player.position - boss.position;
        Vector3 goLeft = Quaternion.AngleAxis(-splitAngle, Vector3.up) * movementDirection.normalized;
        Vector3 goRight = Quaternion.AngleAxis(splitAngle, Vector3.up) * movementDirection.normalized;

        bubbleInstance1.GetComponent<Rigidbody>().AddForce(goLeft);
        bubbleInstance2.GetComponent<Rigidbody>().AddForce(goRight);

        bubbleInstance1.GetComponent<BouncingSplittingBubble>().availableSplits = availableSplits - 1;
        bubbleInstance2.GetComponent<BouncingSplittingBubble>().availableSplits = availableSplits - 1;
    }
}
