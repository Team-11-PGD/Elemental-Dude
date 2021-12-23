using System.Collections;
using UnityEngine;

public class ExampleState1 : State
{
    public override void Enter(int previousStateId)
    {
        StartCoroutine(Timer(2));
    }

    // Each State will have access to the normal MonoBehaviour functionality, since a State is a MonoBehaviour.
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
        context.TransitionTo(AIExample.StateOptions.EnemyAttacking);
    }
}
