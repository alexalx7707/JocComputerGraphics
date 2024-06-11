using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : BaseState
{
    private float searchTimer; //timer for how long the enemy searches for the player
    private float moveTimer; //timer for how long the enemy stays in the same position before searching in another place
    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.LastKnowPos); //set the destination of the enemy to the last known position of the player
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            //if the enemy can see the player
            stateMachine.ChangeState(new AttackState()); //switch to the attack state
        }
        
        if(enemy.Agent.remainingDistance < enemy.Agent.stoppingDistance)
        {
            //if the enemy has reached the last known position of the player
            searchTimer += Time.deltaTime; //increment the search timer
            moveTimer += Time.deltaTime; //increment the move timer
            if (moveTimer > Random.Range(1, 4))
            {
                //if the enemy has been in the same position for too long, move to a new position within a certain range
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 10));
                moveTimer = 0;
            }
            if (searchTimer > 10)
            {
                //if the search timer is greater than 10 seconds
                stateMachine.ChangeState(new PatrolState()); //return to the patrol state
            }
        }
    }

    public override void Exit()
    {
        
    }
}
