using UnityEngine;

[RequireComponent(typeof(DashState))]
public class AirBossAI : BossAI
{
    [SerializeField]
    StateOptions startState = StateOptions.Tornado;

    public enum StateOptions
    {
        Dash,
        Tornado
    }

    protected void Start()
    {
        AddState(StateOptions.Dash, gameObject.AddComponent<DashState>());
        AddState(StateOptions.Tornado, gameObject.AddComponent<TornadoState>());

        StateMachineSetup(startState);
    }
}
