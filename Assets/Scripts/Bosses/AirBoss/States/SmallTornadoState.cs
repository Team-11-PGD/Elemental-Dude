using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallTornadoState : AirBossState
{
    [SerializeField]
    GameObject smallTornado;
    [SerializeField]
    Collider spawnArea;
    [SerializeField]
    int smallTornadoAmount = 5;

    float normalGravityMultiplier;
    List<GameObject> SmallTornados = new List<GameObject>();

    public override void Enter(int previousStateId)
    {
        if (previousStateId == -1)
        {
            normalGravityMultiplier = bossAI.playerModel.GetComponent<MovementScript>().gravityMultiplier;
        }

        if (SmallTornados.Count > 0)
        {
            foreach (GameObject tornado in SmallTornados)
            {
                tornado.GetComponent<ParticleRemover>().ShutDown();
                tornado.GetComponent<SmallTornado>().enabled = false;
            }
        }
        SmallTornados.Clear();
        for (int i = 0; i < smallTornadoAmount; i++)
        {
            GameObject instance = Instantiate(smallTornado, GetPointInCollider(), Quaternion.identity);
            SmallTornado smallTornadoInstance = instance.GetComponent<SmallTornado>();
            smallTornadoInstance.player = bossAI.playerModel.GetComponent<MovementScript>();
            smallTornadoInstance.normalGravityMultiplier = normalGravityMultiplier;
            SmallTornados.Add(instance);
        }

        bossAI.NextRandomState(bossAI.CurrentStateOptions);
    }

    Vector3 GetPointInCollider()
    {
        Vector3 point = new Vector3(
            Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
            Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
            Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));

        Vector3 tmp = spawnArea.ClosestPoint(point);
        return tmp;
    }
}
