using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    //the brain of the enemy, it will control the states
    public BaseState activeState;
    public PatrolState patrolState;

    public void Initialise()
    {
        //set the initial state
        patrolState = new PatrolState();
        ChangeState(patrolState);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activeState != null)
        {
            activeState.Perform();
        }
    }

    //method to change the state
    public void ChangeState(BaseState newState)
    {
        //if there is an active state, make sure we are currently not running a state, so if we are we exit the state
        if(activeState != null)
        {
            //run cleanup on the active state
            activeState.Exit();
        }
        //set the active state to the new state
        activeState = newState;

        //fail safe null check to make sure the new state is not null
        if(activeState != null)
        {
            //setup new state
            activeState.stateMachine = this;
            //assign state enemy class.
            activeState.enemy = GetComponent<Enemy>();
            //run the enter method on the new state
            activeState.Enter();
        }
    }
}
