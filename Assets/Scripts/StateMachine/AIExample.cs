using UnityEngine;

public class AIExample : StateMachine
{
    [SerializeField] 
    StateOptions startState = StateOptions.EnemyAttacking;

    // States will be stored using a int.
    // Therefor it is advised to create a enum with your states.
    public enum StateOptions
    {
        EnemyAttacking,
        EnemyDefending
    }

    protected void Start()
    {
        // Inside your Start method you need to add your states by giving a unique id.
        // If you used a enum as id you need to cast it to an int.
        // You also need to provide the newly created State as a component.
        AddState(StateOptions.EnemyAttacking, gameObject.AddComponent<ExampleState2>());
        AddState(StateOptions.EnemyDefending, gameObject.AddComponent<ExampleState1>());

        // You also need to call the StateMachineSetup method so the statemachine works.
        // Do this after you've added all the states!
        StateMachineSetup(startState);
    }
}
