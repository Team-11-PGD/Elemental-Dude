using UnityEngine;

[RequireComponent(typeof(DashState), typeof(GroundSpikesState))]
public class AirBossAI : BossAI
{
    [SerializeField]
    StateOptions startState = StateOptions.Dash;

    public enum StateOptions
    {
        Dash,
        GroundSpikes
    }

    protected void Start()
    {
        AddState(StateOptions.Dash, gameObject.GetComponent<DashState>());
        AddState(StateOptions.GroundSpikes, gameObject.GetComponent<GroundSpikesState>());

        StateMachineSetup(startState);
    }
}
