using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected StateMachine context;

    /// <summary>
    /// Sets the state machine context to this state
    /// </summary>
    /// <param name="context"> The state machine</param>
    public void SetContext(StateMachine context)
    {
        this.context = context;
    }

    /// <summary>
    /// Enter is called when this state is activated
    /// </summary>
    public virtual void Enter(int previousStateId) { }

    /// <summary>
    /// Exit is called when this state is deactivated
    /// </summary>
    public virtual void Exit(int nextStateId) { }
}
