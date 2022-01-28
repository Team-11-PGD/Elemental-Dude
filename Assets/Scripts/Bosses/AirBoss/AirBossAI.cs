using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(DashState), typeof(GroundSpikesState), typeof(TornadoState))]
[RequireComponent(typeof(VulnerableState), typeof(SmallTornadoState), typeof(CeilingSpikesState))]
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
    [SerializeField]
    Collider dashArea;
    [SerializeField]
    float damage = 1f;
    [SerializeField]
    float attackRange = 1;
    public float dashSpeed = 1;
    Vector3 dashStartPosition;
    Vector3 dashDirection;
    Vector3 dashPosition;
    public bool move = true;

    public enum StateOptions
    {
        Dash,
        GroundSpikes,
        Tornado,
        Vulnerable,
        //SmallTornados,
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

    private static readonly Enum[] firstStateOptions = { StateOptions.Dash, StateOptions.GroundSpikes, StateOptions.Tornado };
    private static readonly Enum[] secondStateOptions = { StateOptions.Dash, StateOptions.GroundSpikes, StateOptions.Tornado, /*StateOptions.Vulnerable, StateOptions.SmallTornados,*/ StateOptions.CeilingSpikes };
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

        Funnel.Instance.funnelEvents.Add(new Funnel.FunnelEvent
        {
            name = "BossStateSwitch",
            data = new Dictionary<string, object>()
            {
                {"BossType", "Air" },
                { "StateID", CurrentStateId},
                { "Player-Boss distance", Vector3.Distance(transform.position, playerModel.position) }
            }
        });
    }

    protected void Start()
    {
        dash = gameObject.GetComponent<DashState>();
        AddState(StateOptions.Dash, dash);
        groundSpikes = gameObject.GetComponent<GroundSpikesState>();
        AddState(StateOptions.GroundSpikes, groundSpikes);
        AddState(StateOptions.Tornado, gameObject.GetComponent<TornadoState>());
        //AddState(StateOptions.Vulnerable, gameObject.GetComponent<VulnerableState>());
        //AddState(StateOptions.SmallTornados, gameObject.GetComponent<SmallTornadoState>());
        ceilingSpikes = gameObject.GetComponent<CeilingSpikesState>();
        AddState(StateOptions.CeilingSpikes, ceilingSpikes);
        AddState(StateOptions.Death, gameObject.GetComponent<BossDeath>());

        StateMachineSetup(startState);
        SetDashPosition();
    }


    private void Update()
    {
        if (!move)
        {
            return;
        }
        transform.position += dashDirection * Time.deltaTime * dashSpeed;

        if (Vector3.Dot(transform.position - dashPosition, dashStartPosition - dashPosition) < 0)
        {
            Attack();
        }
    }

    void Attack()
    {
        //TODO: Animation
        if (Vector3.Distance(playerModel.position, transform.position) <= attackRange)
        {
            playerHealth.Hit(damage);
            //SOUND: hit player
        }
        SetDashPosition();
    }

    public void SetDashPosition(Vector3? newPosition = null)
    {
        if (!newPosition.HasValue)
        {
            dashPosition = new Vector3(
                 UnityEngine.Random.Range(dashArea.bounds.min.x, dashArea.bounds.max.x),
                 UnityEngine.Random.Range(dashArea.bounds.min.y, dashArea.bounds.max.y),
                 UnityEngine.Random.Range(dashArea.bounds.min.z, dashArea.bounds.max.z));
        }
        else
        {
            dashPosition = newPosition.Value;
        }
        dashStartPosition = transform.position;
        dashDirection = (dashPosition - transform.position).normalized;
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
        Funnel.Instance.funnelEvents.Add(new Funnel.FunnelEvent
        {
            name = "BossKillTime",
            data = new Dictionary<string, object>
            {
                {"BossType", "Air" },
                {"Seconds",  activeTime}
            }
        });
    }

    private void UpdateState()
    {
        currentState++;
        groundSpikes.spikePercentage *= 1.5f;
        ceilingSpikes.spikePercentage *= 1.5f;
        nextStateDelay = (int)(nextStateDelay * 0.5f);
        dashSpeed *= 1.5f;
        TransitionTo(StateOptions.Dash);
    }
}
