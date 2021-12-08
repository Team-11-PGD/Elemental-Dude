using UnityEngine;

[RequireComponent(typeof(DashState))]
public class AirBossAI : BossAI
{
    [SerializeField]
    StateOptions startState = StateOptions.Dash;

    public enum StateOptions
    {
        Dash,
        Tornado
    }

    protected void Start()
    {
        AddState(StateOptions.Dash, gameObject.GetComponent<DashState>());
        AddState(StateOptions.Tornado, gameObject.GetComponent<TornadoState>());

        StateMachineSetup(startState);
    }
}
