using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : BaseState
{
    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.LastKnowPos + (Random.insideUnitSphere * 10));
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
            if (enemy.CanSeePlayer())
            {
                stateMachine.ChangeState(new AttackState());
            }
            else
            {
                stateMachine.ChangeState(new SearchState());
            }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
