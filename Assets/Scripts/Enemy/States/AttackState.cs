using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer; //timer for how long the enemy stays in the same position before moving
    private float losePlayerTimer; //timer for how long the enemy has lost sight of the player
                  // ^^^^  wait before searching for the player
    private float shotTimer; //timer for how fast the enemy can shoot
    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer()) //if the enemy can see the player
        {
            //lock the losePlayerTimer and increment the moveTimer and shotTimer
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform); //look at the player
            if (shotTimer > enemy.fireRate)
            {
                //if the shotTimer is greater than the fireRate, shoot
                Shoot();
            }

            if(moveTimer > Random.Range(2, 5))
            {
                //if the enemy has been in the same position for too long, move to a new position within a certain range
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 3));
                moveTimer = 0;
            }
            enemy.LastKnowPos = enemy.Player.transform.position; //update the last known position of the player
        }
        else //lost sight of player
        {
            losePlayerTimer += Time.deltaTime;
            if(losePlayerTimer > 5)
            {
                //switch to the search state.
                stateMachine.ChangeState(new SearchState());
            }
        }
    }

    public void Shoot()
    {
        //implement shooting logic here
        //store reference to the gun barrel
        Transform gunbarrel = enemy.gunBarrel;

        //instantiate a bullet at the barrel's position
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunbarrel.position, enemy.transform.rotation);
        //calculate the direction to the player
        Vector3 shootDirection = (enemy.Player.transform.position - gunbarrel.transform.position).normalized;
        //add force to the rigidbody of the bullet in the direction of the player
        //add a little bit of randomness to the bullet's trajectory, so it's not always a straight line
        //the higher the number, the less accurate the bullet will be
        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-0.2f,0.2f),Vector3.up) * shootDirection * 40; //40 is the speed of the bullet

        Debug.Log("Pew pew");
        shotTimer = 0;
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
