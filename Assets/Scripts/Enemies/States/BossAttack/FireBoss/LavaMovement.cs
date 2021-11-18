using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 1f;

    void FixedUpdate()
    {
        transform.position += new Vector3(Random.Range(-1, 2), 0, Random.Range(-1, 2)).normalized * speed * Time.fixedDeltaTime;
    }
}
