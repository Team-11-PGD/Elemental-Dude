using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgText : MonoBehaviour
{
    // Start is called before the first frame update

    public float RemoveText = 3f;
    public Vector3 offset = new Vector3(0, 3, 0);
    public Vector3 RandomOffset = new Vector3(1, 0, 0);

    void Start()
    {
        //destroy gameobject after RemoveText amount of seconds
        Destroy(gameObject, RemoveText);

        transform.localPosition += offset;
        transform.localPosition += new Vector3(
            Random.Range(-RandomOffset.x, RandomOffset.x),
            Random.Range(-RandomOffset.y, RandomOffset.y), 
            Random.Range(-RandomOffset.z, RandomOffset.z));
    }
}
