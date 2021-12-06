using UnityEngine;

[RequireComponent(typeof(DashState))]
public class AirBossAI : BossAI
{
    [SerializeField]
    StateOptions startState = StateOptions.Dash;

    public enum StateOptions
    {
        Dash
    }

    protected void Start()
    {
        AddState(StateOptions.Dash, gameObject.GetComponent<DashState>());

        StateMachineSetup(startState);
    }
}
