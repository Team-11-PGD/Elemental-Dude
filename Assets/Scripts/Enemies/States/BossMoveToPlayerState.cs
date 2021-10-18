using System.Collections;
using UnityEngine;

public class BossMoveToPlayerState : State
{
    public override void Enter()
    {
        StartCoroutine(Timer(2));
    }

    public override void Exit() { }

    public void Update()
    {
        context.transform.Rotate(Vector3.up, 2);
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        // You can transition between states with the TransitionTo method.
        // This method requires the id of the state.
        // If you used a enum you will again need to cast this to a int.
        context.TransitionTo((int)AIExample.StateOptions.EnemyAttacking);
    }
}
