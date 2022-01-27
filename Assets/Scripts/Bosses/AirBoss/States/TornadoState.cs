using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoState : AirBossState
{
    [SerializeField]
    private BoxCollider rockSpawnArea;
    [SerializeField]
    private GameObject rockPrefab;
    [SerializeField]
    private Transform tornadoCenter;
    [SerializeField]
    private float spawnTime = 2;
    [SerializeField]
    private int rockAmount = 5;
    [SerializeField]
    private float pullForceRock;
    [SerializeField]
    private float pullForcePlayer;
    private List<GameObject> rocks = new List<GameObject>();
    private Vector3 direction;
    private bool throwAtPlayer;

    public override void Enter(int previousStateId)
    {
        StartCoroutine(CreateRocks());
    }

    public override void Exit(int nextStateId) { }

    public void Update()
    {
        //Each rock in the list rocks gets pulled towards vector3 direction wich in the case where throwatplayer == false equals the tornadoCenter
        //This creates a tornado like effect
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
            //Every rock is assinged a random position inside the boxcollider rockspawnarea
            Vector3 randomPosition = new Vector3(
                      Random.Range(rockSpawnArea.bounds.min.x, rockSpawnArea.bounds.max.x),
                      Random.Range(rockSpawnArea.bounds.min.y, rockSpawnArea.bounds.max.y),
                      Random.Range(rockSpawnArea.bounds.min.z, rockSpawnArea.bounds.max.z));
            GameObject rock = Instantiate(rockPrefab, randomPosition, Quaternion.identity);
            rocks.Add(rock);
            //in case the died action is called rock gets removed from the list and the gameobjet is destoryed
            rock.GetComponent<Health>().Died += () =>
            {
                rocks.Remove(rock);
                Destroy(rock);

            };
            // this makes it so the rocks dont spawn in all at the same time
            yield return new WaitForSecondsRealtime(spawnTime);
        }
        StartCoroutine(ThrowBombs());        
    }

    IEnumerator ThrowBombs()
    {
        for (int i = rocks.Count - 1; i >= 0; i--)
        {
            GameObject rock = rocks[i];
            // the direction the rocks are pulled towards gets changed to the player wich causes the rocks to be shot at the player
            direction = bossAI.playerModel.position - rock.transform.position;
            rock.GetComponent<Rigidbody>().AddForce(direction.normalized * pullForcePlayer);

            yield return new WaitForSecondsRealtime(spawnTime);
        }
        throwAtPlayer = false;
        context.TransitionTo(AirBossAI.StateOptions.Dash);
    }
}
