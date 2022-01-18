using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoState : AirBossState
{
    [SerializeField]
    BoxCollider bombSpawnArea;
    [SerializeField]
    GameObject tornadoPrefab;
    [SerializeField]
    GameObject bombPrefab;
    [SerializeField]
    Transform tornadoCenter;
    [SerializeField]
    float tornadoTime = 30;
    [SerializeField]
    float spawnTime = 2;
    [SerializeField]
    int bombAmount = 5;
    [SerializeField]
    float pullForce;
    [SerializeField]
    float pullForcePlayer;
    [SerializeField]
    float rotateSpeed = 500;
    [SerializeField]
    float damage = 1f;
    List<GameObject> bombs = new List<GameObject>();
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
        foreach (GameObject bomb in bombs)
        {
            if (throwAtPlayer == false)
            {
                direction = tornadoCenter.position - bomb.transform.position;
            }
            bomb.GetComponent<Rigidbody>().AddForce(direction.normalized * pullForce);
        }
    }

    IEnumerator CreateRocks()
    {
        //Instantiate(tornadoPrefab, tornadoCenter.position, context.transform.rotation);
        for (int i = 0; i < bombAmount; i++)
        {
            Vector3 randomPosition = new Vector3(
                      Random.Range(bombSpawnArea.bounds.min.x, bombSpawnArea.bounds.max.x),
                      Random.Range(bombSpawnArea.bounds.min.y, bombSpawnArea.bounds.max.y),
                      Random.Range(bombSpawnArea.bounds.min.z, bombSpawnArea.bounds.max.z));
            GameObject bomb = Instantiate(bombPrefab, randomPosition, Quaternion.identity);
            bomb.GetComponent<Health>().Died += () =>
            {               
                bombs.Remove(bomb);
                Destroy(bomb);

            };
            bombs.Add(bomb);
            yield return new WaitForSecondsRealtime(spawnTime);
        }
        StartCoroutine(ThrowBombs());
        //yield return new WaitForSecondsRealtime(tornadoTime);
    }

    IEnumerator ThrowBombs()
    {
        for (int i = bombs.Count - 1; i <= 0; i--)
        {
            GameObject bomb = bombs[i];
          
                direction = bossAI.playerModel.position - bomb.transform.position;
                bomb.GetComponent<Rigidbody>().AddForce(direction.normalized * pullForcePlayer);
                Debug.Log("ROCK");
            
            yield return new WaitForSecondsRealtime(spawnTime);
        }
        throwAtPlayer = false;
        context.TransitionTo(AirBossAI.StateOptions.Dash);
    }
}
