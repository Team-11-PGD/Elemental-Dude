using System.Collections;
using UnityEngine;

public class BossDefendingFireballState : State
{
    [SerializeField]
    BoxCollider spawnArea;
    [SerializeField]
    GameObject fireball;
    [SerializeField]
    int fireballAmount = 10;

    FireBossAI bossAI;

    public override void Enter()
    {
        bossAI = context as FireBossAI;
        StartCoroutine(Timer(2));
    }

    public override void Exit() { }

    public void Update()
    {
        context.transform.Rotate(Vector3.up, 2);
    }

    IEnumerator Timer(float time)
    {
        for (int i = 0; i < fireballAmount; i++)
        {
            Vector3 randomPosition = new Vector3();
            Instantiate(fireball, randomPosition, Quaternion.identity, null);
            yield return new WaitForSecondsRealtime(time);
        }
        bossAI.NextDefendState();
    }
}
