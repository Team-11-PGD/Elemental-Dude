using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoState : AirBossState
{
    [SerializeField]
    BoxCollider rockSpawnArea;
    [SerializeField]
    GameObject rockPrefab;
    [SerializeField]
    Transform tornadoCenter;
    [SerializeField]
    float tornadoTime = 30;
    [SerializeField]
    float spawnTime = 2;
    [SerializeField]
    int rockAmount = 5;
    [SerializeField]
    float pullForceRock;
    [SerializeField]
    float pullForcePlayer;
    List<GameObject> rocks = new List<GameObject>();
    Vector3 direction;
    bool throwAtPlayer;

    public override void Enter(int previousStateId)
    {
        StartCoroutine(CreateRocks());
    }

    public override void Exit(int nextStateId) { }

    public void Update()
    {
        //context.transform.Rotate(Vector3.up, rotateSpeed);
        foreach (GameObject rock in rocks)
        {
            if (throwAtPlayer == false)
            {
                direction = tornadoCenter.position - rock.transform.position;
            }
            rock.GetComponent<Rigidbody>().AddForce(direction.normalized * pullForceRock);
        }
    }

    IEnumerator CreateRocks()
    {
        for (int i = 0; i < rockAmount; i++)
        {
            Vector3 randomPosition = new Vector3(
                      Random.Range(rockSpawnArea.bounds.min.x, rockSpawnArea.bounds.max.x),
                      Random.Range(rockSpawnArea.bounds.min.y, rockSpawnArea.bounds.max.y),
                      Random.Range(rockSpawnArea.bounds.min.z, rockSpawnArea.bounds.max.z));
            GameObject rock = Instantiate(rockPrefab, randomPosition, Quaternion.identity);
            rocks.Add(rock);
            rock.GetComponent<Health>().Died += () =>
            {
                rocks.Remove(rock);
                Destroy(rock);

            };
            yield return new WaitForSecondsRealtime(spawnTime);
        }
        StartCoroutine(ThrowBombs());        
    }

    IEnumerator ThrowBombs()
    {
        for (int i = rocks.Count - 1; i >= 0; i--)
        {
            GameObject rock = rocks[i];

            direction = bossAI.playerModel.position - rock.transform.position;
            rock.GetComponent<Rigidbody>().AddForce(direction.normalized * pullForcePlayer);

            yield return new WaitForSecondsRealtime(spawnTime);
        }
        throwAtPlayer = false;
        context.TransitionTo(AirBossAI.StateOptions.Dash);
    }
}
