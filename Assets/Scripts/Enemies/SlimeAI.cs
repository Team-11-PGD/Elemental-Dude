using System;
using System.Collections.Generic;
using Extensions;
using UnityEngine;

public class SlimeAI : StateMachine
{
    public Transform playerModel;
    public Health playerHealth;

    [SerializeField]
    Health health;

    private int randomValue;
    private Vector3 scaleCheck = new Vector3(0.3399999f, 0.3399999f, 0.3399999f);
    public float scaleSize = -0.33f;//negative makes it smaller & positive makes it bigger

    #region State Setup
    public enum StateOptions
    {
        MoveToPlayer,
        Attacking,
        Idle,
        Merge
    }

    [SerializeField]
    protected StateOptions startState;
    [SerializeField]
    List<StateTuple> inspectorStates;

    [SerializeField]
    GameObject slime;

    [Serializable]
    protected class StateTuple : Tuple<StateOptions, State>
    {
        [SerializeField]
        StateOptions item1;
        [SerializeField]
        State item2;

        public new StateOptions Item1 { get { return item1; } }
        public new State Item2 { get { return item2; } }

        public StateTuple(StateOptions item1, State item2) : base(item1, item2)
        {
            this.item1 = item1;
            this.item2 = item2;
        }
    }

    protected void Start()
    {
        randomValue = UnityEngine.Random.Range(2, 5);
        foreach (StateTuple tuple in inspectorStates)
        {
            AddState(tuple.Item1, tuple.Item2);
        }

        StateMachineSetup(startState);
    }
    #endregion State Setup

    protected virtual void OnEnable()
    {
        health.Hitted += Hitted;
        health.Died += Died;
    }

    protected virtual void OnDisable()
    {
        health.Hitted -= Hitted;
        health.Died -= Died;
    }

    protected virtual void Died()
    {
        AudioManager.instance.PlaySoundFromObject(AudioManager.instance.MonsterSounds, this.gameObject, "EnemyDied");
        for (int i = 0; i < randomValue; i++)//checks for the random value to spawn in that amount of slimes
        {
            if (transform.localScale != scaleCheck) SplitSlime();
        }
        Destroy(gameObject);
    }

    protected virtual void Hitted()
    {
        //SOUND: Check (EnemyHitted)
        AudioManager.instance.PlaySoundFromObject(AudioManager.instance.MonsterSounds, this.gameObject, "EnemyGotHit");
    }

    private void SplitSlime()//spawns in new slime and sets the new scale
    {
        Quaternion quaternion = Quaternion.LookRotation(playerModel.transform.position);
        GameObject babySlime = Instantiate(slime, transform.position, quaternion);
        babySlime.GetComponentInChildren<ElementColors>();
        babySlime.transform.Scale(scaleSize, true);//uses extensionMethodes to change the scale.
    }
}
