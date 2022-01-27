using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Chris Huider
public class BouncingSplittingBubble : Projectile
{
    public GameObject bubble;

    public Transform player, boss;

    public int availableSplits = 3;

    [SerializeField]
    [Range(0, 360)]
    private float splitAngle = 90;

    [SerializeField]
    private float bubbleMovementSpeed;

    [SerializeField]
    private float bubbleBounceHeight;

    private void Start()
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce((player.position - boss.position).normalized * bubbleMovementSpeed);
        rigidBody.AddForce(Vector3.up * bubbleBounceHeight);
    }

    void OnCollisionEnter(Collision collision)
    {
        Collided(collision.collider);
    }

    /// <summary>
    /// Checks if ground or player are hit and chooses functionality accordingly.
    /// </summary>
    /// <param name="other"></param>
    protected override void Hit(Collider other)
    {
        if (other.tag == "Player") DamageHandler(other.gameObject.GetComponentInParent<Health>(), other.gameObject.GetComponentInParent<ElementMain>());
        if (other.tag == "Ground" && availableSplits != 0) SplitBubble();
        Destroy(gameObject);
    }

    /// <summary>
    /// Split bubble instance into two new bubble instances.
    /// </summary>
    private void SplitBubble()
    {
        Vector3 movementDirection = (player.position - boss.position) * bubbleMovementSpeed;
        Vector3 goLeft = Quaternion.AngleAxis(-splitAngle, Vector3.up) * movementDirection.normalized;              //Vector3 with defined angle to the left.
        Vector3 goRight = Quaternion.AngleAxis(splitAngle, Vector3.up) * movementDirection.normalized;              //Vector3 with defined angle to the right.

        GameObject bubbleInstance1 = Instantiate(bubble, transform.position + Vector3.up * 2 + movementDirection.normalized + goLeft, Quaternion.identity);
        GameObject bubbleInstance2 = Instantiate(bubble, transform.position + Vector3.up * 2 + movementDirection.normalized + goRight, Quaternion.identity);

        BouncingSplittingBubble bubbleInstance1Script = bubbleInstance1.GetComponent<BouncingSplittingBubble>();
        BouncingSplittingBubble bubbleInstance2Script = bubbleInstance2.GetComponent<BouncingSplittingBubble>();

        bubbleInstance1.GetComponent<Rigidbody>().AddForce(goLeft * bubbleMovementSpeed);                           //Make bubble go left using Vector3 goLeft.
        bubbleInstance2.GetComponent<Rigidbody>().AddForce(goRight * bubbleMovementSpeed);                          //Make bubble go right using Vector3 goRight.

        bubbleInstance1Script.availableSplits = availableSplits - 1;
        bubbleInstance2Script.availableSplits = availableSplits - 1;

        GiveTarget(boss, player, bubbleInstance1Script);
        GiveTarget(boss, player, bubbleInstance2Script);
    }

    /// <summary>
    /// Give a target to bubble instance which it moves towards.
    /// </summary>
    /// <param name="boss"></param>
    /// <param name="player"></param>
    /// <param name="instance"></param>
    public void GiveTarget(Transform boss, Transform player, BouncingSplittingBubble instance = null)
    {
        if(instance != null)
        {
            instance.player = player;
            instance.boss = boss;
        }
        else
        {
            this.boss = boss;
            this.player = player;
        }
    }
}
