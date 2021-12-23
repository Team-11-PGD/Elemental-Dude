using System;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(DashState), typeof(GroundSpikesState), typeof(TornadoState))]
[RequireComponent(typeof(VulnerableState) ,typeof(SmallTornadoState), typeof(CeilingSpikesState))]
[RequireComponent(typeof(BossDeath))]
public class AirBossAI : BossAI
{
    [SerializeField]
    StateOptions startState = StateOptions.Dash;
    [SerializeField]
    [Range(0, 1)]
    float nextStatePercentage = 0.5f;
    [SerializeField]
    int nextStateDelay = 1000;

    public enum StateOptions
    {
        Dash,
        GroundSpikes,
        //Tornado,
        Vulnerable,
        SmallTornados,
        CeilingSpikes,
        Death
    }

    public Enum[] CurrentStateOptions
    {
        get
        {
            return currentState == 1 ? firstStateOptions : secondStateOptions;
        }
    }

    private static readonly Enum[] firstStateOptions = { StateOptions.Dash, StateOptions.GroundSpikes /*, StateOptions.Tornado*/ };
    private static readonly Enum[] secondStateOptions = { StateOptions.Dash, StateOptions.GroundSpikes /*, StateOptions.Tornado*/, StateOptions.Vulnerable, StateOptions.SmallTornados, StateOptions.CeilingSpikes };
    private int currentState = 1;
    private DashState dash;
    private SpawnSpikesState groundSpikes;
    private SpawnSpikesState ceilingSpikes;

    /// <summary>
    /// Use a delayed transition
    /// </summary>
    /// <param name="nextState"></param>
    public new async void TransitionTo(Enum nextState)
    {
        await Task.Delay(nextStateDelay);
        base.TransitionTo(nextState);
    }

    protected void Start()
    {
        dash = gameObject.GetComponent<DashState>();
        AddState(StateOptions.Dash, dash);
        groundSpikes = gameObject.GetComponent<GroundSpikesState>();
        AddState(StateOptions.GroundSpikes, groundSpikes);
        //AddState(StateOptions.Tornado, gameObject.GetComponent<TornadoState>());
        AddState(StateOptions.Vulnerable, gameObject.GetComponent<VulnerableState>());
        AddState(StateOptions.SmallTornados, gameObject.GetComponent<SmallTornadoState>());
        ceilingSpikes = gameObject.GetComponent<CeilingSpikesState>();
        AddState(StateOptions.CeilingSpikes, ceilingSpikes);
        AddState(StateOptions.Death, gameObject.GetComponent<BossDeath>());

        StateMachineSetup(startState);
    }

    protected override void Hitted()
    {
        if (health.HpPercentage <= nextStatePercentage && currentState < 2)
        {
            UpdateState();
        }
    }

    protected override void Died()
    {
        base.TransitionTo(StateOptions.Death);
    }

    private void UpdateState()
    {
        currentState++;
        groundSpikes.spikePercentage *= 1.5f;
        ceilingSpikes.spikePercentage *= 1.5f;
        nextStateDelay = (int)(nextStateDelay * 0.5f);
        dash.speed *= 1.5f;
        TransitionTo(StateOptions.SmallTornados);
    }
}
